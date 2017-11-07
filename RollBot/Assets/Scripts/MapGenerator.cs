using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour
{
	public const int MAP_SIZE = 40;
	public GameObject tilePrefab;

	// Use this for initialization
	void Start()
	{
		for (int x = 0; x < MAP_SIZE / 2; x ++) {
			for (int y = 0; y < MAP_SIZE / 2; y ++) {
				GameObject o = Instantiate(tilePrefab, new Vector3(x * 2, y * 2), Quaternion.identity) as GameObject;
				o.transform.SetParent(transform);
			}
		}
		transform.position = new Vector3(-MAP_SIZE / 2f, -MAP_SIZE / 2f, 0);
	}
}
