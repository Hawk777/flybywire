using UnityEngine;

public class Target : MonoBehaviour {
	void OnCollisionEnter2D(Collision2D collision) {
		GameObject projectile = collision.gameObject;
		if(projectile.tag == "Projectile") {
			ComputerControls launcher = projectile.GetComponent<Projectile>().launcher;

			FixedJoint2D joint = projectile.AddComponent<FixedJoint2D>();
			joint.connectedBody = GetComponent<Rigidbody2D>();
			joint.enableCollision = false;

			SpringJoint2D spring = launcher.gameObject.AddComponent<SpringJoint2D>();
			spring.autoConfigureDistance = true;
			spring.dampingRatio = 0f;
			spring.frequency = 1f;
			spring.connectedBody = GetComponent<Rigidbody2D>();
			spring.enableCollision = true;
			launcher.spring = spring;

			launcher.activationTarget = GetComponent<Activatable>();
		}
	}
}
