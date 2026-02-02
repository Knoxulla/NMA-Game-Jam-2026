using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Video;
using System.Collections;

public class GameOverController : MonoBehaviour
{
    public TMP_Text endTXT;
    public VideoPlayer videoPlayer;
    [SerializeField] VideoClip clipGoodEnd;
    [SerializeField] VideoClip clipBadEnd;
    [SerializeField] GameObject vidImage;
    [SerializeField] Animator endCS_Controller;
    [SerializeField] Animator gachaMachineAnim;
    

    const string BAD_END_BOOL_KEY = "isBadEnd";
    const string PLAY_CS_TRIGGER_KEY = "playCS";
    const string FADE_CS_TRIGGER_KEY = "fadeCS";
    const string FADE_TEXT_TRIGGER_KEY = "fadeText";
    const string ANGRY_KEY = "isAngry";

    private void Start()
    {

        // Set text based on ending
        SetEndingValues();

        // play CS based on ending bool

        // when vid ends, do credits

    }


    private void SetEndingValues()
    {
        float vidLength = 0f;

        if (GameManager.Instance.isBadEnd)
        {
            endTXT.text = "Dear roller, you have completed your journey and sustained the world’s foundation.\r\n";
        }
        else
        {
            vidImage.SetActive(true);
            videoPlayer.clip = clipGoodEnd;
            videoPlayer.Play();
            endTXT.text = "Dear roller, you discovered that the machine is a lie, and its foundation a facade. \r\n";
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
            gachaMachineAnim.SetTrigger(ANGRY_KEY);
            videoLength = endCS_Controller.GetCurrentAnimatorClipInfo(0).Length;
        }


        yield return new WaitForSeconds(videoLength);

        endCS_Controller.SetTrigger(FADE_CS_TRIGGER_KEY);
        float length = endCS_Controller.GetCurrentAnimatorClipInfo(0).Length;

        yield return new WaitForSeconds(length);
        vidImage.SetActive(false);

        yield return new WaitForSeconds(10f);

        endCS_Controller.SetTrigger(FADE_TEXT_TRIGGER_KEY);

        length = endCS_Controller.GetCurrentAnimatorClipInfo(0).Length;
        yield return new WaitForSeconds(length);

        SceneManager.LoadScene("Credits");
        // turn on a credit object here that goes through all our names and roles w/ btn at the bottom for navigation
    }

}
