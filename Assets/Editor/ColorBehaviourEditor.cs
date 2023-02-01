using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace JK.Tweening
{
    [CustomEditor (typeof (ColorBehaviour))]
    public class ColorBehaviourEditor : Editor
    {
        private readonly List<string> _propertiesToHide = new ();

        private readonly string _colorTarget = ColorBehaviour.ColorTargetPropertyName;
        private readonly string _rendererProperty = ColorBehaviour.TargetRendererPropertyName;
        private readonly string _imageProperty = ColorBehaviour.TargetImagePropertyName;
        private readonly string _materialProperty = ColorBehaviour.TargetMaterialPropertyName;
        private readonly string _materialIndexProperty = ColorBehaviour.MaterialIndexPropertyName;
        private readonly string _startProperty = ColorBehaviour.StartPropertyName;
        private readonly string _endProperty = ColorBehaviour.EndPropertyName;
        private readonly string _gradientProperty = ColorBehaviour.GradientPropertyName;
        private readonly string _loopTypeProperty = ColorBehaviour.LoopTypePropertyName;
        private readonly string _targetSelfProperty = ColorBehaviour.TargetSelfPropertyName;
        private readonly string _tweenTypeProperty = ColorBehaviour.TweenTypePropertyName;

        private readonly string[] _loopProperties = new string[2]
        {
            ColorBehaviour.LoopCountPropertyName,
            ColorBehaviour.LoopDelayPropertyName
        };

        public override void OnInspectorGUI ()
        {
            ColorBehaviour tweenBehaviour = (ColorBehaviour)target;
            SerializedObject serializedObject = new (tweenBehaviour);

            var tweenType = (ImageTweenType)serializedObject.FindProperty (_tweenTypeProperty).enumValueIndex;
            var colorTarget = (ColorTarget)serializedObject.FindProperty (_colorTarget).intValue;

            SelectPropertiesToHide (tweenBehaviour, tweenType, colorTarget);
            DrawInspector (tweenBehaviour, colorTarget);

            _propertiesToHide.Clear ();
        }

        private void DrawInspector (ColorBehaviour tweenBehaviour, ColorTarget colorTarget)
        {
            var hideButtons = colorTarget.Matches (ColorTarget.Renderer) && !Application.isPlaying;

            if (hideButtons)
            {
                EditorGUILayout.HelpBox ("Renderer tween can only be previewed in playmode", MessageType.Info);
            }
            else
            {
                EditorGUILayout.BeginHorizontal ();

                if (GUILayout.Button ("Play"))
                    tweenBehaviour.Play ();

                if (GUILayout.Button ("Stop"))
                    tweenBehaviour.Stop ();

                EditorGUILayout.EndHorizontal ();
                EditorGUILayout.Space ();
            }

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

        private void SelectPropertiesToHide (ColorBehaviour colorBehaviour, ImageTweenType tweenType, ColorTarget colorTarget)
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

            if (!colorTarget.Matches (ColorTarget.Renderer))
                _propertiesToHide.Add (_materialIndexProperty);

            var loopType = (LoopType)serializedObject.FindProperty (_loopTypeProperty).intValue;

            if (loopType.Matches (LoopType.None))
                _propertiesToHide.AddRange (_loopProperties);

            var targetSelf = serializedObject.FindProperty (_targetSelfProperty).boolValue;

            if (colorTarget.Matches (ColorTarget.Material))
            {
                if (targetSelf)
                    colorBehaviour.SetTargetSelf (false);

                _propertiesToHide.Add (_targetSelfProperty);
            }

            if (targetSelf)
            {
                _propertiesToHide.Add (_rendererProperty);
                _propertiesToHide.Add (_materialProperty);
                _propertiesToHide.Add (_imageProperty);
            }
            else
            {
                switch (colorTarget)
                {
                    case ColorTarget.Image:
                        _propertiesToHide.Add (_rendererProperty);
                        _propertiesToHide.Add (_materialProperty);
                        break;
                    case ColorTarget.Material:
                        _propertiesToHide.Add (_rendererProperty);
                        _propertiesToHide.Add (_imageProperty);
                        break;
                    case ColorTarget.Renderer:
                        _propertiesToHide.Add (_materialProperty);
                        _propertiesToHide.Add (_imageProperty);
                        break;
                }
            }
        }
    }
}