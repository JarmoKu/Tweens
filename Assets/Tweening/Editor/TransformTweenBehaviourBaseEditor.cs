using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace JK.Tweening
{
    [CustomEditor (typeof (TransformTweenBehaviourBase), true)]
    public class TransformTweenBehaviourBaseEditor : Editor
    {
        private TweenClass _tweenClass;

        private readonly List<string> _propertiesToHide = new ();
        private readonly string _targetProperty = TransformTweenBehaviourBase.TargetTransformPropertyName;
        private readonly string _startProperty = TransformTweenBehaviourBase.StartPropertyName;
        private readonly string _endProperty = TransformTweenBehaviourBase.EndPropertyName;

        private readonly string[] _loopProperties = new string[2] 
        { 
            TransformTweenBehaviourBase.LoopCountPropertyName, 
            TransformTweenBehaviourBase.LoopDelayPropertyName 
        };

        public override void OnInspectorGUI ()
        {
            var transformTweenBehaviour = (TransformTweenBehaviourBase)target;
            SerializedObject serializedObject = new (transformTweenBehaviour);

            if (transformTweenBehaviour is TweenBehaviour tweenBehaviour)
            {
                TweenBehaviourGUI (transformTweenBehaviour, tweenBehaviour);
            }

            SelectPropertiesToHide ();

            DrawInspector (transformTweenBehaviour);

            _propertiesToHide.Clear ();
        }

        private void DrawInspector (TransformTweenBehaviourBase transformTweenBehaviour)
        {
            EditorGUILayout.BeginHorizontal ();

            if (GUILayout.Button ("Play"))
                transformTweenBehaviour.Play ();

            if (GUILayout.Button ("Stop"))
                transformTweenBehaviour.Stop ();

            EditorGUILayout.EndHorizontal ();

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
        }

        private void SelectPropertiesToHide ()
        {
            _propertiesToHide.Add ("m_Script");

            var tweenType = (TweenType)serializedObject.FindProperty (
                TransformTweenBehaviourBase.TweenTypePropertyName).enumValueIndex;

            if (tweenType.Matches (TweenType.From))
            {
                _propertiesToHide.Add (_endProperty);
            }
            else if (tweenType.Matches (TweenType.To))
            {
                _propertiesToHide.Add (_startProperty);
            }

            var loopType = (LoopType)serializedObject.FindProperty (
                TransformTweenBehaviourBase.LoopTypePropertyName).intValue;

            if (loopType.Matches (LoopType.None))
                _propertiesToHide.AddRange (_loopProperties);

            var targetSelf = serializedObject.FindProperty (
                TransformTweenBehaviourBase.TargetSelfPropertyName).boolValue;

            if (targetSelf)
                _propertiesToHide.Add (_targetProperty);

        }

        private void TweenBehaviourGUI (TransformTweenBehaviourBase transformTweenBehaviour, TweenBehaviour tweenBehaviour)
        {
            SerializedObject serializedObject1 = new (transformTweenBehaviour);
            var tweenClass = (TweenClass)serializedObject1.FindProperty (
                TweenBehaviour.TweenClassPropertyName).enumValueIndex;

            if (!tweenClass.Matches (TweenClass.Jump))
                _propertiesToHide.Add (TweenBehaviour.ArcPeakPropertyName);

            _tweenClass = (TweenClass)EditorGUILayout.EnumPopup ("Tween Class", _tweenClass);
            tweenBehaviour.SetTweenClass (_tweenClass);

            _propertiesToHide.Add (TweenBehaviour.TweenClassPropertyName);

            if (_tweenClass.Matches (TweenClass.PunchPosition) || 
                _tweenClass.Matches (TweenClass.PunchRotation) || 
                _tweenClass.Matches (TweenClass.PunchScale))
            {
                _propertiesToHide.Add (_startProperty);
            }
        }
    }
}