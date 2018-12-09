using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreClient : MonoBehaviour {
    [System.Serializable]

	public class JsonTransform {
		public Vector3 pos;
        public Vector3 rot;
	}

    private JsonTransform localTransform = new JsonTransform();
    public Vector3 position;
    public Vector3 rotation;

    void Update() {
        //StartCoroutine(Get());
        //position = transform.position;
        //rotation = transform.rotation.eulerAngles;
        // set position 
        //setScore();
        // set rotation
        //setTransform(position, rotation);
        getTransform();
    }


    public Vector3 GetPosition() {
        return localTransform.pos;
    }

    public Vector3 GetRotation() {
        return localTransform.rot;
    }

    public void getTransform() {
        StartCoroutine(Get());
    }

    IEnumerator Get() {
		WWW www;

		string url = "http://internal.mcmentos.com:62802/getTransform";
		www = new WWW(url);

		yield return www;

        if (www.error == "" || www.error == null) {
            Debug.Log("Get succeeded!");
            Debug.Log(www.text);
            JsonTransform t = JsonUtility.FromJson<JsonTransform>(www.text);
            localTransform.pos = t.pos;
            localTransform.rot = t.rot;
            print("networked p" + localTransform.pos);
            print("networked r" + localTransform.rot);
            Debug.Log(localTransform.pos);
        } else {
            Debug.Log(www.error);
        }
    }

    public void setTransform(Vector3 pos, Vector3 rot)
    {
        StartCoroutine(Set(pos, rot));
    }

    IEnumerator Set(Vector3 pos, Vector3 rot) {
		WWW www;
		Dictionary<string, string> postHeader = new Dictionary<string, string>();
		postHeader.Add("Content-Type", "application/json");

        JsonTransform t = new JsonTransform();
        t.pos = pos;
        t.rot = rot;

		string transformStr = JsonUtility.ToJson(t);

		var formData = System.Text.Encoding.UTF8.GetBytes(transformStr);
		string url = "http://internal.mcmentos.com:62802/setTransform";

		www = new WWW(url, formData, postHeader);

		yield return www;

		if (www.error == "" || www.error == null) {
			Debug.Log("Set succeeded!");
			Debug.Log(www.text);
		} else {
            Debug.Log(www.error);
        }
	}

    //public void addScore(int val)
    //{
    //    StartCoroutine(Add(val));
    //}

    //IEnumerator Add(int val)
    //{
    //    WWW www;
    //    Dictionary<string, string> postHeader = new Dictionary<string, string>();
    //    postHeader.Add("Content-Type", "application/json");

    //    Score score = new Score();
    //    score.value = val;

    //    string scoreStr = JsonUtility.ToJson(score);

    //    var formData = System.Text.Encoding.UTF8.GetBytes(scoreStr);
    //    string url = "http://internal.mcmentos.com:62802/addScore";

    //    www = new WWW(url, formData, postHeader);

    //    yield return www;

    //    if (www.error != "")
    //    {
    //        Debug.Log(www.error);
    //    }
    //    else
    //    {
    //        Debug.Log("Add succeeded!");
    //        Debug.Log(www.text);
    //    }
    //}
}
