using UnityEngine;
using System.Collections;

// debug objects are not active during regular gameplay
public class DebugObject : MonoBehaviour 
{

	// turns off the object if debug mode is not active
	void Start () {
		if (!DebugManager.Instance.debugObjectsActive) {
			this.gameObject.SetActive(false);
		}
	}
}
