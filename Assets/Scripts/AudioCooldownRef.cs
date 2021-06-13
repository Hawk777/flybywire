using UnityEngine;

public class AudioCooldownRef : MonoBehaviour {
	private AudioCooldown Instance {
		get {
			return GameObject.Find("Audio Cooldown").GetComponent<AudioCooldown>();
		}
	}

	public void PlayIfCool(AudioSource source) {
		Instance.PlayIfCool(source);
	}
}
