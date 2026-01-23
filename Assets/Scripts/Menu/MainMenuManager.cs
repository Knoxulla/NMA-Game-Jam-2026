using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager Instance;

    [SerializeField] Button btn_play;
    [SerializeField] Button btn_ExitGame;
    [SerializeField] Button btn_OpenOptions;
    [SerializeField] GameObject optionsMenu;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        { 
            Destroy(this);
        }
           
    }

    private void Start()
    {
        btn_play.onClick.AddListener(PlayGame);
        btn_ExitGame.onClick.AddListener(ExitGame);
        btn_OpenOptions.onClick.AddListener(OpenOptions);
    }

    private void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    private void OpenOptions()
    { 
        Instantiate(optionsMenu);
    }


    private void ExitGame()
    { 
        Application.Quit();
    }


}
