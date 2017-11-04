using UnityEngine;
using System.Collections;

public class SimpleAnimationPlayer : MonoBehaviour {

	public SpriteRenderer sr;
	public SimpleAnimation anim;

	private int frameIndex;

	public bool playOnStart = false;
	public bool looping;
	public bool ignoreTimeScaling;
	public bool isPlaying { get; private set; }

	public bool destroyOnFinish;

	private Coroutine playAnimRoutine;

	void Awake()
	{
		sr = GetComponent<SpriteRenderer>();
	}

	void Start()
	{
		//secondsPerFrame = 1.0f / anim.fps;
		frameIndex = 0;
		if (playOnStart)
			Play ();
		//sr.sprite = anim.frames [0];
	}

	public virtual void Play()
	{
		UnityEngine.Assertions.Assert.IsNotNull (anim);
		Reset ();
		playAnimRoutine = StartCoroutine(PlayAnim());
	}

	public void Reset()
	{
		frameIndex = 0;
		sr.sprite = anim.frames[0];
		if (playAnimRoutine != null)
			StopCoroutine (playAnimRoutine);
	}

	protected virtual IEnumerator PlayAnim()
	{
		isPlaying = true;
		while (frameIndex < anim.frames.Length)
		{
			sr.sprite = anim.frames[frameIndex];
			frameIndex++;
			// if looping, set animation to beginning and do not stop playing
			if (looping)
			{
				if (frameIndex >= anim.frames.Length)
					frameIndex = 0;
			}
			if (ignoreTimeScaling)
				yield return new WaitForSecondsRealtime(anim.SecondsPerFrame);
			else
				yield return new WaitForSeconds(anim.SecondsPerFrame);
		}
		isPlaying = false;
		if (destroyOnFinish)
			Destroy (gameObject);
		yield return null;
	}
}