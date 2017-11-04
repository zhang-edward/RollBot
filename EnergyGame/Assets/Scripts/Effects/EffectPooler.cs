using UnityEngine;
using System.Collections;

public class EffectPooler : ObjectPooler
{
	public static EffectPooler instance;
	public const string EFFECT_POOL_NAME = "Effect";

	protected override void Init()
	{
		poolType = EFFECT_POOL_NAME;
		base.Init();
		instance = this;
	}

	public static void PlayEffect(SimpleAnimation toPlay, Vector3 position, bool randRotation = false, float fadeOutTime = 0f)
	{
		GameObject o = instance.GetPooledObject();
		SimpleAnimationPlayer anim = o.GetComponent<SimpleAnimationPlayer>();
		TempObject tempObj = o.GetComponent<TempObject>();
		tempObj.info = new TempObjectInfo(true, 0f, toPlay.TimeLength - fadeOutTime, fadeOutTime, new Color(1, 1, 1, 0.8f));
		anim.anim = toPlay;
		Quaternion rot;
		if (randRotation)
			rot = Quaternion.Euler(0, 0, Random.Range(0, 360));
		else
			rot = Quaternion.identity;
		tempObj.Init(rot,
					 position,
					 toPlay.frames[0]);
		anim.Play();
	}

	public static void PlayEffect(SimpleAnimation toPlay, Vector3 position, TempObjectInfo info)
	{
		GameObject o = instance.GetPooledObject();
		SimpleAnimationPlayer anim = o.GetComponent<SimpleAnimationPlayer>();
		TempObject tempObj = o.GetComponent<TempObject>();
		tempObj.info = info;
		anim.anim = toPlay;
		tempObj.Init(Quaternion.identity,
					 position,
					 toPlay.frames[0]);
		anim.Play();
	}
}
