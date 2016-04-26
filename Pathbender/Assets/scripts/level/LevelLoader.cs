using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

// loads a level based on the saved PlayerPref
public class LevelLoader : Singleton<LevelLoader> 
{
	public static string LOAD_LEVEL_KEY = "level.to.load";

	public static string LEVEL_PREFIX = "level.";
	private const string DEFAULT_LEVEL_NUM = "0-1";

	public string debugLevelLoad;

	private string loadedLevelName;

	// ---

	void Start() {
		loadedLevelName = "";
		LoadLevel();
	}

	// ---

	// load debug level selection (set in editor)
	public void LoadLevel() {
		string levelToLoad = PlayerPrefs.GetString(LOAD_LEVEL_KEY);

		if (DebugManager.Instance.isLevelDebug) {
			Debug.Log("Loading debug level " + debugLevelLoad);
			SceneManager.LoadScene(LEVEL_PREFIX + debugLevelLoad, LoadSceneMode.Additive);
			loadedLevelName = LEVEL_PREFIX + debugLevelLoad;
		}
		// if key missing, or no level number provided
		// no scene to load - load the test level.0
		else if (!PlayerPrefs.HasKey(LOAD_LEVEL_KEY) || levelToLoad == "" ) {
			Debug.Log("Loading default level " + DEFAULT_LEVEL_NUM);
			SceneManager.LoadScene(LEVEL_PREFIX + DEFAULT_LEVEL_NUM, LoadSceneMode.Additive);
			loadedLevelName = LEVEL_PREFIX + DEFAULT_LEVEL_NUM;
		}
		// load level pointed to by player pref
		else {
			// only load level if it exists (can be loaded)
			if (Application.CanStreamedLevelBeLoaded(LEVEL_PREFIX + levelToLoad)) {
				Debug.Log("Loading level " + levelToLoad);
				SceneManager.LoadScene(LEVEL_PREFIX + levelToLoad, LoadSceneMode.Additive);
				loadedLevelName = LEVEL_PREFIX + levelToLoad;
			}
			else {	
				Debug.Log("Cannot load level " + levelToLoad + "; loading default level " + DEFAULT_LEVEL_NUM);
				SceneManager.LoadScene(LEVEL_PREFIX + DEFAULT_LEVEL_NUM, LoadSceneMode.Additive);
				loadedLevelName = LEVEL_PREFIX + DEFAULT_LEVEL_NUM;
			}
		}
	}

	// -

	// loads the level specified by the string arg
	// assumes levelToLoad is a valid level name
	public void LoadLevel(string levelNameToLoad) {
		// only load level if it exists (can be loaded)
		if (Application.CanStreamedLevelBeLoaded(levelNameToLoad)) {
			SceneManager.LoadScene(levelNameToLoad, LoadSceneMode.Additive);
			loadedLevelName = levelNameToLoad;
		}
		else {	
			SceneManager.LoadScene(LEVEL_PREFIX + DEFAULT_LEVEL_NUM, LoadSceneMode.Additive);
			loadedLevelName = LEVEL_PREFIX + DEFAULT_LEVEL_NUM;
		}
	}

	// ---

	// unloads the currently-loaded level
	public void UnloadLevel() {
		SceneManager.UnloadScene(loadedLevelName);
	}

	// ---

	// unloads and re-loads the currently-loaded level (reset)
	public void ReloadLevel() {
		UnloadLevel();
		LoadLevel(loadedLevelName);
	}

	// ---

	// unloads the level and returns to the map
	public void BackToMap() {
		UnloadLevel();
		SceneManager.LoadScene("level.select", LoadSceneMode.Single);
	}
}
