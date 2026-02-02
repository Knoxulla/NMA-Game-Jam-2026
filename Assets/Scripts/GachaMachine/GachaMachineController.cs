using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GachaMachineController : MonoBehaviour
{

    [Header("Round Handling"),SerializeField] RoundInfoSO round;
    [SerializeField] List<RoundInfoSO> rounds;
    [SerializeField] int currentRound = 0;
    [SerializeField] int currentQuota = 0;
    [SerializeField] int currentNumOfItems = 0;
    [SerializeField] float currentTimeLimit = 0f;
    [SerializeField] Transform gachaponSpawnpoint;
    [SerializeField] GameObject powerUpPopUp;
    [SerializeField] Button BTN_powerUpClose;
    [SerializeField] TMP_Text powerUpTitle;
    [SerializeField] TMP_Text powerUpDesc;
    [SerializeField] GameObject tutorialObj;
    [SerializeField] GameObject testInfo;

    [Header("Item Management"), SerializeField] List<Transform> itemSpawnPoints;
    [SerializeField] List<GameObject> itemsInScene;

    [Header("Cutscene Controls"), SerializeField] GameObject CS_Camera;
    [SerializeField] GameObject CS_FaceOn_Camera;
    Animator animator;

    bool isFirstQuota = true;
    bool isLastQuota = false;
    public bool timerOn = false;

    [Header("Connections"),SerializeField] HUD_Manager hud;
    PlayerCollectMechanicController playerController;
    public PlayerMovement playerMov;

    const string ANGRY_KEY = "isAngry";

    bool isPaused = false;

    private void OnEnable()
    {
        GameManager.Instance.events.OnPauseGame += HandlePause;
    }

    private void OnDisable()
    {
        GameManager.Instance.events.OnPauseGame -= HandlePause;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();

        currentRound = 0;
        GameManager.Instance.events.ResetScore();
        powerUpPopUp.SetActive(false);
        BTN_powerUpClose.onClick.AddListener(ClosePowerUpWindow);

        
    }

    public void ClosePowerUpWindow()
    {
        powerUpPopUp.SetActive(false);
        
        Destroy(gachaponSpawnpoint.GetChild(0).gameObject);

        if (currentRound < rounds.Count)
        {
            DialogueController.Instance.ShowText(rounds[currentRound].startRoundText);

        }
    }

    private void HandlePause()
    {
        isPaused = !isPaused;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerController = collision.gameObject.GetComponent<PlayerCollectMechanicController>();
            playerMov = collision.gameObject.GetComponent<PlayerMovement>();
            playerMov.inRangeOfMachine = true;
            playerMov.gachaController = this;
            playerMov.interactIndicator.SetActive(true);

            StartCoroutine(WaitToStartFirstQuota());
        }
    }

    IEnumerator WaitToStartFirstQuota()
    {
        yield return new WaitForSecondsRealtime(7);

        if (isFirstQuota)
        {
            StartQuota();
        }

        // add tutorial info on ground (interact + WASD)
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerMov.inRangeOfMachine = false;
            playerMov.interactIndicator.SetActive(false);
        }
    }

    IEnumerator MakeGachaMachineAngry()
    {
        playerMov.canMove = false;


        // do anim
        yield return new WaitForSeconds(1);
        MakeAngry(true);

        CS_FaceOn_Camera.SetActive(true);

        DialogueController.Instance.ShowText("You have not hit the quota, get more!"); // "You have not hit the quota, get more!"
         yield return new WaitForSeconds(3);
        MakeAngry(false);
        playerMov.canMove = true;
        CS_FaceOn_Camera.SetActive(false);
    }

    IEnumerator PlayInGameGachaCutscene()
    {
        MakeAngry(false);

        DialogueController.Instance.ShowText(rounds[currentRound].endRoundText);

        CS_Camera.SetActive(true);
        timerOn = false;

        //remove all props
        foreach (GameObject x in itemsInScene)
        {
            Destroy(x);
        }

        playerMov.canMove = false;
        // submit anim

        yield return new WaitForSeconds(5f);
        CS_Camera.SetActive(false);
        CS_FaceOn_Camera.SetActive(true);

        // gacha roll anim


        yield return new WaitForSeconds(1f);
        CS_FaceOn_Camera.SetActive(false);
        // give gacha ball anim
        yield return new WaitForSeconds(1.5f);
        // open ball
        RewardSO reward = SpawnReward();
        yield return new WaitForSeconds(3f);
        GameManager.Instance.events.ResetScore();
        ShowPopUp(reward);
        currentRound++;

        StartQuota();

        //playerMov.canMove = true;

        

    }

    private void ShowPopUp(RewardSO reward)
    {
        if (isLastQuota)
        {
            // addiction end
            Debug.Log("Succumbed to addiction");
            GameManager.Instance.isBadEnd = true;
            SceneManager.LoadScene("GameOver");
            return;
        }

        powerUpDesc.text = reward.description;
        powerUpTitle.text = reward.rarity.ToString();

        powerUpPopUp.SetActive(true);

    }

    private RewardSO SpawnReward()
    {
        int random = Random.Range(1, 100);
        UpgradeRarity rarityToSpawn = UpgradeRarity.Tier1;

        if (isFirstQuota)
        {
            random += 65;
            isFirstQuota = false;
            tutorialObj.SetActive(false);
            testInfo.SetActive(false);
        }    

        if (random <= 40 && random > 0)
        {
            rarityToSpawn = UpgradeRarity.Tier1;
        }
        else if (random <= 65 && random > 40)
        {
            rarityToSpawn = UpgradeRarity.Tier2;
        }
        else if (random <= 80 && random > 65)
        {
            rarityToSpawn = UpgradeRarity.Tier3;
        }
        else if (random <= 90 && random > 80)
        {
            rarityToSpawn = UpgradeRarity.Tier4;
        }
        else if (random <= 97 && random > 90)
        {
            rarityToSpawn = UpgradeRarity.Tier5;
        }
        else if (random > 97)
        {
            rarityToSpawn = UpgradeRarity.Tier5;
        }

        foreach (RewardSO x in rounds[currentRound].rewardPosibilities)
        {
            if (x.rarity == rarityToSpawn)
            {
                Instantiate(x.gachaponBallObj, gachaponSpawnpoint);
                Debug.Log($"Spawned {rarityToSpawn} Gachapon");
                ApplySpeedUp(x);
                return x;
                
            }
            else
            {
                Debug.Log($"Wanted to spawn: {rarityToSpawn} Gachapon, but no gachapon of that rarity in list");
            }
        }

        return null;      

    }

    public void ApplySpeedUp(RewardSO reward)
    {
        playerMov.spdModifier += reward.speedUpgradeAmt;
    }

    public void MakeAngry(bool check)
    {
        animator.SetBool(ANGRY_KEY, check);
   }

    private void SetQuota()
    {
        if (isFirstQuota)
        {
            DialogueController.Instance.ShowText(rounds[currentRound].startRoundText);
            tutorialObj.SetActive(true);
            testInfo.SetActive(true);
            timerOn = true;
        }

        playerController.startingSize = new Vector3(1f,1f,1f);
        playerController.GetComponent<SphereCollider>().radius = playerController.colliderRadius;
        playerController.playerSize = 1;
        playerController.isScaling = true;

        round = rounds[currentRound];
        currentQuota = round.quotaValue;
        currentNumOfItems = round.items.Count;
        currentTimeLimit = round.timeLimit;

        hud.timerSlider.maxValue = currentTimeLimit;
        hud.timerSlider.value = currentTimeLimit;

        hud.UpdateQuotaDisplay(currentQuota);

        GameManager.Instance.events.SetQuota(currentQuota);

        SpawnObjects();
    }

    private void Update()
    {
        if (isPaused)
        {
            return;
        }

        if (currentTimeLimit <= 0.0f && isFirstQuota == false)
        {
            currentTimeLimit = 0.0f;

            //if (playerController.points < currentQuota)
            //{
                GameManager.Instance.isBadEnd = false;
                SceneManager.LoadScene("GameOver");
            //}

            timerOn = false;
        }

        if (timerOn)
        {
            currentTimeLimit -= Time.deltaTime;
            hud.timerSlider.value = currentTimeLimit;
            
        }
    }

    public void StartQuota()
    {
        playerController.points = 0;

        SetQuota();
       
    }

    public void SpawnObjects()
    {
        List<Transform> listToChange = new List<Transform>();

        foreach (Transform x in itemSpawnPoints)
        {
            listToChange.Add(x);
        }

        foreach (PropInfo x in rounds[currentRound].items)
        {
            int random = Random.Range(0, listToChange.Count - 1);
            int randomItem = Random.Range(0, x.itemPrefabs.Count - 1);
            GameObject item = Instantiate(x.itemPrefabs[randomItem], listToChange[random]);
            itemsInScene.Add(item);
            listToChange.RemoveAt(random);
        }
    }

    public void SubmitQuota()
    {
        if (currentRound+1 >= rounds.Count && !isLastQuota)
        { 
            isLastQuota = true;
            return;
        }

        if (currentQuota == 0)
        {
            StartQuota();
            return;
        }

        if (playerController.points < rounds[currentRound].quotaValue && timerOn)
        {
            // not enough points, should be going to get more
            StartCoroutine(MakeGachaMachineAngry());
            return;
        }

        if (playerController.points < rounds[currentRound].quotaValue)
        {
            // Family End
            GameManager.Instance.isBadEnd = false;
            SceneManager.LoadScene("GameOver");
            return;
        }

        if (playerController.points >= rounds[currentRound].quotaValue && isLastQuota)
        {
            // Gamba end
            GameManager.Instance.isBadEnd = true;
            SceneManager.LoadScene("GameOver");
            return;

        }

        if (playerController.points >= rounds[currentRound].quotaValue)
        {
            // move to next round
            Debug.Log("Getting Gachapon");

            StartCoroutine(PlayInGameGachaCutscene());
            return;
        }
    }
}
