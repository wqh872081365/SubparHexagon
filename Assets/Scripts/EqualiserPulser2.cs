using UnityEngine;
using System.Collections;

using UnityEngine;
using System.Collections;

public class EqualiserPulser2 : MonoBehaviour {
	public static AudioSource song;
	private static AudioSettings audioSettings;
	public float pulsePeriod;
	public float mag;
	
	public float upperBand;
	public float lowerBand;
	

	private Vector3 originalScale;
	
	private float startY;
	private float startTime;
	
	private Vector3 dirVector;
	public direction dir;
	public enum direction{WHY,EX,ZEE};
	static bool playing =false;
	
	public static float lastCalculatedVol = -10000;

	private float originalHeight;


	public float triggerVol =0.08f;
	private static float lastBeat=-1000f;
	public float bpm=130f;
	public static float vol;
	private float timePerBeat;
	
	// Use this for initialization
	void Start () {
		this.timePerBeat = (60f/bpm)*0.8f;
		//Get hexagon and set song
		if(this.tag=="Hex"){
			song=this.audio;
			
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
		this.originalHeight = this.transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		
			
//			//recalculate the volume if hasn't been done recently
			if(this.tag=="Hex"){
				if(Time.time-lastCalculatedVol>0.01){
					lastCalculatedVol = Time.time;
					vol= BandVol(lowerBand,upperBand);
				}
				if(vol>this.triggerVol&&(Time.time-lastBeat)>this.timePerBeat){
					lastBeat = Time.time;
				}
			}

			
			float t = (Time.time) % lastBeat;
			
			if(t<pulsePeriod/4f){
				float proportion = t/pulsePeriod;
				proportion = proportion * Mathf.PI*2f;
				float sinVal = (float)Mathf.Sin(proportion)*mag;
				
				Vector3 scaleVector = dirVector*mag*sinVal;

				if(this.tag!="ShipBase"){
					Vector3 pos = originalScale+scaleVector;
					this.transform.localScale = pos;
					
				}else{
					this.transform.position = new Vector3(this.transform.position.x,originalHeight+ sinVal,this.transform.position.z);
					
				}
			}else 	if(t<pulsePeriod){
				float proportion = (t-(pulsePeriod/4f))/pulsePeriod;
				proportion = proportion/1.5f;
				
				proportion = proportion+pulsePeriod/4f;
				proportion = proportion * Mathf.PI*2f;
				
				float sinVal = (float)Mathf.Sin(proportion)*mag;
				
				Vector3 scaleVector = dirVector*mag*sinVal;
				
				if(this.tag!="ShipBase"){
					Vector3 pos = originalScale+scaleVector;
					this.transform.localScale = pos;
					
				}else{
					this.transform.position = new Vector3(this.transform.position.x,originalHeight+ sinVal,this.transform.position.z);
					
				}
			}
			

		
	}
	
	public static float BandVol(float fLow, float fHigh) {
		float fMax =10000;
		int nSamples = 512;
		fLow = Mathf.Clamp(fLow, 20, fMax); // limit low...
		fHigh = Mathf.Clamp(fHigh, fLow, fMax); // and high frequencies
		// get spectrum
		float[] freqData = song.GetSpectrumData(nSamples, 0, FFTWindow.BlackmanHarris); 
		int n1 =(int) Mathf.Floor(((float)(fLow * nSamples)) / fMax);
		int n2 = (int) Mathf.Floor(((float)(fHigh * nSamples)) / fMax);
		float sum = 0;
		
		float max =0;
		// average the volumes of frequencies fLow to fHigh
		for (int i=n1; i<=n2; i++){
			sum += freqData[i];
			if(freqData[i]>max)
				max=freqData[i];
		}
		
		return max;
		//		return sum / (n2 - n1 + 1);
	}
}
