#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace JK.Tweening
{
    public static class TweenPreviewUpdater
    {
        private static readonly List<TweenBase> ActivePreviews = new();

        private static int UndoGroupIndex = -1;
        private static int FrameSkipsLeft;

        [InitializeOnLoadMethod]
        private static void Init ()
        {
            EditorApplication.update -= OnEditorUpdate;
            EditorApplication.update += OnEditorUpdate;

            TweenBehaviourBase.StartedTween -= StartPreviev;
            TweenBehaviourBase.StartedTween += StartPreviev;

            TweenBehaviourBase.StoppedTween -= StopPreview;
            TweenBehaviourBase.StoppedTween += StopPreview;
        }

        public static void StartPreviev (TweenBase tween)
        {
            StopPreview ();

            Undo.IncrementCurrentGroup ();
            UndoGroupIndex = Undo.GetCurrentGroup ();

            ActivePreviews.Add (tween);
            FrameSkipsLeft = 5;

            EditorApplication.QueuePlayerLoopUpdate ();
        }

        public static void StopPreview ()
        {
            ActivePreviews.Clear ();

            if (UndoGroupIndex <= 0) return;

            Undo.RevertAllDownToGroup (UndoGroupIndex);
            UndoGroupIndex = -1;

            EditorApplication.QueuePlayerLoopUpdate ();
        }

        private static void OnEditorUpdate ()
        {
            if (ActivePreviews.Count == 0 || Application.isPlaying) return;

            if (FrameSkipsLeft > 0)
            {
                --FrameSkipsLeft;
                EditorApplication.QueuePlayerLoopUpdate ();

                return;
            }

            for (int i = 0; i < ActivePreviews.Count; i++)
            {
                ActivePreviews[i].Update (Time.deltaTime);

                if (ActivePreviews[i].IsCompleted)
                    ActivePreviews.RemoveAt (i);
            }

            if (ActivePreviews.Count > 0)
                EditorApplication.QueuePlayerLoopUpdate ();
            else
                StopPreview ();
        }
    }
}
#endif