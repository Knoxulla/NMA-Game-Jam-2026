using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Video;
using System.Collections;

public class GameOverController : MonoBehaviour
{
    public TMP_Text endTXT;
    public Button replayBTN;
    public Button mainMenuBTN;
    public VideoPlayer videoPlayer;
    [SerializeField] VideoClip clipGoodEnd;
    [SerializeField] VideoClip clipBadEnd;
    [SerializeField] GameObject vidImage;
    [SerializeField] Animator endCS_Controller;
    [SerializeField] GachaMachineController gachaMachine;
    

    const string BAD_END_BOOL_KEY = "isBadEnd";
    const string PLAY_CS_TRIGGER_KEY = "playCS";
    const string FADE_CS_TRIGGER_KEY = "fadeCS";

    private void Start()
    {

        // Set text based on ending
        SetEndingValues();

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

    private void SetEndingValues()
    {
        float vidLength = 0f;

        if (GameManager.Instance.isBadEnd)
        {

            //videoPlayer.clip = clipBadEnd;
            //videoPlayer.Play();

            //endCS_Controller.SetBool();

            endTXT.text = "ending 1";
        }
        else
        {
            vidImage.SetActive(true);
            videoPlayer.clip = clipGoodEnd;
            videoPlayer.Play();
            endTXT.text = "ending 2";
            vidLength = (float)videoPlayer.clip.length;
        }


        StartCoroutine(WaitBeforeCredits(vidLength));
    }

    IEnumerator WaitBeforeCredits(float videoLength)
    {


        endCS_Controller.SetBool(BAD_END_BOOL_KEY, GameManager.Instance.isBadEnd);
        endCS_Controller.SetTrigger(PLAY_CS_TRIGGER_KEY);

        if (GameManager.Instance.isBadEnd)
        {
            gachaMachine.MakeAngry();
            videoLength = endCS_Controller.GetCurrentAnimatorClipInfo(0).Length;
        }


        yield return new WaitForSeconds(videoLength);

        endCS_Controller.SetTrigger(FADE_CS_TRIGGER_KEY);
        float length = endCS_Controller.GetCurrentAnimatorClipInfo(0).Length;

        yield return new WaitForSeconds(length);
        vidImage.SetActive(false);


        // turn on a credit object here that goes through all our names and roles w/ btn at the bottom for navigation
    }

}
