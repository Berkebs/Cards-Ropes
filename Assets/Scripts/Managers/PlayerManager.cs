using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    public int Money { get; private set; }


    private void Awake()
    {
        Instance = this;
    }

    public void EarningMoney(int money)
    {
        Money += money;
        PlayerPrefs.SetInt("Money", Money);
        EventManager.ChangedMoney();
    }
    public void SpendMoney(int money)
    {
        Money -= money;
        PlayerPrefs.SetInt("Money", Money);
        EventManager.ChangedMoney();
    }

    public int GetMoney() { return Money; }


}
