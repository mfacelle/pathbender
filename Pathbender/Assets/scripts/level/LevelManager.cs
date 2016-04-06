using UnityEngine;
using System.Collections;

// class containing values used in levels
// object is singleton, but each level has it's own instance
// must be a non-persistent object for each new LevelValue to overwrite the Instance
public class LevelManager : Singleton<LevelManager> 
{
	// current values
	public Vector2 angle { get; private set; }
	public float thrust { get; private set; }

	// values per-level
	public ChargedObject startPoint;	// start object
	public ChargedObject endPoint;		// goal object
	public int levelNumber;	// int value (i.e. level 1, level 2, ...)
	public int minThrust;	// min thrust on slider
	public int maxThrust;	// max thrust on slider

	// ---

	void Start () 
	{
		// ensure that thrust values are in the correct order
		if (minThrust > maxThrust) {
			int temp = maxThrust;
			maxThrust = minThrust;
			minThrust = temp;
		}
		// set UI slider min/max values
		LevelUIManager.Instance.setThrustRange(minThrust, maxThrust);
		// initialize thrust and angle variables
		thrust = minThrust;
		angle = new Vector2(0,0);
	}

	// ---

	public void setThrust(float mThrust) {	thrust = mThrust; }
	public void setAngle(Vector2 mAngle) { angle = mAngle; }
}
