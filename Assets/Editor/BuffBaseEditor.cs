using UnityEngine;
using UnityEditor;

namespace ZomboTerrain
{
    [CustomEditor(typeof(BuffBase))]
    public class DataBaseEditor : Editor
    {
        private BuffBase _buffBase;

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
            }

            if (GUILayout.Button("<="))
                _buffBase.PrevItem();
            if (GUILayout.Button("=>"))
                _buffBase.NextItem();
            if (GUILayout.Button("Remove"))
                _buffBase.RemoveItem();
             

            GUILayout.EndHorizontal();

            GUILayout.Label($"Buffs In Base {_buffBase.BuffSamples.Count}");

            base.OnInspectorGUI();
        }
    }
}
