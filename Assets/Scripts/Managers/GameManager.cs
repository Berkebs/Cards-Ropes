using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField] private GameSettingsSO GameSettings;
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private BoardManager boardManager;
    [SerializeField] private LevelManager levelManager;

    private void Start()
    {
        if (PlayerPrefs.HasKey("Money"))
            playerManager.EarningMoney(PlayerPrefs.GetInt("Money"));

        if (!PlayerPrefs.HasKey("GamePlayed"))
        {
            PlayerPrefs.SetInt("GamePlayed", 1);
            playerManager.EarningMoney(GameSettings.GameStartMoney);
        }


    }

    public void BuyCard()
    {
        int NeededMoney = GameSettings.CardPrice;

        if (NeededMoney > playerManager.GetMoney() && !boardManager.HasNullGrid())
            return;

        boardManager.SetCard();
        playerManager.SpendMoney(NeededMoney);
        EventManager.ChangedBoard();

    }
    public void StartWar()
    {
        boardManager.CallWarObjects();
    }

    void WinGame()
    {
        levelManager.LevelUp();
        StartCoroutine(LoadLevel());

    }
    void LoseGame()
    {

        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        yield return new WaitForSeconds(1f);


        SceneManager.LoadScene(0);
    }
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



    public int GetCardPrice() { return GameSettings.CardPrice; }
}
