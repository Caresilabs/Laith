using UnityEngine;
using System.Collections;

public class Layer : MonoBehaviour {

	public static int players = 8;
	public static GameObject[] FindGameObjectsWithLayer (int layer)  {
		GameObject[] goArray = FindObjectsOfType(GameObject);
		GameObject goList = new System.Collections.Generic.List.<GameObject>();
		for (int i = 0; i < goArray.Length; ++i) {
			if (goArray[i].layer == layer) {
				goList.Add(goArray[i]);
			}
		}
		if (goList.Count == 0) {
			return null;
		}
		return goList.ToArray();
	}
}
