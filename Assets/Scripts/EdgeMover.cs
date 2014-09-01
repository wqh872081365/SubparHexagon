using UnityEngine;
using System.Collections;

public class EdgeMover : MonoBehaviour {
	private Transform center;
	private bool isMoving = false;
	public float spd;

	private float widthPerDist = 0.86602540378f;

	private float scaleY;
	private float scaleZ;

	// Use this for initialization
	void Start () {
		this.scaleY = this.transform.localScale.y;
		this.scaleZ = this.transform.localScale.z;
		GameObject hexagon = GameObject.FindGameObjectWithTag ("Hex");
		this.center = hexagon.GetComponent <Transform>();
		print (center.position);
		this.transform.LookAt(center);
		this.rigidbody.velocity=Vector3.zero;
//		this.startMoving();

	}


	public void startMoving(){
		this.isMoving=true;
	}
		
	// Update is called once per frame
	void FixedUpdate () {
		//check if the moving flag has indicated to go
		if(this.isMoving){
			this.rigidbody.velocity = transform.forward * spd;
			this.isMoving = false;
		}
			
	}

	void Update(){
		//scale according to how far is from hexagon .
		float dist = (this.transform.position-this.center.position).magnitude;

		this.transform.localScale= new Vector3(dist*1.135f/this.widthPerDist,this.transform.localScale.y,dist*1.1f/this.widthPerDist);

	}


}
