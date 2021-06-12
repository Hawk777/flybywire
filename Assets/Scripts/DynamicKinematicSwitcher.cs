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

	private static Collider2D[] touchingColliders = new Collider2D[20];

	void Update() {
		Rigidbody2D body = GetComponent<Rigidbody2D>();
		bool touchingFloor = TouchingFloor();
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

	private bool TouchingFloor() {
		Rigidbody2D body = GetComponent<Rigidbody2D>();
		ContactFilter2D filter = new ContactFilter2D();
		filter.NoFilter();
		int touchingCount = -1;
		while(touchingCount == -1) {
			touchingCount = floorCollider.OverlapCollider(filter, touchingColliders);
			if(touchingCount == touchingColliders.Length) {
				// Too many; enlarge array and try again.
				touchingColliders = new Collider2D[touchingColliders.Length * 2];
				touchingCount = -1;
			}
		}

		// Only consider us to be touching the floor if one of the colliders is
		// a solid object (i.e. not a trigger) and is not a piece of ourself.
		for(int i = 0; i != touchingCount; ++i) {
			Rigidbody2D touchingBody = touchingColliders[i].attachedRigidbody;
			if(!touchingColliders[i].isTrigger && touchingBody != body) {
				return true;
			}
		}
		return false;
	}
}
