using UnityEngine;
using System.Collections;

public class BirdsMovement : MonoBehaviour
{

	[SerializeField] Transform parent;
	float pnt_currentZ;
	float go_currentZ;


	void Update (){

		go_currentZ = transform.rotation.eulerAngles.z;
		pnt_currentZ = parent.transform.rotation.eulerAngles.y;

		if ((pnt_currentZ < 360) && (pnt_currentZ > 180)) {
			
			if ((go_currentZ < 180) && ((int)go_currentZ > 35)) {
				return;
			}
				transform.Rotate (0, 0, Time.deltaTime * 8);
			
		} else if ((pnt_currentZ >= 0) && (pnt_currentZ < 180)) {
			
			if ((go_currentZ > 180) && ((int)go_currentZ < 315)) {
				return;
			}
				transform.Rotate (0, 0, Time.deltaTime * -8);
			
		}

	}

}