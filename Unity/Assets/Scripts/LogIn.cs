
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using PlantReality;
using SimpleJSON;

public class LogIn : MonoBehaviour
{
    public InputField userCodeField;
    public Text usernameText;
    public InputField pinCodeField;
    public Button submitButton;

    public void CallLogIn()
    {
        StartCoroutine(LoginPlayer());
    }

    // Try to log in player based on username and pincode.
    IEnumerator LoginPlayer()
    {
        // Create new webrequest.
        WWWForm form = new WWWForm();
        form.AddField("usercode", userCodeField.text);
        form.AddField("pincode", pinCodeField.text);
        using (UnityWebRequest webRequest = UnityWebRequest.Post("http://localhost:8000/api/auth/login", form))
        {
            yield return webRequest.SendWebRequest();

            // Catch bad responses.
            if (webRequest.isNetworkError || webRequest.isHttpError)
            {
                Debug.Log(webRequest.error);
                Debug.Log(webRequest.downloadHandler.text);
            }
            else
            {
                // Receive and deal with response.
                var response = JSON.Parse(webRequest.downloadHandler.text);
                Account.accessToken = response["access_token"];
                Account.tokenType = response["token_type"];
                Account.expiresAt = response["expires_at"];
                Account.user = JsonUtility.FromJson<User>(response["user"].ToString());
                usernameText.text = Account.user.username + " is logged in!";
                Debug.Log("Login succesfull");
                SceneManager.LoadScene("clock");
            }
        }
    }



    public void VerifyInputs()
    {
        submitButton.interactable = (userCodeField.text.Length >= 2 && pinCodeField.text.Length >= 2);
    }
}
