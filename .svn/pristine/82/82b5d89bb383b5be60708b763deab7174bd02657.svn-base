#pragma warning disable 0618
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Facebook.Unity;
using UnityEngine.Networking;
using System.Text;
using System;

[System.Serializable]
public class QueryData
{
    public List<Progress> results;
}

[System.Serializable]
public class ProgressP
{
    public string userId;
    public int ruby;
    public int unlockedWorld;
    public int unlockedSubworld;
    public int unlockedLevel;
}

[System.Serializable]
public class Progress : ProgressP
{
    public string objectId;
}

public class ProgressController : MonoBehaviour {

    public string userID = null;
    public static ProgressController instance;

    private Dictionary<string, string> headers = new Dictionary<string, string>();

    public Action onProgressDownloaded;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        CurrencyController.onBalanceChanged += UploadProgress;

        headers.Add("X-Parse-Application-Id", "wordchef");
        headers.Add("Content-Type", "application/json");

        StartCoroutine(IEUpdate());
    }

    private IEnumerator IEUpdate()
    {
        while (true)
        {
            //if (FB.IsLoggedIn)
            //{
            //    userID = AccessToken.CurrentAccessToken.UserId;
            //    Debug.Log("userID: " + userID);

            //    StartCoroutine(IEDownloadProgress());
            //    yield return new WaitForSeconds(2);
            //    StartCoroutine(IEUploadProgress());

            //    yield break;
            //}

            yield return new WaitForSeconds(0.5f);
        }
    }

    private IEnumerator IEDownloadProgress()
    {
        if (string.IsNullOrEmpty(userID)) yield break;

        string condition = string.Format("?where={{\"userId\":\"{0}\"}}", userID);
        WWW www = new WWW("https://sellgamesource.com:1337/parse/classes/GameProgress" + condition, null, headers);

        yield return www;
        if (www.error != null) yield break;

        QueryData data = JsonUtility.FromJson<QueryData>(www.text);
        if (data.results != null && data.results.Count != 0)
        {
            var progress = data.results[0];
            if (ShouldDownload(progress))
            {
                CurrencyController.SetBalance(progress.ruby);
                //Prefs.unlockedWorld = progress.unlockedWorld;
                //Prefs.unlockedSubWorld = progress.unlockedSubworld;
                //Prefs.unlockedLevel = progress.unlockedLevel;

                if (onProgressDownloaded != null) onProgressDownloaded();
            }
        }
    }

    public void UploadProgress()
    {
        StartCoroutine(IEUploadProgress());
    }

    private IEnumerator IEUploadProgress()
    {
        if (string.IsNullOrEmpty(userID)) yield break;

        string condition = string.Format("?where={{\"userId\":\"{0}\"}}", userID);
        WWW www = new WWW("https://sellgamesource.com:1337/parse/classes/GameProgress" + condition, null, headers);

        yield return www;
        if (www.error != null) yield break;

        QueryData data = JsonUtility.FromJson<QueryData>(www.text);

        bool isEmpty = data.results == null || data.results.Count == 0;

        var progress = isEmpty ? new Progress() : data.results[0];

        if (isEmpty || ShouldUpload(progress))
        {
            progress.userId = userID;
            progress.ruby = CurrencyController.GetBalance();
            //progress.unlockedWorld = Prefs.unlockedWorld;
            //progress.unlockedSubworld = Prefs.unlockedSubWorld;
            //progress.unlockedLevel = Prefs.unlockedLevel;

            var progressP = (ProgressP)progress;
            string json = JsonUtility.ToJson(progressP);

            if (isEmpty)
            {
                www = new WWW("https://sellgamesource.com:1337/parse/classes/GameProgress", Encoding.ASCII.GetBytes(json), headers);
                yield return www;
            }
            else
            {
                UnityWebRequest webRequest = UnityWebRequest.Put("https://sellgamesource.com:1337/parse/classes/GameProgress/" + progress.objectId, Encoding.ASCII.GetBytes(json));

                webRequest.SetRequestHeader("X-Parse-Application-Id", "wordchef");
                webRequest.SetRequestHeader("Content-Type", "application/json");

                yield return webRequest.Send();
            }
        }
    }

    private int CompareLevel(Progress progress)
    {
        //int upWorld = progress.unlockedWorld;
        //int upSubworld = progress.unlockedSubworld;
        //int upLevel = progress.unlockedLevel;

        //int world = Prefs.unlockedWorld;
        //int subworld = Prefs.unlockedSubWorld;
        //int level = Prefs.unlockedLevel;


        //if (upWorld == world && upSubworld == subworld && upLevel == level) return 0;
        //if (upWorld > world || upWorld == world && upSubworld > subworld || upWorld == world && upSubworld == subworld && upLevel > level)
        //{
        //    return 1;
        //}
        return -1;
    }

    private bool ShouldDownload(Progress progress)
    {
        int upRuby = progress.ruby;
        int compare = CompareLevel(progress);
        return compare == 1 || compare == 0 && upRuby > CurrencyController.GetBalance();
    }

    private bool ShouldUpload(Progress progress)
    {
        int upRuby = progress.ruby;
        int compare = CompareLevel(progress);
        return compare == -1 || compare == 0 && upRuby != CurrencyController.GetBalance();
    }
}
