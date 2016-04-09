using UnityEngine;
using System.Collections;

// represents the player
// singleton provides universal access to the ChargedProjectile object
public class PlayerProjectile : Singleton<PlayerProjectile> 
{
	// the ChargedProjectile object also attached to this GameObject
	public ChargedProjectile projectile { get; private set; }

	void Start() {
		projectile = this.gameObject.GetComponent<ChargedProjectile>();
	}

	// ---

	public void ApplyForce(Vector2 force) {
		projectile.ApplyForce(force);
	}
}
