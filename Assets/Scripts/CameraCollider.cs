using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollider : MonoBehaviour {
	[Tooltip("New location for the camera to move to")]
	public Vector3 cameraTargetPosition = new Vector3(0, 0, -10);

	[Tooltip("New camera size")]
	public float cameraTargetSize = 10f;

	private Camera targetCamera;
	private FollowWithSmoothing targetSmoothing;

	private void Start() {
		GameObject targetObject = GameObject.Find("Main Camera");
		targetCamera = targetObject.GetComponentInChildren<Camera>();
		targetSmoothing = targetObject.GetComponentInChildren<FollowWithSmoothing>();
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.tag == "Player") {
			if (targetSmoothing) {
				targetSmoothing.SetNewTarget(cameraTargetPosition, cameraTargetSize);
			} else {
				targetCamera.transform.position = cameraTargetPosition;
				targetCamera.orthographicSize = cameraTargetSize;
			}
		}
	}
}
