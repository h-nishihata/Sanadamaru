using UnityEngine;
using System.Collections;

public class AfterEffect : MonoBehaviour {
	
	public int lod = 0;
	public Material m;
	RenderTexture rt;


	void Start(){
		
		if(m == null) Destroy(this);
		
	}


	void OnRenderImage(RenderTexture src, RenderTexture dst){
		
		if (rt == null) {
			var sizeDiv = 1 << lod;
			var width = src.width / sizeDiv;
			var height = src.height / sizeDiv;
			rt = new RenderTexture(width, height, 0, src.format);
		}

		Graphics.Blit(src, rt, m);
		Graphics.Blit (rt, dst);
	}

}
