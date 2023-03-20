using UnityEngine;
using System.Collections;

public class FirstSceneController : MonoBehaviour
{
	public static FirstSceneController instance;

	private void Awake()
	{
		instance = this;
		Application.targetFrameRate = 60;
        CPlayerPrefs.useRijndael(CommonConst.ENCRYPTION_PREFS);

		CUtils.SetAndroidVersion(GameVersion.ANDROID);
		CUtils.SetIOSVersion(GameVersion.IOS);
		CUtils.SetWindowsPhoneVersion(GameVersion.WP);
	}

	private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !DialogController.instance.IsDialogShowing())
		{
            DialogController.instance.ShowDialog(DialogType.QuitGame);
        }
    }
}
