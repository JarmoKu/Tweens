﻿using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace JK.Tweening
{
    [CustomEditor (typeof (ImageTweenBehaviour), true)]
    public class ImageColorBehaviourEditor : Editor
    {
        private readonly List<string> _propertiesToHide = new ();
        private readonly string _targetProperty = ImageTweenBehaviour.TargetImagePropertyName;
        private readonly string _startProperty = ImageTweenBehaviour.StartPropertyName;
        private readonly string _endProperty = ImageTweenBehaviour.EndPropertyName;
        private readonly string _gradientProperty = ImageTweenBehaviour.GradientPropertyName;

        private readonly string[] _loopProperties = new string[2]
        {
            ImageTweenBehaviour.LoopCountPropertyName,
            ImageTweenBehaviour.LoopDelayPropertyName
        };

        public override void OnInspectorGUI ()
        {
            ImageTweenBehaviour tweenBehaviour = (ImageTweenBehaviour)target;
            SerializedObject serializedObject = new (tweenBehaviour);

            var tweenType = (ImageTweenType)serializedObject.FindProperty (
                ImageTweenBehaviour.TweenTypePropertyName).enumValueIndex;

            SelectPropertiesToHide (tweenType);

            DrawInspector (tweenBehaviour);

            _propertiesToHide.Clear ();
        }

        private void DrawInspector (ImageTweenBehaviour tweenBehaviour)
        {
            EditorGUILayout.BeginHorizontal ();

            if (GUILayout.Button ("Play"))
                tweenBehaviour.Play ();

            if (GUILayout.Button ("Stop"))
                tweenBehaviour.Stop ();

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
        }

        private void SelectPropertiesToHide (ImageTweenType tweenType)
        {
            switch (tweenType)
            {
                case ImageTweenType.FromTo:
                    _propertiesToHide.Add (_gradientProperty);
                    break;
                case ImageTweenType.From:
                    _propertiesToHide.Add (_endProperty);
                    _propertiesToHide.Add (_gradientProperty);
                    break;
                case ImageTweenType.To:
                    _propertiesToHide.Add (_startProperty);
                    _propertiesToHide.Add (_gradientProperty);
                    break;
                case ImageTweenType.Gradient:
                    _propertiesToHide.Add (_startProperty);
                    _propertiesToHide.Add (_endProperty);
                    break;
            }

            var loopType = (LoopType)serializedObject.FindProperty (
                ImageTweenBehaviour.LoopTypePropertyName).intValue;

            var targetSelf = serializedObject.FindProperty (
                ImageTweenBehaviour.TargetSelfPropertyName).boolValue;

            if (loopType.Matches (LoopType.None))
                _propertiesToHide.AddRange (_loopProperties);

            if (targetSelf)
                _propertiesToHide.Add (_targetProperty);
        }
    }
}