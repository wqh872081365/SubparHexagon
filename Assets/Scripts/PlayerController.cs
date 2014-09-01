using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	private Edge edge;
	public float speed;
	public float mag;
	
	public enum Edge{Top,Left,Bottom,Right};
	// Use this for initialization
	void Start () {
		this.edge=Edge.Top;
	}
	
	// Update is called once per frame
	void Update () {
		
					float distance = Input.GetAxis("Horizontal");
			distance = distance*speed*Time.deltaTime;
		
		//on top edge
		if(this.edge==Edge.Top){
			this.transform.Translate(new Vector3(distance,0,0));		
			if(this.transform.position.x<-this.mag)
				this.edge=Edge.Left;
			else if(this.transform.position.x>this.mag)
				this.edge=Edge.Right;
		}
		//on left edge
		else if(this.edge==Edge.Left){
			this.transform.Translate(new Vector3(0,0,distance));					
			if(this.transform.position.z>this.mag)
				this.edge=Edge.Top;
			else if(this.transform.position.z<-this.mag)
				this.edge=Edge.Bottom;
		}
		//on bottom edge
		
		else if (this.edge==Edge.Bottom){
			this.transform.Translate(new Vector3(-distance,0,0));					
			if(this.transform.position.x>this.mag)
				this.edge=Edge.Right;
			else if(this.transform.position.x<-this.mag)
				this.edge=Edge.Left;
		}
		//on right edge
		else if (this.edge==Edge.Right){
			this.transform.Translate(new Vector3(0,0,-distance));					
			if(this.transform.position.z>this.mag)
				this.edge=Edge.Top;
			else if(this.transform.position.z<-this.mag)
				this.edge=Edge.Bottom;
		}
		
	}
	
	

}
