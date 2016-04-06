using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelUIManager : Singleton<LevelUIManager> 
{
	public Text thrustText;
	public Slider thrustSlider;

	// ---

	void Start()
	{
		thrustText.text = "" + LevelManager.Instance.thrust;
	}

	// -

	public void setThrustText(string value) 
	{	
		Debug.Log(this.GetHashCode() + " | setThrustText : " + value);
		thrustText.text = value;	
	}

	// -

	public void setThrustRange(int min, int max)
	{
		thrustSlider.minValue = min;
		thrustSlider.maxValue = max;
	}
}
