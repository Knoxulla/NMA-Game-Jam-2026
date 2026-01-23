using UnityEngine;

public class PropController : MonoBehaviour
{
    //Rigidbody rb;
    Collider col;

    private void Start()
    {
        //rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    public void MergeToMain()
    { 
        col.enabled = false;
    }
}
