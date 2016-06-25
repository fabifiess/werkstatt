using UnityEngine;
using System.Collections;

public class PlayVideo : MonoBehaviour {

	public MovieTexture movie;
	private Renderer r;
	private AudioSource aud;

	void Start () {
		r =GetComponent<Renderer>();
		r.material.mainTexture = movie;
		aud = GetComponent<AudioSource>();
		aud.clip = movie.audioClip;
		movie.Play();
		aud.Play();
		movie.loop = true;
		aud.loop = true;
	}

	void Update(){
		if (Input.GetKeyDown ("q")) {

			MovieTexture movie = (MovieTexture)r.material.mainTexture;

			if (movie.isPlaying) {
				movie.Pause ();
				aud.Pause();
			} else {
				movie.Play ();
				aud.Play();
			}
		}
	}
}