using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class ReportAudioSourceDone : MonoBehaviour {
	[Tooltip("Emitted when the audio source finishes playing.")]
	public UnityEvent onDone;

	private bool wasPlaying = false;

	private void Update() {
		AudioSource source = GetComponent<AudioSource>();
		if(wasPlaying && (!source.isPlaying || AudioListener.volume == 0f)) {
			onDone.Invoke();
		}
		wasPlaying = source.isPlaying;
	}
}
