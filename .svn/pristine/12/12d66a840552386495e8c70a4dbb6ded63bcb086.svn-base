using UnityEngine;
using System.Collections;
using SimpleJSON;
using Superpow;

public class ConnectServer : MonoBehaviour
{
	[Tooltip("Example: http://superpow.herokuapp.com/bubble_dragon/")]
	public string rootUrl;

	[Tooltip("Example: 1.0")]
	public string versionAPI;

	protected delegate void OnGetDataSuccess(string data);

	protected IEnumerator GetDataFromServer(string url, OnGetDataSuccess onGetDataSuccess)
	{
		WWW www = new WWW(url);
		yield return www;
        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.Log("Error: GetDataFromServer - " + www.error);
            yield break;
        }

        if (string.IsNullOrEmpty(www.text)) yield break;
		onGetDataSuccess(www.text);
	}
}
