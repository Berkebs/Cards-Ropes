using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallParent : MonoBehaviour
{
    public void SetChildWalls(int Columnhp, int money)
    {
        int Wallhp = Columnhp / transform.childCount;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Wall>().SetWall(Wallhp, money);
        }
    }
}
