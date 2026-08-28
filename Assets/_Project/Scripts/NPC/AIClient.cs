using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class AIClient : MonoBehaviour
{
    [Header("Backend")]
    [SerializeField] private string backendUrl = "http://127.0.0.1:5000/chat";

    [Serializable]
    private class ChatRequest
    {
        public string npcId;
        public string playerId;
        public string message;
    }

    [Serializable]
    private class ChatResponse
    {
        public string npcId;
        public string response;
    }

    public IEnumerator SendMessage(
        string npcId,
        string playerId,
        string message,
        Action<string> onSuccess,
        Action<string> onError)
    {
        ChatRequest requestData = new ChatRequest
        {
            npcId = npcId,
            playerId = playerId,
            message = message
        };

        string json = JsonUtility.ToJson(requestData);

        using UnityWebRequest request = new UnityWebRequest(
            backendUrl,
            UnityWebRequest.kHttpVerbPOST
        );

        byte[] bodyRaw =
            System.Text.Encoding.UTF8.GetBytes(json);

        request.uploadHandler =
            new UploadHandlerRaw(bodyRaw);

        request.downloadHandler =
            new DownloadHandlerBuffer();

        request.SetRequestHeader(
            "Content-Type",
            "application/json"
        );

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string responseJson =
                request.downloadHandler.text;

            ChatResponse responseData =
                JsonUtility.FromJson<ChatResponse>(responseJson);

            onSuccess?.Invoke(responseData.response);
        }
        else
        {
            Debug.LogError(
                $"AIClient Error: {request.error}"
            );

            onError?.Invoke(request.error);
        }
    }
}