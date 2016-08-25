using UnityEngine;
using System;
using System.Collections;
[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter),typeof(MeshRenderer))]
public class Star : MonoBehaviour {


    //public Vector3 point = Vector3.up;  //(0,1,0)
    //public int numberOfPoints = 10;
    [System.Serializable]
    public class Point
    {
        public Color color;
        public Vector3 offset;
    }

    public Point[] points;
    public int frequency = 1;

    private Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;
    private Color[] colors;
    public Color centerColor;

    void OnEnable()
    {
        UpdateStar();
    }

    public void UpdateStar()
    {
        if (mesh == null)
        {
            GetComponent<MeshFilter>().mesh = mesh = new Mesh();
            mesh.name = "Star Mesh";
            mesh.hideFlags = HideFlags.HideAndDontSave;//设置HideFlags来阻止Unity保存Mesh
        }
        if (frequency < 1)
        {
            frequency = 1;
        }
        if (points == null || points.Length == 0)
        {
            points = new Point[] { new Point() };
        }

        int numberOfPoints = points.Length * frequency;

        vertices = new Vector3[numberOfPoints + 1];
        colors = new Color[numberOfPoints + 1];
        triangles = new int[numberOfPoints * 3];
        float angle = -360f / numberOfPoints;
        colors[0] = centerColor;

        for (int iF = 0, v = 1, t = 1; iF < frequency; iF++)
        {
            for (int iP = 0; iP < points.Length; iP += 1, v += 1, t += 3)
            {
                vertices[v] = Quaternion.Euler(0f, 0f, angle * (v - 1)) * points[iP].offset;
                triangles[t] = v;
                colors[v] = points[iP].color;
                triangles[t + 1] = v + 1;
            }
        }
        triangles[triangles.Length - 1] = 1;

        mesh.vertices = vertices;
        mesh.colors = colors;
        mesh.triangles = triangles;
    }

    void OnDisable()//清理MeshFilter来阻止它发出缺少Mesh的警告
    {
        if (Application.isEditor)
        {
            GetComponent<MeshFilter>().mesh = null;
            DestroyImmediate(mesh);
        }
    }

    //void Reset()
    //{
    //    UpdateStar();
    //}
}
