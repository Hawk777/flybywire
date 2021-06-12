using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Transform))]
public class HorizontalKinematicMover : MonoBehaviour {
	[Tooltip("The target X coordinate the machine is trying to move towards.")]
	public float target;

	[Tooltip("The acceleration rate, in units per second².")]
	public float acceleration = 1f;

	[Tooltip("The maximum movement speed in normal mode, in units per second.")]
	public float maxSpeed = 1f;

	[Range(0.000000000001f, 1f)]
	[Tooltip("The distance threshold below which the object is considered arrived.")]
	public float distanceThreshold = 0.01f;

	[Range(0.000000000001f, 1f)]
	[Tooltip("The speed threshold below which the object is considered stationary.")]
	public float speedThreshold = 0.01f;

	[Tooltip("Emitted when the machine arrives at its target.")]
	public UnityEvent onArrived;

	public void SetAcceleration(float accel) {
		acceleration = accel;
	}

	public void SetMaxSpeed(float max) {
		maxSpeed = max;
	}

	void FixedUpdate() {
		Rigidbody2D body = GetComponent<Rigidbody2D>();
		if(body.bodyType == RigidbodyType2D.Kinematic) {
			float error = target - GetComponent<Transform>().position.x;
			if(Mathf.Abs(error) <= distanceThreshold && body.velocity.x <= speedThreshold) {
				onArrived.Invoke();
			}
			float vx = Mathf.Sign(error) * Mathf.Sqrt(2f * acceleration * Mathf.Abs(error));
			vx = Mathf.Clamp(vx, -maxSpeed, maxSpeed);
			float oldVX = body.velocity.x;
			float accelAsSpeedPerTick = acceleration * Time.fixedDeltaTime;
			vx = Mathf.Clamp(vx, oldVX - accelAsSpeedPerTick, oldVX + accelAsSpeedPerTick);
			body.velocity = new Vector2(vx, 0);
		}
	}
}
