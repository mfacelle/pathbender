using UnityEngine;
using System.Collections;

// for other classes to check if debug functionality is activated
public class DebugManager : Singleton<DebugManager> 
{
	public bool isMouseClickDebug;		// if mouse clicks are enabled
	public bool debugObjectsActive;		// if debug "reload" button active
	public bool isLevelDebug;			// if debug level is always loaded
}
