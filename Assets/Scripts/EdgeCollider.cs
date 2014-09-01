using UnityEngine;
using System.Collections;

public class EdgeCollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	
	void OnTriggerEnter(Collider other){
		if(other.tag=="Hex"){
			//find out if long piece
			warper w = GetComponent<warper>();
			if(w.rows<=1)
				Destroy(this.gameObject,0.5f);
		}
	}
}
