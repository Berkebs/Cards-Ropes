using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager
{
    public delegate void OnChangeMoney();
    public delegate void OnFinishGame();
    public delegate void OnBoardChanged();
    public static event OnChangeMoney onChangeMoney;
    public static event OnFinishGame onCompletedGame;
    public static event OnFinishGame onLoseGame;
    public static event OnBoardChanged onBoardChanged;

    public static void ChangedMoney() { onChangeMoney.Invoke(); }
    public static void WinGame() { onCompletedGame.Invoke(); }
    public static void LoseGame() { onLoseGame.Invoke(); }
    public static void ChangedBoard() { onBoardChanged.Invoke(); }
}
