using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    int HP;
    int RewardMoney;
    public void SetWall(int hp, int Money)
    {
        HP = hp;
        RewardMoney = Money;
    }
    public void HitWall()
    {
        PlayerManager.Instance.EarningMoney(RewardMoney);
        this.gameObject.SetActive(false);
    }

    public int GetHP() { return HP; }
}
