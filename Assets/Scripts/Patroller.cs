using UnityEngine;

[RequireComponent(typeof(HorizontalKinematicMover))]
[RequireComponent(typeof(Transform))]
public class Patroller : MonoBehaviour {
	[Tooltip("The patrol points.")]
	public Transform[] points;

	[Tooltip("The transform to mirror if the next target is to the right of the current position.")]
	public Transform mirrorTransform;

	private int currentTarget;

	void Start() {
		SetTarget();
	}

	public void OnArrived() {
		currentTarget = (currentTarget + 1) % points.Length;
		SetTarget();
	}

	private void SetTarget() {
		float targetX = points[currentTarget].position.x;
		GetComponent<HorizontalKinematicMover>().target = targetX;
		if(mirrorTransform != null) {
			float xScale = Mathf.Sign(targetX - GetComponent<Transform>().position.x);
			mirrorTransform.localScale = new Vector3(xScale, 1f, 1f);
		}
	}
}
