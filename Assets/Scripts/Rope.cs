using UnityEngine;

public class Rope : MonoBehaviour {
	[Tooltip("The renderer to draw the rope, or null to not draw.")]
	public LineRenderer cableRenderer;

	[Tooltip("The length of the rope, in world units")]
	public float length;

	[Tooltip("The spring constant, in force units per world unit, applied when the rope is overextended.")]
	public float springConstant;

	[Tooltip("The two objects to which the rope is connected.")]
	public Rigidbody2D[] objects;

	[Tooltip("The two offset vectors from the object positions to the attachment positions.")]
	public Vector2[] offsets;

	[Tooltip("The Z coordinate of the cable.")]
	public float z;

	[Tooltip("The number of line segments to use to draw the catenary when the rope is slack.")]
	public int catenarySteps;

	public void InitLength() {
		Vector2[] positions = Positions;
		length = (positions[1] - positions[0]).magnitude;
	}

	private Vector2[] Positions {
		get {
			Vector2[] ret = new Vector2[2];
			for(int i = 0; i != 2; ++i) {
				ret[i] = objects[i].position + objects[i].GetRelativeVector(offsets[i]);
			}
			return ret;
		}
	}

	void FixedUpdate() {
		Vector2[] positions = Positions;
		Vector2 delta = positions[1] - positions[0];
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

	private void Update() {
		Vector2[] positions = Positions;
		float actualLengthSq = (positions[1] - positions[0]).sqrMagnitude;
		if(actualLengthSq >= length * length) {
			// The rope is taut. Draw a straight line.
			cableRenderer.positionCount = 2;
			cableRenderer.SetPositions(new Vector3[]{
				new Vector3(positions[0].x, positions[0].y, z),
				new Vector3(positions[1].x, positions[1].y, z),
			});
		} else {
			// The rope is loose. Draw a catenary.
			Vector2[] catenary = Catenary.generate(positions[0], positions[1], length, catenarySteps);
			Vector3[] catenary3 = new Vector3[catenary.Length];
			for(int i = 0; i != catenary.Length; ++i) {
				catenary3[i] = new Vector3(catenary[i].x, catenary[i].y, z);
			}
			cableRenderer.positionCount = catenary3.Length;
			cableRenderer.SetPositions(catenary3);
		}
	}
}
