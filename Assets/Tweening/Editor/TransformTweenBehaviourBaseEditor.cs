using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace JK.Tweening
{
    [CustomEditor (typeof (TransformTweenBehaviourBase), true)]
    public class TransformTweenBehaviourBaseEditor : Editor
    {
        private readonly List<string> _propertiesToHide = new ();
        private readonly string _targetProperty = TransformTweenBehaviourBase.TargetTransformPropertyName;

        private readonly string[] _loopProperties = new string[2] 
        { 
            TransformTweenBehaviourBase.LoopTypePropertyName, 
            TransformTweenBehaviourBase.LoopDelayPropertyName 
        };

        public override void OnInspectorGUI ()
        {
            var tweenBehaviour = (TransformTweenBehaviourBase)target;
            SerializedObject serializedObject = new (tweenBehaviour);

            var loopCount = serializedObject.FindProperty (TransformTweenBehaviourBase.LoopCountPropertyName).intValue;
            var targetSelf = serializedObject.FindProperty (TransformTweenBehaviourBase.TargetSelfPropertyName).boolValue;

            if (loopCount == 0)
                _propertiesToHide.AddRange (_loopProperties);

            if (targetSelf)
                _propertiesToHide.Add (_targetProperty);

            if (_propertiesToHide.Count > 0)
            {
                serializedObject.Update ();
                DrawPropertiesExcluding (serializedObject, _propertiesToHide.ToArray ());
                serializedObject.ApplyModifiedProperties ();
            }
            else
            {
                DrawDefaultInspector ();
            }

            _propertiesToHide.Clear ();

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