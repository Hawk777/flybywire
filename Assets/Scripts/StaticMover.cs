using UnityEngine;

[RequireComponent(typeof(Transform))]
[RequireComponent(typeof(Toggleable))]
public class StaticMover : MonoBehaviour {
	[Tooltip("The distance the machine moves when turned on.")]
	public Vector2 moveDistance;

	[Tooltip("The time the machine takes to move between positions, in seconds.")]
	[Range(0.0f, 1000.0f)]
	public float moveTime;

	private Vector2 offPosition, onPosition;
	private float moveProgress;

	void Start() {
		Transform t = GetComponent<Transform>();
		offPosition = t.position;
		onPosition = offPosition + moveDistance;
		UpdatePosition();
	}

	void Update() {
		moveProgress = Mathf.Clamp01(moveProgress + (GetComponent<Toggleable>().isOn ? 1f : -1f) * Time.deltaTime / moveTime);
		UpdatePosition();
	}

	private void UpdatePosition() {
		Transform t = GetComponent<Transform>();
		Vector2 pos = Vector2.LerpUnclamped(offPosition, onPosition, moveProgress);
		t.position = new Vector3(pos.x, pos.y, t.position.z);
	}
}
