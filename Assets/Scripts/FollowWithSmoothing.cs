using UnityEngine;

[RequireComponent(typeof(Transform))]
public class FollowWithSmoothing : MonoBehaviour {
	[Tooltip("Name of the object that the follower should follow")]
	public string targetName;

	[Tooltip("Speed at which the follower settles")]
	public float speed = 2.0f;

	[Tooltip("Distance at which interpolation stops and the follower jumps")]
	public float jumpDist = 0.1f;

	private Transform target;

	void Start() {
		target = GameObject.Find(targetName).GetComponent<Transform>();
	}

	void Update() {
		float interpolation = speed * Time.deltaTime;
		Transform me = GetComponent<Transform>();
		Vector2 oldPos = me.position;
		Vector2 targetPos = target.position;
		Vector2 newPos;
		if((targetPos - oldPos).sqrMagnitude <= jumpDist * jumpDist) {
			newPos = targetPos;
		} else {
			newPos = Vector2.Lerp(oldPos, targetPos, interpolation);
		}
		me.position = new Vector3(newPos.x, newPos.y, me.position.z);
	}
}
