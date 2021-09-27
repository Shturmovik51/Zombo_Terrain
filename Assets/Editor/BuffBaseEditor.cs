using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BuffBase))]
public class DataBaseEditor : Editor
{
    private BuffBase _buffBase;
    private int _buffsInBase;

    private void Awake()
    {
        _buffBase = (BuffBase)target;
    }

    public override void OnInspectorGUI()
    {
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("New Item"))
        {
            _buffBase.CreateItem();
            _buffsInBase++;
        }

        if (GUILayout.Button("<="))
            _buffBase.PrevItem();
        if (GUILayout.Button("=>"))
            _buffBase.NextItem();
        if (GUILayout.Button("Remove"))
        {
            _buffBase.RemoveItem();
            _buffsInBase--;

            if (_buffsInBase < 0)
                _buffsInBase = 0;
        }

        GUILayout.EndHorizontal();
                
        GUILayout.Label($"Buffs In Base   {_buffsInBase}");

        base.OnInspectorGUI();
    }


}
