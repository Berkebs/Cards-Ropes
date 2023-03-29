using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchController : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{

    [SerializeField] private Camera cam;

    private GameObject SelectedGameObject;
    private GridObject SelectedObjectBaseGrid;

    public void OnDrag(PointerEventData eventData)
    {
        if (SelectedGameObject == null)
            return;

        SelectedGameObject.transform.position = GetHighPosition();
    }



    public void OnPointerDown(PointerEventData eventData)
    {
        SelectedGameObject = GetSelectObject(GetTouchPosition());
        OnDrag(eventData);
    }



    public void OnPointerUp(PointerEventData eventData)
    {
        if (SelectedGameObject == null || SelectedObjectBaseGrid == null)
            return;

        GridObject droppedGrid = GetGridObject(GetTouchPosition());
        IBoardObject SelectedBoardObject = SelectedGameObject.GetComponent<IBoardObject>();

        if (droppedGrid == null)
        {
            SelectedObjectBaseGrid.SetBoardObject(SelectedBoardObject);
            ClearSelectedObjects();
            return;
        }
        if (droppedGrid == SelectedObjectBaseGrid)
        {
            SelectedObjectBaseGrid.SetBoardObject(SelectedBoardObject);
            SelectedBoardObject.onClickObject();
            ClearSelectedObjects();
            return;
        }
        IBoardObject boardObject = droppedGrid.GetBoardObject();


        if (droppedGrid.GetBoardObject() == null)
        {
            //Move Null Grid

            SelectedObjectBaseGrid.RemoveBoardObject();
            droppedGrid.SetBoardObject(SelectedGameObject.GetComponent<IBoardObject>());
        }
        else
        {
            if (boardObject.GetLevel() != SelectedBoardObject.GetLevel() || boardObject.GetObjectType() != SelectedBoardObject.GetObjectType())
            {
                //Change grid
                droppedGrid.ChangeObjects(SelectedObjectBaseGrid, SelectedBoardObject);
            }
            else
            {
                //Merge
                boardObject.Merge();
                SelectedObjectBaseGrid.RemoveBoardObject();
                SelectedGameObject.SetActive(false);
                SelectedGameObject.transform.position = Vector3.zero;

            }
        }
        EventManager.ChangedBoard();
        ClearSelectedObjects();
    }




    void ClearSelectedObjects() { SelectedGameObject = null; SelectedObjectBaseGrid = null; }
    GridObject GetGridObject(Vector3 worldPosition)
    {
        GameObject grid = GetObjectwithWorldPosition(GetTouchPosition(), "Board");

        if (grid == null || grid.GetComponent<GridObject>() == null)
            return null;

        return grid.GetComponent<GridObject>();
    }


    GameObject GetSelectObject(Vector3 worldPosition)
    {
        GridObject grid = GetGridObject(worldPosition);
        if (grid == null || !grid.HasBoardObject())
            return null;

        SelectedObjectBaseGrid = grid;
        return grid.GetBoardObject().GetGameObject();

    }
    GameObject GetObjectwithWorldPosition(Vector3 worldPosition)
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.ScreenPointToRay(worldPosition), out hit))
        {
            return hit.transform.gameObject;
        }
        return null;
    }
    GameObject GetObjectwithWorldPosition(Vector3 worldPosition, string layer)
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.ScreenPointToRay(worldPosition), out hit, Mathf.Infinity, LayerMask.GetMask(layer)))
        {
            return hit.transform.gameObject;
        }
        return null;
    }
    Vector3 GetWorldPosition()
    {
        Ray ray = cam.ScreenPointToRay(GetTouchPosition());

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
            return hit.point;

        return Vector3.zero;
    }
    Vector3 GetHighPosition()
    {
        Vector3 worldPos = GetWorldPosition();
        return new Vector3(worldPos.x, 2, worldPos.z);

    }
    Vector3 GetTouchPosition()
    {
        if (Input.touchCount <= 0)
            return Vector3.zero;

        return Input.GetTouch(0).position;
    }

}


/*GameObject GetSelectableObject(Vector3 worldPosition)
{
    GameObject selectableObject = GetObjectwithWorldPosition(worldPosition);
    IBoardObject selectable = selectableObject.transform.GetComponent<IBoardObject>();
    if (selectable == null)
        return null;

    return selectableObject;
}*/