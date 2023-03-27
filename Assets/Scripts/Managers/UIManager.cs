using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI MoneyText;
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Button BuyCardButton;
    [SerializeField] private GameSettingsSO GameSettings;
    [SerializeField] private BoardManager boardManager;

    private void OnEnable()
    {
        EventManager.onChangeMoney += PrintMoney;
        BuyCardButton.onClick.AddListener(gameManager.BuyCard);

    }
    private void OnDisable()
    {
        EventManager.onChangeMoney -= PrintMoney;
        BuyCardButton.onClick.RemoveListener(gameManager.BuyCard);
    }

    void PrintMoney()
    {
        MoneyText.text = playerManager.GetMoney().ToString();

        BuyCardButton.enabled = playerManager.GetMoney() >= GameSettings.CardPrice && boardManager.HasNullGrid();
    }
}
