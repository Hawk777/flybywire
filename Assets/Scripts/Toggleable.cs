using UnityEngine;

public class Toggleable : MonoBehaviour {
	[Tooltip("Whether the machine is on or off.")]
	public bool isOn;

	[Tooltip("Emitted each time the state is toggled by the player.")]
	public BooleanEvent toggled;

	public void Activate() {
		isOn = !isOn;
		toggled.Invoke(isOn);
	}
}
