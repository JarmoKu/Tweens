//using UnityEditor;
//using UnityEngine;

//namespace JK.Tweening
//{
//    [CustomEditor (typeof (JumpBehaviour), true)]
//    public class JumpBehaviourEditor : Editor
//    {
//        public override void OnInspectorGUI ()
//        {
//            JumpBehaviour tweenBehaviour = (JumpBehaviour)target;
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