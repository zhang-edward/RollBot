using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	private const float HIGH_PITCH_RANGE = 1.05f;
	private const float LOW_PITCH_RANGE = 0.95f;
	
	public static SoundManager instance;
	public AudioSource music;
	public AudioSource sfx;

	public AudioClip musicClip;
	public float musicVolume;

	public bool playingMusic { get; private set; }

	void Awake()
	{
		// make this a singleton
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (this.gameObject);
		
		music.volume = musicVolume;
		PlayMusicLoop(musicClip, null);
	}

	/// <summary>
	/// Plays the AudioClip with some pitch variance.
	/// </summary>
	/// <param name="clip">Clip.</param>
	/// <param name="stacking">whether or not this clip can be stacked with others (bad for many sounds played at the same time)</param>
	public static void RandomizeSFX(AudioClip clip)
	{
		float randomPitch = Random.Range (LOW_PITCH_RANGE, HIGH_PITCH_RANGE);
		instance.sfx.pitch = randomPitch;
		instance.sfx.PlayOneShot (clip);
	}

	public void PlayInterrupt(AudioClip clip)
	{
		sfx.clip = clip;
		sfx.Play ();
	}

	public void PlaySingle(AudioClip clip)
	{
		sfx.pitch = 1.0f;
		sfx.PlayOneShot(clip);
	}

	public void PlayMusicLoop(AudioClip clip, AudioClip intro = null)
	{
//		Debug.Log ("Playing new music loop: " + clip);
		playingMusic = true;
		StartCoroutine (MusicLoop (clip, intro));
	}

	private IEnumerator MusicLoop(AudioClip clip, AudioClip intro = null)
	{
		// resets any previous music clip
		music.Stop ();
		// play the intro
		if (intro != null)
		{
			music.loop = false;
			music.clip = intro;
			music.Play ();
		}
		while (music.isPlaying)
			yield return null;
		music.loop = true;
		music.clip = clip;
		music.Play ();
	}

	public void PauseMusic()
	{
		music.Pause();
	}

	public void UnPauseMusic()
	{
		music.UnPause();
	}

	private IEnumerator MusicFadeIn(float targetVolume)
	{
		while (music.volume < targetVolume)
		{
			music.volume += 0.05f;
			yield return null;
		}
	}

	private IEnumerator MusicFadeOut(float targetVolume)
	{
		while (music.volume > targetVolume)
		{
			music.volume -= 0.05f;
			yield return null;
		}
	}
}
