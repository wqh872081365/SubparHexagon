using UnityEngine;
using System.Collections;

public class warper : MonoBehaviour {
	MeshFilter meshFilter;
	Mesh mesh;
	Vector3[] verts;

	private Transform center;
	private bool isMoving = false;
	public float spd;
	public float widthPerDist = 0.86602540378f;
	public float rowWidth =0.5f;
	public float rows =1f;
	public float dist;
	//5.4
	public float coefficientOfDavid = 5.2f;


	// Use this for initialization
	void Start () {
		BoxCollider bc = this.GetComponent<BoxCollider>();
		Vector3 cntr = new Vector3(bc.center.x,bc.center.y,bc.center.z-rowWidth/2f);
		bc.center=cntr;
		GameObject hexagon = GameObject.FindGameObjectWithTag ("Hex");
		this.center = hexagon.GetComponent <Transform>();
		this.transform.LookAt(center);
		this.rigidbody.velocity=Vector3.zero;


	}

	public void startMoving(){
		this.isMoving=true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//check if the moving flag has indicated to go
		if(this.isMoving){
			this.rigidbody.velocity = transform.forward * spd;
			this.isMoving = false;
		}

		
	}

	// Update is called once per frame
	void Update () {
		MeshFilter filter = gameObject.GetComponent< MeshFilter >();
		Mesh mesh = filter.mesh;
		mesh.Clear();

		float width =2f;
		float height =this.rowWidth;


		//fudge ever s'slightly
		float fudgeRows = rows*1.08f;


		height = height*fudgeRows;

		dist = (this.transform.position-Vector3.zero).magnitude;

		if(dist<0.866025f){
			this.rigidbody.velocity=Vector3.zero;
			rows-=this.spd*Time.deltaTime/rowWidth;
			if(rows<0)
				Destroy(this.gameObject);
		}



		float length =dist*coefficientOfDavid*widthPerDist;


		BoxCollider bc = this.GetComponent<BoxCollider>();
		Vector3 newSize = new Vector3(length,10,rowWidth);
		
		bc.size=newSize;


		float angledWidth = Mathf.Sin(Mathf.PI/6)*height;
		
		#region Vertices
		//front bottom right
		Vector3 p0 = new Vector3( -length * .5f,	-width * .5f,0.0f);
		//front bottom left 
		Vector3 p1 = new Vector3( length * .5f, 	-width * .5f, 0.0f);

			
		//front top right
		Vector3 p4 = new Vector3( -length * .5f,	width * .5f,  0.0f);

		//front top left
		Vector3 p5 = new Vector3( length * .5f, 	width * .5f,  0.0f);

		//set rear length
		length = (dist+rowWidth*fudgeRows)*widthPerDist;

		//fudge it ever s'slightly
		length = length*1.01f;


		//back ones
		Vector3 p2 = new Vector3( (length * .5f), 	-width * .5f, -height);
		Vector3 p3 = new Vector3( (-length * .5f),	-width * .5f, -height);
		Vector3 p6 = new Vector3( (length * .5f), 	width * .5f,  -height);
		Vector3 p7 = new Vector3( (-length * .5f),	width * .5f,  -height);

		Vector3[] vertices = new Vector3[]
		{
			// Bottom
			p0, p1, p2, p3,
			
			// Left
			p7, p4, p0, p3,
			
			// Front
			p4, p5, p1, p0,
			
			// Back
			p6, p7, p3, p2,
			
			// Right
			p5, p6, p2, p1,
			
			// Top
			p7, p6, p5, p4
		};
		#endregion
		
		#region Normales
		Vector3 up 	= Vector3.up;
		Vector3 down 	= Vector3.down;
		Vector3 front 	= Vector3.forward;
		Vector3 back 	= Vector3.back;
		Vector3 left 	= Vector3.left;
		Vector3 right 	= Vector3.right;
		
		Vector3[] normales = new Vector3[]
		{
			// Bottom
			down, down, down, down,
			
			// Left
			left, left, left, left,
			
			// Front
			front, front, front, front,
			
			// Back
			back, back, back, back,
			
			// Right
			right, right, right, right,
			
			// Top
			up, up, up, up
		};
		#endregion	
		
		#region UVs
		Vector2 _00 = new Vector2( 0f, 0f );
		Vector2 _10 = new Vector2( 1f, 0f );
		Vector2 _01 = new Vector2( 0f, 1f );
		Vector2 _11 = new Vector2( 1f, 1f );
		
		Vector2[] uvs = new Vector2[]
		{
			// Bottom
			_11, _01, _00, _10,
			
			// Left
			_11, _01, _00, _10,
			
			// Front
			_11, _01, _00, _10,
			
			// Back
			_11, _01, _00, _10,
			
			// Right
			_11, _01, _00, _10,
			
			// Top
			_11, _01, _00, _10,
		};
		#endregion
		
		#region Triangles
		int[] triangles = new int[]
		{
			// Bottom
			3, 1, 0,
			3, 2, 1,			
			
			// Left
			3 + 4 * 1, 1 + 4 * 1, 0 + 4 * 1,
			3 + 4 * 1, 2 + 4 * 1, 1 + 4 * 1,
			
			// Front
			3 + 4 * 2, 1 + 4 * 2, 0 + 4 * 2,
			3 + 4 * 2, 2 + 4 * 2, 1 + 4 * 2,
			
			// Back
			3 + 4 * 3, 1 + 4 * 3, 0 + 4 * 3,
			3 + 4 * 3, 2 + 4 * 3, 1 + 4 * 3,
			
			// Right
			3 + 4 * 4, 1 + 4 * 4, 0 + 4 * 4,
			3 + 4 * 4, 2 + 4 * 4, 1 + 4 * 4,
			
			// Top
			3 + 4 * 5, 1 + 4 * 5, 0 + 4 * 5,
			3 + 4 * 5, 2 + 4 * 5, 1 + 4 * 5,
			
		};
		#endregion
		
		mesh.vertices = vertices;
		mesh.normals = normales;
		mesh.uv = uvs;
		mesh.triangles = triangles;
		
		mesh.RecalculateBounds();
		mesh.Optimize();
		
		this.mesh=mesh;



//		MeshCollider mc = this.GetComponent<MeshCollider>();
//		mc.sharedMesh = mesh;

		//bc.size.x = width;
	}

}
