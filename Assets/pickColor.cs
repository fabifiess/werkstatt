using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class pickColor: MonoBehaviour {

	public GameObject cube;
	public Texture2D heightmap;

	void Start () {
		Color[] pixels = heightmap.GetPixels (0, 0, heightmap.width, heightmap.height);



		for (int x = 0; x < heightmap.width; x++) {
			for (int y = 0; y < heightmap.height; y++) {
				Color color = pixels[(x*heightmap.width)+ y];
				GameObject obj = GameObject.CreatePrimitive (PrimitiveType.Cube);
				obj.transform.position = new Vector3 (x, color.grayscale, y);
				obj.GetComponent<Renderer> ().material.color = color;
			}
		}

		/*
		if (Input.GetMouseButtonDown(0)) {
			RaycastHit hit = new RaycastHit ();
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit)){
				Debug.Log ("Getroffenes Objekt: " + hit.collider.gameObject.name);
				Debug.Log ("Klick-Position: " + hit.point);
				Debug.Log ("Grünwert des ganzen Objekts: " + hit.collider.gameObject.GetComponent<Renderer> ().material.color.g);

				var texture =  GetComponent<Renderer> ().material.mainTexture;


				Color co = Color.green;
				co = tex2d.GetPixel ((int) hit.point.x, (int)hit.point.y);
				Debug.Log ("Farbe an x/y: " + co);

				cube.GetComponent<Renderer> ().material.color = co;


				// Color backgroundcolor = hit.collider.gameObject.GetComponent<Renderer> ().material.color;
				// cube.GetComponent<Renderer> ().material.color = backgroundcolor;

			}
		}
		*/
	}
	void Update(){
	}
}