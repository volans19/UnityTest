using UnityEngine;
using System.Collections;

public class Man_You_JD : MonoBehaviour {
    
    public static bool bool_显示环路;
    public static bool bool_显示场路;


    public bool bool_场终点;

    public Man_You_JD[] myjd_环路结点_sz;

    public Man_You_JD[] myjd_场结点_sz;//0:1v1 ,1:刀剑 ,2团体

    public bool bool_连接子物体;


	void Start () {


	}

    void Update()
    {

        for ( int i = 0; i < myjd_环路结点_sz.Length; i++)
        {
            if (bool_显示环路 && myjd_环路结点_sz[i])
                Debug.DrawLine(transform.position, myjd_环路结点_sz[i].transform.position, Color.black);
        }
        for (int i = 0; i < myjd_场结点_sz.Length; i++)
        {
            if (bool_显示场路 && myjd_场结点_sz[i])
                Debug.DrawLine(transform.position, myjd_场结点_sz[i].transform.position, Color.cyan);
        }


        //if (bool_连接子物体)
        //{
        //    lj_连接子物体();
        //    bool_连接子物体 = false;
        //}


    }


    void lj_连接子物体()
    { 
        int i = 0;
        foreach (Transform  t in transform)
        {
            if (t.gameObject.GetComponent<Man_You_JD>())
                i++;
        }
        print(i);
        myjd_环路结点_sz = new Man_You_JD[i];
        i = 0;
        foreach (Transform  t in transform)
        {
            if (t.gameObject.GetComponent<Man_You_JD>())
            { myjd_环路结点_sz[i] = t.gameObject.GetComponent<Man_You_JD>(); i++; } 
        }    




    
    }


}
