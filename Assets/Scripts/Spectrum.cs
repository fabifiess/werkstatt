using UnityEngine;
using System.Collections;


public class Spectrum : MonoBehaviour {
public AudioSource asource;
public int channel;
float[] spectrum; 
float[] volume;
int counter = 0;
public int numSamples;
public GameObject enem;
	// Use this for initialization
	void Start () {
		volume = new float[numSamples];
		spectrum = new float[numSamples];
		
		for (int i=1; i<255; i++){
			GameObject enemyClone = Instantiate(enem,new Vector3(i,0,0), transform.rotation) as GameObject ;
	        enemyClone.name = "sp"+i;
		}
	}

	void Update () {
		counter++;
		asource.GetOutputData(volume, channel);
	    asource.GetSpectrumData(spectrum, channel, FFTWindow.Rectangular);

		for (int i=0; i<256; i++) {
			string	sp = "sp"+(i+1);
			GameObject sp1 = GameObject.Find(sp);
			sp1.transform.localScale= new  Vector3(1,500*spectrum[i],1);
			//sp1.GetComponent<MeshRenderer>().material.color= new Color(100*spectrum[i],1f/(20*spectrum[i]),1f/(20*spectrum[i]),1);	
		}
	}
}
