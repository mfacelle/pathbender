using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

// loads a level based on the saved PlayerPref
public class LevelLoader : Singleton<LevelLoader> 
{
	public static string LOAD_LEVEL_KEY = "level.to.load";

	private const string LEVEL_PREFIX = "level.";
	private const int DEFAULT_LEVEL_NUM = 0;

	public int debugLevelLoad;

	// ---

	void Start() {
		// load debug level selection (set in editor)
		if (DebugManager.Instance.isDebug) {
			SceneManager.LoadScene(LEVEL_PREFIX + debugLevelLoad, LoadSceneMode.Additive);
		}
		// no scene to load - load the test level.0
		else if (!PlayerPrefs.HasKey(LOAD_LEVEL_KEY) || PlayerPrefs.GetInt(LOAD_LEVEL_KEY) == 0) {
			SceneManager.LoadScene(LEVEL_PREFIX + DEFAULT_LEVEL_NUM, LoadSceneMode.Additive);
		}
		// load level pointed to by player pref
		else {
			SceneManager.LoadScene(LEVEL_PREFIX + PlayerPrefs.GetInt(LOAD_LEVEL_KEY), LoadSceneMode.Additive);
		}
	}
}
