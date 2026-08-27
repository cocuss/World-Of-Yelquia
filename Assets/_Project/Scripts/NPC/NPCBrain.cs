using UnityEngine;

public class NPCBrain : MonoBehaviour
{
    [Header("NPC Identity")]
    [SerializeField] private string npcName = "Aldren";

    [TextArea(3, 6)]
    [SerializeField]
    private string personality =
        "Eres Aldren, un habitante de Yelquia. " +
        "Eres amable pero desconfiado con los desconocidos.";

    public string NPCName => npcName;

    public string Personality => personality;

    public string ProcessMessage(string playerMessage)
    {
        Debug.Log($"[{npcName}] recibió: {playerMessage}");

        // Temporalmente simulamos una respuesta.
        return "Interesante... cuéntame más.";
    }
}