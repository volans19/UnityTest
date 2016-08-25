using UnityEngine;
using System.Collections;

public class count : MonoBehaviour {

	// Use this for initialization
	void Start () {

        for (int i = 0; i < 5;i++ ) {
            Debug.Log("执行前");
            StartCoroutine(wait());
            Debug.Log("执行后");
        }
	}
    int i = 0;
	// Update is called once per frame
	void Update () {
        
	}


    IEnumerator wait(){
    yield return new WaitForSeconds(1f);
    Debug.Log(i);
        i++;
    }
}
