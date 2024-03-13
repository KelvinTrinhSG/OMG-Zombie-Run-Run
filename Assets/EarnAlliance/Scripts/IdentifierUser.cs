using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;
using System.Security.Cryptography;
using System.Text;
using UnityEngine.UI;

public class IdentifierUser : MonoBehaviour
{
    public string earnAllianceApiUrl = "https://events.earnalliance.com/v2";
    public string clientId = "ebf312f6-6133-431f-8405-529b1376d074";
    public string clientSecret = "SGdYwTan1ha6gkvUbhmpQOyMC6QLGOvm";
    public string gameId = "938e99dc-2427-41f7-95c8-fada0079bf5f"; // Use string representation of Guid

    public GameObject playerInputPanel;
    public GameObject mainPanel;
    public GameObject playBtn;

    // Other parameters for the custom event
    public string UserId = "";//YourCustomUserId
    public string customUser = "";//YourCustomUser
    public string walletAddress = "0xb794f5ea0ba39494ce839613fffba74279579268";//0xb794f5ea0ba39494ce839613fffba74279579268
    public string appleId = "";//YourAppleId
    public string discord = "";//YourDiscord
    public string email = "";//john@doe.com
    public string epicGames = "YourEpicGames";//YourEpicGames
    public string steam = "YourSteam";//YourSteam
    public string twitter = "";//YourTwitter
    public string updatedAt = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");
    public string createdAt = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");

    public InputField inputFieldUserId;
    public InputField inputFieldCustomUser;
    public InputField inputFieldTwitter;
    public InputField inputFieldAppleId;
    public InputField inputFieldDiscord;
    public InputField inputFieldEmail;

    private void Start()
    {
        inputFieldUserId.onEndEdit.AddListener(delegate { OnInputEndEdit(inputFieldUserId, "UserId"); });
        inputFieldCustomUser.onEndEdit.AddListener(delegate { OnInputEndEdit(inputFieldCustomUser, "CustomUser");});
        inputFieldTwitter.onEndEdit.AddListener(delegate { OnInputEndEdit(inputFieldTwitter, "Twitter"); });
        inputFieldAppleId.onEndEdit.AddListener(delegate { OnInputEndEdit(inputFieldAppleId, "AppleId"); });
        inputFieldDiscord.onEndEdit.AddListener(delegate { OnInputEndEdit(inputFieldDiscord, "Discord"); });
        inputFieldEmail.onEndEdit.AddListener(delegate { OnInputEndEdit(inputFieldEmail, "Email"); }); 
    }

     public void ButtonPressed() {
        if (UserId != "" && customUser != "" && twitter != "" && appleId != "" && discord != "" && email != "") {
            SendCustomEvent(customUser, walletAddress, appleId, discord, email, epicGames, steam, twitter, updatedAt, createdAt);
            playerInputPanel.SetActive(false);
            mainPanel.SetActive(true);
            playBtn.SetActive(true);
        }        
    }

    private void OnInputEndEdit(InputField input, string instruciton)
    {
        if (instruciton == "CustomUser")
        {
            customUser = input.text;
            Debug.Log("CustomUser: " + customUser);
        }
        else if (instruciton == "Twitter")
        {
            twitter = input.text;
            Debug.Log("Twitter: " + twitter);
        }
        else if (instruciton == "AppleId")
        {
            appleId = input.text;
            Debug.Log("AppleId: " + appleId);
        }
        else if (instruciton == "AppleId")
        {
            appleId = input.text;
            Debug.Log("AppleId: " + appleId);
        }
        else if (instruciton == "Discord")
        {
            discord = input.text;
            Debug.Log("Discord: " + discord);
        }
        else if (instruciton == "Email")
        {
            email = input.text;
            Debug.Log("Email: " + email);
        }
        else if (instruciton == "UserId")
        {
            UserId = input.text;
            Debug.Log("UserId: " + UserId);
        }
    }

    private void SendCustomEvent(string customUser, string walletAddress, string appleId, string discord, string email, string epicGames, string steam, string twitter, string updatedAt, string createdAt)
    {
        string timestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();

        // Create an object to store event information
        CustomEvent customEvent = new CustomEvent
        {
            gameId = gameId,
            identifiers = new Identifier[]
       {
            new Identifier
            {
                customUser = customUser,
                walletAddress = walletAddress,
                appleId = appleId,
                discord = discord,
                email = email,
                epicGames = epicGames,
                steam = steam,
                twitter = twitter,
                updatedAt = updatedAt,
                createdAt = createdAt,
                userId = UserId // Assign a valid userId here
            }
       }
        };

        // Convert the event object to JSON string
        string jsonPayload = JsonUtility.ToJson(customEvent);

        // Create a message string for signature creation
        string message = $"{clientId}{timestamp}{jsonPayload}";

        // Generate the signature using HMAC-SHA256
        string signature = GenerateSignature(message);

        StartCoroutine(SendCustomEventCoroutine(jsonPayload, timestamp, signature));
    }

    private string GenerateSignature(string message)
    {
        using (HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(clientSecret)))
        {
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            byte[] signatureBytes = hmac.ComputeHash(messageBytes);
            return BitConverter.ToString(signatureBytes).Replace("-", "").ToLower();
        }
    }

    private IEnumerator SendCustomEventCoroutine(string jsonPayload, string timestamp, string signature)
    {
        string url = $"{earnAllianceApiUrl}/custom-events";

        using (UnityWebRequest webRequest = new UnityWebRequest(url, "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonPayload);
            webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
            webRequest.downloadHandler = new DownloadHandlerBuffer();

            webRequest.SetRequestHeader("x-client-id", clientId);
            webRequest.SetRequestHeader("x-timestamp", timestamp);
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

    [Serializable]
    public class CustomEvent
    {
        public string gameId;
        public Identifier[] identifiers;
    }

    [Serializable]
    public class Identifier
    {
        public string customUser;
        public string walletAddress;
        public string appleId;
        public string discord;
        public string email;
        public string epicGames;
        public string steam;
        public string twitter;
        public string updatedAt;
        public string createdAt;
        public string userId; // Add userId field
    }
}
