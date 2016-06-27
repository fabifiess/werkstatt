using UnityEngine;
using System.Collections;
using System.IO.Ports;


public class Arduino_in_Loetkolben : MonoBehaviour {
	public string serialport = "/dev/cu.usbmodem1411";
	// SerialPort sp = new SerialPort("/dev/cu.usbmodem1411",9600);
	SerialPort sp;

	public AudioSource audio_loetkolben;
	public GameObject loetkolben_lamp;

	public DMXout dmxOut;
	public int DMX_lamp_startAddress = 15;
	public int DMX_feedback_address = 120;

	private float startTime = 0f;
	private float currentTime = 0f;
	private bool inAction = false;

	void Awake(){
		sp = new SerialPort(serialport,9600);
	}

	void Start () {

		// Serial
		sp.Open ();
		sp.ReadTimeout = 20;
		StartCoroutine (dmxOut.fadeColor (loetkolben_lamp, DMX_lamp_startAddress, dmxOut.darkcyan, 0.5f));
	}

	void Update(){
		// Serial
		if(sp.IsOpen){
			try{
				ProcessArduinoData(sp.ReadLine());
				// ProcessArduinoData(sp.ReadByte());
			}catch(System.Exception){
				//throw;
			}
		}

		if (inAction == true) {
			currentTime = Time.time;

			if ((currentTime - startTime) > 3) {
				dmxOut.DMXData [DMX_feedback_address] = (byte)(0);
				StartCoroutine (dmxOut.fadeColor (loetkolben_lamp, DMX_lamp_startAddress, dmxOut.darkcyan, 0.5f));
				inAction = false;
			}
		}

	}

	void ProcessArduinoData(string message){

		// Serial
		message = message.Trim ();


		if (message == "0") {
			//print ("Lötkolben Input 0");
		}


		if (message == "1") {
			//print ("Lötkolben Input 1");
			audio_loetkolben.Play ();
			dmxOut.DMXData [DMX_feedback_address] = (byte)(255);
			StartCoroutine (dmxOut.fadeColor (loetkolben_lamp, DMX_lamp_startAddress, dmxOut.white, 0.5f));
			startTime = Time.time;
			inAction = true;

		}
	}
}  