using UnityEngine;
using System.Collections;

public class move : MonoBehaviour {

    public GameObject cubb;
	void Start () {
	
	}
    Vector3 relative;
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.forward * Time.deltaTime);

        relative = transform.InverseTransformPoint(cubb.transform.position);
        Debug.Log(relative + "+" + transform.position);

        float angle_y = Mathf.Atan2(relative.x, relative.z) * Mathf.Rad2Deg;

        float angle_x = Mathf.Atan2(relative.y, relative.z) * Mathf.Rad2Deg;

        transform.Rotate(0, angle_y * Time.deltaTime * 5, 0);
        transform.Rotate(0, 0, angle_x * Time.deltaTime * 5);
	}
}

/*angle_x * Time.deltaTime*2*/