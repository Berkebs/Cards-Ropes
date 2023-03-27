using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int Money { get; private set; }




    public void EarningMoney(int money)
    {
        Money += money; ;
        EventManager.ChangedMoney();
    }
    public void SpendMoney(int money)
    {
        Money -= money;
        EventManager.ChangedMoney();
    }

    public int GetMoney() { return Money; }


}
