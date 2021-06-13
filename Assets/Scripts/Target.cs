using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class Target : MonoBehaviour {
	[Tooltip("Emitted when the machine is plugged into by the player.")]
	public UnityEvent onPlug;

	[Tooltip("Emitted when the machine is unplugged from by the player.")]
	public UnityEvent onUnplug;

	[Tooltip("Emitted when the machine is interacted with by the player.")]
	public UnityEvent onInteract;

	void OnCollisionEnter2D(Collision2D collision) {
		GameObject projectile = collision.gameObject;
		Projectile projectileComponent = projectile.GetComponent<Projectile>();
		if(projectileComponent != null) {
			// Get the player that launched the projectile.
			ComputerControls launcher = projectileComponent.launcher;

			// Normally this will be the case; however, in the event that the
			// projectile enters the bounding boxes of two different targets in
			// the same frame, it might not be, and we only want to plug into
			// the first target.
			if(launcher.connectedTarget == null) {
				// The projectile should stick, perfectly, to the target, and
				// move with it. To accomplish this, simply destroy its own
				// rigid body and make it a child object of the target.
				Destroy(projectile.GetComponent<Rigidbody2D>());
				projectile.GetComponent<Transform>().SetParent(GetComponent<Transform>(), true);

				// Create the cable between the target and the projectile.
				Rope rope = Instantiate(projectileComponent.ropePrefab).GetComponent<Rope>();
				Rigidbody2D body = GetComponent<Rigidbody2D>();
				Rigidbody2D launcherBody = launcher.GetComponent<Rigidbody2D>();
				rope.objects = new Rigidbody2D[]{body, launcherBody};
				rope.length = (launcherBody.position - body.position).magnitude;
				launcher.rope = rope;

				// Record the object that was hit.
				launcher.connectedTarget = this;

				// Notify listeners.
				onPlug.Invoke();
			}
		}
	}
}
