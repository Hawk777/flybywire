using UnityEngine;

public class Projectile : MonoBehaviour {
	[Tooltip("The cable part of the projectile.")]
	public LineRenderer cable;

	[Tooltip("The prefab rope to instantiate upon hitting a target.")]
	public GameObject ropePrefab;

	[HideInInspector]
	public ComputerControls launcher;

	private void Update() {
		Vector3 projectilePos = GetComponent<Transform>().position;
		Vector2 launcherPos = launcher.gameObject.GetComponent<Transform>().position;
		cable.SetPositions(new Vector3[]{
			projectilePos,
			new Vector3(launcherPos.x, launcherPos.y, projectilePos.z),
		});
	}
}
