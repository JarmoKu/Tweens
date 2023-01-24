using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace JK.Tweening
{
    [CustomEditor (typeof (NumberBehaviour), true)]
    public class NumberBehaviourEditor : Editor
    {
        private readonly List<string> _propertiesToHide = new();

        private readonly string[] _loopProperties = new string[2]
        {
            NumberBehaviour.LoopCountPropertyName,
            NumberBehaviour.LoopDelayPropertyName
        };

        private readonly string[] _floatProperties = new string[3]
        {
            NumberBehaviour.FloatStartPropertyName,
            NumberBehaviour.FloatEndPropertyName,
            NumberBehaviour.FloatEventPropertyName
        };

        private readonly string[] _intProperties = new string[3]
        {
            NumberBehaviour.IntStartPropertyName,
            NumberBehaviour.IntEndPropertyName,
            NumberBehaviour.IntEventPropertyName
        };

        public override void OnInspectorGUI ()
        {
            var numberTweenBehaviour = (NumberBehaviour)target;

            SelectPropertiesToHide ();

            DrawInspector (numberTweenBehaviour);

            _propertiesToHide.Clear ();
        }

        private void DrawInspector (NumberBehaviour numberTweenBehaviour)
        {
            EditorGUILayout.BeginHorizontal ();

            if (GUILayout.Button ("Play"))
                numberTweenBehaviour.Play ();

            if (GUILayout.Button ("Stop"))
                numberTweenBehaviour.Stop ();

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

        private void SelectPropertiesToHide ()
        {
            _propertiesToHide.Add ("m_Script");
            var loopType = (LoopType)serializedObject.FindProperty (NumberBehaviour.LoopTypePropertyName).intValue;

            if (loopType.Matches (LoopType.None))
                _propertiesToHide.AddRange (_loopProperties);

            var tweenType = (NumberTweens)serializedObject.FindProperty (NumberBehaviour.ValueTypePropertyName).enumValueIndex;

            _propertiesToHide.AddRange (tweenType.Equals (NumberTweens.Float) ? _intProperties : _floatProperties);
        }
    }
}