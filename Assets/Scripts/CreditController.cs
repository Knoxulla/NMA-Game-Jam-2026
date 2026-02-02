using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditController : MonoBehaviour
{
    [SerializeField] Button mainMenuBTN;
    [SerializeField] Button replayBTN;

    private void Start()
    {
        mainMenuBTN.onClick.AddListener(BackToMain);
        replayBTN.onClick.AddListener(ReplayGame);
    }

    private void BackToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void ReplayGame()
    {
        SceneManager.LoadScene("GameScene");
    }

}
