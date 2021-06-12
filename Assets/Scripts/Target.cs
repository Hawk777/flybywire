using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class Target : MonoBehaviour {
	[Tooltip("Emitted when the machine is activated by the player.")]
	public UnityEvent activated;

	void OnCollisionEnter2D(Collision2D collision) {
		GameObject projectile = collision.gameObject;
		if(projectile.tag == "Projectile") {
			ComputerControls launcher = projectile.GetComponent<Projectile>().launcher;

			// The projectile should stick, perfectly, to the target, and move
			// with it. To accomplish this, simply destroy its own rigid body
			// and make it a child object of the target.
			Destroy(projectile.GetComponent<Rigidbody2D>());
			projectile.GetComponent<Transform>().SetParent(GetComponent<Transform>(), true);

			// Create the cable between the launcher and the projectile.
			SpringJoint2D spring = launcher.gameObject.AddComponent<SpringJoint2D>();
			spring.autoConfigureDistance = true;
			spring.dampingRatio = 0f;
			spring.frequency = 1f;
			spring.connectedBody = GetComponent<Rigidbody2D>();
			spring.enableCollision = true;
			launcher.spring = spring;

			// Record the object that was hit.
			launcher.connectedTarget = this;
		}
	}
}
