using UnityEngine;
using System.Collections;

// represents stars and planets
// rotates on an axis and has mass
public class PlanetaryObject : MonoBehaviour 
{
	public float mass;			// object mass
	public float rotationSpeed;	// speed of rotation (deg/frame)

	private float rotationY;

	// ---

	void Start () 
	{
		rotationY = 0;
	}

	// ---

	void Update () 
	{
		// rotate object
		rotationY += rotationSpeed;
		if (rotationY >= 360)
			rotationY -= 360;
		transform.eulerAngles = new Vector3(transform.eulerAngles.x, rotationY, transform.eulerAngles.z);
	}
}
