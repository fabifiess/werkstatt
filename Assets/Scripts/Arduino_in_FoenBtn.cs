

using UnityEngine;
using System.Collections;
using System.IO.Ports;


public class Arduino_in_FoenBtn : MonoBehaviour {

	public string serialport = "/dev/cu.usbmodem1421";
	SerialPort sp;

	public AudioSource foen_sound;
	public GameObject foen_lamp;

	public DMXout dmxOut;
	public int DMX_lamp_startAddress = 3;
	public int DMX_feedback_address = 119;

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

		StartCoroutine (dmxOut.fadeColor (foen_lamp, DMX_lamp_startAddress, dmxOut.darkcyan, 0.5f));
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
				// Feedback Ventilator
				dmxOut.DMXData [DMX_feedback_address] = (byte)(0);
				// Lampe
				StartCoroutine (dmxOut.fadeColor (foen_lamp, DMX_lamp_startAddress, dmxOut.darkcyan, 0.5f));



				inAction = false;
			}
		}

	}

	void ProcessArduinoData(string message){

		// Serial
		message = message.Trim ();


		if (message == "0") {
			//print ("Btn Input 0");
		}


		if (message == "1") {
			//print ("Btn Input 1");
			foen_sound.Play ();
			dmxOut.DMXData [DMX_feedback_address] = (byte)(255);

			StartCoroutine (dmxOut.fadeColor (foen_lamp, DMX_lamp_startAddress, dmxOut.white, 0.5f));

			startTime = Time.time;
			inAction = true;

		}
	}
}  