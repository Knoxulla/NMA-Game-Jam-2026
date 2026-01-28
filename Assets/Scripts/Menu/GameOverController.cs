using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverController : MonoBehaviour
{
    public TMP_Text endTXT;
    public Button replayBTN;
    public Button mainMenuBTN;

    private void Start()
    {
        // Set text based on ending
        SetTextValues();

        // play CS based on ending bool

        // when vid ends, do credits

        replayBTN.onClick.AddListener(ReplayGame);
        mainMenuBTN.onClick.AddListener(BackToMain);


    }

    private void BackToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void ReplayGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    private void SetTextValues()
    {
        if (GameManager.Instance.isBadEnd)
        {
            endTXT.text = "ending 1";
        }
        else
        {
            endTXT.text = "ending 2";
        }
    }
}
