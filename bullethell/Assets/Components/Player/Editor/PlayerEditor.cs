using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerController))]
public class PlayerEditor : NetworkBehaviorEditor
{
    //private PlayerController m_target;
    //void OnEnable()
    //{
    //    m_target = (PlayerController)target;
    //}

    //public override void OnInspectorGUI()
    //{
    //    foreach (var prop in m_target.m_Player.GetType().GetProperties())
    //    {
    //        EditorGUILayout.LabelField($"{prop.Name}: {prop.GetValue(m_target.m_Player, null)}");
    //    }

    //    foreach (var prop in typeof(IPlayer).GetProperties())
    //    {
    //        EditorGUILayout.LabelField($"{prop.Name}: {prop.GetValue(m_target.m_Player, null)}");
    //    }
    //}
}
