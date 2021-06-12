using UnityEngine;
using UnityEngine.Events;

public class Activatable : MonoBehaviour {
	[Tooltip("Emitted when the machine is activated by the player.")]
	public UnityEvent activated;
}
