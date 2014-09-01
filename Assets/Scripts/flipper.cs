using UnityEngine;
using System.Collections;

public class flipper : MonoBehaviour {
	public float bpm;
	private float timePerBeat;
	private float triggerVol = 0.08f;

	private float lastBeat=-1000f;
	// Use this for initialization
	void Start () {
		this.timePerBeat=60f/bpm;
		this.timePerBeat=this.timePerBeat*0.8f;
	}
	
	// Update is called once per frame
	void Update () {
		if(EqualiserPulser2.vol>triggerVol&&(Time.time-this.lastBeat)>this.timePerBeat){
			this.lastBeat = Time.time;
			this.transform.RotateAround(Vector3.zero,Vector3.up,60f);
		}
	}
}
