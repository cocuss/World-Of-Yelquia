using UnityEngine;

public class NPCInteractable : MonoBehaviour
{
    [Header("Interaction")]
    [SerializeField] private float interactionDistance = 3f;

    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private GameObject chatWindow;

    private void Start()
    {
        if (chatWindow != null)
        {
            chatWindow.SetActive(false);
        }
    }

    public void TryInteract()
    {
        if (player == null)
        {
            Debug.LogWarning("NPCInteractable: Player no está asignado.");
            return;
        }

        float distance = Vector3.Distance(
            player.position,
            transform.position
        );

        if (distance <= interactionDistance)
        {
            OpenChat();
        }
        else
        {
            Debug.Log("El jugador está demasiado lejos del NPC.");
        }
    }

    private void OpenChat()
    {
        if (chatWindow == null)
        {
            Debug.LogWarning("NPCInteractable: ChatWindow no está asignado.");
            return;
        }

        chatWindow.SetActive(true);

        Debug.Log("Chat con NPC abierto.");
    }

    public void CloseChat()
    {
        if (chatWindow != null)
        {
            chatWindow.SetActive(false);
        }
    }
}