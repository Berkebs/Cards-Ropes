using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Chest : MonoBehaviour, IBoardObject
{
    public TextMeshProUGUI LevelText;
    public BoardObjectVariables Variables;

    public GridObject ConnectedGrid;



    public void SetNewObject(Vector3 position, int level, GridObject grid)
    {
        Variables = new BoardObjectVariables();
        Variables.Level = level;
        Variables.onBoard = true;
        Variables.ObjectType = BoardObject.Chest;
        LevelText.text = level.ToString();
        SetPosition(position);
        ConnectedGrid = grid;
    }

    public void SetPosition(Vector3 position)
    {
        this.transform.position = new Vector3(position.x, position.y + transform.localScale.y / 2, position.z);
    }
    public void Merge()
    {
        Variables.Level++;
        LevelText.text = Variables.Level.ToString();
    }
    public void onClickObject()
    {
        ConnectedGrid.RemoveBoardObject();
        BoardManager.Instance.SetCard(Variables.Level);
        this.gameObject.SetActive(false);
    }
    public GameObject GetGameObject() { return this.gameObject; }
    public BoardObject GetObjectType() { return Variables.ObjectType; }
    public int GetLevel() { return Variables.Level; }

    public BoardObjectVariables GetBoardObjectVariables() { return Variables; }
}
