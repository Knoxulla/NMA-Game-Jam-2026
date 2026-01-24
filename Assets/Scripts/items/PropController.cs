using System.Reflection;
using UnityEngine;

public class PropController : MonoBehaviour
{
    public PropInfo info;
    Collider col;

    private void Awake()
    {
        
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
