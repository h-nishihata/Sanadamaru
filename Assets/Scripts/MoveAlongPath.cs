using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveAlongPath : MonoBehaviour{

	public float flyingTime = 100.0f;
	string path_0 = "Path_0";

	[SerializeField] iTweenPath path;


	void Start (){
		
		this.gameObject.transform.position = path.nodes [0];
		MoveToPath();

	}

	/*
	void Update(){
		if (Input.GetKey ("space")) {
			if (flyingTime < 10.0f) {
				flyingTime -= 0.1f;
				MoveToPath();
			}
		} else {
			if (flyingTime > 30.0f) {
				flyingTime += 0.1f;
				MoveToPath();
			}
		}
	}
	*/

	void MoveToPath(){

			iTween.MoveTo (this.gameObject, iTween.Hash (
				"path", iTweenPath.GetPath (path_0), 
				"time", flyingTime,
				"loopType", "loop",
				"easeType", "linear",
				"orienttopath", true)
			);

	}
		
}