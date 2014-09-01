using UnityEngine;
using System.Collections;

public class Hover : MonoBehaviour {
	public float period=4f;
	public float mag = 0.1f;

	private Vector3 original;
	// Use this for initialization
	void Start () {
		this.original=this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		float t = Time.time %period;
		float sinVal = Mathf.Sin(2*Mathf.PI*t/period);
		Vector3 v3 = new Vector3(original.x,original.y+sinVal*mag,original.z);
		this.transform.position=v3;
	}
}
