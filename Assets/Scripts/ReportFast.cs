using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class ReportFast : MonoBehaviour {
	[Range(0, Mathf.Infinity)]
	[Tooltip("The speed that is considered fast.")]
	public float speedThreshold;

	[Tooltip("Emitted when the object is moving fast.")]
	public UnityEvent onFast;

	private void FixedUpdate() {
		if(GetComponent<Rigidbody2D>().velocity.sqrMagnitude >= speedThreshold * speedThreshold) {
			onFast.Invoke();
		}
	}
}
