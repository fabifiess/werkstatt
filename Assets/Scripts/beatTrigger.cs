using UnityEngine;
using System.Collections;

public class beatTrigger : MonoBehaviour {
	public beatManager beatManager;

	void OnTriggerEnter(){
		beatManager.enterTrigger (gameObject.name);
	}
	void OnTriggerExit(){
		beatManager.exitTrigger (gameObject.name);
	}
}