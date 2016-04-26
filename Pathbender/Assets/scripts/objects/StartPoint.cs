using UnityEngine;
using System.Collections;

// start point singleton - for easy referencing by LevelManager
public class StartPoint : Singleton<StartPoint> {

	public ChargedObject chargedObject { get; private set; }

	void Start() {
		chargedObject = this.gameObject.GetComponent<ChargedObject>();
	}
}
