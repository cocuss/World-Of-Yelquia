using UnityEngine;
using UnityEngine.InputSystem;

public class NPCInteractionRaycaster : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera playerCamera;

    [Header("Raycast")]
    [SerializeField] private float maxRayDistance = 100f;

    private void Update()
    {
        if (Mouse.current == null)
            return;

        if (!Mouse.current.leftButton.wasPressedThisFrame)
            return;

        TryInteract();
    }

    private void TryInteract()
    {
        Ray ray = playerCamera.ScreenPointToRay(
            Mouse.current.position.ReadValue()
        );

        if (Physics.Raycast(ray, out RaycastHit hit, maxRayDistance))
        {
            NPCInteractable npc = hit.collider.GetComponent<NPCInteractable>();

            if (npc != null)
            {
                npc.TryInteract();
            }
        }
    }
}