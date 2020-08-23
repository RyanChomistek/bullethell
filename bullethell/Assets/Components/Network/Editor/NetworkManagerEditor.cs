using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;

[CustomEditor(typeof(NetworkManager))]
public class NetworkManagerEditor : Editor
{
    private NetworkManager m_target;
    bool showNetObjMap = false;

    void OnEnable()
    {
        Debug.Log("OnEnable is called");
        m_target = (NetworkManager)target;
    }

    public override void OnInspectorGUI()
    {
        //EditorGUILayout.IntField(m_target.);
        // EditorGUILayout.LabelField();
        //foreach (var prop in m_target..GetType().GetProperties())
        //{
        //    EditorGUILayout.LabelField($"{prop.Name}: {prop.GetValue(m_target.m_NetworkObject, null)}");
        //}
        base.OnInspectorGUI();

        showNetObjMap = EditorGUILayout.Foldout(showNetObjMap, "NetObjMap");
        if (showNetObjMap && m_target.NetworkBehaviorMap != null)
            foreach (var kvp in m_target.NetworkBehaviorMap)
            {
                EditorGUILayout.LabelField($"{kvp.Key}: {kvp.Value.GetType()}");
            }

        // EditorGUILayout.ObjectField("Script", target, typeof(NetworkManager), true);
        //MethodInfo onInspectorGUI_Method = base.GetType().GetMethod("OnInspectorGUI", BindingFlags.NonPublic | BindingFlags.Instance);
        //onSceneGUI_Method.Invoke(base, null);
        // base.OnInspectorGUI();
    }
}
