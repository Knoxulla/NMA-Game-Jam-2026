using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GachaMachineController : MonoBehaviour
{
    [SerializeField] RoundInfoSO round;
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

    [SerializeField] List<Transform> itemSpawnPoints;
    [SerializeField] List<GameObject> itemsInScene;

    [SerializeField] GameObject CS_Camera;
    [SerializeField] GameObject CS_FaceOn_Camera;

    bool isFirstQuota = true;
    [SerializeField] bool isLastQuota = false;

    public bool timerOn = false;

    [SerializeField] HUD_Manager hud;
    PlayerCollectMechanicController playerController;
    public PlayerMovement playerMov;

    private void Start()
    {
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

        CS_FaceOn_Camera.SetActive(true);

        DialogueController.Instance.ShowText("You have not hit the quota, get more!"); // "You have not hit the quota, get more!"
         yield return new WaitForSeconds(3);
        playerMov.canMove = true;
        CS_FaceOn_Camera.SetActive(false);
    }

    IEnumerator PlayInGameGachaCutscene()
    {
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
            rarityToSpawn = UpgradeRarity.Tier2;
        }
        else if (random <= 90 && random > 80)
        {
            rarityToSpawn = UpgradeRarity.Tier4;
        }
        else if (random <= 97 && random > 90)
        {
            rarityToSpawn = UpgradeRarity.Tier5;
        }
        else if (random <= 100 && random > 97)
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

    private void SetQuota()
    {
        if (isFirstQuota)
        {
            DialogueController.Instance.ShowText(rounds[currentRound].startRoundText);
            timerOn = true;
            isFirstQuota = false;
        }

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
        if (currentTimeLimit <= 0.0f)
        {
            currentTimeLimit = 0.0f;

            if (playerController.points < currentQuota)
            {
                // do "bad" end aka no more gambling addiction
            }

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
        if (currentRound >= rounds.Count)
        {
            // addiction end
            Debug.Log("Succumbed to addiction");
            return;
        }

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
        if (currentRound+1 >= rounds.Count)
        { 
            isLastQuota = true;
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
            // not enough points, should be going to get more
            GameManager.Instance.isBadEnd = false;
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
