using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EdgeSpawn{
	public HexMover2.Edge e;
	public int depth;
	public EdgeSpawn(HexMover2.Edge edge,int depth){
		this.e=edge;
		this.depth = depth;
	}
}



public class ObstacleController : MonoBehaviour {
	public float spawnDist = 100f;
	public GameObject obstacle;

	public float startWait = 1.0f;
	private float spawnWait;
	public float waveWait=1f;	
	public int hazardCount =6;



	public TextAsset patternFile;

	private List<Pattern>patterns = new List<Pattern>();







	// Use this for initialization
	void Start () {
		//calculate necessary frequency of creation
		//this.spawnWait = 0.19f/2f;
		//StartGenerating();
		this.patterns = ReadPatterns(patternFile);

		// calculate spawn wait.
		warper w = obstacle.GetComponent<warper>();
		float spd = w.spd;
		float rw = w.rowWidth;
		this.spawnWait = rw/spd;
		print ("spd:"+spd);
		print ("rw:"+rw);
		print ("sw"+spawnWait);
		//this.spawnWait = width/spd;
		//assume 
	}

	public void StartGenerating(){
		StartCoroutine(SpawnWaves());
	}

	private List<Pattern> ReadPatterns(TextAsset ta){

		List<Pattern> patternList = new List<Pattern>();




		string[] lines = ta.text.Split(new char[] { '\n' }) ;

//		for(int i=0;i<lines.Length;i++)
//			print (lines[i]);
		for(int i=1;i<lines.Length;i++)
			if(lines[i].StartsWith("-"))
				patterns.Add(ReadPat(lines,i));

		return patterns;

	}

	Pattern ReadPat(string[]lines, int idx){
		//skip title 
		idx++;
		string line = lines[idx++];
		List<List<HexMover2.Edge>>waves = new List<List<HexMover2.Edge>>();
		while(!line.StartsWith("-")){
//			print ("MAKING WAVE:"+line);
			//get each edge presence
			List<HexMover2.Edge>edges = new List<HexMover2.Edge>();
			for(int i=0;i<6;i++){

				//check for presence of wall

				if(line.ToCharArray()[i]=='v'){
					edges.Add((HexMover2.Edge)i);
				}
			}
			waves.Add(edges);
			//end if reached eof
			if(idx==lines.Length-1)
				break;
			line = lines[idx++];
		}
		Pattern pat = new Pattern(waves);
		return pat;

	}
	// Update is called once per frame
	void Update () {
	
	}





	IEnumerator SpawnWaves(){
		yield return new WaitForSeconds (startWait);
		while(true){
			//chose a pattern at random

			Pattern pat = this.patterns[(int)Random.Range(0,patterns.Count)];
			pat.restart();

			while(pat.hasNext()){
				float f = Time.time;
				List<GameObject>gos = new List<GameObject>();
				//create all obstacles 
				foreach(EdgeSpawn es in pat.getNextWave()){
					gos.Add(addObstacle(es));
				}
				
				//start them all moving
				foreach(GameObject go in gos){
					warper em  = go.GetComponent <warper>();
					em.startMoving();
				}
				float timeToWait = spawnWait-(Time.time-f);

				//TODO remember i changed this
				timeToWait = spawnWait;
//				print (timeToWait);
				yield return new WaitForSeconds(timeToWait);
			}

			yield return new WaitForSeconds(waveWait);

		}
	}

	public GameObject addObstacle(EdgeSpawn es){
		int depth = es.depth;
		HexMover2.Edge side = es.e;
		
		Vector3 pos = this.transform.position;
		
		float angle = 0;//right pos
		switch(side){
		case HexMover2.Edge.TopRight:
			angle = Mathf.PI/6;
			break;
		case HexMover2.Edge.Top:
			angle =3*Mathf.PI/6;
			break;
		case HexMover2.Edge.TopLeft:
			angle = 5*Mathf.PI/6;
			break;
		case HexMover2.Edge.BotLeft:
			angle = 7*Mathf.PI/6;
			break;
		case HexMover2.Edge.Bot:
			angle = 9*Mathf.PI/6;
			break;
		case HexMover2.Edge.BotRight:
			angle = 11*Mathf.PI/6;
			break;
		}
		
		float x = Mathf.Cos (angle)*spawnDist;
		float z = Mathf.Sin	(angle)*spawnDist;
		pos = new Vector3(x,0.0f,z);
		
		//irrelevant
		Quaternion rot = this.transform.rotation;
		
		GameObject hexEdge = Instantiate(obstacle,pos,rot) as GameObject;
		hexEdge.GetComponent<warper>().rows = (float)depth;
		return hexEdge;
		
	}

	public class Pattern{
		int stage=0;
		private List<List<EdgeSpawn>> waves;
		
		
		public Pattern( List<List<HexMover2.Edge>> lst){
			this.waves = new List<List<EdgeSpawn>>();
			//check for collumns that can be compressed
			for(int i=0;i<lst.Count;i++){
				
				List<EdgeSpawn>wave = new List<EdgeSpawn>();
				//for each dir in wave
				foreach(HexMover2.Edge edge in lst[i]){
					//keep looking forward for edges to compound
					int j=i+1;
					while(j<lst.Count&&lst[j].Contains(edge)){
						//remove element to be compounded
						lst[j].Remove(edge);
						j++;
					}
					EdgeSpawn edgeSpawn = new EdgeSpawn(edge,j-i);
					
					wave.Add(edgeSpawn);
				}
				this.waves.Add(wave);
			}
		}
		
		public bool hasNext(){
			return stage<waves.Count;
		}
		
		public List<EdgeSpawn> getNextWave(){
			return waves[stage++];
		}
		
		public void restart(){
			this.stage=0;
		}
		
	
		
		

		
		
	}
}
