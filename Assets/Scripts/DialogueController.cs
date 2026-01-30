using TMPro;
using System.Collections;
using UnityEngine;


public class DialogueController : MonoBehaviour
{
    [SerializeField] TMP_Text textbox;

    [SerializeField] GameObject dialogueBox;

    [SerializeField] int charactersPerSecond = 5;

    public static DialogueController Instance;

    [SerializeField] GachaMachineController gachaController;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        textbox.text = "";
        dialogueBox.SetActive(false);
    }

    private void ShowTextbox()
    { 
        dialogueBox.SetActive(true);
    }

    private void HideTextbox()
    {
        dialogueBox.SetActive(false);
    }

    IEnumerator TypeText(string line)
    {
        ShowTextbox();

        yield return new WaitForSeconds(1);

        string textBuffer = null;
        foreach (char c in line)
        {
            textBuffer += c;
            textbox.text = textBuffer;
            yield return new WaitForSeconds(1 / charactersPerSecond);
        }

        yield return new WaitForSecondsRealtime(5);

        HideTextbox();

        gachaController.timerOn = true;
        gachaController.playerMov.canMove = true;
    }

    public void ShowText(string text)
    {
        StartCoroutine(TypeText(text));
    }
}
