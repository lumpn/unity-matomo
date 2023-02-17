//----------------------------------------
// MIT License
// Copyright(c) 2021 Jonas Boetel
//----------------------------------------
using UnityEditor;
using UnityEngine;

namespace Lumpn.Matomo.Samples
{
    [CustomEditor(typeof(MatomoTester))]
    public sealed class MatomoTesterEditor : Editor<MatomoTester>
    {
        private const string helpText = "Please see Matomo's Tracking HTTP API documentation for details on query parameters.";
        private const string helpUrl = "https://developer.matomo.org/api-reference/tracking-api";

        public sealed override void OnInspectorGUI(MatomoTester tester)
        {
            if (GUILayout.Button("Send"))
            {
                tester.StartCoroutine(tester.Run());
            }
            EditorGUILayout.Separator();

            EditorGUILayout.HelpBox(helpText, MessageType.Info, true);
            if (GUILayout.Button("Open documentation"))
            {
                Application.OpenURL(helpUrl);
            }
        }
    }
}
