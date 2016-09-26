using UnityEngine;
//using System.Collections;
using UnityEngine.UI;

public class ValueTest : MonoBehaviour {
	public Slider slider;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		slider.value=0.5f;
	}
}
