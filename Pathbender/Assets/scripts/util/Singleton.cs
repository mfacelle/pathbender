using UnityEngine;
using System.Collections;

// singleton based on:
//	http://answers.unity3d.com/questions/408518/dontdestroyonload-duplicate-object-in-a-singleton.html
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	public static T Instance { get; private set; }

	public bool isPersistent = true;	// set in editor

	// -----

	public virtual void Awake() 
	{
		if(isPersistent) {	
			// is persistent: any other instances created 
			//	AFTER the first will be immediately destroyed
			if(!Instance) {
				Instance = this as T;
			}
			else {
				DestroyObject(gameObject);
			}
			DontDestroyOnLoad(gameObject);
		}
		else {
			// not persistent: overwrite any previously-created instances
			Instance = this as T;
		}
	}
}
