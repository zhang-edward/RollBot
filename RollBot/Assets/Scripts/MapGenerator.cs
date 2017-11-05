using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour
{
	public GameObject tilePrefab;
	public int mapSize;

	// Use this for initialization
	void Start()
	{
		for (int x = 0; x < mapSize / 2; x ++) {
			for (int y = 0; y < mapSize / 2; y ++) {
				GameObject o = Instantiate(tilePrefab, new Vector3(x * 2, y * 2), Quaternion.identity) as GameObject;
				o.transform.SetParent(transform);
			}
		}
		transform.position = new Vector3(-mapSize / 2f, -mapSize / 2f, 0);
	}
}
