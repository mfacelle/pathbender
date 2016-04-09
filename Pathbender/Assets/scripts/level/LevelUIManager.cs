using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelUIManager : Singleton<LevelUIManager> 
{
	public Text thrustText;
	public Slider thrustSlider;

	// ---

	void Start() {
		thrustText.text = "" + 0;
		thrustSlider.minValue = 0;
		thrustSlider.maxValue = 1;
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
		thrustText.text = "" + min;
		thrustSlider.value = min;
	}
}
