using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Transform))]
public class DynamicKinematicSwitcher : MonoBehaviour {
	[Tooltip("The collider that collides with the floor.")]
	public Collider2D floorCollider;

	[Range(0.000000000001f, 1f)]
	[Tooltip("The speed threshold below which the object is considered stationary.")]
	public float speedThreshold = 0.01f;

	[Range(0.000000000001f, 10f)]
	[Tooltip("The angular speed threshold below which the object is considered stationary.")]
	public float angularSpeedThreshold = 0.01f;

	[Range(0.000000000001f, 1f)]
	[Tooltip("The rotation threshold below which the object is considered upright.")]
	public float rotationThreshold = 0.2f;

	private static ContactPoint2D[] contactPoints = new ContactPoint2D[1];

	void Update() {
		Rigidbody2D body = GetComponent<Rigidbody2D>();
		bool touchingFloor = floorCollider.GetContacts(contactPoints) != 0;
		bool stoppedMoving = body.velocity.sqrMagnitude <= speedThreshold * speedThreshold;
		bool stoppedRotating = Mathf.Abs(body.angularVelocity) <= angularSpeedThreshold;
		bool upright = Mathf.Approximately(0f, Mathf.MoveTowardsAngle(body.rotation, 0f, rotationThreshold));
		if(touchingFloor && stoppedMoving && stoppedRotating && upright) {
			// The object is touching the floor, not translating, not rotating,
			// and roughly upright. Switch to kinematic physics.
			body.bodyType = RigidbodyType2D.Kinematic;
			body.angularVelocity = 0;
			body.velocity = Vector2.zero;
			body.rotation = 0f;
		} else if(!touchingFloor || !stoppedRotating || !upright) {
			// The object fails one of these conditions, except for not
			// translating. Switch to dynamic physics to allow gravity and
			// collisions to happen. Ignore translation because the translation
			// could be caused by the kinematic driver.
			body.bodyType = RigidbodyType2D.Dynamic;
		}
	}
}
