using UnityEngine;

[RequireComponent(typeof(Transform))]
public class FollowWithSmoothing : MonoBehaviour {
	[Tooltip("Speed at which the follower settles")]
	public float speed = 2.0f;

	[Tooltip("Distance at which interpolation stops and the follower jumps")]
	public float jumpDist = 0.1f;

	private Camera targetCamera;
	private Vector3 targetPosition;
	private float targetSize;
	private GameObject targetObject = null;

	void Start() {
		targetCamera = GetComponentInChildren<Camera>();
		targetPosition = transform.position;
		targetSize = targetCamera.orthographicSize;
	}

	void Update() {
		float interpolation = speed * Time.deltaTime;

		Transform me = GetComponent<Transform>();
		Vector2 oldPos = me.position;
		Vector2 targetPos = targetObject? targetObject.transform.position : targetPosition;
		Vector2 newPos;
		if((targetPos - oldPos).sqrMagnitude <= jumpDist * jumpDist) {
			newPos = targetPos;
		} else {
			newPos = Vector2.Lerp(oldPos, targetPos, interpolation);
		}
		me.position = new Vector3(newPos.x, newPos.y, me.position.z);

		float oldSize = targetCamera.orthographicSize;
		float newSize;
		if (Mathf.Abs(targetSize - oldSize) <= jumpDist) {
			newSize = targetSize;
		} else {
			newSize = Mathf.Lerp(oldSize, targetSize, interpolation);
		}
		targetCamera.orthographicSize = newSize;
	}

	public void SetNewTarget(Vector3 newTargetPosition, float newTargetSize) {
		targetObject = null;
		targetPosition = newTargetPosition;
		targetSize = newTargetSize;
	}

	public void SetNewTarget(GameObject newTargetObject, float newTargetSize) {
		targetObject = newTargetObject;
		targetSize = newTargetSize;
	}
}
