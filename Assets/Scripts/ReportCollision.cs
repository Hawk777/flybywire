using UnityEngine;
using UnityEngine.Events;

public class ReportCollision : MonoBehaviour {
	[Tooltip("Emitted when the object is involved in a collision.")]
	public UnityEvent onCollide;

	private void OnCollisionEnter2D(Collision2D other) {
		onCollide.Invoke();
	}
}
