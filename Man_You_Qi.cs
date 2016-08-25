using UnityEngine;
using System.Collections;
/// <summary>
/// 
/// </summary>
public class Man_You_Qi : MonoBehaviour {
  public  enum ENUM_漫游模式{终止,环路漫游,场路漫游 }
  public bool _显示环路;
  public bool _显示场路;
    public ENUM_漫游模式 enum_漫游模式;
    public Man_You_JD[] myjd_战斗入口结点_sz;

    public bool bool_场路终点;
    public JDC_OB jdc_ob;
    public Man_You_JD myjd_目标漫游结点;
    public float float_环路速度 = 1;
    public float float_场路速度 = 3;
    public float float_位移平滑度;
    public AnimationCurve animc_加速曲线;
    float float_加速值;
    float float_切换距离;

    float float_到目标结点距离;
    float float_前后结点间距;


    byte byte_场路id;

    Man_You_JD myjd_初始漫游结点;
    Man_You_JD myjd_上一个漫游结点;
    Vector3 v3_位移值;
    //public float f;
    //public float f1;
    //public float f2;
    //public float f3;


	// Use this for initialization
	void Start () {
       myjd_上一个漫游结点= myjd_初始漫游结点 = myjd_目标漫游结点;
       v3_位移值 = transform.position;
       transform.eulerAngles = myjd_初始漫游结点.transform.eulerAngles;
	}
	
	// Update is called once per frame
	void Update () 
    {
        Man_You_JD.bool_显示环路 = _显示环路;
        Man_You_JD.bool_显示场路 = _显示场路;


   //  f= jd_角度偏移(f1, f2, f3/10);
        switch (enum_漫游模式)
        {
            case ENUM_漫游模式.终止:
                this.enabled = false;
                break;
            case ENUM_漫游模式.环路漫游:

                v3_位移值 = Vector3.MoveTowards(v3_位移值, myjd_目标漫游结点.transform.position, Time.deltaTime * float_环路速度);
                transform.position = Vector3.Lerp(transform.position, v3_位移值,Time.deltaTime*float_位移平滑度*float_环路速度);
                float_到目标结点距离 = Vector3.Distance(v3_位移值, myjd_目标漫游结点.transform.position);
                if (float_前后结点间距 > 0) 
                transform.eulerAngles =jd_角度偏移(myjd_上一个漫游结点.transform.eulerAngles, myjd_目标漫游结点.transform.eulerAngles, 1f- float_到目标结点距离 / float_前后结点间距);
                qh_切换环路结点();

                break;
            case ENUM_漫游模式.场路漫游:
         //     if(float_加速值<2)  float_加速值 += Time.deltaTime;

                v3_位移值 = Vector3.MoveTowards(v3_位移值, myjd_目标漫游结点.transform.position, Time.deltaTime * float_环路速度);
                transform.position = Vector3.Lerp(transform.position, v3_位移值, Time.deltaTime * float_位移平滑度 * float_环路速度*3);
                float_到目标结点距离 = Vector3.Distance(v3_位移值, myjd_目标漫游结点.transform.position);
                if (float_到目标结点距离 != 0)
                transform.eulerAngles =jd_角度偏移(myjd_上一个漫游结点.transform.eulerAngles, myjd_目标漫游结点.transform.eulerAngles, 1f- float_到目标结点距离 / float_前后结点间距);
                if (float_到目标结点距离 < 0.00001f)//如果接近结点
                {
                    if (myjd_目标漫游结点.myjd_场结点_sz.Length > byte_场路id)
                    {
                        if (myjd_目标漫游结点.bool_场终点)
                        {
                           // jdc_ob.激发("到达场终点");
                           // enabled = false;
                        }
                        else
                        {
                            myjd_上一个漫游结点 = myjd_目标漫游结点;
                            myjd_目标漫游结点 = myjd_目标漫游结点.myjd_场结点_sz[byte_场路id];
                            float_前后结点间距 = Vector3.Distance(myjd_上一个漫游结点.transform.position,myjd_目标漫游结点.transform.position);
                        }
                    }
                    else { qh_切换环路结点(); }
                }
                break;
        }
	}

    void qh_切换环路结点()
    { 
                if (float_到目标结点距离< 0.00001f)//如果接近结点
                {
                    if (myjd_目标漫游结点.myjd_环路结点_sz != null && myjd_目标漫游结点.myjd_环路结点_sz.Length > 0)
                    {
                        myjd_上一个漫游结点 = myjd_目标漫游结点;
                        myjd_目标漫游结点 = myjd_目标漫游结点.myjd_环路结点_sz[Random.Range(0, myjd_目标漫游结点.myjd_环路结点_sz.Length)];//随机目标结点
                    float_前后结点间距 = Vector3.Distance(myjd_上一个漫游结点.transform.position, myjd_目标漫游结点.transform.position);

                    }
                    else { myjd_目标漫游结点 = myjd_初始漫游结点;transform.position= v3_位移值 = myjd_初始漫游结点.transform.position; }
                }    
    }

    public void zd_直达战斗目标点(byte  _场路id)
    {
        print("zd_直达战斗目标点");
        byte_场路id = _场路id;
     myjd_目标漫游结点=myjd_上一个漫游结点 = myjd_战斗入口结点_sz[_场路id];
       
        transform.position = myjd_战斗入口结点_sz[_场路id].transform.position;
        transform.eulerAngles = myjd_战斗入口结点_sz[_场路id].transform.eulerAngles;
        v3_位移值=myjd_战斗入口结点_sz[_场路id].transform.position;

        enum_漫游模式 = ENUM_漫游模式.场路漫游;
    }

    public void qh_切换漫游(ENUM_漫游模式 _漫游模式,byte _场路id)
    {
        float_加速值 = 0;
        byte_场路id = _场路id;
        enum_漫游模式 = _漫游模式;
        enabled = true;
    }


    Vector3 jd_角度偏移(Vector3 _A角度, Vector3 _B角度, float _偏量)
    {
        return new Vector3(jd_角度偏移(_A角度.x, _B角度.x, _偏量), jd_角度偏移(_A角度.y, _B角度.y, _偏量), jd_角度偏移(_A角度.z, _B角度.z, _偏量));
    }

    float jd_角度偏移(float _A角, float _B角, float _偏量)
    {
        if (_偏量 < 0) _偏量 = 0;
        if (_偏量 > 1) _偏量 = 1;
        float a = _A角;
        float b = _B角;
        float c;
        float e = 1;
        if (a > b)
        {
            c = a - b;
            if (c > 180)
            {
                c = (360 - c) * _偏量;
                e = (a + c) % 360;
            }
            else  { e = a - (c * _偏量); }
        }
        else
        {
            c = b - a;
            if (c > 180)
            {
                c = (360 - c) * (1 - _偏量);
                e = (b + c) % 360;
            }
            else { e =b- c *(1- _偏量); }
        }

        return e;

    }
}
