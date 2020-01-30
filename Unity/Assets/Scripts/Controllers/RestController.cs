using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using SimpleJSON;
using System;

namespace PlantReality
{
    public abstract class RestController<T> : MonoBehaviour where T : Model
    {
        // The server is local (for now).
        private readonly string baseUrl = "http://localhost:8000/";
        private string endPoint;

        public RestController(string endPoint)
        {
            this.endPoint = endPoint;
        }

        // Get all items.
        public IEnumerator Index(System.Action<T[]> callback, Func<string, T[]> parser = null)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(baseUrl + endPoint))
            {
                webRequest.SetRequestHeader("Authorization", $"{Account.tokenType} {Account.accessToken}");
                webRequest.SetRequestHeader("accept", "application/json");
                yield return webRequest.SendWebRequest();

                if (webRequest.isNetworkError || webRequest.isHttpError)
                {
                    Debug.Log(webRequest.error);
                    Debug.Log(webRequest.downloadHandler.text);
                }
                else
                {
                    string response = webRequest.downloadHandler.text;
                    Debug.Log(response);
                    T[] tList = new T[0];
                    if (response != "[]")
                    {
                        if (parser == null)
                        {
                            tList = JsonHelper.FromJson<T>(response);
                        }
                        else
                        {
                            tList = parser(response);
                        }
                    }
                    callback(tList);
                }
            }
        }

        // Get single item.
        public IEnumerator Get(int id, System.Action<T> callback)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get($"{baseUrl + endPoint}/{id}"))
            {
                webRequest.SetRequestHeader("Authorization", $"{Account.tokenType} {Account.accessToken}");
                yield return webRequest.SendWebRequest();

                if (webRequest.isNetworkError || webRequest.isHttpError)
                {
                    Debug.Log(webRequest.error);
                    Debug.Log(webRequest.downloadHandler.text);
                }
                else
                {
                    string response = webRequest.downloadHandler.text;
                    T t = JsonUtility.FromJson<T>(response);
                    callback(t);
                }
            }
        }
        public IEnumerator Create(T t, System.Action<T> callback)
        {
            // Create.
            string data = JsonUtility.ToJson(t);
            using (UnityWebRequest webRequest = UnityWebRequest.Post(baseUrl + endPoint, data))
            {
                webRequest.SetRequestHeader("Authorization", $"{Account.tokenType} {Account.accessToken}");
                yield return webRequest.SendWebRequest();

                if (webRequest.isNetworkError || webRequest.isHttpError)
                {
                    Debug.Log(webRequest.error);
                    Debug.Log(webRequest.downloadHandler.text);
                }
                else
                {
                    string response = webRequest.downloadHandler.text;
                    callback(JsonUtility.FromJson<T>(response));
                }
            }
        }
        // Update.
        public IEnumerator Updates(T t, System.Action<T> callback)
        {
            string data = JsonUtility.ToJson(t);
            using (UnityWebRequest webRequest = UnityWebRequest.Put($"{baseUrl + endPoint}/{t.id}", data))
            {
                webRequest.SetRequestHeader("Authorization", $"{Account.tokenType} {Account.accessToken}");
                yield return webRequest.SendWebRequest();

                if (webRequest.isNetworkError || webRequest.isHttpError)
                {
                    Debug.Log(webRequest.error);
                    Debug.Log(webRequest.downloadHandler.text);
                }
                else
                {
                    string response = webRequest.downloadHandler.text;
                    callback(JsonUtility.FromJson<T>(response));
                }
            }
        }
        
        // Delete.
        public IEnumerator Delete(T t, System.Action<bool?> callback)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Delete($"{baseUrl + endPoint}/{t.id}"))
            {
                webRequest.SetRequestHeader("Authorization", $"{Account.tokenType} {Account.accessToken}");
                yield return webRequest.SendWebRequest();

                if (webRequest.isNetworkError || webRequest.isHttpError)
                {
                    Debug.Log(webRequest.error);
                    Debug.Log(webRequest.downloadHandler.text);
                }
                else
                {
                    string response = webRequest.downloadHandler.text;
                    bool? b = JsonUtility.FromJson<bool?>(response);
                    callback(b);
                }
            }
        }
    }
}
