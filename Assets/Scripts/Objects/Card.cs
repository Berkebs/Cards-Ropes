using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Card : MonoBehaviour, IBoardObject, IWarObject
{
    public TextMeshProUGUI LevelText;
    CardMove move;
    public BoardObjectVariables Variables;

    int HP;

    public bool isDie { get; set; }

    private void OnEnable()
    {
        move = this.GetComponent<CardMove>();
        move.enabled = false;
    }

    public void GoWar()
    {
        move.enabled = true;
    }
    void StopMove()
    {
        move.enabled = false;
    }

    public void SetNewObject(Vector3 position, int level, GridObject grid)
    {
        Variables = new BoardObjectVariables();
        Variables.Level = level;
        Variables.onBoard = true;
        Variables.ObjectType = BoardObject.Card;
        LevelText.text = level.ToString();
        HP = Variables.Level * 10;
        isDie = false;
        SetPosition(position);
    }

    public void SetPosition(Vector3 position)
    {
        this.transform.position = new Vector3(position.x, position.y + transform.localScale.y / 2, position.z);
    }
    public void Merge()
    {
        Variables.Level++;
        HP = Variables.Level * 10;
        LevelText.text = Variables.Level.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FinishLine")
        {
            EventManager.WinGame();
        }

        if (other.tag == "Chest")
        {
            if (BoardManager.Instance.HasNullGrid())
            {
                BoardManager.Instance.SetChest();
                EventManager.ChangedBoard();
                other.gameObject.SetActive(false);

                PlayerPrefs.SetInt("CollectedChest", 1);

            }
        }

        if (other.tag == "Wall")
        {
            Wall wall = other.GetComponent<Wall>();
            if (wall.GetHP() > HP)
            {
                StopMove();
                isDie = true;
                BoardManager.Instance.CheckWar();

                Debug.Log(isDie);
                this.gameObject.SetActive(false);
            }
            else
            {
                HP -= wall.GetHP();
                wall.HitWall();
            }
        }
    }
    public bool CheckDie() { return isDie; }
    public int GetLevel() { return Variables.Level; }
    public BoardObject GetObjectType() { return Variables.ObjectType; }
    public GameObject GetGameObject() { return this.gameObject; }
    public void onClickObject() { return; }
    public BoardObjectVariables GetBoardObjectVariables() { return Variables; }
}

