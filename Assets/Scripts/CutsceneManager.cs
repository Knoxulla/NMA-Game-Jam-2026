using UnityEngine;
using System.Collections;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] GameObject openingCS_Cam;

    const string INTRO_CS_KEY = "playIntro";
    const string CS_CONTROL_KEY = "csIsPlaying";

    Animator animator;
    Animator openingCamAnimator;

    void Start()
    {
        animator = GetComponent<Animator>();
        openingCamAnimator = openingCS_Cam.gameObject.GetComponent<Animator>();

        StartCoroutine(PlayIntroCS());
    }

    IEnumerator PlayIntroCS()
    {
        animator.SetTrigger(INTRO_CS_KEY);

        yield return new WaitForSecondsRealtime(3);
        openingCS_Cam.SetActive(true);
        openingCamAnimator.SetBool(CS_CONTROL_KEY, true);

        yield return new WaitForSecondsRealtime(10);
        openingCamAnimator.SetBool(CS_CONTROL_KEY, false);

        //openingCS_Cam.SetActive(false);
    }
}
