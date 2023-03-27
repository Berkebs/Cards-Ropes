using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameSettingsSO GameSettings;
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private BoardManager boardManager;

    private void Start()
    {
        playerManager.EarningMoney(GameSettings.GameStartMoney);
    }

    public void BuyCard()
    {
        int NeededMoney = GameSettings.CardPrice;

        if (NeededMoney > playerManager.GetMoney() && !boardManager.HasNullGrid())
            return;

        playerManager.SpendMoney(NeededMoney);
        boardManager.SetCard();
    }
    void WinGame() { }
    void LoseGame() { }
    private void OnEnable()
    {
        EventManager.onCompletedGame += WinGame;
        EventManager.onLoseGame += LoseGame;
    }
    private void OnDisable()
    {
        EventManager.onCompletedGame -= WinGame;
        EventManager.onLoseGame -= LoseGame;
    }
}
