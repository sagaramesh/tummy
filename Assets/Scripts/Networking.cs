using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Networking : MonoBehaviour {
    [System.Serializable]
	public class Score {
		public float value1 = 0;
        public float value2 = 0;
        public float value3 = 0;
        public float value4 = 0;
        public float value5 = 0;
        public float value6 = 0;
        public float value7 = 0;
        public float value8 = 0;
        public float value9 = 0;
        public float value10 = 0;
    }

    private Score localScore = new Score();
    public GameObject SlothHead;
    void Update() {
        //StartCoroutine(Get());
    }


    public float GetScore1() {
        return localScore.value1;
    }
    public float GetScore2()
    {
        return localScore.value2;
    }
    public float GetScore3()
    {
        return localScore.value3;
    }
    public float GetScore4()
    {
        return localScore.value4;
    }
    public float GetScore5()
    {
        return localScore.value5;
    }
    public float GetScore6()
    {
        return localScore.value6;
    }
    public float GetScore7()
    {
        return localScore.value7;
    }
    public float GetScore8()
    {
        return localScore.value8;
    }
    public float GetScore9()
    {
        return localScore.value9;
    }
    public float GetScore10()
    {
        return localScore.value10;
    }

    IEnumerator Get() {
		WWW www;

		string url = "http://floaternal.mcmentos.com:62802/getScore";
		www = new WWW(url);

		yield return www;

		if (www.error != null) {
			Debug.Log(www.error);
		} else {
			Debug.Log("Get succeeded!");
			Debug.Log(www.text);
			Score score = JsonUtility.FromJson<Score>(www.text);
            localScore.value1 = score.value1;
            Debug.Log(score.value1);
		}
	}

    public void setScore(float val)
    {
        StartCoroutine(Set(val));
    }

    IEnumerator Set(float val) {
		WWW www;
		Dictionary<string, string> postHeader = new Dictionary<string, string>();
		postHeader.Add("Content-Type", "application/json");

		Score score = new Score();
		score.value1 = val;

		string scoreStr = JsonUtility.ToJson(score);

		var formData = System.Text.Encoding.UTF8.GetBytes(scoreStr);
		string url = "http://floaternal.mcmentos.com:62802/setScore";

		www = new WWW(url, formData, postHeader);

		yield return www;

		if (www.error != "") {
			Debug.Log(www.error);
		} else {
			Debug.Log("Set succeeded!");
			Debug.Log(www.text);
		}
	}

    public void addScore(float val)
    {
        StartCoroutine(Add(val));
    }

    IEnumerator Add(float val)
    {
        WWW www;
        Dictionary<string, string> postHeader = new Dictionary<string, string>();
        postHeader.Add("Content-Type", "application/json");

        Score score = new Score();
        score.value1 = val;

        string scoreStr = JsonUtility.ToJson(score);

        var formData = System.Text.Encoding.UTF8.GetBytes(scoreStr);
        string url = "http://floaternal.mcmentos.com:62802/addScore";

        www = new WWW(url, formData, postHeader);

        yield return www;

        if (www.error != "")
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Add succeeded!");
            Debug.Log(www.text);
        }
    }
}
