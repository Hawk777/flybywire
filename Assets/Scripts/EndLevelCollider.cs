using UnityEngine;
using UnityEngine.Events;

public class EndLevelCollider : MonoBehaviour {
	[Tooltip("Emitted when the player touches this collider..")]
	public UnityEvent onTouch;

	private void OnTriggerEnter2D(Collider2D collision) {
		if(collision.gameObject.tag == "Player") {
			onTouch.Invoke();
			Time.timeScale = 0f;
		}
	}
}
