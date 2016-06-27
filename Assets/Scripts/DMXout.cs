using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using ArtNet;

public class DMXout : MonoBehaviour {

	public byte[] DMXData = new byte[512];

	//public List<GameObject> climbingholds = new List<GameObject>();
	public GameObject steinB;
	public GameObject steinC;

	int toggle = 0; 
	// private float[] hue_A = {1.0f, 0.0f, 0.0f};
	private float[] hue_B = {0.5f, 1.0f, 1.0f};
	private float[] hue_C = {1.0f, 0.0f, 1.0f};
	public float[] white = {1.0f, 1.0f, 1.0f};
	public float[] cyan = {0.0f, 1.0f, 1.0f};
	public float[] darkcyan = {0.0f, 0.6f, 0.6f};
	public float[] dark = {0.1f, 0.1f, 0.1f};
	public float[] yellow = {1.0f, 1.0f, 0.0f};
	public float[] violet = {1.0f, 0.0f, 1.0f};
	public float[] black = {0.0f, 0.0f, 0.0f};


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
				StartCoroutine (fadeColor (steinC, 0, hue_C, 2.0f));
			} else {
				StartCoroutine (fadeColor (steinC, 0, hue_B, 2.0f));
			}
		}
	}

	public IEnumerator fadeColor(GameObject gameobj, int DMX_startAddress, float [] fadeToColor, float fadingTime){
		float prevRed = gameobj.GetComponent<Renderer> ().material.color.r;
		float prevGreen = gameobj.GetComponent<Renderer> ().material.color.g;
		float prevBlue = gameobj.GetComponent<Renderer> ().material.color.b;

		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / fadingTime){

			DMXData [DMX_startAddress] = (byte)(Mathf.Lerp(prevRed, fadeToColor[0], t)*255);
			DMXData [DMX_startAddress + 1] = (byte)(Mathf.Lerp(prevGreen, fadeToColor[1], t)*255);
			DMXData [DMX_startAddress + 2] = (byte)(Mathf.Lerp(prevBlue, fadeToColor[2], t)*255);

			Color newColor = new Color(
				Mathf.Lerp(prevRed, fadeToColor[0], t),
				Mathf.Lerp(prevGreen, fadeToColor[1], t),
				Mathf.Lerp(prevBlue, fadeToColor[2], t),
				1
			);
			gameobj.GetComponent<Renderer> ().material.color = newColor;

			yield return null;
		}
		/*
		DMXData [DMX_startAddress] = (byte)(fadeToColor[0]);
		DMXData [DMX_startAddress + 1] = (byte)(fadeToColor[1]);
		DMXData [DMX_startAddress + 2] = (byte)(fadeToColor[2]);
		*/
	}


	void sendDmxData(){
		ArtEngine.SendDMX (0, DMXData, DMXData.Length);
	}


}