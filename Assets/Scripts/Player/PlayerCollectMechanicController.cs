using UnityEngine;

public class PlayerCollectMechanicController : MonoBehaviour
{
    SphereCollider col;

    [SerializeField] float playerSize = 1f;
    [SerializeField] float divideSizeGainBy = 100;
    [SerializeField] Vector3 startingSize = Vector3.zero;
    [SerializeField] float scaleSpeed = 1f;

    bool isScaling = false;

    private void Start()
    {
        col = GetComponent<SphereCollider>();
        //playerSize = col.radius;
        startingSize = transform.localScale;
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject obj =  collision.gameObject;
        
        if (obj.CompareTag("Prop"))
        {
            //float objSize = obj.GetComponent<Collider>().bounds.size.y;

            Vector3 sizeOfPlayer = new Vector3(startingSize.x + playerSize, startingSize.y + playerSize, startingSize.z + playerSize); ;
            Vector3 objSize = obj.GetComponent<Collider>().bounds.size;


            // if object larger than player, do not connect
            if (sizeOfPlayer.magnitude < objSize.magnitude)
            {
                return;
            }

            playerSize += objSize.magnitude / divideSizeGainBy;

            col.radius += playerSize / (divideSizeGainBy * 10);
            obj.transform.parent = transform;
            obj.GetComponent<PropController>().MergeToMain();
            //transform.localScale = new Vector3(sizeOfPlayer.x + playerSize, sizeOfPlayer.y + playerSize, sizeOfPlayer.z + playerSize);
            startingSize = transform.localScale;

            isScaling = true;
        }
    }

    private void FixedUpdate()
    {
        if (isScaling)
        {
            // Gradually scale the player towards the target scale
            transform.localScale = Vector3.Lerp(transform.localScale, startingSize * playerSize, scaleSpeed * Time.deltaTime);

            // Check if the difference between current scale and target scale is small
            if (Vector3.Distance(transform.localScale, startingSize * playerSize) < 0.01f)
            {
                transform.localScale = startingSize * playerSize; // Set exact target scale
                isScaling = false; // Stop scaling
            }
        }
    }
}

