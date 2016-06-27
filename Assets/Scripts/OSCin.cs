using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityOSC;

public class OSCin : MonoBehaviour {
	
	private Dictionary<string, ServerLog> servers;
	public AudioSource audio_schliff;
	public GameObject schleif_lamp;
	public DMXout dmxOut;
	public int DMX_lamp_startAddress = 6;
	public int DMX_feedback_address = 120;

	public string fader1 = "/1/fader1";
	public int thisPort = 8000;
	private string thatIpAddress;
	private int thatPort;

	private float duration = 0.5f;
	private float littleTrigger;
	private float bigTrigger;
	private bool littleVal = true;
	private bool bigVal = true;
	private int counter = 0;

	private float startTime = 0f;
	private float currentTime = 0f;
	private bool inAction = false;

	void Start() {	
		thatIpAddress = "127.0.0.1";
		thatPort = 9000;

		// public void Init(int thisPort, string thatIpAddress, int thatPort)
		OSCHandler.Instance.Init(thisPort, thatIpAddress, thatPort); //init OSC
		servers = new Dictionary<string, ServerLog>();

		StartCoroutine (dmxOut.fadeColor (schleif_lamp, DMX_lamp_startAddress, dmxOut.darkcyan, 0.5f));

	}

	// NOTE: The received messages at each server are updated here
	void Update() {
		if (inAction == true) {

			currentTime = Time.time;
			if ((currentTime - startTime) > 3) {
				dmxOut.DMXData [DMX_feedback_address] = (byte)(0);
				StartCoroutine (dmxOut.fadeColor (schleif_lamp, DMX_lamp_startAddress, dmxOut.darkcyan, 0.5f));
				inAction = false;
			}
		}


		servers = OSCHandler.Instance.Servers;
		OSCHandler.Instance.UpdateLogs();

		foreach (KeyValuePair<string, ServerLog> item in servers) {
			// If we have received at least one packet,
			// show the last received from the log in the Debug console
			if (item.Value.log.Count > 0) {
				int lastPacketIndex = item.Value.packets.Count - 1;

				/*
				UnityEngine.Debug.Log (String.Format ("SERVER: {0} ADDRESS: {1} VALUE : {2}", 
					                                    item.Key, // Server name
					                                    item.Value.packets [lastPacketIndex].Address, // OSC address
					                                    item.Value.packets [lastPacketIndex].Data [0].ToString ())); //First data value
				*/


				float tempVal = float.Parse (item.Value.packets [lastPacketIndex].Data [0].ToString ());
			    //Debug.Log ("Print Fader: "+tempVal);






				if (item.Value.packets [lastPacketIndex].Address == fader1) {
					// cube.transform.localScale = new Vector3 (tempVal, tempVal, tempVal);
					if (tempVal < 0.3f) {
						littleVal = true;

						currentTime = Time.time;
						if ((currentTime  - bigTrigger) < duration) {
							if (bigVal == true) {
								counter++;
								//Debug.Log ("decreased !! " + counter);
								bigVal = false;
								littleTrigger = Time.time;
								//Debug.Log ("littleTrigger - bigTrigger: " + (littleTrigger - bigTrigger));
							
								schleifAction ();
							}
						}
						littleTrigger = Time.time;
					}

					if (tempVal > 0.7f) {
						bigVal = true;

						currentTime = Time.time;
						if ((currentTime  - littleTrigger) < duration) {
							if (littleVal == true) {
								counter++;
								//Debug.Log ("increased !! " + counter);
								littleVal = false;
								bigTrigger = Time.time;
								//Debug.Log ("bigTrigger - littleTrigger: " + (bigTrigger - littleTrigger));
						
								schleifAction ();
							}
						}

						bigTrigger = Time.time;
					}
				} 
			}
		}

	}

	void schleifAction (){
		audio_schliff.Play ();

		startTime = Time.time;
		inAction = true;
		dmxOut.DMXData [DMX_feedback_address] = (byte)(100);
		StartCoroutine (dmxOut.fadeColor (schleif_lamp, DMX_lamp_startAddress, dmxOut.white, 0.5f));
	}
}