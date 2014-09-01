using UnityEngine;
using System.Collections;

public class CircleMover2 : MonoBehaviour {
	public float speed;
	public float tilt;
	private float radius;
	
	private float circumfrence;
	public float baseHeight = 0.8f;
	private EqualiserPulser ep;
	public float pulseMag;

	public enum direction{CW,CCW,STILL};
	public GameObject lapFlagPreFab;
	private direction dir;
	
	public float jumpVel = 3f;
	public float gravitySpd =0.1f;

	public float baseRadius = 1.2f;

	private float vel = 0f;
	private int ignoreRaycast;
	// Use this for initialization
	void Start () {
		this.ignoreRaycast = LayerMask.NameToLayer("Ignore Raycast");
		this.dir = direction.STILL;
		this.radius = this.transform.position.x;
		this.circumfrence= 2f*Mathf.PI*radius;
		this.ep = GameObject.FindGameObjectWithTag("Hex").GetComponent<EqualiserPulser>();

		this.rigidbody.AddForce(this.transform.TransformDirection(Vector3.right));

		
	}
	
	// Update is called once per frame
	void Update () {
		float angularVel = 360*speed/circumfrence;
		float controlAxis =-1f;

		float radius = getRadius();
		print (radius);
		//spin around hex
		this.transform.RotateAround(Vector3.zero,Vector3.up,speed*Time.deltaTime);

		//set height from equaliser 
		Vector3 newPos = new Vector3(transform.position.x,baseHeight+EqualiserPulser.vol*this.pulseMag,transform.position.z);
		this.transform.position=newPos;

		if(Input.GetButtonDown("Fire1")){
			print("Jump");
			this.vel=jumpVel;
		}

		this.vel-=gravitySpd*Time.deltaTime;
		if(radius<baseRadius){
			if(this.vel<0)
				this.vel=0;
		}

		//move appropriate velocity away from /towards center
		Vector3 center = new Vector3(0f,this.transform.position.y,0f);
		Vector3 diff = (this.transform.position-center).normalized;
		this.transform.position+=diff*(vel)*Time.deltaTime;

		//change y with eq
		Vector3 pos = new Vector3(transform.position.x,this.baseHeight+EqualiserPulser.vol,transform.position.z);
		this.transform.position=pos;

	}

	private float getRadius(){
		//get centre at current height
		Vector3 center = new Vector3(0f,this.transform.position.y,0f);
		return Vector3.Distance(this.transform.position,center);

	}

	public void OnTriggerEnter(Collider other){
		if(other.tag=="Hex"||other.tag=="Obstacle"){
			GameController2.it.endGame();
		}

	}


}
