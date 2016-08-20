using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityOSC;

public class WerkstattController : MonoBehaviour {
	
	private Dictionary<string, ServerLog> servers;

	public DMXout dmxOut;
	public int DMX_lamp_startAddress = 6;
	public int DMX_feedback_address = 120;

	public string fader1 = "/1/pushRed";
	public int thisPort = 8000;
	private 	string thatIpAddress;
	private int thatPort;

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


				//float tempVal = float.Parse (item.Value.packets [lastPacketIndex].Data [0].ToString ());
			    //Debug.Log ("Print Fader: "+tempVal);






				if (item.Value.packets [lastPacketIndex].Address == fader1) {
					// cube.transform.localScale = new Vector3 (tempVal, tempVal, tempVal);

				} 
			}
		}
	}
}