using UnityEngine;
using System.Collections;

// represents the player
// singleton provides universal access to the ChargedProjectile object
public class PlayerProjectile : Singleton<PlayerProjectile> 
{
	// if distance is below min, ignore it (likely the same particle)
	private const float MIN_DISTANCE = 0.05f;
	// if distance is greater than max, ignore it (negligible anyway)
	private const float MAX_DISTANCE = 8.0f;

	// the ChargedProjectile object also attached to this GameObject
	public ChargedProjectile projectile { get; private set; }

	// the other charged particles in the scene
	public ChargedObject[] objects { get; private set; }

	// ---

	void Start() {
		projectile = this.gameObject.GetComponent<ChargedProjectile>();
		objects = LevelManager.Instance.objectContainer.GetComponentsInChildren<ChargedObject>();
	}

	// ---

	// iterate over all charged objects and apply electromagnetic force
	void FixedUpdate() {
		// only apply force if the projectile has been launched
		if (LevelManager.Instance.isProjectileLaunched) {
			Vector2 projectilePosition = projectile.transform.position;
			float totalForceX = 0;
			float totalForceY = 0;
			float forceMag;
			float forceAngle;
			float dx, dy, r;
			ChargedObject obj;
			for (int i = 0; i < objects.Length; i++) {
				obj = objects[i];
				// ignore if object is neutral-charged
				if (obj.charge == 0) {
					continue;
				}
				// calculate force and get angle
				dx = projectilePosition.x - obj.transform.position.x;
				dy = projectilePosition.y - obj.transform.position.y;
				r = Mathf.Sqrt(dx*dx + dy*dy);
				if (MIN_DISTANCE < r && r < MAX_DISTANCE) {
					forceAngle = Mathf.Atan2(dx, dy);
					forceMag = ElectrostaticForce(obj.charge, projectile.charge, r);
					// adjust force is projectile is
					if (projectile.isAttractAll) {
						if (obj.isRepelAll) {	
							forceMag = 0;	// omnitive and abnitive have no effect on each other
						}
						else if (forceMag > 0) {
							forceMag *= -1;	// reverse force direction if it's not attractive
						}
					}
					if (projectile.isRepelAll) {
						if (obj.isAttractAll) {	
							forceMag = 0;	// omnitive and abnitive have no effect on each other
						}
						else if (forceMag < 0) {
							forceMag *= -1;	// reverse force direction if it's repulsive
						}
					}
					//Debug.Log("ADDING FORCE FROM CHARGE="+obj.charge);
					Debug.Log("\tFORCE:"+forceMag+" angle:"+forceAngle);
					totalForceX += forceMag * (dx/r);
					totalForceY += forceMag * (dy/r);
				}
			}
			//Debug.Log("force to apply: " + totalForceX + ", " + totalForceY);
			ApplyForce(new Vector2(totalForceX, totalForceY));
		}
	}

	// ---

	public void ApplyForce(Vector2 force) {
		projectile.ApplyForce(force);
	}

	// ---

	// compute electrostatic force
	// 1/r^2 force too weak (although realistic)
	// 1/r force too strong
	private float ElectrostaticForce(float q1, float q2, float r) {
		return PhysicsManager.Instance.E * q1 * q2 / (r*r);
	}

}
