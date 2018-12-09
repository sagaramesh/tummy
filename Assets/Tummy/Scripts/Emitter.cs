using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emitter : MonoBehaviour {


	//this index separates the objects in the array to two groups:
	//every obj before the index will be emitted based on BPM
	//every obj after the index will be randomly emitted

	public int obstacleStartIndex= 2;



	//change this to control beats per minute
	public float BPM =120f;
	private float bpmTimer=0;
	private float bpSecond;

	//these two have the same purpose, just add them together;
	public float songOffset=3f;
	public float delay = 1f;


	private float offsetTimer = 0;
	public GameObject[] objs;
	//public GameObject obj2;



	public float emissionSpeed= 2f;

	public float emitX_Range= 5f;
	public float emitY_Range= 5f;

	public float emitObstacleFrequency = 1f;

	public GameObject targetOBJ;

    bool stopEmitting = false;


	// Use this for initialization
	void Start () {
		emitOffBeat ();
	}
	
	// Update is called once per frame
	void Update () {
		//update current beats per second
		bpSecond = BPM / 60f;
        if(!stopEmitting)
        {
            if (offsetMet())
            {
                emitOnBeat();
            }
        }
        else
        {
            CancelInvoke();
        }
	}



	void emitOnBeat(){
		if (internalTimer()) {

			//random select
			int objIndex = Random.Range (0, obstacleStartIndex);

		
			//Quaternion randRot = Random.rotation;
			GameObject objNew =Instantiate (objs[objIndex], createRandomPos(), Random.rotation);
			Rigidbody rb = objNew.GetComponent<Rigidbody> ();


			rb.velocity = new Vector3 (0, 0, -1f*emissionSpeed);

		}
	}

	void emitOffBeat(){

		if (offsetMet ()) {
			int objIndex = Random.Range (obstacleStartIndex, objs.Length);
			GameObject objNew = Instantiate (objs [objIndex], createRandomPos (), Random.rotation);


			Rigidbody rb = objNew.GetComponent<Rigidbody> ();
			var heading = targetOBJ.transform.position - objNew.transform.position;
			var distance = heading.magnitude;
			var direction = heading / distance;

			rb.velocity = direction * emissionSpeed;
		}

        //Invoke ("emitOffBeat", Random.Range (0, bpSecond/emitObstacleFrequency));
        Invoke ("emitOffBeat", Random.Range (1, 5));
    }

    //calculate a randomized position based on the range
    Vector3 createRandomPos(){
		Vector3 newPos = transform.position;
		float curRange = Random.Range (0, emitY_Range);
		newPos.x = newPos.x - emitX_Range/2 + curRange;
		newPos.y = newPos.y - emitY_Range / 2 + curRange;
		newPos.z -= 5;
		return newPos;
	}


	bool internalTimer(){
		bpmTimer += Time.deltaTime;
		if (bpmTimer >= 1/ bpSecond) {
			bpmTimer = 0;
			return true;
		}
		return false;
	}

	bool offsetMet(){
		offsetTimer += Time.deltaTime;
		if (offsetTimer >= songOffset+delay) {

			return true;
		} 
		//Debug.Log ("offset is not met!!");
		return false;
	}

    public void Stop()
    {
        stopEmitting = true;
        print("stop");
    }
}
