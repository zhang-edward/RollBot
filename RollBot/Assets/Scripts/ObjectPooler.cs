using UnityEngine;
using System.Collections.Generic;

public class ObjectPooler : MonoBehaviour {

	public static List<ObjectPooler> objectPoolers = new List<ObjectPooler>();

	public bool isGlobal = true;
	public string poolType;
	public GameObject pooledObject;
	public int poolAmount = 10;
	public bool willGrow = true;

	protected List<GameObject> pooledObjects; 

	void Awake()
	{
		Init();
	}

	protected virtual void Init()
	{
		if (isGlobal)
			AddSelfToGlobalList();
		pooledObjects = new List<GameObject>();
		for (int i = 0; i < poolAmount; i++)
		{
			GameObject obj = Instantiate(pooledObject) as GameObject;
			obj.transform.SetParent(this.transform);
			obj.SetActive(false);
			pooledObjects.Add(obj);
		}
	}

	protected void AddSelfToGlobalList()
	{
		for (int i = 0; i < objectPoolers.Count; i ++)
		{
			if (objectPoolers[i].poolType == this.poolType)
			{
				objectPoolers [i] = this;
				return;
			}
		}
		objectPoolers.Add (this);

	}

	public GameObject GetPooledObject()
	{
		foreach (GameObject obj in pooledObjects)
		{
			if (!obj.activeInHierarchy)
				return obj;
		}

		if (willGrow)
		{
			GameObject obj = Instantiate (pooledObject);
			obj.transform.SetParent (this.transform);
			pooledObjects.Add (obj);
			return obj;
		}
		return null;
	}

	/// <summary>
	/// Gets the object pooler with the specified name. You should call this in the 'Start' method.
	/// </summary>
	/// <returns>The object pooler.</returns>
	/// <param name="name">Name of the object pooler.</param>
	public static ObjectPooler GetObjectPooler(string name)
	{
		foreach (ObjectPooler pooler in objectPoolers)
		{
			if (pooler.poolType == name)
			{
				return pooler;
			}
		}
		return null;
	}
}
