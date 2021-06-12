using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Transform))]
public class ComputerControls : MonoBehaviour {
	[Tooltip("The scaling factor from mouse distance to force applied.")]
	public float forceScale = 1.0f;

	[Tooltip("The maximum limit of force applied when firing.")]
	public float forceMax = 10.0f;

	[Tooltip("The projectile prefab to spawn when launching a projectile.")]
	public GameObject projectilePrefab;

	private Vector2 aim;
	private GameObject projectile;

	void OnAim(InputValue inputValue) {
		aim = Camera.main.ScreenToWorldPoint(inputValue.Get<Vector2>()) - GetComponent<Transform>().position;
	}

	void OnFire() {
		if(projectile == null) {
			projectile = Instantiate(projectilePrefab, GetComponent<Transform>());
			Physics2D.IgnoreCollision(GetComponent<Collider2D>(), projectile.GetComponent<Collider2D>());
			Rigidbody2D projectileBody = projectile.GetComponent<Rigidbody2D>();
			projectileBody.AddForce(Vector2.ClampMagnitude(aim * forceScale, forceMax), ForceMode2D.Impulse);
		}
	}

	void OnUnplug() {
		if(projectile != null) {
			Destroy(projectile);
			projectile = null;
		}
	}
}
