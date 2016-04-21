using UnityEngine;
using System.Collections;

// debug objects are not active during regular gameplay
public class DebugObject : MonoBehaviour 
{

	// turns off the object if debug mode is not active
	void Start () {
		if (!(DebugManager.Instance.isDeviceDebug || DebugManager.Instance.isPCDebug)) {
			this.gameObject.SetActive(false);
		}
	}
}
