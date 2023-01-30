using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace JK.Tweening
{
    [CustomEditor (typeof (TransformTweenBehaviour), true)]
    public class TransformTweenBehaviourEditor : Editor
    {
        private SerializedProperty _tweenClassSerialized;

        private readonly List<string> _propertiesToHide = new ();

        private readonly string _classProperty = TransformTweenBehaviour.TweenClassPropertyName;
        private readonly string _endProperty = TransformTweenBehaviour.EndPropertyName;
        private readonly string _arcPeakProperty = TransformTweenBehaviour.ArcPeakPropertyName;
        private readonly string _loopTypeProperty = TransformTweenBehaviour.LoopTypePropertyName;
        private readonly string _startProperty = TransformTweenBehaviour.StartPropertyName;
        private readonly string _targetProperty = TransformTweenBehaviour.TargetTransformPropertyName;
        private readonly string _targetSelfProperty = TransformTweenBehaviour.TargetSelfPropertyName;
        private readonly string _typeProperty = TransformTweenBehaviour.TweenTypePropertyName;

        private readonly string[] _loopProperties = new string[2] 
        {
            TransformTweenBehaviour.LoopCountPropertyName,
            TransformTweenBehaviour.LoopDelayPropertyName 
        };

        private void OnEnable ()
        {
            _tweenClassSerialized = serializedObject.FindProperty (_classProperty);
        }

        public override void OnInspectorGUI ()
        {
            var transformTweenBehaviour = (TransformTweenBehaviour)target;

            SelectPropertiesToHide (_tweenClassSerialized);
            DrawInspector (transformTweenBehaviour);

            _propertiesToHide.Clear ();
        }

        private void DrawInspector (TransformTweenBehaviour transformTweenBehaviour)
        {
            serializedObject.Update ();
            EditorGUILayout.PropertyField (_tweenClassSerialized);

            EditorGUILayout.BeginHorizontal ();

            if (GUILayout.Button ("Play"))
                transformTweenBehaviour.Play ();

            if (GUILayout.Button ("Stop"))
                transformTweenBehaviour.Stop ();

            EditorGUILayout.EndHorizontal ();

            if (_propertiesToHide.Count > 0)
            {
                DrawPropertiesExcluding (serializedObject, _propertiesToHide.ToArray ());
                serializedObject.ApplyModifiedProperties ();
            }
            else
            {
                DrawDefaultInspector ();
            }
        }

        private void SelectPropertiesToHide (SerializedProperty tweenClassSerialized)
        {
            _propertiesToHide.Add ("m_Script");
            _propertiesToHide.Add (_classProperty);

            var tweenClass = (TweenClass)tweenClassSerialized.enumValueIndex;
            CheckJump (tweenClass);
            CheckPunch (tweenClass);

            CheckTweenType ();
            CheckLoopProperties ();
            CheckTargetSelf ();
        }

        private void CheckJump (TweenClass tweenClass)
        {
            if (!tweenClass.Matches (TweenClass.Jump))
                _propertiesToHide.Add (_arcPeakProperty);
        }

        private void CheckPunch (TweenClass tweenClass)
        {
            if (IsPunch (tweenClass))
                _propertiesToHide.Add (_startProperty);
        }

        private void CheckTweenType ()
        {
            var tweenType = (TweenType)serializedObject.FindProperty (_typeProperty).enumValueIndex;

            if (tweenType.Matches (TweenType.From))
                _propertiesToHide.Add (_endProperty);
            else if (tweenType.Matches (TweenType.To))
                _propertiesToHide.Add (_startProperty);
        }

        private void CheckLoopProperties ()
        {
            var loopType = (LoopType)serializedObject.FindProperty (_loopTypeProperty).intValue;

            if (loopType.Matches (LoopType.None))
                _propertiesToHide.AddRange (_loopProperties);
        }

        private void CheckTargetSelf ()
        {
            var targetSelf = serializedObject.FindProperty (_targetSelfProperty).boolValue;

            if (targetSelf)
                _propertiesToHide.Add (_targetProperty);
        }

        private bool IsPunch (TweenClass tweenClass)
        {
            return tweenClass.Matches (TweenClass.PunchPosition) ||
                tweenClass.Matches(TweenClass.PunchRotation) ||
                tweenClass.Matches(TweenClass.PunchScale);
        }
    }
}