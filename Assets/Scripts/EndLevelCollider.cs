using UnityEngine;

public class EndLevelCollider : MonoBehaviour {
	[Tooltip("The objects to activate when the player touches this collider.")]
	public GameObject[] objects;

	private void OnTriggerEnter2D(Collider2D collision) {
		if(collision.gameObject.tag == "Player") {
			foreach(GameObject i in objects) {
				i.SetActive(true);
			}
			Time.timeScale = 0f;
		}
	}
}
