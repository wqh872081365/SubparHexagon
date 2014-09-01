using UnityEngine;
using System.Collections;

public class AnimatingScript : MonoBehaviour {
	Animator anim;
	int hashCW;
	int hashCCW;

	int stillStateHash = Animator.StringToHash("Base Layer.Still");
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		this.hashCW = Animator.StringToHash("TriggerCW");
		this.hashCCW = Animator.StringToHash("TriggerCCW");
	}
	
	// Update is called once per frame
	void Update () {
		AnimatorStateInfo asi = anim.GetNextAnimatorStateInfo(0);
		float lr = Input.GetAxis("Horizontal");
		print(asi.ToString());
		if(lr<0){
			anim.SetTrigger("TriggerCCW");
		}else if (lr>0){
			anim.SetTrigger("TriggerCW");
		}

	}
}

