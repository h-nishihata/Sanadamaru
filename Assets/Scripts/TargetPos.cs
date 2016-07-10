using UnityEngine;
using System.Collections;

public class TargetPos : MonoBehaviour
{

	public float interval = 3f;
	public float minDistance = 1f;
	public float maxDistance = 10f;


	// Use this for initialization
	void Start ()
	{
		InvokeRepeating ("UpdatePosition", 0, interval);
	}


	void UpdatePosition ()
	{
		Vector3 pos = Vector3.right * Random.Range(-0.5f, 0.5f) + Vector3.up * Random.Range (-2f, 2f);
		pos.z = Random.Range (minDistance, maxDistance);
		transform.position = pos;
	}

}