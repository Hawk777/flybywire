using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Transform))]
[RequireComponent(typeof(Toggleable))]
public class StaticMover : MonoBehaviour {
	[Tooltip("The object the machine moves to when turned on.")]
	public Transform onPosition;

	[Tooltip("The speed of the machine’s movement, in units per second.")]
	[Range(0.0f, 1000.0f)]
	public float speed = 1f;

	[Tooltip("Emitted when the machine starts moving away from the off position.")]
	public UnityEvent turnedOn;

	[Tooltip("Emitted when the machine reaches the off position and stops moving.")]
	public UnityEvent turnedOff;

	private bool wasOn;
	private Vector2 offPosition;

	void Start() {
		wasOn = false;
		offPosition = GetComponent<Transform>().position;
	}

	void Update() {
		bool isOn = GetComponent<Toggleable>().isOn;
		if(isOn && !wasOn) {
			wasOn = true;
			turnedOn.Invoke();
		}
		Vector2 target = isOn ? (Vector2) onPosition.position : offPosition;
		Transform t = GetComponent<Transform>();
		Vector2 pos = Vector2.MoveTowards(t.position, target, speed * Time.deltaTime);
		t.position = new Vector3(pos.x, pos.y, t.position.z);
		if((pos - target).sqrMagnitude < 0.0001f && wasOn && !isOn) {
			wasOn = false;
			turnedOff.Invoke();
		}
	}
}
