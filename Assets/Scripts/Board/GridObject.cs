using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject : MonoBehaviour
{

    IBoardObject BoardObject;

    public void SetBoardObject(IBoardObject setObject)
    {
        BoardObject = setObject;
        BoardObject.SetPosition(GetCenterPosition());
    }

    public void RemoveBoardObject()
    {
        BoardObject = null;
    }

    public IBoardObject GetBoardObject()
    {
        if (BoardObject == null)
            return null;

        return BoardObject;
    }

    public Vector3 GetCenterPosition()
    {
        return transform.position;
    }
}
