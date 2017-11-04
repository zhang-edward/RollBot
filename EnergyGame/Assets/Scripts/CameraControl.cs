using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{

	public static CameraControl instance;
	public Camera cam;
	public Transform focus;
	public Transform secondaryFocus;

	public SpriteRenderer screenFlash;
	public SpriteRenderer screenOverlay;

	private Coroutine flashRoutine;
	private Coroutine overlayRoutine;

	void Awake()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(this.gameObject);

		transform.position = focus.position;

		float height = cam.orthographicSize * 2.0f;
		float width = height * Screen.width / Screen.height;
		screenFlash.transform.localScale = new Vector3(width, height, 1);
		screenFlash.color = new Color(1, 1, 1, 0);
		screenOverlay.transform.localScale = new Vector3(width, height, 1);
		screenOverlay.color = new Color(1, 1, 1, 0);
	}

	void Update()
	{
		if (secondaryFocus != null)
		{
			Vector3 target = Vector3.Lerp(focus.position, secondaryFocus.position, 0.2f);
			transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * 8f);
		}
		else
		{
			transform.position = Vector3.Lerp(transform.position, focus.position, Time.deltaTime * 10f);
		}
	}

	public void SetFocus(Transform tr)
	{
		focus = tr;
	}

	public void StartShake(float time, float magnitude, bool vertical, bool horizontal)
	{
		StartCoroutine(CameraShake(time, magnitude, vertical, horizontal));
	}

	private IEnumerator CameraShake(float time, float magnitude, bool vertical, bool horizontal)
	{
		int signX = 1;
		int signY = 1;
		while (time > 0)
		{
			time -= Time.unscaledDeltaTime;
			float randX = 0;
			float randY = 0;
			if (horizontal)
				randX = signX * magnitude;
			if (vertical)
				randY = signY * magnitude;
			signX *= -1;
			signY *= -1;

			cam.transform.localPosition = new Vector3(randX, randY, -10);
			yield return null;
		}
		cam.transform.localPosition = new Vector3(0, 0, -10);
	}

	// ==========
	// Flash
	// ==========
	public void StartFlashColor(Color color, float a = 1f, float fadeInTime = 1.0f, float persistTime = 1.0f, float fadeOutTime = 1.0f)
	{
		if (flashRoutine != null)
			StopCoroutine(flashRoutine);
		flashRoutine = StartCoroutine(FlashColor(color, a, fadeInTime, persistTime, fadeOutTime));
	}

	private IEnumerator FlashColor(Color color, float a, float fadeInTime = 1.0f, float persistTime = 1.0f, float fadeOutTime = 1.0f)
	{
		if (fadeInTime > 0)
		{
			StartCoroutine(FadeIn(screenFlash, color, a, fadeInTime));
			yield return new WaitForSecondsRealtime(fadeInTime);
		}
		screenFlash.color = new Color(color.r, color.g, color.b, a);
		yield return new WaitForSecondsRealtime(persistTime);
		if (fadeOutTime > 0)
		{
			StartCoroutine(FadeOut(screenFlash, fadeOutTime));
			yield return new WaitForSecondsRealtime(fadeOutTime);
		}
		screenFlash.color = new Color(color.r, color.g, color.b, 0);
	}

	// ==========
	// Overlay
	// ==========
	public void SetOverlayColor(Color color, float a, float fadeInTime = 1.0f)
	{
		if (overlayRoutine != null)
			StopCoroutine(overlayRoutine);
		overlayRoutine = StartCoroutine(FadeIn(screenOverlay, color, a, fadeInTime));
	}

	public void DisableOverlay(float fadeOutTime)
	{
		if (overlayRoutine != null)
			StopCoroutine(overlayRoutine);
		overlayRoutine = StartCoroutine(FadeOut(screenOverlay, fadeOutTime));
	}

	private IEnumerator FadeIn(SpriteRenderer sr, Color color, float a, float time)
	{
		sr.color = new Color(color.r, color.g, color.b, 0);
		float t = 0f;
		while (t < time)
		{
			t += Time.unscaledDeltaTime;
			sr.color = new Color(color.r, color.g, color.b, Mathf.Lerp(sr.color.a, a, t / time));
			yield return null;
		}
		sr.color = new Color(color.r, color.g, color.b, a);
	}

	private IEnumerator FadeOut(SpriteRenderer sr, float time)
	{
		Color color = sr.color;
		float t = 0f;
		while (t < time)
		{
			t += Time.unscaledDeltaTime;
			sr.color = new Color(color.r, color.g, color.b, Mathf.Lerp(sr.color.a, 0, t / time));
			yield return null;
		}
		sr.color = new Color(color.r, color.g, color.b, 0);
	}
}
