using UnityEngine;

public class Target : MonoBehaviour {
	void OnCollisionEnter2D(Collision2D collision) {
		GameObject projectile = collision.gameObject;
		if(projectile.tag == "Projectile") {
			FixedJoint2D joint = projectile.AddComponent<FixedJoint2D>();
			joint.connectedBody = GetComponent<Rigidbody2D>();
			joint.enableCollision = false;
		}
	}
}
