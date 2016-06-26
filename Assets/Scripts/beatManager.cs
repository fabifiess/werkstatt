using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class beatManager : MonoBehaviour {
	public List<GameObject> triggers = new List<GameObject>(); 
	public GameObject trigger1;
	public GameObject trigger2;
	public GameObject trigger3;
	public GameObject trigger4;
	public GameObject trigger5;

	public GameObject LedStripe;

	private GameObject hotTrigger;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time < 62) {
			hotTrigger = triggers[0];
		}
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
	}


	public void enterTrigger(string triggeredObject){
		if (triggeredObject == hotTrigger.name) {
			hotTrigger.GetComponent<Renderer> ().material.color = Color.cyan;
			LedStripe.GetComponent<Renderer> ().material.color = Color.cyan;
		}	
	}

	public void exitTrigger(string triggeredObject){
		if (triggeredObject == hotTrigger.name) {
			hotTrigger.GetComponent<Renderer> ().material.color = Color.white;
			LedStripe.GetComponent<Renderer> ().material.color = Color.white;
		}
	}

	void OnGUI(){
		GUILayout.Label (Time.time.ToString());
	}
}
