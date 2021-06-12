using UnityEngine;
using UnityEngine.Events;

public class Toggleable : MonoBehaviour {
	[Tooltip("Whether the machine is on or off.")]
	public bool isOn;

	[Tooltip("Emitted each time the state is toggled by the player.")]
	public BooleanEvent toggled;

	[Tooltip("Emitted each time the state becomes on.")]
	public UnityEvent turnedOn;

	[Tooltip("Emitted each time the state becomes off.")]
	public UnityEvent turnedOff;

	public void Interact() {
		isOn = !isOn;
		toggled.Invoke(isOn);
		(isOn ? turnedOn : turnedOff).Invoke();
	}
}
