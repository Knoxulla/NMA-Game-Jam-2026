using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoundInfoSO_", menuName = "Create Round...")]
public class RoundInfoSO : ScriptableObject
{
    public List <PropInfo> items;
    public List <RewardSO> rewardPosibilities;
    public int quotaValue;
    public float timeLimit;
}
