using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Transform))]
public class ComputerControls : MonoBehaviour {
	[Range(0.0001f, 1000.0f)]
	[Tooltip("The scaling factor from aiming vector (which is magnitude ≤1) to force applied.")]
	public float forceScale = 1.0f;

	[Range(0.0001f, 1000.0f)]
	[Tooltip("The minimum limit of force applied when firing.")]
	public float forceMin = 1.0f;

	[Tooltip("The projectile prefab to spawn when launching a projectile.")]
	public GameObject projectilePrefab;

	[Range(0.0001f, 1000.0f)]
	[Tooltip("The minimum length the user can pull in the cable to.")]
	public float minCableLength = 1.0f;

	[Range(0.0001f, 10.0f)]
	[Tooltip("The length of cable to pull in per tick when retracting.")]
	public float cableRetractSpeed = 0.1f;

	[Tooltip("The audio source to play when the connector plugs in.")]
	public AudioSource plugSound;

	[Tooltip("The audio source to play when the connector unplugs.")]
	public AudioSource unplugSound;

	private InputAction fireAction;

	private Vector2 lastAimScreen, lastAimAbsolute;
	private GameObject projectile;
	[HideInInspector]
	public Rope rope;
	[HideInInspector]
	public Target connectedTarget;

	private Vector2 Aim {
		get {
			if(lastAimScreen != null) {
				// Get the distance from the computer to the mouse pointer.
				Camera cam = Camera.main;
				Vector2 worldDistance = cam.ScreenToWorldPoint(lastAimScreen) - GetComponent<Transform>().position;

				// Scale so that camera zoom does not affect the vector length;
				// putting the mouse at the top of the screen while the
				// computer is in the exact middle will result in a vector of
				// length 1 no matter the camera zoom level.
				Vector2 scaled = worldDistance / cam.orthographicSize;

				// The aim vector could still be of magnitude >1 for a two
				// reasons: (1) the computer isn’t in the exact middle of the
				// screen, or (2) the player is aiming left or right, and their
				// screen is wider than it is high. To accommodate those cases,
				// clamp.
				return Vector2.ClampMagnitude(scaled, 1f);
			} else if(lastAimAbsolute != null) {
				return lastAimAbsolute;
			} else {
				return Vector2.zero;
			}
		}
	}

	void Start() {
		fireAction = GetComponent<PlayerInput>().actions["Fire"];
	}

	void OnAimScreen(InputValue inputValue) {
		lastAimScreen = inputValue.Get<Vector2>();
	}

	void OnAimAbsolute(InputValue inputValue) {
		lastAimAbsolute = inputValue.Get<Vector2>();
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
		if(rope != null) {
			Destroy(rope.gameObject);
			rope = null;
		}
		if(connectedTarget != null) {
			connectedTarget.onUnplug.Invoke();
			connectedTarget = null;
			unplugSound.Play();
		}
	}

	void OnReset() {
		GameObject.Find("Startup").GetComponent<Startup>().ResetLevel();
	}

	void FixedUpdate() {
		bool firing = fireAction.phase == InputActionPhase.Started;
		if(firing) {
			if(projectile == null && connectedTarget == null) {
				Vector2 aim = Aim;
				Transform t = GetComponent<Transform>();
				float rot = Vector2.Angle(Vector2.right, aim);
				projectile = Instantiate(projectilePrefab, t.position, Quaternion.Euler(0f, 0f, rot), t.parent);
				Rigidbody2D myBody = GetComponent<Rigidbody2D>();
				Rigidbody2D projectileBody = projectile.GetComponent<Rigidbody2D>();
				projectileBody.velocity = myBody.velocity;
				projectileBody.angularVelocity = myBody.angularVelocity;
				projectile.GetComponent<Projectile>().launcher = this;
				Physics2D.IgnoreCollision(GetComponent<Collider2D>(), projectile.GetComponent<Collider2D>());
				Vector2 forceVector = aim * forceScale;
				if (forceVector.sqrMagnitude < forceMin * forceMin) {
					forceVector = forceVector.normalized * forceMin;
				}
				projectileBody.AddForce(forceVector, ForceMode2D.Impulse);
			} else if(rope != null) {
				float oldDist = rope.length;
				float newDist = Mathf.Max(oldDist - cableRetractSpeed, minCableLength);
				rope.length = newDist;
				GetComponent<Rigidbody2D>().WakeUp();
				connectedTarget.GetComponent<Rigidbody2D>().WakeUp();
			}
		}
	}
}
