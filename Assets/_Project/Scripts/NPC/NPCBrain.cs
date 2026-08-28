using System;
using System.Collections;
using UnityEngine;

public class NPCBrain : MonoBehaviour
{
    [Header("NPC Identity")]
    [SerializeField] private string npcId = "aldren";
    [SerializeField] private string npcName = "Aldren";

    [Header("Player")]
    [SerializeField] private string playerId = "player_001";

    [TextArea(3, 6)]
    [SerializeField]
    private string personality =
        "Eres Aldren, un habitante de Yelquia. " +
        "Eres amable pero desconfiado con los desconocidos.";

    [Header("References")]
    [SerializeField] private AIClient aiClient;

    public string NPCName => npcName;
    public string NPCId => npcId;

    private void Awake()
    {
        if (aiClient == null)
        {
            aiClient = GetComponent<AIClient>();
        }
    }

    public void ProcessMessage(
        string playerMessage,
        Action<string> onResponse,
        Action<string> onError)
    {
        StartCoroutine(
            ProcessMessageRoutine(
                playerMessage,
                onResponse,
                onError
            )
        );
    }

    private IEnumerator ProcessMessageRoutine(
        string playerMessage,
        Action<string> onResponse,
        Action<string> onError)
    {
        Debug.Log(
            $"[{npcName}] recibió: {playerMessage}"
        );

        if (aiClient == null)
        {
            onError?.Invoke(
                "AIClient no está disponible."
            );

            yield break;
        }

        yield return aiClient.SendMessage(
            npcId,
            playerId,
            playerMessage,
            onResponse,
            onError
        );
    }
}