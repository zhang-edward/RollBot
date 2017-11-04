using UnityEngine;
using System.Collections;

/// <summary>
/// A temporary object which disappears after some time, such as an effect
/// </summary>
public class TempObject : MonoBehaviour {

	/*public float targetAlpha = 1;
	public bool isSelfDeactivating = false;
	public float fadeInTime = 0;
	public float lifeTime = 0;
	public float fadeOutTime = 0.1f;*/
	public TempObjectInfo info = new TempObjectInfo();

	private SpriteRenderer sr;
	void Awake () {
		sr = GetComponent<SpriteRenderer> ();
	}

	/// <summary>
	/// Init the specified rotation, sprite and isSelfDeactivating variables.
	/// </summary>
	/// <param name="rotation">Rotation.</param>
	/// <param name="sprite">Sprite.</param>
	/// <param name="isSelfDeactivating">If set to <c>true</c> is self deactivating.</param>
	/// <param name="fadeInTime">Amount of time for this effect to fade in</param>
	public void Init(Quaternion rotation, Vector3 position, Sprite sprite, bool isSelfDeactivating, float fadeInTime = 0, float lifeTime = 0, float fadeOutTime = 0.2f)
	{
		gameObject.SetActive (true);
		transform.rotation = rotation;
		transform.position = position;
		sr.sprite = sprite;
		info.isSelfDeactivating = isSelfDeactivating;
		info.fadeInTime = fadeInTime;
		info.lifeTime = lifeTime;
		info.fadeOutTime = fadeOutTime;
		info.targetColor = new Color (1, 1, 1, 1);
		StartCoroutine (FadeIn());
	}

	/// <summary>
	/// Init the specified rotation, position and sprite, with <see cref="isSelfDeactivating/>, <see cref="fadeInTime/>,
	/// <see cref="lifetime"/>, and <see cref="fadeOutTime/> set to their default values set in the Inspector
	/// </summary>
	/// <param name="rotation">Rotation.</param>
	/// <param name="position">Position.</param>
	/// <param name="sprite">Sprite.</param>
	public void Init(Quaternion rotation, Vector3 position, Sprite sprite)
	{
		gameObject.SetActive (true);
		transform.rotation = rotation;
		transform.position = position;
		sr.sprite = sprite;
		StartCoroutine (FadeIn());
	}

	/// <summary>
	/// Init the specified rotation, position and sprite, with <see cref="isSelfDeactivating/>, <see cref="fadeInTime/>,
	/// <see cref="lifetime"/>, and <see cref="fadeOutTime/> set to their default values set in the Inspector
	/// </summary>
	/// <param name="rotation">Rotation.</param>
	/// <param name="position">Position.</param>
	/// <param name="sprite">Sprite.</param>
	public void Init(Quaternion rotation, Vector3 position, Sprite sprite, TempObjectInfo tempObjectInfo)
	{
		gameObject.SetActive (true);
		transform.rotation = rotation;
		transform.position = position;
		sr.sprite = sprite;
		this.info = new TempObjectInfo(tempObjectInfo);
		StartCoroutine (FadeIn());
	}

	private IEnumerator FadeIn()
	{
		// get the initial color, but set at alpha = 0
		Color initialColor = new Color (info.targetColor.r, 
			                     info.targetColor.b,
			                     info.targetColor.g,
			                     0);
		sr.color = initialColor;
		float t = 0;	// used for lerp
		while (t < info.fadeInTime)
		{
			t += Time.deltaTime;
			sr.color = Color.Lerp(initialColor, 
				info.targetColor,
				t / info.fadeInTime);
			yield return null;
		}
		sr.color = info.targetColor;
		if (info.isSelfDeactivating)
		{
			yield return new WaitForSeconds (info.lifeTime);
			StartCoroutine (FadeOut ());
		}
	}

	private IEnumerator FadeOut()
	{
		Color initialColor = sr.color;
		Color finalColor = new Color (info.targetColor.r,
			                   info.targetColor.b,
			                   info.targetColor.g,
			                   0);
		float t = 0;
		while (t < info.fadeOutTime)
		{
			t += Time.deltaTime;
			sr.color = Color.Lerp(initialColor, 
				finalColor,
				t / info.fadeOutTime);
			yield return null;
		}
		gameObject.SetActive (false);
	}

	public void Deactivate()
	{
		StartCoroutine (FadeOut ());
	}
}
