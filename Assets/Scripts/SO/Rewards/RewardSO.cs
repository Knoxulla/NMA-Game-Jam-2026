using UnityEngine;

[CreateAssetMenu(fileName = "RewardSO_", menuName = "Create Reward...")]
public class RewardSO : ScriptableObject
{
    public UpgradeRarity rarity;
    [Tooltip("How much speed is increased by")] public float speedUpgradeAmt;
    [Tooltip("feel free to use a random 3D obj from the files for now")]public GameObject gachaponBallObj;
}
