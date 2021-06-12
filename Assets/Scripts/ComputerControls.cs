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

	private Vector2 aim;

	void OnAim(InputValue inputValue) {
		aim = Camera.main.ScreenToWorldPoint(inputValue.Get<Vector2>()) - GetComponent<Transform>().position;
	}

	void OnFire() {
		Rigidbody2D body = GetComponent<Rigidbody2D>();
		body.AddForce(Vector2.ClampMagnitude(aim * forceScale, forceMax), ForceMode2D.Impulse);
	}
}
