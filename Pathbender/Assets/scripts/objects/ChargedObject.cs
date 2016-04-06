using UnityEngine;
using System.Collections;

// an object that has a charge (attracts/repels player)
public class ChargedObject : MonoBehaviour 
{
	public float charge;		// value of charge (+/- magnitude)
	public bool isAttractAll;	// true: force always attracts
	public bool isRepelAll;		// true: force always repels
}
