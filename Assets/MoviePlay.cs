using UnityEngine;
using System.Collections;

public class MoviePlay : MonoBehaviour {
	public MovieTexture myMovie;
	private AudioSource myAudio;
	// Use this for initialization
	void Start () {
		myMovie.Play();
		myMovie.loop=true;
		myAudio=this.GetComponent<AudioSource>();
		myAudio.Play();
		myAudio.loop=true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
