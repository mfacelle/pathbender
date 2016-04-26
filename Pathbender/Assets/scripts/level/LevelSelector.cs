using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

// class that selects a level to load by setting playerprefs and loading level.base scene
public class LevelSelector : MonoBehaviour {

	// sets flag of the level to load, then loads level.base, which loads the actual level
	public void LoadLevel(string levelToLoad) {
		Debug.Log("PREPARING LEVEL " + levelToLoad);
		PlayerPrefs.SetString(LevelLoader.LOAD_LEVEL_KEY, levelToLoad);
		SceneManager.LoadScene("level.base", LoadSceneMode.Single);
	}
}
