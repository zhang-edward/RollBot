using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SimpleAnimationPlayerImage : MonoBehaviour {

	public Image image;
	public SimpleAnimation anim;

	private int frameIndex;

	public bool playOnStart = false;
	public bool isPlaying { get; private set; }
	public bool looping;

	public void Start()
	{
		//sr = GetComponent<SpriteRenderer>();
		//secondsPerFrame = 1.0f / anim.fps;
		frameIndex = 0;
		if (playOnStart)
			Play ();
		//sr.sprite = anim.frames [0];
	}

	public void Play()
	{
		UnityEngine.Assertions.Assert.IsNotNull (anim);
		Reset ();
		StartCoroutine("PlayAnim");
	}

	public void Reset()
	{
		frameIndex = 0;
		image.sprite = anim.frames[0];
		StopAllCoroutines ();
	}

	private IEnumerator PlayAnim()
	{
		isPlaying = true;
		while (frameIndex < anim.frames.Length)
		{
			image.sprite = anim.frames[frameIndex];
			frameIndex++;
			// if looping, set animation to beginning and do not stop playing
			if (looping)
			{
				if (frameIndex >= anim.frames.Length)
					frameIndex = 0;
			}
			yield return new WaitForSeconds(anim.SecondsPerFrame);
		}
		isPlaying = false;
		yield return null;
	}
}