using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

// class that selects a level to load by setting playerprefs and loading level.base scene
// also manages which levels are accesible
public class LevelSelector : MonoBehaviour {


	// ---

	void Start() {
		// TODO - check flags to see if a level should now be unlocked (if coming from level success)
	}

	// ---

	// sets flag of the level to load, then loads level.base, which loads the actual level
	public void LoadLevel(string levelToLoad) {
		PlayerPrefs.SetString(LevelLoader.LOAD_LEVEL_KEY, levelToLoad);
		SceneManager.LoadScene("level.base", LoadSceneMode.Single);
	}
}
