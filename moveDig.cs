using UnityEngine;
using System.Collections;

public class moveDig : MonoBehaviour {
		public int botID;
		//true is right false is left
		bool direction=false;
		bool up=false;
		bool left=false;
		bool right=false;
		bool drill=false;
		bool drilling=false;
		bool grounded=true;
		bool started;
		bool[] Arr;
		GameObject drilled;
		
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
					if(grounded==true&&drilling==false&&drill==false)
					{
						if (left==true&&right==false)
						{
							RaycastHit hit;
							Ray ray = new Ray(transform.position,new Vector3(-1,0,0));
							Physics.Raycast(ray, out hit,2);
							if (hit.collider == null || hit.distance!=0.5f)
							{
								this.gameObject.rigidbody.velocity=new Vector3(-1,0,0); 
								direction=false;
							}
						}
						else if (right==true&&left==false)
						{
							RaycastHit hit;
							Ray ray = new Ray(transform.position,new Vector3(1,0,0));
							Physics.Raycast(ray, out hit,2);
							if (hit.collider == null || hit.distance!=0.5f)
							{
								this.gameObject.rigidbody.velocity=new Vector3(1,0,0);
								direction=true;
							}

							
						}
					}
					else if(drilling==true)
					{
						Destroy(drilled);	
						drilling=false;
					}
					else if(drill==true)
					{
						this.gameObject.rigidbody.velocity=new Vector3(0,0,0); 
						//animation point
						RaycastHit hit;
						
						Vector3 dir=new Vector3();
						if(up==true){dir=new Vector3(0,-1,0);}
						else if(direction==false){dir=new Vector3(-1,0,0);}
						else if(direction==true){dir=new Vector3(1,0,0);}
						
						Ray ray = new Ray(transform.position,dir);
						if (Physics.Raycast(ray, out hit))
							if (hit.collider != null && hit.distance==0.5f)
						{
							drilled=hit.collider.gameObject;
							drilling=true;
						}
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
			drill=false;
			
			if(Arr[masterI-1]==true)
			{left=true;}
			if(Arr[masterI+999]==true)
			{right=true;}
			if(Arr[masterI+1999]==true)
			{drill=true;}
			if(Arr[masterI+2999]==true)
			{up=!up;}
		}
		
		public void ReciveInfo(bool[] info)
		{
			Arr=info;
			started=true;
		}
	}

