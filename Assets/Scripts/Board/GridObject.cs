using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject : MonoBehaviour
{

    IBoardObject BoardObject;

    public void ChangeObjects(GridObject otherGrid, IBoardObject newBoardObject)
    {
        otherGrid.SetBoardObject(BoardObject);
        SetBoardObject(newBoardObject);
    }
    public void SetBoardObject(IBoardObject setObject)
    {
        BoardObject = setObject;
        BoardObject.SetPosition(GetCenterPosition());
    }
    public void SetBoardObject(IBoardObject setObject, int Level)
    {
        BoardObject = setObject;
        BoardObject.SetNewObject(GetCenterPosition(), Level, this);
    }

    public void RemoveBoardObject()
    {
        BoardObject = null;
    }

    public void CallWarObject()
    {
        if (HasWarObject())
            BoardObject.GetGameObject().GetComponent<IWarObject>().GoWar();

    }


    public IBoardObject GetBoardObject() { return BoardObject; }
    public bool HasBoardObject() { return BoardObject != null; }
    public bool HasWarObject()
    {
        if (HasBoardObject())
        {
            return BoardObject.GetGameObject().GetComponent<IWarObject>() != null;
        }

        return false;
    }

    public IWarObject GetWarObject()
    {
        if (HasWarObject())
            return BoardObject.GetGameObject().GetComponent<IWarObject>();

        return null;
    }
    public Vector3 GetCenterPosition() { return new Vector3(transform.position.x, transform.position.y + transform.localScale.y / 2, transform.position.z); }
}
