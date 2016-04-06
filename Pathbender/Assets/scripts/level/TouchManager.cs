using UnityEngine;
using System.Collections;

public class TouchManager : Singleton<TouchManager>
{

	// ---
	
	void Update () 
	{
		// if there are any touches, get the first one
		if (Input.touchCount > 0) {
			Touch t = Input.touches[0];
			angleInput(calculateTouchAngle(Camera.main.ScreenToWorldPoint(t.position)));
		}
	}

	// ---

	// called when slider value is changed
	// sets the current LevelValues singleton to the slider value
	public void thrustInput(float value) 
	{
		LevelManager.Instance.setThrust(value);
		LevelUIManager.Instance.setThrustText("" + value);
	}

	// ---

	// determines the launch angle based on touch position and start planet position
	private Vector2 calculateTouchAngle(Vector2 touch) {
		Vector2 startPos = LevelManager.Instance.startPoint.transform.position;
		float dx = touch.x - startPos.x;
		float dy = touch.y - startPos.y;
		float mag = Mathf.Sqrt(dx*dx + dy*dy);

		Debug.Log("start pos: " + startPos);
		Debug.Log("touch pos: " + touch);
		Debug.Log("angle: " + new Vector2(dx/mag, dy/mag));
		return new Vector2(dx/mag, dy/mag);
	}

	// sets the current LevelValues singleton to the slider value
	public void angleInput(Vector2 value) 
	{
		LevelManager.Instance.setAngle(value);
	}

	// ---
	// DEBUG TOUCH FUNCTIONS

	void OnMouseDown() 
	{
		if (DebugManager.Instance.isDebug) {
			angleInput(calculateTouchAngle(Camera.main.ScreenToWorldPoint(Input.mousePosition)));
		}
	}
}
