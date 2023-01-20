//using UnityEditor;
//using UnityEngine;

//namespace JK.Tweening
//{
//    [CustomEditor (typeof (ScaleBehaviour), true)]
//    public class ScaleBehaviourEditor : Editor
//    {
//        public override void OnInspectorGUI ()
//        {
//            ScaleBehaviour tweenBehaviour = (ScaleBehaviour)target;
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