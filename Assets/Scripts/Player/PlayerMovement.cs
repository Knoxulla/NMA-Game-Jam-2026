using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] float rollSpeed = 1;
    public float spdModifier = 1;
    Vector2 movementDir;
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference interactAction;
    public bool inRangeOfMachine = false;

    public GachaMachineController gachaController;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        interactAction.action.started += OnInteract;
    }

    //public void OnMove(InputValue value)
    //{
    //    movementDir = value.Get<Vector2>();


    //    rb.AddForce(movementDir * rollSpeed * Time.deltaTime);
    //}

    public void OnInteract(InputAction.CallbackContext ctx)
    {
        if (ctx.started && inRangeOfMachine)
        {
            gachaController?.SubmitQuota();
        }
    }

    private void Move(Vector2 movement)
    {
        Vector3 modifiedDir = new Vector3(movementDir.x, 0f, movementDir.y);
        rb.AddForce(modifiedDir * rollSpeed * Time.deltaTime * 100 * spdModifier);

        // Add move animation here
    }

    private void Update()
    {
        movementDir = moveAction.action.ReadValue<Vector2>();
        if (movementDir != Vector2.zero)
        {
            Move(movementDir);
        }
        else 
        {
            movementDir = Vector2.zero;
        }
    }
}
