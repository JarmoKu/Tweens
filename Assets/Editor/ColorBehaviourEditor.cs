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
        private readonly string _spriteProperty = ColorBehaviour.TargetSpritePropertyName;
        private readonly string _lightProperty = ColorBehaviour.TargetlightPropertyName;
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
            TryDrawButtons (tweenBehaviour, colorTarget);

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

        private void TryDrawButtons (ColorBehaviour tweenBehaviour, ColorTarget colorTarget)
        {
            var hideButtons = colorTarget.Matches (ColorTarget.Renderer) && !Application.isPlaying;
            if (hideButtons)
            {
                EditorGUILayout.HelpBox ("Renderer tween can only be previewed in playmode", MessageType.Info);
                return;
            }

            EditorGUILayout.BeginHorizontal ();

            if (GUILayout.Button ("Play"))
                tweenBehaviour.Play ();

            if (GUILayout.Button ("Stop"))
                tweenBehaviour.Stop ();

            EditorGUILayout.EndHorizontal ();
            EditorGUILayout.Space ();
        }

        private void SelectPropertiesToHide (ColorBehaviour colorBehaviour, ImageTweenType tweenType, ColorTarget colorTarget)
        {
            ShowOrHideColoFields (tweenType);

            ShowOrHideLoopProperties ();

            var targetSelf = serializedObject.FindProperty (_targetSelfProperty).boolValue;

            if (!colorTarget.Matches (ColorTarget.Renderer))
                _propertiesToHide.Add (_materialIndexProperty);

            if (colorTarget.Matches (ColorTarget.Material))
            {
                if (targetSelf)
                    colorBehaviour.SetTargetSelf (false);

                _propertiesToHide.Add (_targetSelfProperty);
            }

            ShowOrHideTargetProperties (targetSelf, colorTarget);
        }

        private void ShowOrHideLoopProperties ()
        {
            var loopType = (LoopType)serializedObject.FindProperty (_loopTypeProperty).intValue;

            if (loopType.Matches (LoopType.None))
                _propertiesToHide.AddRange (_loopProperties);
        }

        private void ShowOrHideColoFields (ImageTweenType tweenType)
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
        }

        private void ShowOrHideTargetProperties (bool targetSelf, ColorTarget colorTarget)
        {
            _propertiesToHide.Add (_rendererProperty);
            _propertiesToHide.Add (_materialProperty);
            _propertiesToHide.Add (_imageProperty);
            _propertiesToHide.Add (_spriteProperty);
            _propertiesToHide.Add (_lightProperty);

            if (!targetSelf)
            {
                switch (colorTarget)
                {
                    case ColorTarget.Image:
                        _propertiesToHide.Remove (_imageProperty);
                        break;
                    case ColorTarget.Material:
                        _propertiesToHide.Remove (_materialProperty);
                        break;
                    case ColorTarget.Renderer:
                        _propertiesToHide.Remove (_rendererProperty);
                        break;
                    case ColorTarget.SpriteRenderer:
                        _propertiesToHide.Remove (_spriteProperty);
                        break;
                    case ColorTarget.Light:
                        _propertiesToHide.Remove (_lightProperty);
                        break;
                }
            }
        }
    }
}