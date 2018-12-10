using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformTarget : MonoBehaviour {

    private GameObject target;
    public GameObject sceneManager;

    [SerializeField]
    private float targetXRot;
    [SerializeField]
    private float targetZRot;

	// Use this for initialization
	void Start () {
        target = this.gameObject;
	}
	
	// Update is called once per frame
	void Update () {

        target.transform.localPosition = sceneManager.GetComponent<ScoreClient>().GetPosition();
        //if (target.transform.localEulerAngles.y >= -28.0f && target.transform.localEulerAngles.y <= 28.0f)
        target.transform.localEulerAngles = new Vector3 (targetXRot, sceneManager.GetComponent<ScoreClient>().GetRotation().y, targetZRot);
        //target.transform.localEulerAngles = sceneManager.GetComponent<ScoreClient>().GetRotation();
	}
}
