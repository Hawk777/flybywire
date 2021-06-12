using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollider : MonoBehaviour {
	public Camera targetCamera;
	public Vector3 cameraTargetPosition = new Vector3(0, 0, -10);
	public float cameraTargetSize = 10f;

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.tag == "Player") {
			targetCamera.transform.position = cameraTargetPosition;
			targetCamera.orthographicSize = cameraTargetSize;
		}
	}
}
