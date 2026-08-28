using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCChatUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TMP_Text npcName;
    [SerializeField] private Transform chatContent;
    [SerializeField] private TMP_InputField messageInput;
    [SerializeField] private Button sendButton;

    [Header("Message")]
    [SerializeField] private GameObject messagePrefab;

    [Header("NPC")]
    [SerializeField] private NPCBrain npcBrain;

    private void Awake()
    {
        sendButton.onClick.AddListener(SendMessage);
    }

    private void OnDestroy()
    {
        sendButton.onClick.RemoveListener(SendMessage);
    }

    private void SendMessage()
    {
        string message = messageInput.text.Trim();

        if (string.IsNullOrEmpty(message))
            return;

        // Mostrar mensaje del jugador inmediatamente.
        AddMessage("Tú", message);

        // Limpiar el input.
        messageInput.text = "";

        // Obtener respuesta del NPC.
        if (npcBrain != null)
        {
            npcBrain.ProcessMessage(
                message,

                // Respuesta correcta.
                response =>
                {
                    AddMessage(
                        npcBrain.NPCName,
                        response
                    );

                    messageInput.ActivateInputField();
                },

                // Error.
                error =>
                {
                    AddMessage(
                        "Sistema",
                        $"Error: {error}"
                    );

                    messageInput.ActivateInputField();
                }
            );
        }
        else
        {
            Debug.LogWarning(
                "NPCChatUI: NPCBrain no está asignado."
            );
        }
    }

    private void AddMessage(string sender, string message)
    {
        GameObject newMessage = Instantiate(
            messagePrefab,
            chatContent
        );

        TMP_Text messageText = newMessage.GetComponentInChildren<TMP_Text>();

        if (messageText != null)
        {
            messageText.text = $"{sender}: {message}";
        }
    }
}