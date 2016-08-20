using UnityEngine;
using System.Collections;
using System.IO.Ports;


public class Arduino_in_Bohrer : MonoBehaviour {
	public string serialport = "/dev/cu.usbmodem14311";
	SerialPort sp;

	public AudioSource audio_bohrer;
	public GameObject bohrer_lamp;

	public DMXout dmxOut;
	public int DMX_lamp_startAddress = 39;
	public int DMX_feedback_address = 121;

	private float startTime = 0f;
	private float currentTime = 0f;
	private bool inAction = false;

	private int receivedVal = -1;

	void Awake(){
		sp = new SerialPort(serialport,9600);
	}

	void Start () {

		// Serial
		sp.Open ();
		sp.ReadTimeout = 20;
		StartCoroutine (dmxOut.fadeColor (bohrer_lamp, DMX_lamp_startAddress, dmxOut.darkcyan, 0.5f));
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

			if ((currentTime - startTime) > 1.0f) {
				Debug.Log ("Bohrer aus");
				dmxOut.DMXData [DMX_feedback_address] = (byte)(0);
				StartCoroutine (dmxOut.fadeColor (bohrer_lamp, DMX_lamp_startAddress, dmxOut.darkcyan, 0.5f));
				inAction = false;
			}
		}

	}

	void ProcessArduinoData(string message){
		// Serial
		message = message.Trim ();
		receivedVal = System.Int32.Parse(message);
		Debug.Log ("receivedVal: " + receivedVal);


		//print ("Bohrer Input 1");
		audio_bohrer.Play ();

		// Debug.Log ("receivedVal: " + receivedVal);
		dmxOut.DMXData [DMX_feedback_address] = (byte)(155);
		StartCoroutine (dmxOut.fadeColor (bohrer_lamp, DMX_lamp_startAddress, dmxOut.white, 0.5f));
		startTime = Time.time;
		inAction = true;
	}
}  


