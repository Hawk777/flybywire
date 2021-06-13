using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class ReportAudioSourceDone : MonoBehaviour {
	[Tooltip("Emitted when the audio source finishes playing.")]
	public UnityEvent onDone;

	private void Update() {
		AudioSource source = GetComponent<AudioSource>();
		if(source.timeSamples != 0) {
			if(!source.isPlaying || AudioListener.volume == 0f) {
				onDone.Invoke();
			}
		}
	}
}
