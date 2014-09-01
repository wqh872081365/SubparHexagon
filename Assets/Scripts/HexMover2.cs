using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class HexMover2 : MonoBehaviour {
	public float radius;
	public float spd;
	public enum Edge{Top,TopLeft,BotLeft,Bot,BotRight,TopRight};

	private bool isColliding =false;
	private Edge edge;
	
	/**  _____ _  _  _ _
	 /     \         |____VHeight
	/       \ _  _  _|
	\		/
     \_____/
	**/
	
	private float VHeight;
	private float halfTri;

	private List<Collider> collisionVictims = new List<Collider>();
	// Use this for initialization
	void Start () {
		VHeight = radius*Mathf.Cos(Mathf.PI/6);
		halfTri = radius*Mathf.Sin (Mathf.PI/6);
		
		this.edge = Edge.BotLeft;
		//updateSide();

	}
	
	// Update is called once per frame
	void Update () {

		float distance = -Input.GetAxis("Horizontal");
		distance = distance*spd*Time.deltaTime;
		Vector3 dest=Vector3.zero;

		if(distance>0){
			dest = getLeftDest();
			//this.transform.Rotate(new Vector3(0.0f,0.2f,0));
		}
		else if (distance<0)
			dest = getRightDest();

		//add height
		dest.y=0.9f;
		Vector3 back = transform.position+(transform.forward*-0.1f);
		Vector3 front = transform.position+(transform.forward*0.1f);

		Vector3 newPos = Vector3.MoveTowards(this.transform.position,dest,Mathf.Abs(distance));
		float dist = 0.1f;


		if(distance>0){
			//check front
			if(Physics.Raycast(transform.position,transform.TransformDirection(Vector3.left),dist)){
				print ("Obstructed on left");
				return;
			}
//			if(Physics.Raycast(back,transform.TransformDirection(Vector3.left),dist*3)){
//				print ("Obstructed on left");
//				return;
//			}
			//check rear

		}else if(distance<0){
			if(Physics.Raycast(transform.position,transform.TransformDirection(Vector3.right),dist)){
					print ("Obstructed on right");
				return;
			}
//			if(Physics.Raycast(front,transform.TransformDirection(Vector3.right),dist*3)){
//				print ("Obstructed on right");
//				return;
//			}
		}
		this.transform.position= newPos;



		updateSide();

		if(distance.Equals(0.0f)){
//			this.rigidbody.velocity=Vector3.zero;

		}

	}




	
	
	private bool isOverLeftEdge(float x){

	switch (edge)
		{
		case Edge.Top:
			return x<=-halfTri;
		case Edge.TopLeft:
			return x<=-radius;
		case Edge.BotLeft:
			return x>=-halfTri;
		case Edge.Bot:
			return x>=halfTri;
		case Edge.BotRight:
				return x>=radius;
		case Edge.TopRight:
				return x<=halfTri;			
		}
		return false;
	}
	
		private bool isOverRightEdge(float x){
		switch (edge)
		{
		case Edge.Top:
			return x>=halfTri;
		case Edge.TopLeft:
			return x>=-halfTri;
		case Edge.BotLeft:
			return x<=-radius;
		case Edge.Bot:
			return x<=-halfTri;
		case Edge.BotRight:
				return x<=halfTri;
		case Edge.TopRight:
				return x>=radius;			
		}
		return false;
	}
	
	private void updateSide(){
		//check left edge
		if(isOverLeftEdge(this.transform.position.x)){				
			//swap to next edge
			edge++;
			this.transform.Rotate (new Vector3(0,-60,0));

			//cap
			if(((int)edge)>=6)
				edge =Edge.Top;
		}
		
		//check right edge
		
		else if(isOverRightEdge(this.transform.position.x)){
			edge--;
			this.transform.Rotate (new Vector3(0,60,0));
			if(((int)edge)<0)
				edge = Edge.TopRight;
			
		}


	}
//	
//		Destroy(other.gameObject);
//		audio.Play ();
//	}

	
	private Vector3 getLeftDest(){
	switch (edge)
		{
		case Edge.Top:
			return new Vector3(-halfTri,0.0f,VHeight);
		case Edge.TopLeft:
			return new Vector3(-radius,0.0f,0.0f);
		case Edge.BotLeft:
			return new Vector3(-halfTri,0.0f,-VHeight);
		case Edge.Bot:
			return new Vector3(halfTri,0.0f,-VHeight);
		case Edge.BotRight:
			return new Vector3(radius,0.0f,0.0f);
		case Edge.TopRight:
			return new Vector3(halfTri,0.0f,VHeight);
		}
		print ("ERROR");
		return new Vector3(1,1,1);
	}

	private Vector3 getRightDest(){
			switch (edge)
			{
			case Edge.Top:
				return new Vector3(halfTri,0.0f,VHeight);
			case Edge.TopLeft:
				return new Vector3(-halfTri,0.0f,VHeight);
			case Edge.BotLeft:
				return new Vector3(-radius,0.0f,0.0f);
			case Edge.Bot:
				return new Vector3(-halfTri,0.0f,-VHeight);
			case Edge.BotRight:
				return new Vector3(halfTri,0.0f,-VHeight);
			case Edge.TopRight:
				return new Vector3(radius,0.0f,0.0f);
			}
			print ("ERROR");
			return new Vector3(1,1,1);
		}
	void OnTriggerEnter(Collider other){
		this.collisionVictims.Add(other);
	}
	void OnTriggerExit(Collider other) {
		this.collisionVictims.Remove(other);

	}
}
