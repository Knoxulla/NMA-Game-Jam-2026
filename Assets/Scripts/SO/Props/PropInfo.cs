using UnityEngine;

[CreateAssetMenu(fileName = "PropInfo_", menuName = "Create Prop...")]
public class PropInfo : ScriptableObject
{
    public string itemName;
    [Tooltip("Not really used, just here if we want to add effects based on it")] 
    public PropRarity rarity;
    public int quotaValue;
    public GameObject itemPrefab;

}