



using UnityEngine;
using System.Collections;

public class ColorChanger : MonoBehaviour {

	public float mag = 2f;
	public float minDarkness = 0.2f;

	public static float minFlipTime=5f;
	public static float maxFlipTime=5f;
	public static float flipLength =1f;

	public static bool isCycling;

	private static Color initialColor=Color.black;

	private static ColorChanger it=null;

//	GameObject 
	// Use this for initialization
	void Start () {

		if(this.tag=="Hex"){
			StartCycling();
			this.StartCoroutine (Cycler ());

		}

	}


	public void StartCycling(){
		isCycling=true;
	}

	public static void ChooseNewColor(){
		initialColor = NextColor();

	}

	private static Color NextColor(){
		int choice =Random.Range(0,5);
		Color col = Color.black;
		switch(choice){
		case(0):
			col = Color.blue;
			break;
		case(1):
			col = Color.red;
			break;
		case(2):
			col = Color.green;
			break;
		case(3):
			col = Color.white;
			break;
		case(4):
			col = Color.magenta;
			break;
		}
		if(col==initialColor)
			return NextColor();
		else 
			return col;
	}


	public static void transition(){


	}
	
	// Update is called once per frame
	void Update () {
		float vol = EqualiserPulser2.vol;
		Color newCol = Color.black;


		newCol.r=initialColor.r*(vol*mag+minDarkness);
		newCol.b=initialColor.b*(vol*mag+minDarkness);
		newCol.g=initialColor.g*(vol*mag+minDarkness);
		this.renderer.material.color=newCol;


	}


	private static IEnumerator Cycler(){
		while(isCycling){
			float waitTime =4f;

			yield return new WaitForSeconds(waitTime);
			Color oldColor = initialColor;
			Color newColor = NextColor();
			for(float i=0f;i<1;i+=Time.deltaTime/flipLength){
				initialColor=Color.Lerp(oldColor,newColor,i);
				yield return new WaitForSeconds(0f);
			}
			initialColor=newColor;
		}
	}



}
