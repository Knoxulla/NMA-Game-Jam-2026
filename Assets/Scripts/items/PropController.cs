using System.Collections;
using System.Reflection;
using UnityEngine;

public class PropController : MonoBehaviour
{
    public PropInfo info;
    Collider col;
    Rigidbody rb;

    private void Start()
    {
        col = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();

        StartCoroutine(WaitASec());
    }

    IEnumerator WaitASec()
    { 
        yield return new WaitForSeconds(1f);
        Destroy(rb);
    }

    public void MergeToMain()
    { 
        col.enabled = false;
      
    }
}
