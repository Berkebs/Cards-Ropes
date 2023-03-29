using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI MoneyText, CardPriceText, LevelText;
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Button BuyCardButton, StartWarButton;
    [SerializeField] private GameSettingsSO GameSettings;
    [SerializeField] private BoardManager boardManager;
    [SerializeField] private LevelManager levelManager;


    private void OnEnable()
    {
        EventManager.onChangeMoney += PrintMoney;
        BuyCardButton.onClick.AddListener(gameManager.BuyCard);
        StartWarButton.onClick.AddListener(StartWar);

    }
    private void OnDisable()
    {
        EventManager.onChangeMoney -= PrintMoney;
        BuyCardButton.onClick.RemoveListener(gameManager.BuyCard);
        StartWarButton.onClick.RemoveListener(StartWar);
    }
    private void Start()
    {
        CardPriceText.text = gameManager.GetCardPrice().ToString();
        LevelText.text = "Level " + levelManager.GetLevel().ToString();
    }
    void PrintMoney()
    {
        MoneyText.text = playerManager.GetMoney().ToString();
        BuyCardButton.enabled = playerManager.GetMoney() >= GameSettings.CardPrice && boardManager.HasNullGrid();
    }


    void StartWar()
    {
        if (boardManager.HasOneBoardObject())
        {
            StartWarButton.enabled = false;
            gameManager.StartWar();
        }


    }
}
