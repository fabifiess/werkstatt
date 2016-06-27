using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class beatManager : MonoBehaviour {
	public List<GameObject> triggers = new List<GameObject>(); 
	public DMXout dmxOut;
	public int DMX_lamp_startAddress = 124;

	public GameObject LedStripe;

	private GameObject hotTrigger;

	void Start () {
		hotTrigger=triggers[0];
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time < 600) {
			hotTrigger = triggers[0];
		}
		/*
		else if (Time.time >= 62 && Time.time < 135) {
			hotTrigger = triggers[1];
		}
		else if (Time.time >= 135 && Time.time < 170) {
			hotTrigger = triggers[2];
		}
		else if (Time.time >= 170 && Time.time < 230) {
			hotTrigger = triggers[3];
		}
		else if (Time.time >= 230) {
			hotTrigger = triggers[4];
		}
		*/
	}


	public void enterTrigger(string triggeredObject){
		if (triggeredObject == hotTrigger.name) {
			hotTrigger.GetComponent<Renderer> ().material.color = Color.cyan;
			LedStripe.GetComponent<Renderer> ().material.color = Color.cyan;
		
			dmxOut.DMXData [DMX_lamp_startAddress] = (byte)(255);
			dmxOut.DMXData [DMX_lamp_startAddress+1] = (byte)(255);
			dmxOut.DMXData [DMX_lamp_startAddress+2] = (byte)(255);
		
			StartCoroutine (dmxOut.fadeColor (LedStripe, DMX_lamp_startAddress, dmxOut.white, 0.1f));
		}	
	}

	public void exitTrigger(string triggeredObject){
		if (triggeredObject == hotTrigger.name) {
			hotTrigger.GetComponent<Renderer> ().material.color = Color.white;
			LedStripe.GetComponent<Renderer> ().material.color = Color.white;
		
			dmxOut.DMXData [DMX_lamp_startAddress] = (byte)(30);
			dmxOut.DMXData [DMX_lamp_startAddress+1] = (byte)(30);
			dmxOut.DMXData [DMX_lamp_startAddress+2] = (byte)(30);

		}
	}

	void OnGUI(){
		GUILayout.Label (Time.time.ToString());
	}
}
