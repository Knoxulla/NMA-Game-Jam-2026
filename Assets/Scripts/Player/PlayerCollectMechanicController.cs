using UnityEngine;

public class PlayerCollectMechanicController : MonoBehaviour
{
    SphereCollider col;

    [SerializeField] float playerSize;
    [SerializeField] float divideSizeGainBy = 100;

    private void Start()
    {
        col = GetComponent<SphereCollider>();
        playerSize = col.radius;
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject obj =  collision.gameObject;
        
        if (obj.CompareTag("Prop"))
        {
            playerSize += obj.GetComponent<Collider>().bounds.size.magnitude / divideSizeGainBy;

            col.radius = playerSize;
            obj.transform.parent = col.transform;
            obj.GetComponent<PropController>().MergeToMain();
        }
    }
}

