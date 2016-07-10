using UnityEngine;
using System.Collections;

public class BirdsSpawner : MonoBehaviour {

	public BirdsMovement[] Birds;

	// Use this for initialization
	void Start () {
		BirdsMovement[] eagles = new BirdsMovement[Birds.Length];
		for (int i = 0; i < Birds.Length; i++) {
			eagles [i] = (BirdsMovement)Instantiate (Birds [i], transform.position + Random.onUnitSphere * 3f, Birds[i].transform.rotation);
			eagles [i].transform.parent = transform;
		}	
	}

}