using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GachaMachineController : MonoBehaviour
{
    [SerializeField] RoundInfoSO round;
    [SerializeField] List<RoundInfoSO> rounds;
    [SerializeField] int currentRound = 0;
    [SerializeField] int currentQuota = 0;
    [SerializeField] int currentNumOfItems = 0;
    [SerializeField] float currentTimeLimit = 0f;
    bool timerOn = false;

    [SerializeField] HUD_Manager hud;


    private void OnTriggerEnter(Collider collision)
    {
        PlayerCollectMechanicController playerController = collision.gameObject.GetComponent<PlayerCollectMechanicController>();
        
        if (collision.gameObject.CompareTag("Player"))
        {
            if (playerController.points > rounds[currentRound].quotaValue)
            {
                StartCoroutine(PlayInGameGachaCutscene());
            }
            else if (currentQuota != 0)
            {
                StartCoroutine(MakeGachaMachineAngry());
            }
            else
            {

            }

        }
    }


    IEnumerator MakeGachaMachineAngry()
    { 
        yield return new WaitForSeconds(1);

    }

    IEnumerator PlayInGameGachaCutscene()
    { 
        yield return new WaitForSeconds(1);    
    }

    public void SetQuota()
    {
        round = rounds[currentRound];
        currentQuota = round.quotaValue;
        currentNumOfItems = round.items.Count;
        currentTimeLimit = round.timeLimit;

        hud.UpdateQuotaDisplay(currentQuota);

        GameManager.Instance.events.SetQuota(currentQuota);
        GameManager.Instance.events.UpdateScore(0);
        
    }

    IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(currentTimeLimit);

    }

    private void ResolveTimer()
    { 
        
    }

    public void StartQuota() 
    { 
    
    }

}
