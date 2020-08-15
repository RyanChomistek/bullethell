using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;

[CustomEditor(typeof(NetworkBehavior))]
public class NetworkBehaviorEditor : Editor
{
    private NetworkBehavior m_target;
    void OnEnable()
    {
        Debug.Log("OnEnable is called");
        m_target = (NetworkBehavior)target;
    }

    public override void OnInspectorGUI()
    {
        //foreach (var prop in m_target.m_NetworkObject.GetType().GetProperties())
        //{
        //    object value = prop.GetValue(m_target.m_NetworkObject, null);
        //    EditorGUILayout.LabelField($"{prop.Name}: {value}");

        //    foreach (var prop in value.GetType().GetProperties())
        //    {
        //        EditorGUILayout.LabelField($"{prop.Name}: {prop.GetValue(m_target.m_NetworkObject, null)}");
        //    }
        //}

        ShowProps(m_target.m_NetworkObject, 0, 2);
    }

    public void ShowProps(object obj, int currDepth = 0, int maxDepth = 2)
    {
        Debug.Log($"{currDepth}/{maxDepth} {obj.GetType()}");
        if(maxDepth <= currDepth || obj == null)
        {
            return;
        }

        currDepth++;

        foreach (var prop in obj.GetType().GetProperties())
        {
            object value = prop.GetValue(obj, null);
            EditorGUILayout.LabelField($"{prop.Name}: {value}");

            ShowProps(value, currDepth, maxDepth);
        }
    }
}
