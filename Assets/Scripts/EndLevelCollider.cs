using UnityEngine;
using UnityEngine.Events;

public class EndLevelCollider : MonoBehaviour {
	[Tooltip("The objects to activate when the player touches this collider.")]
	public GameObject[] objects;

	[Tooltip("Emitted when the player touches this collider..")]
	public UnityEvent onTouch;

	private void OnTriggerEnter2D(Collider2D collision) {
		if(collision.gameObject.tag == "Player") {
			foreach(GameObject i in objects) {
				i.SetActive(true);
			}
			onTouch.Invoke();
			Time.timeScale = 0f;
		}
	}
}
