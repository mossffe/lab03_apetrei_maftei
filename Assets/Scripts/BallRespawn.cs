using UnityEngine;
using UnityEngine.InputSystem;


public class BallRespawn : MonoBehaviour
{
    [SerializeField] InputActionReference respawnAction;
    [SerializeField] Transform head;            // Main Camera under XR Origin
    [SerializeField] float distance = 0.6f;
    [SerializeField] float heightOffset = -0.3f;

    Rigidbody rb;
    UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grab;
    BallShot shot;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        grab = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        shot = GetComponent<BallShot>();
    }

    void OnEnable()
    {
        respawnAction.action.Enable();
        respawnAction.action.performed += OnPressed;
    }

    void OnDisable() => respawnAction.action.performed -= OnPressed;

    void OnPressed(InputAction.CallbackContext ctx)
    {
        Respawn();
    }

    public void Respawn()
    {
        if (grab.isSelected)
        {
            grab.interactionManager.CancelInteractableSelection((UnityEngine.XR.Interaction.Toolkit.Interactables.IXRSelectInteractable)grab);
        }

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        Vector3 forward = head.forward;
        forward.y = 0f;
        forward.Normalize();

        transform.position = head.position + forward * distance + Vector3.up * heightOffset;
        shot.throwOrigin = head.position;
        shot.scoreDisabled = false;
    }
}