using UnityEngine;
using UnityEngine.Events;

public class Counter : MonoBehaviour {
	[Tooltip("The number to count to before emitting the done event.")]
	public int maxCount;

	[Tooltip("Emitted when Increment has been called maxCount times.")]
	public UnityEvent onDone;

	private int count;

	public void Increment() {
		if(++count == maxCount) {
			onDone.Invoke();
		}
	}
}
