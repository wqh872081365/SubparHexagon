using UnityEngine;
using System.Collections;

public class CircleMover : MonoBehaviour {
	public float speed;
	public float tilt;
	private float radius;

	private float angularVel;
	private float circumfrence;
	public float baseHeight = 0.8f;
	private EqualiserPulser ep;
	public float pulseMag;

	public enum direction{CW,CCW,STILL};
//	public GameObject lapFlagPreFab;
	private direction dir;

//	private GameObject lapFlag;

	private int ignoreRaycast;
	// Use this for initialization
	void Start () {
		this.ignoreRaycast = LayerMask.NameToLayer("Ignore Raycast");
		this.dir = direction.STILL;
		this.radius = this.transform.position.x;
		this.circumfrence= 2f*Mathf.PI*radius;
		this.ep = GameObject.FindGameObjectWithTag("Hex").GetComponent<EqualiserPulser>();

	}
	
	// Update is called once per frame
	void Update () {

		this.angularVel = 360*speed/circumfrence;

		float controlAxis = Input.GetAxis("Horizontal");



		float dist = 0.2f+Time.deltaTime*speed;


		if(controlAxis<0){
//			print ("<0");
//			if(dir!=direction.CCW)
//				if(lapFlag!=null)
//					Destroy(lapFlag.gameObject);
//			
//			if(lapFlag==null)
//				this.lapFlag = Instantiate(lapFlagPreFab,this.transform.position,Quaternion.identity) as GameObject;
//

			dir = direction.CCW;
			RaycastHit hitInfo;
			Ray ray = new Ray();
			ray.origin=transform.position;
			ray.direction = transform.TransformDirection(Vector3.left);
			if(Physics.Raycast(ray,out hitInfo,dist)){
				//check hit obstacle
				if(hitInfo.collider.gameObject.tag=="Obstacle")
				return;
			}
			
		}else if(controlAxis>0){
			//remove old flag if necessary
//			if(dir!=direction.CW)
//				if(lapFlag!=null)
//					Destroy(lapFlag.gameObject);
//
//			if(lapFlag==null)
//				this.lapFlag = Instantiate(lapFlagPreFab,this.transform.position,Quaternion.identity) as GameObject;
//
//
//
//
//			
			dir = direction.CW;
			RaycastHit hitInfo;
			Ray ray = new Ray();
			ray.origin=transform.position;
			ray.direction = transform.TransformDirection(Vector3.right);
			if(Physics.Raycast(ray,out hitInfo,dist)){
				//check hit obstacle
				if(hitInfo.collider.gameObject.tag=="Obstacle")
					return;
			}

		}else{
//			if(lapFlag!=null){
//				Destroy(lapFlag.gameObject);
//			}
			dir = direction.STILL;
		}
		//tilt like a ship banking 
		Vector3 newEuler = new Vector3(transform.eulerAngles.x,transform.eulerAngles.y,tilt*-controlAxis);
		transform.eulerAngles = newEuler;


		//spin around hex
		this.transform.RotateAround(Vector3.zero,Vector3.up,controlAxis*Time.deltaTime*angularVel);

		//set height from equaliser 
//		Vector3 newPos = new Vector3(transform.position.x,baseHeight+EqualiserPulser.vol*this.pulseMag,transform.position.z);
//		this.transform.position=newPos;

	}
}
