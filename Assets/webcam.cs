using UnityEngine;
using System.Collections;

public class webcam : MonoBehaviour {
	public WebCamTexture wct;
	public GameObject cube;
	Camera cam;

	void Start () {
		cam = Camera.main;
		wct = new WebCamTexture ();
		GetComponent<Renderer> ().material.mainTexture = wct;
		wct.Play ();
	
	}

	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			RaycastHit hit = new RaycastHit ();

			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit)){
				Debug.Log ("Click position: " + hit.point);
				Vector3 hitpoint = hit.point;
				Vector3 screenPos = cam.WorldToScreenPoint(hitpoint);

	
				Color color = wct.GetPixel ((int)screenPos.x, (int)screenPos.y);
				// Color color = wct.GetPixel ((int)(screenPos.x/Screen.width), (int)(screenPos.x/Screen.height));
				Debug.Log ("Color x/y: " + color);

				cube.GetComponent<Renderer> ().material.color = color;
			}
		}
	}
}
