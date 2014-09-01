using UnityEngine;
using System.Collections;

public class Appear : MonoBehaviour {
	public float timeBeforeAppear=4f;
	private float startTime;
	private string realText;
	private bool appeared;
	// Use this for initialization
	void Start () {
		this.realText = this.GetComponent<GUIText>().text;
		this.startTime=Time.time;
		this.appeared=false;
		//hide actual message
		this.GetComponent<GUIText>().text="";
	}
	
	// Update is called once per frame
	void Update () {
		if(appeared)
			return;
		if((Time.time-this.startTime)>this.timeBeforeAppear){
			this.GetComponent<GUIText>().text=this.realText;
			this.appeared = true;
		}
	}
}
