using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityOSC;

public class OSCin : MonoBehaviour {
	
	private Dictionary<string, ServerLog> servers;
	public GameObject cube;

	public string fader1 = "/1/fader1";
	public int thisPort = 8000;
	private string thatIpAddress;
	private int thatPort;

	private float prevTrigger = 0.0f;
	public float duration = 0.5f;
	private float littleTrigger;
	private float bigTrigger;
	private bool littleVal = true;
	private bool bigVal = true;
	private int counter = 0;

	void Start() {	
		thatIpAddress = "127.0.0.1";
		thatPort = 9000;

		// public void Init(int thisPort, string thatIpAddress, int thatPort)
		OSCHandler.Instance.Init(thisPort, thatIpAddress, thatPort); //init OSC
		servers = new Dictionary<string, ServerLog>();

	}

	// NOTE: The received messages at each server are updated here
	void Update() {
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
					cube.transform.localScale = new Vector3 (tempVal, tempVal, tempVal);
					if (tempVal < 0.3f) {
						littleVal = true;
						float fadeDuration = littleTrigger - bigTrigger;
						if (fadeDuration < duration) {
							if (bigVal == true) {
								counter++;
								Debug.Log ("decreased !! " + counter);
								bigVal = false;
								littleTrigger = Time.realtimeSinceStartup;
								Debug.Log ("littleTrigger - bigTrigger: " + (littleTrigger - bigTrigger));
								// prevTrigger = Time.realtimeSinceStartup;
							}
						}
						littleTrigger = Time.realtimeSinceStartup;
					}

					if (tempVal > 0.7f) {
						bigVal = true;
						float fadeDuration = bigTrigger - littleTrigger;
						if (fadeDuration < duration) {
							if (littleVal == true) {
								counter++;
								Debug.Log ("increased !! " + counter);
								littleVal = false;
								bigTrigger = Time.realtimeSinceStartup;
								Debug.Log ("bigTrigger - littleTrigger: " + (bigTrigger - littleTrigger));
								//prevTrigger = Time.realtimeSinceStartup;
							}
						}

						bigTrigger = Time.realtimeSinceStartup;
					}
				} 
			}
		}
	}
}