using UnityEngine;
using System.Collections;

public class UVtranslate : MonoBehaviour
{

    float x = 0;
    float y = 0;
    public float f_xlerp;
    public float f_ylerp;



	void Start () {

        //renderer.material.mainTextureOffset = new Vector2(-0.5f, 0);
	}
    void Update () {
        if (Mathf.Abs(x) > 1)
        {
            if (x > 1) { x = -1; } else { x = 1; }
        }
        if (Mathf.Abs(y) > 1)
        {
            if (y > 1) { y = -1; } else { y = 1; }
        }
        x += f_xlerp;
        y += f_ylerp;
        renderer.material.mainTextureOffset = new Vector2(x, y);
        renderer.material.mainTextureOffset = new Vector2(x, y);
        //renderer.material.mainTextureScale = new Vector2(1, 1);
    }
	
}
