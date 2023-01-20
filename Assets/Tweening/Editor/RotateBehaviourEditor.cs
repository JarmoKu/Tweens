//using UnityEditor;
//using UnityEngine;

//namespace JK.Tweening
//{
//    [CustomEditor (typeof (RotateBehaviour))]
//    public class RotateBehaviourEditor : Editor
//    {
//        public override void OnInspectorGUI ()
//        {
//            var tweenBehaviour = (RotateBehaviour)target;
//            SerializedObject serializedObject = new (tweenBehaviour);
//            var loopCount = serializedObject.FindProperty ("m_loopCount").intValue;

//            if (loopCount == 0)
//            {
//                serializedObject.Update ();
//                DrawPropertiesExcluding (serializedObject, "m_loopType", "m_loopDelay");

//                serializedObject.ApplyModifiedProperties ();
//            }
//            else
//            {
//                DrawDefaultInspector ();
//            }

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