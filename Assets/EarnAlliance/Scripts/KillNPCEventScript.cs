using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

public class KillNPCEventScript : MonoBehaviour
{
    public string earnAllianceApiUrl = "https://events.earnalliance.com/v2"; // Correct
    public string clientId = "ebf312f6-6133-431f-8405-529b1376d074"; // Correct
    public string clientSecret = "SGdYwTan1ha6gkvUbhmpQOyMC6QLGOvm"; // Correct

    //public string GameId = "938e99dc-2427-41f7-95c8-fada0079bf5f"; // Use string representation of Guid

    private long timestampforGenerateSignature;
    private string timestampforSendKillNPCEvent;
    private string timestampforEvents;

    public void SendKillNPCEventToEarnAlliance()
    {
        //Debug.Log("Sent Signal");
        timestampforGenerateSignature = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        timestampforSendKillNPCEvent = timestampforGenerateSignature.ToString();
        timestampforEvents = DateTimeOffset.FromUnixTimeMilliseconds(timestampforGenerateSignature).DateTime.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
        //Debug.Log(IdentifierUser.Ins.UserId);
        SendKillNPCEvent("Silver_123", IdentifierUser.Ins.UserId,20, new Traits { zombiekilled = 20, type = "ZOMBIE" });
    }

    private void SendKillNPCEvent(string groupId, string userId, int value, Traits traits)
    {
        KillNPCEvent killNPCEvent = new KillNPCEvent
        {
            gameId = "938e99dc-2427-41f7-95c8-fada0079bf5f",
            events = new Event[]
                {
                new Event
                {
                    groupId = groupId,
                    userId = userId,
                    value = value,
                    eventName = "KILL_NPC",                   
                    Traits = traits,
                    time = timestampforEvents
                }
                }
        };

        string json = JsonConvert.SerializeObject(killNPCEvent);
        string signature = GenerateSignature(clientId, clientSecret, json);
        StartCoroutine(SendKillNPCEventCoroutine(json, signature));
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

    private IEnumerator SendKillNPCEventCoroutine(string jsonPayload, string signature)
    {
        string url = $"{earnAllianceApiUrl}/custom-events";
        using (UnityWebRequest webRequest = new UnityWebRequest(url, "POST"))
        {
            //Debug.Log("jsonPayload "+jsonPayload);
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonPayload);
            webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("x-client-id", clientId);
            webRequest.SetRequestHeader("x-timestamp", timestampforSendKillNPCEvent);
            webRequest.SetRequestHeader("x-signature", signature);
            webRequest.SetRequestHeader("Content-Type", "application/json");
            yield return webRequest.SendWebRequest();
            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Error {webRequest.responseCode}: {webRequest.error}\n{webRequest.downloadHandler.text}");
            }
            else
            {
                Debug.Log("killNPC Event sent successfully!");
                Debug.Log(jsonPayload);
            }
        }
    }

    public class KillNPCEvent
    {
        public string gameId { get; set; }
        public Event[] events { get; set; }
    }

    public class Event
    {
        public string groupId { get; set; }
        public string userId { get; set; }
        public int value { get; set; }

        [JsonProperty("event")]
        public string eventName { get; set; }
        public Traits Traits { get; set; }
        public string time { get; set; }
    }
    [Serializable]
    public class Traits
    {
        public int zombiekilled;
        public string type;
    }
}
