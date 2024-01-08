//----------------------------------------
// MIT License
// Copyright(c) 2021 Jonas Boetel
//----------------------------------------
using UnityEditor;
using UnityEngine;

namespace Lumpn.Matomo
{
    [CustomEditor(typeof(MatomoConfig))]
    public sealed class MatomoConfigEditor : Editor<MatomoConfig>
    {
        private const string helpText = "Please see Matomo's documentation for details on how to create and manage projects (called `websites`) to track.";
        private const string helpUrl = "https://matomo.org/faq/how-to/create-and-manage-websites/";

        public sealed override void OnInspectorGUI(MatomoConfig data)
        {
            EditorGUILayout.HelpBox(helpText, MessageType.Info);

            if (GUILayout.Button("Open documentation"))
            {
                Application.OpenURL(helpUrl);
            }
        }
    }
}
