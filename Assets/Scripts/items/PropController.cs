using System.Reflection;
using UnityEngine;

public class PropController : MonoBehaviour
{
    public PropInfo info;
    Collider col;

    private void Awake()
    {
        Instantiate(info.itemPrefab, transform);
    }

    private void Start()
    {
        col = GetComponent<Collider>();
    }

    public void MergeToMain()
    { 
        col.enabled = false;
    }
}
