//using UnityEditor;
//using UnityEngine;

//namespace JK.Tweening
//{
//    [CustomEditor (typeof (PunchBehaviour), true)]
//    public class PunchBehaviourEditor : Editor
//    {
//        public override void OnInspectorGUI ()
//        {
//            PunchBehaviour tweenBehaviour = (PunchBehaviour)target;
//            DrawDefaultInspector ();

//            EditorGUILayout.Space ();

//            EditorGUILayout.BeginHorizontal ();

//            if (GUILayout.Button ("Play"))
//                tweenBehaviour.Play ();

//            if (GUILayout.Button ("Stop"))
//                tweenBehaviour.Stop ();

//            EditorGUILayout.EndHorizontal ();
//        }
//    }
//}