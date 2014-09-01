using UnityEngine;
using System.Collections;

public class TipCollide : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider other){
		if(other.tag=="Obstacle"){
//			this.audio.Play();
//		}
		GameController2.it.endGame();
	}
	}
}
