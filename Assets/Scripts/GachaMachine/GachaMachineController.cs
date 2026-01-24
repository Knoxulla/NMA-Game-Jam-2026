using System.Collections.Generic;
using UnityEngine;

public class GachaMachineController : MonoBehaviour
{
    [SerializeField] RoundInfoSO round;
    [SerializeField] List<RoundInfoSO> rounds;
    [SerializeField] int currentRound = 0;
    [SerializeField] int currentQuota = 0;
    [SerializeField] int currentNumOfItems = 0;
    [SerializeField] float currentTimeLimit = 0f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        { 
         
        }
    }

    private void Start()
    {
        SetQuota();
    }

    public void SetQuota()
    {
        round = rounds[currentRound];
        currentQuota = round.quotaValue;
        currentNumOfItems = round.items.Count;
        currentTimeLimit = round.timeLimit;

        GameManager.Instance.events.SetQuota(currentQuota);
        GameManager.Instance.events.UpdateScore(0);
        Debug.Log("Quota Set");
    }

    public void StartQuota() 
    { 
    
    }

}
