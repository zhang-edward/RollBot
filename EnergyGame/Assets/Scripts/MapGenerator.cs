using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour
{
	public GameObject tilePrefab;
	public int mapSize;

	// Use this for initialization
	void Start()
	{
		for (int x = 0; x < mapSize; x ++) {
			for (int y = 0; y < mapSize; y ++) {
				GameObject o = Instantiate(tilePrefab, new Vector3(x, y), Quaternion.identity) as GameObject;
				o.transform.SetParent(transform);
			}
		}
		transform.position = new Vector3(-mapSize / 2f, -mapSize / 2f, 0);
	}

	// Update is called once per frame
	void Update()
	{
			
	}
}
