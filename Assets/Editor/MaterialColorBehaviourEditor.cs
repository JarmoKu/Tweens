using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace JK.Tweening
{
    [CustomEditor (typeof (MaterialColorBehaviour))]
    public class MaterialColorBehaviourEditor : Editor
    {
        private readonly List<string> _propertiesToHide = new ();
        private readonly string _startProperty = MaterialColorBehaviour.StartPropertyName;
        private readonly string _endProperty = MaterialColorBehaviour.EndPropertyName;
        private readonly string _gradientProperty = MaterialColorBehaviour.GradientPropertyName;

        private readonly string[] _loopProperties = new string[2]
        {
            MaterialColorBehaviour.LoopCountPropertyName,
            MaterialColorBehaviour.LoopDelayPropertyName
        };

        public override void OnInspectorGUI ()
        {
            MaterialColorBehaviour tweenBehaviour = (MaterialColorBehaviour)target;
            SerializedObject serializedObject = new (tweenBehaviour);

            var tweenType = (ImageTweenType)serializedObject.FindProperty (
                ImageTweenBehaviour.TweenTypePropertyName).enumValueIndex;

            SelectPropertiesToHide (tweenType);

            DrawInspector (tweenBehaviour);

            _propertiesToHide.Clear ();
        }

        private void DrawInspector (MaterialColorBehaviour tweenBehaviour)
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

            if (loopType.Matches (LoopType.None))
                _propertiesToHide.AddRange (_loopProperties);
        }
    }
}