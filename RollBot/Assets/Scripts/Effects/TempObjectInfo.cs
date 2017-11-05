using System;
using UnityEngine;

[Serializable]
public class TempObjectInfo
{
	public bool isSelfDeactivating = true;
	public float fadeInTime = 0;
	public float lifeTime = 0;
	public float fadeOutTime = 0.2f;
	public Color targetColor = new Color(1, 1, 1);

	/// <summary>
	/// Initializes a new instance of the <see cref="TempObjectInfo"/> class with default values.
	/// </summary>
	public TempObjectInfo()
	{
	}

	public TempObjectInfo(TempObjectInfo info)
	{
		this.isSelfDeactivating = info.isSelfDeactivating;
		this.fadeInTime = info.fadeInTime;
		this.lifeTime = info.lifeTime;
		this.fadeOutTime = info.fadeOutTime;
		this.targetColor = info.targetColor;
	}

	public TempObjectInfo (bool isSelfDeactivating, float fadeInTime, float lifeTime, float fadeOutTime)
	{
		this.isSelfDeactivating = isSelfDeactivating;
		this.fadeInTime = fadeInTime;
		this.lifeTime = lifeTime;
		this.fadeOutTime = fadeOutTime;
	}

	public TempObjectInfo (bool isSelfDeactivating, float fadeInTime, float lifeTime, float fadeOutTime, Color color)
	{
		this.isSelfDeactivating = isSelfDeactivating;
		this.fadeInTime = fadeInTime;
		this.lifeTime = lifeTime;
		this.fadeOutTime = fadeOutTime;
		this.targetColor = color;
	}
}

