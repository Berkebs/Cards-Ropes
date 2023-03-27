using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Card : MonoBehaviour, ISelectableObject, IBoardObject
{

    int level = 1;
    public TextMeshPro LevelText;


    public IBoardObject GetBoardObject()
    {
        return this;
    }

    public void SetPosition(Vector3 position)
    {
        this.transform.position = new Vector3(position.x, position.y + transform.localScale.y / 2, position.z);
    }

}
