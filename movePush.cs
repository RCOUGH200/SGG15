using UnityEngine;
using System.Collections;

public class movePush : MonoBehaviour {
	public int botID;
	//true is right false is left
	bool direction=false;
	bool up=false;
	bool left=false;
	bool right=false;
	bool push=false;
	bool pushing=false;
	bool pulling=false;
	bool grounded=true;
	bool started;
	bool[] Arr;
	GameObject pushed;
	
	float currTime=0.0f;
	float lastTime=0.0f;
	int masterI=0;
	
	// Use this for initialization
	void Start () 
	{
	}
	// Update is called once per frame
	void Update () 
	{
		
	}
	void FixedUpdate()
	{
		if(started)
		{
			currTime=Time.deltaTime+currTime;
			if (currTime>=1+lastTime)
			{
				lastTime=currTime;
				Master ();
				if(grounded==true&&pushing==false&&push==false&&pulling==false)
				{
					if (left==true&&right==false)
					{
						RaycastHit hit;
						Ray ray = new Ray(transform.position,new Vector3(-1,0,0));
						if (Physics.Raycast(ray, out hit))
						{
							Debug.Log(hit.distance);
							if (hit.collider == null || hit.distance!=0.5f)
							{
								this.gameObject.rigidbody.velocity=new Vector3(-1,0,0); 
								direction=false;
							}
						}
					}
					else if (right==true&&left==false)
					{
						RaycastHit hit;
						Ray ray = new Ray(transform.position,new Vector3(1,0,0));
						if (Physics.Raycast(ray, out hit))
						{
							Debug.Log(hit.distance);
							if (hit.collider == null || hit.distance!=0.5f)
							{
								this.gameObject.rigidbody.velocity=new Vector3(1,0,0);
								direction=true;
							}
						}

					}
				}
				else if(pulling==true)
				{
					pushed.rigidbody.velocity=new Vector3(0,-1,0);
					pushed.transform.position=new Vector3(Mathf.Floor(pushed.transform.position.x)+0.5f,Mathf.Floor(pushed.transform.position.y)+0.5f,pushed.transform.position.z);
					pushing=true;
				}
				else if (pushing==true)
				{
					pushed.rigidbody.velocity=new Vector3(0,0,0);
					pushed.transform.position=new Vector3(Mathf.Floor(pushed.transform.position.x)+0.5f,Mathf.Floor(pushed.transform.position.y)+0.5f,pushed.transform.position.z);
					pushing=false;
				}
				else if(push==true)
				{
					this.gameObject.rigidbody.velocity=new Vector3(0,0,0); 
					//animation point
					RaycastHit hit;

					Vector3 dir=new Vector3();
					if(up==true){dir=new Vector3(0,1,0);}
					else if(direction==false){dir=new Vector3(-1,0,0);}
					else if(direction==true){dir=new Vector3(1,0,0);}

					Ray ray = new Ray(transform.position,dir);
					if (Physics.Raycast(ray, out hit))
						if (hit.collider != null && hit.distance==0.5f)
						{
							hit.rigidbody.velocity=dir;
							pushed=hit.rigidbody.gameObject;
						pushing=true;
						}
					//push=false;
				}
				else if(grounded==false)
				{
					this.gameObject.rigidbody.velocity=new Vector3(0,-1,0); 
					grounded=true;
				}
			}
		}
	}
	
	void Master()
	{
		transform.position=new Vector3(Mathf.Floor(transform.position.x)+0.5f,Mathf.Floor(transform.position.y)+0.5f,transform.position.z);
		this.gameObject.rigidbody.velocity=new Vector3(0,0,0); 

		masterI++;
		left=false;
		right=false;
		push=false;
		pulling=false;

		if(Arr[masterI-1]==true)
		{left=true;}
		if(Arr[masterI+999]==true)
		{right=true;}
		if(Arr[masterI+1999]==true)
		{push=true;}
		if(Arr[masterI+2999]==true&&up==true)
		{pulling=true;}
		if(Arr[masterI+3999]==true)
		{up=!up;}
	}
	
	public void ReciveInfo(bool[] info)
	{
		Arr=info;
		started=true;
	}
}
