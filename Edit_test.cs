using UnityEngine;
using UnityEditor;
[CanEditMultipleObjects]//支持选中多对象编辑
[CustomEditor(typeof(Star))] //让指定的test脚本，可自定义他的各种inspector行为
public class Edit_test : Editor {

    private static GUIContent pointContent = GUIContent.none,
                              insertContent = new GUIContent("+", "duplicate this point"),
                              deleteContent = new GUIContent("-", "delete this point");
    private static GUILayoutOption colorWidth = GUILayout.MaxWidth(50f),
                                   buttonWidth = GUILayout.MaxWidth(20f);

    private SerializedObject star;
    private SerializedProperty
        points,
        frequency,
        centerColor;

    private static Vector3 pointSnap = Vector3.one * 0.05f;

    void OnEnable()
    {
        star = new SerializedObject(targets);//targets编辑多对象，target编辑单个对象
        points = star.FindProperty("points");
        frequency = star.FindProperty("frequency");
        centerColor = star.FindProperty("centerColor");
    }


    public override void OnInspectorGUI()
    {
        //DrawDefaultInspector();//绘制默认面板
        //test t = (test)target;
        //t.test_1 = EditorGUILayout.IntField("ts值", t.test_1);
        star.Update();

        //EditorGUILayout.PropertyField(points, true);
        GUILayout.Label("Points");

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button(insertContent, EditorStyles.miniButtonLeft, buttonWidth))
        {
            if (points.arraySize == 0)
            {
                points.InsertArrayElementAtIndex(0);
            }
            points.InsertArrayElementAtIndex(points.arraySize - 1);
        }
        if (GUILayout.Button(deleteContent, EditorStyles.miniButtonRight, buttonWidth) && points.arraySize != 1)
        {
            points.DeleteArrayElementAtIndex(points.arraySize - 1);
        }
        EditorGUILayout.EndHorizontal();


        for (int i = 0; i < points.arraySize; i++)
        {

            SerializedProperty
                point = points.GetArrayElementAtIndex(i),
                offset = point.FindPropertyRelative("offset");
            if (offset == null)
            {
                break;
            }

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(offset, pointContent);
            EditorGUILayout.PropertyField(point.FindPropertyRelative("color"), pointContent, colorWidth);
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.PropertyField(frequency);
        EditorGUILayout.PropertyField(centerColor);

        //star.ApplyModifiedProperties();
        if (star.ApplyModifiedProperties()) //||
            //(Event.current.type == EventType.ValidateCommand &&
            //Event.current.commandName == "UndoRedoPerformed"))
        {
            foreach(Star s in targets){//多对象选中编辑
				if(PrefabUtility.GetPrefabType(s) != PrefabType.Prefab){
					s.UpdateStar();
				}
			}
        }
    }


    void OnSceneGUI()//在场景中直接编辑
    {
        Star star = (Star)target;
        Transform starTransform = star.transform;
        Undo.SetSnapshotTarget(star, "Move Star Point");

        float angle = -360f / (star.frequency * star.points.Length);
        for (int i = 0; i < star.points.Length; i++)
        {
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle * i);
            Vector3 oldPoint = starTransform.TransformPoint(rotation * star.points[i].offset),
                    newPoint = Handles.FreeMoveHandle(oldPoint, Quaternion.identity, 0.04f, pointSnap, Handles.DotCap);
            if (oldPoint != newPoint)
            {
                star.points[i].offset = Quaternion.Inverse(rotation) * starTransform.InverseTransformPoint(newPoint);
                star.UpdateStar();
            }
        }
    }

}


