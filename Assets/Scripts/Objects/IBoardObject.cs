using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBoardObject
{
    public void SetNewObject(Vector3 position, int level, GridObject grid);
    public void SetPosition(Vector3 position);
    public void Merge();
    public void onClickObject();
    public GameObject GetGameObject();
    public BoardObjectVariables GetBoardObjectVariables();
    public BoardObject GetObjectType();
    public int GetLevel();
}
[System.Serializable]
public class BoardObjectVariables
{
    public BoardObject ObjectType;
    public int Level;
    public bool onBoard;
}