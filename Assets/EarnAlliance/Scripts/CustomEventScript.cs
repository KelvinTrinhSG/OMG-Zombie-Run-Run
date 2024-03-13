using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

public class CustomEventScript : MonoBehaviour
{
    public string earnAllianceApiUrl = "https://events.earnalliance.com/v2";
    public string clientId = "ebf312f6-6133-431f-8405-529b1376d074";
    public string clientSecret = "SGdYwTan1ha6gkvUbhmpQOyMC6QLGOvm";

    //public string GameId = "938e99dc-2427-41f7-95c8-fada0079bf5f"; // Use string representation of Guid

    private long timestampforGenerateSignature;
    private string timestampforSendCustomEvent;
    private string timestampforEvents;

    private void Start()
    {
        timestampforGenerateSignature = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        timestampforSendCustomEvent = timestampforGenerateSignature.ToString();
        timestampforEvents = DateTimeOffset.FromUnixTimeMilliseconds(timestampforGenerateSignature).DateTime.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ"); ;
        SendCustomEvent("CUSTOM_EVENT", "Kelvin", 1000, "Silver_123", new Traits { score = 1250 });
    }

    private void SendCustomEvent(string eventName, string userId, int value, string groupId, Traits traits)
    {
        CustomEvent customEvent = new CustomEvent
        {
            gameId = "938e99dc-2427-41f7-95c8-fada0079bf5f",
            events = new Event[]
                {
                new Event
                {
                    eventName = "CUSTOM_EVENT",
                    userId = userId,
                    value = value,
                    groupId = groupId,
                    Traits = traits,
                    time = timestampforEvents
                }
                }
        };

        string json = JsonConvert.SerializeObject(customEvent);
        string signature = GenerateSignature(clientId, clientSecret, json);
        StartCoroutine(SendCustomEventCoroutine(json, signature));
    }

    private string GenerateSignature(string clientId, string clientSecret, string body)
    {
        string message = $"{clientId}{timestampforGenerateSignature}{body}";
        using (HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(clientSecret)))
        {
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            byte[] signatureBytes = hmac.ComputeHash(messageBytes);
            return BitConverter.ToString(signatureBytes).Replace("-", "").ToLower();
        }
    }

    private IEnumerator SendCustomEventCoroutine(string jsonPayload, string signature)
    {
        string url = $"{earnAllianceApiUrl}/custom-events";
        using (UnityWebRequest webRequest = new UnityWebRequest(url, "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonPayload);
            webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("x-client-id", clientId);
            webRequest.SetRequestHeader("x-timestamp", timestampforSendCustomEvent);
            webRequest.SetRequestHeader("x-signature", signature);
            webRequest.SetRequestHeader("Content-Type", "application/json");
            yield return webRequest.SendWebRequest();
            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Error {webRequest.responseCode}: {webRequest.error}\n{webRequest.downloadHandler.text}");
            }
            else
            {
                Debug.Log("Custom event sent successfully!");
            }
        }
    }

    public class CustomEvent
    {
        public string gameId { get; set; }
        public Event[] events { get; set; }
    }

    public class Event
    {
        [JsonProperty("event")]
        public string eventName { get; set; }

        public string userId { get; set; }
        public int value { get; set; }
        public string groupId { get; set; }
        public Traits Traits { get; set; }
        public string time { get; set; }
    }
    [Serializable]
    public class Traits
    {
        public int score;
    }
}
