using UnityEngine;
using System.Collections;

public class TouchManager : Singleton<TouchManager>
{
	private Collider2D startCollider;	// retrieved on level load
	public Collider2D touchArea;		// the area where touches are valid (doesnt include UI objects)

	// speed at which to rotate the projectile when new angle is selected
	public float ROTATION_SPEED;

	// ---

	void Update() {
		// if there are any touches, get the first one
		if (DebugManager.Instance.isMouseClickDebug) {
			if (Input.GetMouseButton(0)) {
				ProcessTouchInput(Camera.main.ScreenToWorldPoint(Input.mousePosition), TouchPhase.Ended);
			}
		}
		else {
			if (Input.touchCount > 0) {
				Touch t = Input.touches[0];
				ProcessTouchInput(Camera.main.ScreenToWorldPoint(t.position), t.phase);
			}
		}
	}

	// ---

	// called when slider value is changed
	// updates thrust value in LevelManager
	public void ThrustInput(float value) {
		LevelManager.Instance.SetThrust(value);
	}

	// ---

	// called whenever a touch is received
	// either launches projectile (if start point is pressed)
	//  or selects a new launch angle and updates LevelManager
	private void ProcessTouchInput(Vector2 touch, TouchPhase phase) {

		// only process input if it falls inside the touch area
		if (!touchArea.OverlapPoint(touch)) {
			return;
		}
			
		// if touching the start planet, launch projectile (on release - or if mouse click used)
		if (startCollider.OverlapPoint(touch) && phase == TouchPhase.Ended ) {
			LevelManager.Instance.LaunchProjectile();
		}
		else {
			AngleInput(CalculateTouchAngle(touch));
		}
	}

	// ---

	// determines the launch angle based on touch position and start planet position
	private Vector2 CalculateTouchAngle(Vector2 touch) {
		Vector2 startPos = LevelManager.Instance.startPoint.transform.position;
		float dx = touch.x - startPos.x;
		float dy = touch.y - startPos.y;
		float mag = Mathf.Sqrt(dx*dx + dy*dy);
		return new Vector2(dx/mag, dy/mag);
	}

	// sets the current LevelValues singleton to the slider value
	public void AngleInput(Vector2 value) {
		LevelManager.Instance.SetAngle(value);
	}

	// ---

	// loads any touch-able Collider2D objects
	public void LoadColliders() {
		startCollider = LevelManager.Instance.startPoint.GetComponent<Collider2D>();
	}


}
