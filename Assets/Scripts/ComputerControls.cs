using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Transform))]
public class ComputerControls : MonoBehaviour {
	[Range(0.0001f, 10.0f)]
	[Tooltip("The scaling factor from mouse distance to force applied.")]
	public float forceScale = 1.0f;

	[Range(0.0001f, 1000.0f)]
	[Tooltip("The minimum limit of force applied when firing.")]
	public float forceMin = 1.0f;

	[Range(0.0001f, 1000.0f)]
	[Tooltip("The maximum limit of force applied when firing.")]
	public float forceMax = 10.0f;

	[Tooltip("The projectile prefab to spawn when launching a projectile.")]
	public GameObject projectilePrefab;

	[Range(0.0001f, 1000.0f)]
	[Tooltip("The minimum length the user can pull in the cable to.")]
	public float minCableLength = 1.0f;

	[Range(0.0001f, 10.0f)]
	[Tooltip("The length of cable to pull in per tick when retracting.")]
	public float cableRetractSpeed = 0.1f;

	private InputAction fireAction;

	private Vector2 aim;
	private GameObject projectile;
	[HideInInspector]
	public SpringJoint2D spring;
	[HideInInspector]
	public Target connectedTarget;

	void Start() {
		fireAction = GetComponent<PlayerInput>().actions["Fire"];
	}

	void OnAim(InputValue inputValue) {
		aim = Camera.main.ScreenToWorldPoint(inputValue.Get<Vector2>()) - GetComponent<Transform>().position;
	}

	void OnInteract() {
		if(connectedTarget != null) {
			connectedTarget.onInteract.Invoke();
		}
	}

	void OnUnplug() {
		if(projectile != null) {
			Destroy(projectile);
			projectile = null;
		}
		if(spring != null) {
			Destroy(spring);
			spring = null;
		}
		connectedTarget = null;
	}

	void OnReset() {
		GameObject.Find("Startup").GetComponent<Startup>().ResetLevel();
	}

	void FixedUpdate() {
		if(spring != null) {
			spring.autoConfigureDistance = false;
		}

		bool firing = fireAction.phase == InputActionPhase.Started;
		if(firing) {
			if(projectile == null) {
				Transform t = GetComponent<Transform>();
				projectile = Instantiate(projectilePrefab, t.position, t.rotation, t.parent);
				Rigidbody2D myBody = GetComponent<Rigidbody2D>();
				Rigidbody2D projectileBody = projectile.GetComponent<Rigidbody2D>();
				projectileBody.velocity = myBody.velocity;
				projectileBody.angularVelocity = myBody.angularVelocity;
				projectile.GetComponent<Projectile>().launcher = this;
				Physics2D.IgnoreCollision(GetComponent<Collider2D>(), projectile.GetComponent<Collider2D>());
				Vector2 forceVector = Vector2.ClampMagnitude(aim * forceScale, forceMax);
				if (forceVector.magnitude < forceMin) {
					forceVector = forceVector.normalized * forceMin;
				}
				projectileBody.AddForce(forceVector, ForceMode2D.Impulse);
			} else if(spring != null) {
				float oldDist = spring.distance;
				float newDist = Mathf.Max(oldDist - cableRetractSpeed, minCableLength);
				spring.distance = newDist;
				GetComponent<Rigidbody2D>().WakeUp();
			}
		}
	}
}
