using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragController : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{

    [SerializeField] private Camera cam;

    private GameObject SelectedGameObject;


    public void OnDrag(PointerEventData eventData)
    {
        if (SelectedGameObject == null)
            return;

        SelectedGameObject.transform.position = GetHighPosition();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        SelectedGameObject = GetSelectableObject(GetTouchPosition());
        OnDrag(eventData);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (SelectedGameObject == null)
            return;

        GameObject GridObject = GetObjectwithWorldPosition(GetTouchPosition(), "Board");
        GridObject grid = GridObject.GetComponent<GridObject>();
        if (grid == null)
            return;

        grid.SetBoardObject(SelectedGameObject.GetComponent<ISelectableObject>().GetBoardObject());
        SelectedGameObject = null;

    }








    GameObject GetSelectableObject(Vector3 worldPosition)
    {
        GameObject selectableObject = GetObjectwithWorldPosition(worldPosition);
        ISelectableObject selectable = selectableObject.transform.GetComponent<ISelectableObject>();
        if (selectable == null)
            return null;

        return selectableObject;
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