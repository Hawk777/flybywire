using UnityEngine;

public class Projectile : MonoBehaviour {
	[Tooltip("The cable part of the projectile.")]
	public LineRenderer cable;

	[HideInInspector]
	public ComputerControls launcher;

	private void Update() {
		if(cable != null) {
			Vector3 projectilePos = GetComponent<Transform>().position;
			Vector2 launcherPos = launcher.gameObject.GetComponent<Transform>().position;
			cable.SetPositions(new Vector3[]{
				projectilePos,
				new Vector3(launcherPos.x, launcherPos.y, projectilePos.z),
			});
		}
	}
}
