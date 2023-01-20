//using UnityEditor;
//using UnityEngine;

//namespace JK.Tweening
//{
//    [CustomEditor (typeof (MoveBehaviour), true)]
//    public class MoveBehaviourEditor : Editor
//    {
//        public override void OnInspectorGUI ()
//        {
//            MoveBehaviour tweenBehaviour = (MoveBehaviour)target;
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