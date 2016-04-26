using UnityEngine;
using System.Collections;

// end point singleton - for easy referencing by LevelManager
public class EndPoint : Singleton<EndPoint> {

	public ChargedObject chargedObject { get; private set; }

	void Start() {
		chargedObject = this.gameObject.GetComponent<ChargedObject>();
	}
}
