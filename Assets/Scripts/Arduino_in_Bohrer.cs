using UnityEngine;
using System.Collections;
using System.IO.Ports;


public class Arduino_in_Bohrer : MonoBehaviour {
	public string serialport = "/dev/cu.usbmodem14311";
	SerialPort sp;

	public AudioSource audio_bohrer;

	public DMXout dmxOut;
	public int DMX_lamp_startAddress = 39;
	public int DMX_feedback_address = 121;

	private float startTime = 0f;
	private float currentTime = 0f;
	private bool inAction = false;

	private int receivedVal = -1;
	private int prevReceivedVal = -1;

	void Awake(){
		sp = new SerialPort(serialport,9600);
	}

	void Start () {

		// Serial
		sp.Open ();
		sp.ReadTimeout = 20;
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

			if ((currentTime - startTime) > 1.5f) {
				Debug.Log ("Bohrer aus");
				dmxOut.DMXData [DMX_feedback_address] = (byte)(0);
				dmxOut.DMXData [DMX_lamp_startAddress] = (byte)(0);
				dmxOut.DMXData [DMX_lamp_startAddress+1] = (byte)(0);
				dmxOut.DMXData [DMX_lamp_startAddress+2] = (byte)(0);
				inAction = false;
			}
		}

	}

	void ProcessArduinoData(string message){
		// Serial
		message = message.Trim ();
		receivedVal = System.Int32.Parse(message);

		if (prevReceivedVal < 0) {
			prevReceivedVal = receivedVal;
			Debug.Log ("Bohrer -1");
		} else {
			if (receivedVal > prevReceivedVal + 30) {
				//print ("Bohrer Input 1");
				audio_bohrer.Play ();

				Debug.Log ("Bohrer an");
				dmxOut.DMXData [DMX_feedback_address] = (byte)(255);
				dmxOut.DMXData [DMX_lamp_startAddress] = (byte)(255);
				dmxOut.DMXData [DMX_lamp_startAddress+1] = (byte)(255);
				dmxOut.DMXData [DMX_lamp_startAddress+2] = (byte)(255);
				startTime = Time.time;
				inAction = true;
			}
		}



	}
}  


