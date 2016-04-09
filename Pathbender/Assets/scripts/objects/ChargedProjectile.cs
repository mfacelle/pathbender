using UnityEngine;
using System.Collections;

// a charged object that is used as a projectile
// used by the "player" 
public class ChargedProjectile : ChargedObject
{
	private Rigidbody2D body;

	// ---

	void Start() {
		body = GetComponent<Rigidbody2D>();
	}

	// ---

	public void ApplyForce(Vector2 force) {
		body.AddForce(force, ForceMode2D.Impulse);
	}
}
