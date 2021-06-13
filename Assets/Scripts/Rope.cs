using UnityEngine;

public class Rope : MonoBehaviour {
	[Tooltip("The length of the rope, in world units")]
	public float length;

	[Tooltip("The spring constant, in force units per world unit, applied when the rope is overextended.")]
	public float springConstant;

	[Tooltip("The two objects to which the rope is connected.")]
	public Rigidbody2D[] objects;

	void FixedUpdate() {
		Vector2 delta = objects[1].position - objects[0].position;
		Vector2 direction = delta.normalized;
		float actualLength = delta.magnitude;
		if(actualLength > length) {
			float halfForce = (actualLength - length) * springConstant * 0.5f;
			for(int i = 0; i != 2; ++i) {
				objects[i].AddForce(direction * halfForce);
				direction = -direction;
			}
		}
	}
}
