using UnityEngine;
using System.Collections;

public class CameraSpinner : MonoBehaviour {
	public float spd;
	public float minFlipTime=3;
	public float maxFlipTime=10;

	private float amnesty = 3f;
	private float startTime;

	private float nextFlip;
	// Use this for initialization
	void Start () {
		this.startTime=Time.time;
		this.nextFlip =Time.time+ Random.Range(minFlipTime,maxFlipTime);
	}
	

	
	// Update is called once per frame
	void Update () {
//		if((Time.time-startTime)>amnesty){
			if(Time.time>nextFlip){
				this.spd=-spd;
				this.nextFlip =Time.time+ Random.Range(minFlipTime,maxFlipTime);
			}
			this.transform.RotateAround(Vector3.zero,Vector3.up,Time.deltaTime*spd);

//		}
	}
}
