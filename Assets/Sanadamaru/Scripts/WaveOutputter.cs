using UnityEngine;
using System.Linq;

public class WaveOutputter : MonoBehaviour 
{
	private float[] waveData_ = new float[1024];

	void Update()
	{
		AudioListener.GetOutputData(waveData_, 1);
		var volume = waveData_.Select(x => x*x).Sum() / waveData_.Length;
		gameObject.transform.localScale = Vector3.one * volume;
	}

}