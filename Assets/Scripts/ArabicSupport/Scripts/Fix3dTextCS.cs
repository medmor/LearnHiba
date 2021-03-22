using UnityEngine;
using System.Collections;
using ArabicSupport;

public class Fix3dTextCS : MonoBehaviour {
	
	
	// Use this for initialization
	void Start () {
		var t = gameObject.GetComponent<TextMesh>();
		var txt = t.text;
		t.text = ArabicFixer.Fix(txt);
	}
	
}
