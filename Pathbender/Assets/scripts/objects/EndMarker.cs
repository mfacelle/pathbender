using UnityEngine;
using System.Collections;

// a small object rotating around the EndPoint - serves as a marker for players
public class EndMarker : MonoBehaviour {

	public float rotationSpeed;


	void Update() {
		// rotate the marker around the parent (on z-axis)
		transform.RotateAround(transform.parent.transform.position, new Vector3(0,0,1), rotationSpeed);
	}
}
