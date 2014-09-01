using UnityEngine;
using System.Collections;
using System;
public class CameraZoomer : MonoBehaviour {
	public float period;
	public float mag;
	
	private float startY;
	private float startTime;
	// Use this for initialization
	void Start () {
		this.startTime = Time.time;	
		this.startY = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		float proportion = ((Time.time-startTime))/period;
		proportion = proportion * Mathf.PI;
		float sinVal = (float)Math.Sin(proportion)*mag;
		Vector3 pos = new Vector3(this.transform.position.x, sinVal+startY,this.transform.position.z);
		this.transform.position= pos;
	}
}
