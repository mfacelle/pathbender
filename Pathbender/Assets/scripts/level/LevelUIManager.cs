using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelUIManager : Singleton<LevelUIManager> 
{
	public Text thrustText;
	public Slider thrustSlider;
	public Button changeParticleButton;
	public PopUpWindow startMenu;
	public Text startMenuText;

	// flag for is the level is paused (hasn't started, in menu, ended)
	public bool isPaused { get; private set; }

	// ---

	void Start() {
		thrustText.text = "" + 0;
		thrustSlider.minValue = 0;
		thrustSlider.maxValue = 1;

		startMenuText.text = "";
		// set the game to paused and bring down the start menu
		SetPausedState(true);
		startMenu.OpenWindow();
	}

	// -

	public void SetThrustText(string value) {	
		thrustText.text = value;	
	}
		
	// ---

	// loads the thrust sliders [min,max] range and sets thrustText to be min value
	public void LoadThrustSlider(int min, int max) {
		thrustSlider.minValue = min;
		thrustSlider.maxValue = max;
		thrustText.text = "" + min.ToString("0.0");
		thrustSlider.value = min;
	}

	// ---

	// toggles pause (from UI)
	public void TogglePause() {
		isPaused = !isPaused;
		SetInteractableUIState();
	}

	// set paused state directly
	public void SetPausedState(bool newState) {
		isPaused = newState;
		SetInteractableUIState();
	}
		
	private void SetInteractableUIState() {
		if (isPaused) {	// moving to paused state
			thrustSlider.interactable = false;
			changeParticleButton.interactable = false;
		}
		else {	// if coming un-paused
			thrustSlider.interactable = true;
			changeParticleButton.interactable = true;
		}
	}

	// ---

	// loads the level: pauses game and brings down the start menu
	public void LoadLevel() {
		SetPausedState(true);
		startMenu.OpenWindow();
	}

	// ---

	// starts the level: unpauses game and closes the start menu
	public void StartLevel() {
		SetPausedState(false);
		startMenu.CloseWindow();
	}

	// ---

	public void SetStartMenuLevelNumber(string levelNumber) {
		startMenuText.text = "Level " + levelNumber;
	}
}
