using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using ArtNet;

public class DMXout : MonoBehaviour {

	public byte[] DMXData = new byte[512];

	public List<GameObject> climbingholds = new List<GameObject>();

	int toggle = 0; 
	// private float[] hue_A = {1.0f, 0.0f, 0.0f};
	private float[] hue_B = {0.5f, 1.0f, 1.0f};
	private float[] hue_C = {1.0f, 0.0f, 1.0f};

	ArtNet.Engine ArtEngine; 

	void Start () {
		for (int i = 0; i < DMXData.Length; i++) {
			DMXData [i] = (byte)(0);
		}

		// Artnet sender / client
		ArtEngine = new ArtNet.Engine("Open DMX Etheret", "192.168.0.90");
		ArtEngine.Start ();

		// climbingholds[0].GetComponent<Renderer>().material.color = Color.magenta;
	}

	void Update () {

		// get position of stone
		// Vector3 stonePos = new Vector3 (climbingholds [1].transform.position.x, climbingholds [1].transform.position.y, climbingholds [1].transform.position.z);
		// Debug.Log ("x: " + stonePos.x + ", y: " + stonePos.y + ", z: " + stonePos.z);
		// get the color of the Movie texture at that position
		// render this color to the virtual stone

		sendDmxData ();


		if (Input.GetKeyDown("c")){
			toggle++;

			// fade to value fadeTo(aValue, aTime);
			if (toggle % 2 == 1) {
				StartCoroutine (FadeRender (0, hue_C));
				StartCoroutine (FadeDmx (0, hue_C));
			} else {
				StartCoroutine (FadeRender (0, hue_B));
				StartCoroutine (FadeDmx (0, hue_B));
			}
		}

		if (Input.GetKeyDown("b")){
			toggle++;

			// fade to value fadeTo(aValue, aTime);
			if (toggle % 2 == 1) {
				StartCoroutine (FadeRender (1, hue_C));
				StartCoroutine (FadeDmx (1, hue_C));
			} else {
				StartCoroutine (FadeRender (1, hue_B));
				StartCoroutine (FadeDmx (1, hue_B));
			}
		}
	}



	IEnumerator FadeDmx(int hold_nr, float [] fadeToColor){
		float fadingTime = 2.0f;
		float prevRed = climbingholds[hold_nr].GetComponent<Renderer> ().material.color.r;
		float prevGreen = climbingholds[hold_nr].GetComponent<Renderer> ().material.color.g;
		float prevBlue = climbingholds[hold_nr].GetComponent<Renderer> ().material.color.b;

		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / fadingTime){



			DMXData [hold_nr * 3 + 0] = (byte)(Mathf.Lerp(prevRed, fadeToColor[0], t)*255);
			DMXData [hold_nr * 3 + 1] = (byte)(Mathf.Lerp(prevGreen, fadeToColor[1], t)*255);
			DMXData [hold_nr * 3 + 2] = (byte)(Mathf.Lerp(prevBlue, fadeToColor[2], t)*255);



			yield return null;
		}
	}

	IEnumerator FadeRender(int hold_nr, float [] fadeToColor){
		float fadingTime = 2.0f;
		float prevRed = climbingholds[hold_nr].GetComponent<Renderer> ().material.color.r;
		float prevGreen = climbingholds[hold_nr].GetComponent<Renderer> ().material.color.g;
		float prevBlue = climbingholds[hold_nr].GetComponent<Renderer> ().material.color.b;

		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / fadingTime){
			Color newColor = new Color(
				Mathf.Lerp(prevRed, fadeToColor[0], t),
				Mathf.Lerp(prevGreen, fadeToColor[1], t),
				Mathf.Lerp(prevBlue, fadeToColor[2], t),
				1
			);
			climbingholds[hold_nr].GetComponent<Renderer> ().material.color = newColor;
			yield return null;
		}
	}
	void sendDmxData(){
		ArtEngine.SendDMX (0, DMXData, DMXData.Length);
	}

	/*
	void renderColor (float[] newColor){
		Color color2paint = new Color (newColor[0], newColor[1], newColor[2]);
		climbingholds[0].GetComponent<Renderer>().material.color = color2paint;
	}
	*/

	/*
	void sendDMX(float[] newColor){
		byte[] DMXData = new byte[512];

		DMXData [0] = (byte)(newColor[0]*255);
		DMXData [1] = (byte)(newColor[1]*255);
		DMXData [2] = (byte)(newColor[2]*255);

		ArtEngine.SendDMX (0, DMXData, DMXData.Length);
	}
	*/
}