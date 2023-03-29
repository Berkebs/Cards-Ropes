using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance;
    List<GridObject> Grids = new List<GridObject>();
    [SerializeField] private Transform BoardParentTransform;
    BoardObjectJsonContainer JsonList = new BoardObjectJsonContainer();

    List<IWarObject> WarObjects = new List<IWarObject>();


    private void Start()
    {
        Instance = this;

        for (int i = 0; i < BoardParentTransform.childCount; i++)
        {
            GridObject grid = BoardParentTransform.GetChild(i).GetComponent<GridObject>();
            if (grid != null)
                Grids.Add(grid);
        }
        LoadBoard();


    }

    #region BoardFuncs
    public void SetCard()
    {
        SetCard(1);
    }
    public void SetCard(int Level)
    {
        SetCard(Level, GetNullGrid());
    }
    public void SetCard(int Level, int Grid)
    {
        Grids[Grid].SetBoardObject(ObjectPool.Instance.SpawnObject(ObjectTypes.Card).GetComponent<IBoardObject>(), Level);
    }
    public void SetChest()
    {
        SetChest(1);
    }
    public void SetChest(int Level)
    {
        SetChest(Level, GetNullGrid());
    }
    public void SetChest(int Level, int Grid)
    {
        Grids[Grid].SetBoardObject(ObjectPool.Instance.SpawnObject(ObjectTypes.Chest).GetComponent<IBoardObject>(), Level);
    }
    public void CallWarObjects()
    {
        for (int i = 0; i < Grids.Count; i++)
        {
            Grids[i].CallWarObject();

            if (Grids[i].GetWarObject() != null)
                WarObjects.Add(Grids[i].GetWarObject());

        }
    }
    #endregion
    public void CheckWar()
    {

        for (int i = 0; i < WarObjects.Count; i++)
        {
            if (!WarObjects[i].CheckDie())
                return;
        }
        EventManager.LoseGame();

    }
    #region Save Load
    void SaveBoard()
    {

        JsonList.Objects = new List<BoardObjectVariables>();
        for (int i = 0; i < Grids.Count; i++)
        {
            if (Grids[i].HasBoardObject())
            {
                JsonList.Objects.Add(Grids[i].GetBoardObject().GetBoardObjectVariables());
            }
            else
            {
                JsonList.Objects.Add(null);
            }
        }

        string jsonData = JsonUtility.ToJson(JsonList);
        PlayerPrefs.SetString("BoardData", jsonData);
        JsonList.Objects.Clear();
    }


    void LoadBoard()
    {
        if (!PlayerPrefs.HasKey("BoardData"))
            return;


        JsonList = JsonUtility.FromJson<BoardObjectJsonContainer>(PlayerPrefs.GetString("BoardData"));
        for (int i = 0; i < Grids.Count; i++)
        {
            if (JsonList.Objects[i].onBoard)
            {
                if (JsonList.Objects[i].ObjectType == BoardObject.Card)
                    SetCard(JsonList.Objects[i].Level, i);
                else if (JsonList.Objects[i].ObjectType == BoardObject.Chest)
                    SetChest(JsonList.Objects[i].Level, i);
            }
            else
            {
                Grids[i].RemoveBoardObject();
            }
        }


    }
    #endregion


    public bool HasNullGrid() { return GetNullGridObject() != null; }
    public bool HasOneBoardObject()
    {
        for (int i = 0; i < Grids.Count; i++)
        {
            if (Grids[i].HasBoardObject())
            {
                return true;
            }
        }
        return false;
    }

    int GetNullGrid()
    {
        for (int i = 0; i < Grids.Count; i++)
        {
            if (!Grids[i].HasBoardObject())
            {
                return i;
            }
        }
        return 0;
    }
    GridObject GetNullGridObject()
    {
        for (int i = 0; i < Grids.Count; i++)
        {
            if (!Grids[i].HasBoardObject())
            {
                return Grids[i];
            }
        }
        return null;
    }

    private void OnEnable()
    {
        EventManager.onBoardChanged += SaveBoard;

    }
    private void OnDisable()
    {
        EventManager.onBoardChanged -= SaveBoard;

    }

}
[System.Serializable]
class BoardObjectJsonContainer
{
    public List<BoardObjectVariables> Objects;
}
public enum BoardObject
{
    Card,
    Chest
}
