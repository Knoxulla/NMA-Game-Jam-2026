using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoundInfoSO_", menuName = "Create Round...")]
public class RoundInfoSO : ScriptableObject
{
    public List <PropInfo> items;
    public List <RewardSO> rewardPosibilities;
    public int quotaValue;
    public float timeLimit;
    [Tooltip("What does the gacha machine say as he gives the next quota?")]public string startRoundText;
    [Tooltip("What does he say once satiated?")] public string endRoundText;
}
