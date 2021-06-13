using UnityEngine;
using UnityEngine.Events;

public class ReportCombineDone : MonoBehaviour {
	[Tooltip("Emitted when the CombineDone animation event occurs.")]
	public UnityEvent onCombineDone;

	private void CombineDone() {
		onCombineDone.Invoke();
	}
}
