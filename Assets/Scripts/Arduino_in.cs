using UnityEngine;
using System.Collections;
using System.IO.Ports;


public class Arduino_in : MonoBehaviour {
	public GameObject movingobject;
	// Smooth Transition
	private Vector3 newPosition;
	// Animation
	Vector3 positionA = new Vector3 (0, 1, 0);
	Vector3 positionB = new Vector3 (9, 6, 9);

	// Serial
	SerialPort sp = new SerialPort("/dev/tty.usbmodem1421",9600);
	public int smoothFlicker=3;
	private int countSameMessages =0;
	private string prevMessage="0";


	void Start () {
		// Animation
		newPosition = movingobject.transform.position;

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


		// Falls kein Arduino vorhanden ist ...
		if(Input.GetKeyDown (KeyCode.A)){
			print ("Nix da");
			newPosition = positionA;
		}

		
		if(Input.GetKeyDown (KeyCode.D)){
			print ("Jawohl");
			newPosition = positionB;
		}
	}

	void ProcessArduinoData(string message){
		// Interpolate A to B
		movingobject.transform.position = Vector3.Lerp (movingobject.transform.position,newPosition,Time.deltaTime*3);

		// Serial
		message = message.Trim ();
		if (message == prevMessage)
			countSameMessages++;
		else if (message != prevMessage) {
			countSameMessages = 1;
		}

		if (countSameMessages < smoothFlicker)print ("wait");
		if (countSameMessages == smoothFlicker) {

			if (message == "0") {
				print ("Arduino input 0");
				newPosition = positionA;
			}


			if (message == "1" || Input.GetKeyDown (KeyCode.D)) {
				print ("Arduino input 1");

				//   ruckartige Bewegung von a nach b
				//	 transform.position = new Vector3 (15, 0, 0); 
				
				//   Kontinuierliche Bewegung
				//newPosition = new Vector3(25,0,0);
				//transform.position=Vector3.Lerp(transform.position, newPosition, Time.deltaTime*4);

				// Interpolate A to B
				newPosition = positionB;
			}
		}
		prevMessage = message;
	}
}