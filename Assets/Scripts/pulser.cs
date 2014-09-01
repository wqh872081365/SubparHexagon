using UnityEngine;
using System.Collections;

public class pulser : MonoBehaviour {
	private static AudioSource song;
	private static AudioSettings audioSettings;
	public float interPulsePeriod;
	public float pulsePeriod;
	public float mag;

	public float offSet;

	private Vector3 originalScale;

	private float startY;
	private float startTime;

	private Vector3 dirVector;
	public direction dir;
	public enum direction{WHY,EX,ZEE};
	static bool playing =false;

	// Use this for initialization
	void Start () {
		//Get hexagon and set song
		if(this.tag=="Hex"){
			pulser.song=this.audio;

			song.Play ();

		}

		switch(dir){
				case direction.WHY:
					this.dirVector=new Vector3(0.0f,1.0f,0.0f);
					break;
				case direction.EX:
					this.dirVector=new Vector3(1.0f,0.0f,0.0f);
					break;
				case direction.ZEE:
					this.dirVector=new Vector3(0.0f,0.0f,1.0f);
					break;
		};
		this.startTime = Time.time;	
		this.originalScale = this.transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {

		if(song!=null&&song.isPlaying){
		float t = (song.time+offSet) % interPulsePeriod;
		if(t<pulsePeriod){
			if(!playing){
				if (this.tag=="Hex"){
					audio.Play ();
					pulser.playing = true;
				}
						
			}
			float proportion = t/pulsePeriod;
			proportion = proportion * Mathf.PI;
			float sinVal = (float)Mathf.Sin(proportion)*mag;

			Vector3 scaleVector = dirVector*mag*sinVal;
			Vector3 pos = originalScale+scaleVector;
			this.transform.localScale = pos;
		}
		}

	}
}
