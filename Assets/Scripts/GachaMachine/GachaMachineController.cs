using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GachaMachineController : MonoBehaviour
{
    [SerializeField] RoundInfoSO round;
    [SerializeField] List<RoundInfoSO> rounds;
    [SerializeField] int currentRound = 0;
    [SerializeField] int currentQuota = 0;
    [SerializeField] int currentNumOfItems = 0;
    [SerializeField] float currentTimeLimit = 0f;
    [SerializeField] Transform gachaponSpawnpoint;
    bool timerOn = false;

    [SerializeField] HUD_Manager hud;
    PlayerCollectMechanicController playerController;
    PlayerMovement playerMov;


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerController = collision.gameObject.GetComponent<PlayerCollectMechanicController>();
            playerMov = collision.gameObject.GetComponent<PlayerMovement>();
            playerMov.inRangeOfMachine = true;
            playerMov.gachaController = this;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerMov.inRangeOfMachine = false;
        }
    }

    IEnumerator MakeGachaMachineAngry()
    {
        // do anim
        yield return new WaitForSeconds(1);
        // "You have not hit the quote, get more!"
    }

    IEnumerator PlayInGameGachaCutscene()
    {
        // submit anim
        yield return new WaitForSeconds(1);
        // gacha roll anim
        yield return new WaitForSeconds(1);
        // give gacha ball anim
        yield return new WaitForSeconds(1);
        // open ball

        SpawnReward();

        currentRound++;

        // put up menu with given powerup (speed boost but vague), close button starts round


    }

    private void SpawnReward()
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
                break;
            }
            else
            {
                Debug.Log($"Wanted to spawn: {rarityToSpawn} Gachapon, but no gachapon of that rarity in list");
            }
        }

    }

    public void ApplySpeedUp(RewardSO reward)
    {
        playerMov.spdModifier += reward.speedUpgradeAmt;
    }

    private void SetQuota()
    {
        round = rounds[currentRound];
        currentQuota = round.quotaValue;
        currentNumOfItems = round.items.Count;
        currentTimeLimit = round.timeLimit;

        hud.timerSlider.maxValue = currentTimeLimit;
        hud.timerSlider.value = currentTimeLimit;

        hud.UpdateQuotaDisplay(currentQuota);

         timerOn = true;

        GameManager.Instance.events.SetQuota(currentQuota);
        GameManager.Instance.events.UpdateScore(0);

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
        SetQuota();
       
    }

    public void SubmitQuota()
    {
        if (currentRound >= rounds.Count)
        {
            // addiction end
            Debug.Log("Succumbed to addiction");
            return;
        }

        if (currentQuota == 0)
        {
            StartQuota();
            return;
        }

        if (playerController.points < rounds[currentRound].quotaValue)
        {
            // not enough points, should be going to get more
            StartCoroutine(MakeGachaMachineAngry());
            return;
        }

        timerOn = false;

        

        if (playerController.points > rounds[currentRound].quotaValue)
        {
            // move to next round
            Debug.Log("Getting Gachapon");

            StartCoroutine(PlayInGameGachaCutscene());
            return;
        }
    }
}
