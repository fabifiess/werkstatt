
using UnityEngine;
using System.Collections;
using System.IO.Ports;


public class Arduino_in_Gummi : MonoBehaviour {

	public AudioSource hauptstueck;
	public GameObject gummi_lamp;

	public string serialport = "/dev/cu.usbmodem1441";
	SerialPort sp;


	public DMXout dmxOut;
	public int DMX_lamp_startAddress = 0;

	private float startTime = 0f;
	private float currentTime = 0f;
	private bool inAction = false;

	private int receivedVal = 0;
	private int counter = 0;


	void Awake(){
		sp = new SerialPort(serialport,9600);
	}

	void Start () {

		// Serial
		sp.Open ();
		sp.ReadTimeout = 20;
		StartCoroutine (dmxOut.fadeColor (gummi_lamp, DMX_lamp_startAddress, dmxOut.darkcyan, 0.5f));
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

		Debug.Log("upd Pitch: " + hauptstueck.pitch);

		currentTime = Time.time;
		if (inAction == true) {
			if ((currentTime - startTime) > 0.5) {
				hauptstueck.pitch = 1.0f;
				StartCoroutine (dmxOut.fadeColor (gummi_lamp, DMX_lamp_startAddress, dmxOut.darkcyan, 0.5f));
				inAction = false;
			}
		}

		// Falls pitch tun was er will: Ausschalten mit n, einschalten mit m

		if(Input.GetKeyDown("n")){
			hauptstueck.pitch = 1.0f;
			inAction = true;
		}
		if(Input.GetKeyDown("m")){
			hauptstueck.pitch = 1.0f;
			inAction = false;
		}

	}

	void ProcessArduinoData(string message){

		// Serial
		message = message.Trim ();

		counter++;
	    receivedVal = System.Int32.Parse(message);
		//print ("receivedVal " + receivedVal);

		//Werte zwischen 19 und 60
		float tonhoehe = ((float)receivedVal - 19.0f) / 41.0f + 1.0f;
		//print (counter + "tonhoehe " + tonhoehe);

		if (tonhoehe >= 1.1f) {
			if (inAction == false) {
				


				startTime = Time.time;
				inAction = true;
				hauptstueck.pitch = tonhoehe;
				//print("Pitch: " + hauptstueck.pitch);
				StartCoroutine (dmxOut.fadeColor (gummi_lamp, DMX_lamp_startAddress, dmxOut.white, 0.5f));
			}

		}

		if (tonhoehe < 1.1f) {
			hauptstueck.pitch = 1.0f;
			//print("<1.1 Pitch: " + hauptstueck.pitch);
		}


	
	}
}  