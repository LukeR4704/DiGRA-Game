using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    public InputActionReference interactAction;

    private NPC currentNPC;
    private AttachObject currentAttachable;

    void OnEnable()
    {
        interactAction.action.Enable();
        interactAction.action.performed += OnInteract;
    }

    void OnDisable()
    {
        interactAction.action.performed -= OnInteract;
        interactAction.action.Disable();
    }

    void OnInteract(InputAction.CallbackContext context)
    {
        // Priority: Attachable > NPC (you can swap this if you want)
        if (currentAttachable != null)
        {
            currentAttachable.Attach(transform);
        }
        else if (currentNPC != null)
        {
            currentNPC.Interact();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check for NPC
        NPC npc = other.GetComponent<NPC>();
        if (npc != null)
        {
            currentNPC = npc;
        }

        // Check for Attachable
        AttachObject attachable = other.GetComponent<AttachObject>();
        if (attachable != null)
        {
            currentAttachable = attachable;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Clear NPC
        NPC npc = other.GetComponent<NPC>();
        if (npc != null && npc == currentNPC)
        {
            currentNPC = null;
        }

        // Clear Attachable
        AttachObject attachable = other.GetComponent<AttachObject>();
        if (attachable != null && attachable == currentAttachable)
        {
            currentAttachable = null;
        }
    }
}