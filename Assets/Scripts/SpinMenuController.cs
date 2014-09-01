using UnityEngine;
using System.Collections;

public class SpinMenuController : MonoBehaviour {
	enum Item{Hard,Harder,Hardest};
	Item selected;
	public float speed = 100;
	private bool isSpinning = false;

	private float spinDir = 0f;
	// Use this for initialization
	void Start () {
		this.selected = Item.Harder;
	}
	
	// Update is called once per frame
	void Update () {
		if(this.spinDir==0f){
			this.spinDir = Input.GetAxis("Horizontal");
			if(this.spinDir<0f){
				this.selected++;
				print (selected);
			}else if (this.spinDir>0f){
				this.selected--;
				print (selected);
			}
		}
		else{
			float destY = getRotationOf(selected);
			print ("Current:"+this.transform.rotation.eulerAngles.y+"Dest:"+destY);
			if(this.spinDir>0f){
				if(destY>this.transform.rotation.eulerAngles.y)
					transform.RotateAround(Vector3.zero,Vector3.up,this.speed*Time.deltaTime*this.spinDir);
				else
					this.spinDir=0f;
			}else if (this.spinDir<0f){
				if(destY<this.transform.rotation.eulerAngles.y)
					transform.RotateAround(Vector3.zero,Vector3.up,this.speed*Time.deltaTime*this.spinDir);
				else
					this.spinDir=0f;

			}
		}

	}

	private float getRotationOf(Item item){
		switch(item){
		case(Item.Hard):
			return 240f;
		case(Item.Harder):
			return 180f;
		case(Item.Hardest):
			return 120f;
		}
		return 0f;
	}


}
