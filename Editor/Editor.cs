//----------------------------------------
// MIT License
// Copyright(c) 2021 Jonas Boetel
//----------------------------------------
using UnityEditor;
using UnityEngine;

namespace Lumpn.Matomo
{
    public abstract class Editor<T> : Editor where T : Object
    {
        public sealed override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Separator();
            EditorGUILayout.BeginVertical(GUI.skin.box);
            OnInspectorGUI((T)target);
            EditorGUILayout.EndVertical();
        }

        public abstract void OnInspectorGUI(T target);
    }
}
