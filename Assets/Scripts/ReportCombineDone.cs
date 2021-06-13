using UnityEngine;
using UnityEngine.Events;

public class ReportCombineDone : MonoBehaviour {
	[Tooltip("Emitted when the AnimationDone animation event occurs.")]
	public UnityEvent onCombineDone;

	private void AnimationDone() {
		onCombineDone.Invoke();
	}
}
