using UnityEngine;
using UnityEngine.Events;

public class ReportStart : MonoBehaviour {
	[Tooltip("Emitted when the object starts.")]
	public UnityEvent onStart;

	private void Start() {
		onStart.Invoke();
	}
}
