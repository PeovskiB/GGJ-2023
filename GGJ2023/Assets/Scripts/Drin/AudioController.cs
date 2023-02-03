using UnityEngine;

public class AudioController : MonoBehaviour {

	private static AudioController instance;
	private AudioSource audioSource;

	void Awake () {
		instance = this;
		audioSource = GetComponent<AudioSource>();
	}
	
	public static void Play(AudioClip clip, float pitch = 1)
	{
		instance.audioSource.clip = clip;
		instance.audioSource.pitch = pitch;
		instance.audioSource.Play();
	}
}
