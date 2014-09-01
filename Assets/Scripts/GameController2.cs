using UnityEngine;
using System.Collections;

public class GameController2 : MonoBehaviour {
	public static Vector3 playerStartPosition = new Vector3(1.2f,0.7f,0f);
	public static Vector3 playerStartRotation = new Vector3(0f,90f,0f);

	private static GameObject p1;
	private static GameObject oc;
	private static GameObject hex;
	public static bool isRunning = true;
	public static float startTime;

	public static float runTime=0f;
	public static GameController2 it;
	//GUI elements
	public GUIText scoreText; 
	public GUIText retryText;
	public GUITexture flashTexture;
	private GameObject cam;
	private Animator anim;

	private static GUITexture ft;
	private bool firstTime = true;
	private int stillState;



	// Use this for initialization
	void Start () {
		ColorChanger.ChooseNewColor();
		stillState= Animator.StringToHash("Still");
		it = this;
		startTime = Time.time;
		p1 = GameObject.FindGameObjectWithTag("ShipBase");
		oc=GameObject.FindGameObjectWithTag("ObstacleCreator");
		hex=GameObject.FindGameObjectWithTag("Hex");
		ft = this.flashTexture;
		cam = GameObject.FindGameObjectWithTag("MainCamera");
		anim = cam.GetComponent<Animator>();

		this.retryText.gameObject.SetActive(true);

		oc.SetActive(false);
		p1.SetActive(false);
		hex.audio.Stop();
		isRunning=false;
		this.firstTime=true;


		//set static fields to local versions
	}
	
	// Update is called once per frame
	void Update () {
		if(isRunning){
			this.retryText.gameObject.SetActive(false);
			runTime=Time.time-startTime;
			string dispTime = runTime.ToString();
			//chop of decimal places after two if necessary
			int preDecimalChars = dispTime.Split('.')[0].Length;
			if(dispTime.Length>preDecimalChars+3)
				dispTime = dispTime.Substring(0,preDecimalChars+3);

			this.scoreText.text = "Time:"+dispTime;

		}
		else if(!isRunning){
			this.retryText.gameObject.SetActive(true);
		}
		if (Input.GetKey(KeyCode.R)&&isRunning==false){
			isRunning=true;
			Restart ();

		}
		//update gui
//		if(anim.GetCurrentAnimatorStateInfo(0).nameHash==stillState){
//			anim.enabled=false;
//		}
		//if end state 
	}


	public  void endGame(){
		StartCoroutine(Flash(ft,0.5f));
		isRunning = false;
		//disable player
		p1.SetActive(false);
		oc.SetActive(false);
		hex.audio.Stop();
		it.audio.Play ();

		ColorChanger.isCycling=false;
		//destroy all current obstacles
		foreach(GameObject go in GameObject.FindGameObjectsWithTag("Obstacle")){
			Destroy(go);
		}
	}

	public void Restart(){


		if(!firstTime){
			print ("Second time");
			StartCoroutine(Flash(ft,0.5f));
			ColorChanger.ChooseNewColor();
			float songStartTime = Random.Range(0f,30f);
			hex.audio.time= songStartTime;
			hex.audio.Play();

		}else{



			GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");
		Animator anim = cam.GetComponent<Animator>();
		int startHash = Animator.StringToHash("StartGame");
		anim.SetTrigger(startHash);
		firstTime=false;
			cam.GetComponent<CameraSpinner>().enabled=true;
			StartCoroutine(StopAnim());

		}

		//ensure splash screen hidden
		GameObject splash = GameObject.FindGameObjectWithTag("Splash");
		if(splash!=null)
			splash.SetActive(false);


		//


		hex.GetComponent<ColorChanger>().StartCycling();
		//reset times
		runTime=0f;
		startTime = Time.time;

		//reset player position
		p1.transform.position = playerStartPosition;
		p1.transform.rotation=Quaternion.identity;
		p1.transform.RotateAround(p1.transform.position,Vector3.up,90f);

		//play song




		//set player to active
		p1.SetActive(true);

		//set obstacle creator to active
		oc.SetActive(true);
		ObstacleController obstCon = oc.GetComponent<ObstacleController>();
		obstCon.StartGenerating();
	}
	private IEnumerator StopAnim(){
		yield return new WaitForSeconds(1.5f);
		anim.enabled=false;

	}

	private  static IEnumerator Flash(GUITexture gt,float length){
		//ensure active
		ft.gameObject.SetActive(true);
		//set to pure white

		Color startCol = Color.white;
		Color endCol = new Color(startCol.r,startCol.g,startCol.b,0.0f);
		for(float i=0.0f;i<1.0f;i+=Time.deltaTime){
			gt.color= Color.Lerp(startCol,endCol,i);
			yield return new WaitForSeconds (0f);
		}
		gt.color=endCol;

	}

}
