using UnityEditor;
using UnityEngine;

namespace JK.Tweening
{
    [CustomEditor (typeof (ImageColorBehaviour), true)]
    public class ImageColorBehaviourEditor : Editor
    {
        public override void OnInspectorGUI ()
        {
            ImageColorBehaviour tweenBehaviour = (ImageColorBehaviour)target;
            DrawDefaultInspector ();

            EditorGUILayout.Space ();

            EditorGUILayout.BeginHorizontal ();

            if (GUILayout.Button ("Play"))
                tweenBehaviour.Play ();

            if (GUILayout.Button ("Stop"))
                tweenBehaviour.Stop ();

            EditorGUILayout.EndHorizontal ();
        }
    }
}