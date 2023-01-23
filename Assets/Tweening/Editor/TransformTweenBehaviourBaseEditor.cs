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
                //EditorGUILayout.Space ();
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

            EditorGUILayout.Space ();

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

            /*EditorGUILayout.Space ();

            EditorGUILayout.BeginHorizontal ();

            if (GUILayout.Button ("Play"))
                transformTweenBehaviour.Play ();

            if (GUILayout.Button ("Stop"))
                transformTweenBehaviour.Stop ();

            EditorGUILayout.EndHorizontal ();*/
        }

        private void SelectPropertiesToHide ()
        {
            var tweenType = (TweenType)serializedObject.FindProperty (
                TransformTweenBehaviourBase.TweenTypePropertyName).enumValueIndex;

            if (tweenType.Equals (TweenType.From))
            {
                _propertiesToHide.Add (_endProperty);
            }
            else if (tweenType.Equals (TweenType.To))
            {
                _propertiesToHide.Add (_startProperty);
            }

            var loopType = (LoopType)serializedObject.FindProperty (
                TransformTweenBehaviourBase.LoopTypePropertyName).intValue;

            if (loopType.Equals (LoopType.None))
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

            if (!tweenClass.Equals (TweenClass.Jump))
                _propertiesToHide.Add (TweenBehaviour.ArcPeakPropertyName);

            _tweenClass = (TweenClass)EditorGUILayout.EnumPopup ("Tween Class", _tweenClass);//
            tweenBehaviour.SetTweenClass (_tweenClass);

            _propertiesToHide.Add (TweenBehaviour.TweenClassPropertyName);

            if (_tweenClass.Equals (TweenClass.PunchPosition) || 
                _tweenClass.Equals (TweenClass.PunchRotation) || 
                _tweenClass.Equals (TweenClass.PunchScale))
            {
                _propertiesToHide.Add (_startProperty);
            }
        }
    }
}