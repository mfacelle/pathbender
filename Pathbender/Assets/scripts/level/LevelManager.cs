﻿using UnityEngine;
using System.Collections;

// class containing values used in levels
// object is singleton, but each level has it's own instance
// must be a non-persistent object for each new LevelValue to overwrite the Instance
public class LevelManager : Singleton<LevelManager> 
{
	// const across all levels
	public const float TOLERANCE = 30;	// how many degrees past setAngle before correction
	private const float END_LEVEL_WAIT_TIME = 1;	// how many seconds after fail/success message appears to reload/close

	// current values
	public Vector2 angleVec { get; private set; }	// set angle (vector2)
	private float setAngle;					// set angle (degrees)
	private float currentAngle;			// current angle (moves towards set angle)
	public float thrust { get; private set; }

	// values per-level
	public ChargedObject startPoint { get; private set; }	// start object
	public ChargedObject endPoint { get; private set; }		// goal object
	public GameObject objectContainer { get; private set; }	// container for all charged objects
	public string levelNumber;	// string value (i.e. level 1-1, level 1-2, ...)
	public int minThrust;	// min thrust on slider
	public int maxThrust;	// max thrust on slider

	private Transform startObject;		// the entire start object (used for rotations)


	private bool rotateCW;	// rotate CW flag
	private bool rotateCCW;	// rotate CCW flag

	// flag for other classes to check if LevelManager is ready (i.e. PlayerProjectile)
	public bool isLoaded { get; private set; }

	// wait time after success/fail message before reloading or going back to map
	private float currentWaitTime;
	private bool isLevelFailed;
	private bool isLevelPassed;

	// ---

	void Start () {
		isLoaded = false;
		// ensure that thrust values are in the correct order
		if (minThrust > maxThrust) {
			int temp = maxThrust;
			maxThrust = minThrust;
			minThrust = temp;
		}
		// initialize thrust and angle variables
		thrust = minThrust;
		// initial direction is facing upwards (0deg, [0,1])
		angleVec = new Vector2(0,1);
		setAngle = 0;
		currentAngle = 0;

		LoadLevel();
		isLoaded = true;

		isLevelFailed = false;
		isLevelPassed = false;
		currentWaitTime = 0;
	}

	// ---

	// rotates object until currentAngle = setAngle
	// also counts down to reload/gobacktomap when level failed/passed
	void Update() {
		
		if (!LevelUIManager.Instance.isPaused && currentAngle != setAngle) {
			if (rotateCW) {
				currentAngle += TouchManager.Instance.ROTATION_SPEED * Time.deltaTime;
				// keep currentAngle within [0,360)
				if (currentAngle >= 360) {
					currentAngle -= 360;
				}
				// make sure currentAngle doesn't pass over setAngle
				if (passedSetAngle(true)) {
					currentAngle = setAngle;
				}
				// rotate object - negative direction, because +z goes into screen and angle on [0,360)
				startObject.eulerAngles = new Vector3(0, 0, -currentAngle);
			}
			else if (rotateCCW) {
				currentAngle -= TouchManager.Instance.ROTATION_SPEED * Time.deltaTime;
				// keep currentAngle within [0,360)
				if (currentAngle < 0) {
					currentAngle += 360;
				}
				// make sure currentAngle doesn't pass over setAngle
				if (passedSetAngle(false)) {
					currentAngle = setAngle;
				}
				// rotate object - negative direction, because +z goes into screen and angle on [0,360)
				startObject.eulerAngles = new Vector3(0, 0, -currentAngle);
			}
		}
		else {	// dont rotate at all - unset flags
			rotateCW = false;
			rotateCCW = false;
		}

		// if level failed, countdown to reload/back to map
		if (isLevelFailed || isLevelPassed) {
			// increment timer
			currentWaitTime += Time.deltaTime;
			// is time passed, either reload level or return to map
			if (currentWaitTime >= END_LEVEL_WAIT_TIME) {
				if (isLevelFailed) {
					LevelLoader.Instance.ReloadLevel();
					LevelUIManager.Instance.UnloadFailureMessage();
				}
				else { // isLevelPassed
					LevelLoader.Instance.BackToMap();
					LevelUIManager.Instance.UnloadSuccessMessage();
				}
			}
		}
	}

	// ---

	// applies anything that needs to be registered in other objects
	private void LoadLevel() {
		startPoint = StartPoint.Instance.chargedObject;
		endPoint = EndPoint.Instance.chargedObject;
		objectContainer = ObjectContainer.Instance.gameObject;
		startObject = startPoint.transform.parent.transform;

		// THE FOLLOWING COULD BE UNSAFE - they rely on script execution order
		// could use some kind of coroutine or a simple polling-wait for non-null instance to set these...

		// set UI slider min/max values
		LevelUIManager.Instance.LoadThrustSlider(minThrust, maxThrust);
		// loads touchable colliders (start point) from level scene
		TouchManager.Instance.SetStartCollider(startPoint.GetComponent<Collider2D>());
		// loads the charged objects into PlayerProjectile
		PlayerProjectile.Instance.SetChargedObjects(GetAllChargedObjects());
		// sets the level number in the start menu UI object
		LevelUIManager.Instance.SetStartMenuLevelNumber(levelNumber);
		// set paused state and bring down start menu
		LevelUIManager.Instance.LoadLevel();
	}

	// ---

	public void LaunchProjectile() {
		if (!LevelUIManager.Instance.isPaused) {
			PlayerProjectile.Instance.Launch(new Vector2(angleVec.x, angleVec.y) * thrust);
		}
	}

	// ---

	public void SetThrust(float mThrust) {	
		if (!PlayerProjectile.Instance.isLaunched && !LevelUIManager.Instance.isPaused) {
			thrust = mThrust;
			LevelUIManager.Instance.SetThrustText(mThrust.ToString("0.0"));
		}
	}

	// -

	public void SetAngle(Vector2 mAngle) { 
		if (!PlayerProjectile.Instance.isLaunched && !LevelUIManager.Instance.isPaused) {
			angleVec = mAngle; 
			// technically Atan2 takes (y,x), not (x,y)... but this works really well somehow
			setAngle = Mathf.Atan2(mAngle.x, mAngle.y) * 180 / Mathf.PI;
			// convert angle to be on [0,360) instead of (-180,180]
			if (setAngle < 0) {
				setAngle += 360;
			}

			// determine angle to rotate (work done in notebook
			bool angleDiff = (setAngle - currentAngle) > 0;
			bool oppositeAngleDiff = (360 - Mathf.Abs(setAngle-currentAngle)) > Mathf.Abs(setAngle-currentAngle);
			rotateCW = angleDiff == oppositeAngleDiff;	// XNOR
			rotateCCW = angleDiff != oppositeAngleDiff;	// XOR
		}
	}

	// ---

	// determines if currentAngle has passed over setAngle
	// checks if it falls withing (setAngle, setAngle+TOLERANCE]
	//  to avoid changing currentAngle dramatically (ie, moving from 300deg -> 2deg)
	private bool passedSetAngle(bool isCW) {
		if (isCW) {
			return (currentAngle > setAngle) && (currentAngle <= (setAngle+TOLERANCE));
		}
		else {
			return (currentAngle < setAngle) && (currentAngle >= (setAngle-TOLERANCE));
		}
	}

	// ---

	public ChargedObject[] GetAllChargedObjects() {
		return objectContainer.GetComponentsInChildren<ChargedObject>();
	}

	// ---

	// pause game, displays failure message, reloads the level
	public void LevelFail() {
		LevelUIManager.Instance.SetPausedState(true);
		LevelUIManager.Instance.LoadFailureMessage();
		isLevelFailed = true;
	}

	// ---

	// pause game, and displays level success message
	public void LevelSucceed() {
		LevelUIManager.Instance.SetPausedState(true);
		LevelUIManager.Instance.LoadSuccessMessage();
		// TODO - set flags to mark level as completed and unlock next level
		isLevelPassed = true;
	}


}
