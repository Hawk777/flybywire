using System.Collections;
using UnityEngine;

public class AudioCooldown : MonoBehaviour {
	[Range(0f, Mathf.Infinity)]
	[Tooltip("The cooldown time after which the same sound can be played again, in seconds.")]
	public float cooldownTime;

	private Hashtable lastPlayTimes = new Hashtable();

	private void Start() {
		lastPlayTimes.Clear();
	}

	public void PlayIfCool(AudioSource source) {
		float now = Time.realtimeSinceStartup;
		string name = source.clip.name;
		object lastPlayTimeObj = lastPlayTimes[name];
		if(lastPlayTimeObj != null) {
			float lastPlayTime = (float) lastPlayTimeObj;
			if((now - lastPlayTime) < cooldownTime) {
				return;
			}
		}
		lastPlayTimes[name] = now;
		source.Play();
	}
}
