using UnityEngine;
using System.Collections;

public class pickedUp : MonoBehaviour {
	private static float t1;
	// Use this for initialization
	void Start () {
		CapsuleCollider cc = this.GetComponent<CapsuleCollider>();
		cc.radius=0f;
		t1 = Time.time;	
	}
	
	// Update is called once per frame
	void Update () {
		if (Age()>0.2f){
			CapsuleCollider cc = this.GetComponent<CapsuleCollider>();
			cc.radius=0.25f;
		}
	}

	public static float Age(){
		return Time.time-t1;
	}

	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag.Equals("ShipBase")){
			print ("HERE");
			GameController2.startTime=GameController2.startTime-2f;
			SoundEffects.playPowerupEffect();
			Destroy(this.gameObject);


		}
	}
}
