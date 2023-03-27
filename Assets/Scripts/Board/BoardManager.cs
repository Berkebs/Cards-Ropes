using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    List<GridObject> Grids = new List<GridObject>();
    [SerializeField] private Transform BoardParentTransform;

    private void Awake()
    {
        for (int i = 0; i < BoardParentTransform.childCount; i++)
        {
            GridObject grid = BoardParentTransform.GetChild(i).GetComponent<GridObject>();
            if (grid != null)
                Grids.Add(grid);
        }
    }


    public void SetCard()
    {
        GridObject grid = GetNullGrid();
        grid.SetBoardObject(ObjectPool.Instance.SpawnObject(ObjectTypes.Card).GetComponent<IBoardObject>());

    }


    public bool HasNullGrid() { return GetNullGrid() != null; }

    GridObject GetNullGrid()
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
}

