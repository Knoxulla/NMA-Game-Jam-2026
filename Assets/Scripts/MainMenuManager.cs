using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager Instance;
    
    private void Awake()
    {
        if (Instance != null)
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
        
    }

    private void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }


    private void ExitGame()
    { 
        Application.Quit();
    }


}
