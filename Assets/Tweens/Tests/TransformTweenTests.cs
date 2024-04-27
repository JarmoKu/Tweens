using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace JK.Tweening.Tests
{
    public class TransformTweenTests : TweenTestFixture
    {
        private Transform _testChild;

        [UnitySetUp]
        public IEnumerator UnitySetUp ()
        {
            yield return TestUtilities.WaitForTestCondition (() =>
            {
                var testChild = GameObject.Find ("TestChild");
                _testChild = testChild != null ? testChild.transform : null;

                return _testChild != null;
            }, context: "Find TestChild", TweenTimeOut);

            Assert.That (_testChild != null);
        }

        [UnityTest]
        [TestCase (TweenType.From, Space.Self, 1f, 0f, ExpectedResult = null)]
        [TestCase (TweenType.From, Space.World, 1f, 0f, ExpectedResult = null)]
        [TestCase (TweenType.To, Space.Self, 0f, 1f, ExpectedResult = null)]
        [TestCase (TweenType.To, Space.World, 0f, 1f, ExpectedResult = null)]
        [TestCase (TweenType.FromTo, Space.Self, 0f, 1f, ExpectedResult = null)]
        [TestCase (TweenType.FromTo, Space.World, 0f, 1f, ExpectedResult = null)]
        public IEnumerator MoveTween_MovesTargetToCorrectPosition_InSetDuration 
            (TweenType tweenType, Space space, float startY, float targetY)
        {
            var startPosition = tweenType.Matches (TweenType.To) ?
                space.Matches (Space.Self) ? _testChild.localPosition : _testChild.position :
                new Vector3 (0f, startY, 0f);

            var targetPosition = tweenType.Matches (TweenType.From) ?
                space.Matches (Space.Self) ? _testChild.localPosition : _testChild.position :
                new Vector3 (0f, targetY, 0f);

            Tween = TestUtilities.GetMoveTweenType (
                tweenType, _testChild, startPosition, targetPosition, TweenDuration, space);

            var startTime = Time.timeAsDouble;
            Tween.Play ();

            yield return TestUtilities.WaitForTestCondition (() =>
                {
                    return TestUtilities.PositionIsCorrect (_testChild, targetPosition, space);
                },
                context: "Wait for transform to reach target position",
                TweenTimeOut,
                tweenType.Matches (TweenType.From));

            Assert.That (TestUtilities.TweenWasMeantToFinishNow (startTime, TweenDuration));
        }

        [UnityTest]
        [TestCase (TweenType.From, Space.Self, 90f, 0f, ExpectedResult = null)]
        [TestCase (TweenType.From, Space.World, 90f, 0f, ExpectedResult = null)]
        [TestCase (TweenType.To, Space.Self, 0f, 90f, ExpectedResult = null)]
        [TestCase (TweenType.To, Space.World, 0f, 90f, ExpectedResult = null)]
        [TestCase (TweenType.FromTo, Space.Self, 0f, 90f, ExpectedResult = null)]
        [TestCase (TweenType.FromTo, Space.World, 0f, 90f, ExpectedResult = null)]
        public IEnumerator RotateTween_ChangesTargetToCorrectRotation_InSetDuration
            (TweenType tweenType, Space space, float startY, float targetY)
        {
            var startRotation = tweenType.Matches (TweenType.To) ? 
                space.Matches (Space.Self) ? _testChild.localEulerAngles : _testChild.eulerAngles : 
                new Vector3 (0f, startY, 0f);

            var targetRotation = tweenType.Matches (TweenType.From) ?
                space.Matches (Space.Self) ? _testChild.localEulerAngles : _testChild.eulerAngles : 
                new Vector3 (0f, targetY, 0f);

            Tween = TestUtilities.GetRotateTweenType (
                tweenType, _testChild, startRotation, targetRotation, TweenDuration, space);

            var startTime = Time.timeAsDouble;
            Tween.Play ();

            yield return TestUtilities.WaitForTestCondition (() =>
                {
                    return TestUtilities.RotationIsCorrect (_testChild, targetRotation, space);
                },
                context: "Wait for transform to reach target rotation",
                TweenTimeOut,
                tweenType.Matches (TweenType.From));

            Assert.That (TestUtilities.TweenWasMeantToFinishNow (startTime, TweenDuration));
        }

        [UnityTest]
        [TestCase (TweenType.From, 2f, 0f, ExpectedResult = null)]
        [TestCase (TweenType.From, 2f, 0f, ExpectedResult = null)]
        [TestCase (TweenType.To, 0f, 2f, ExpectedResult = null)]
        [TestCase (TweenType.To, 0f, 2f, ExpectedResult = null)]
        [TestCase (TweenType.FromTo, 0f, 2f, ExpectedResult = null)]
        [TestCase (TweenType.FromTo, 0f, 2f, ExpectedResult = null)]
        public IEnumerator ScaleTween_ChangesTargetToCorrectScale_InSetDuration (
            TweenType tweenType, float startY, float targetY)
        {
            var startScale = tweenType.Matches (TweenType.To) ? 
                _testChild.localScale : new Vector3 (0f, startY, 0f);
            
            var targetScale = tweenType.Matches (TweenType.From) ? 
                _testChild.localScale : new Vector3 (0f, targetY, 0f);

            Tween = TestUtilities.GetScaleTweenType (
                tweenType, _testChild, startScale, targetScale, TweenDuration);

            var startTime = Time.timeAsDouble;
            Tween.Play ();

            yield return TestUtilities.WaitForTestCondition (() =>
                {
                    return _testChild.localScale == targetScale;
                },
                context: "Wait for transform to reach target scale",
                TweenTimeOut,
                tweenType.Matches (TweenType.From));

            Assert.That (TestUtilities.TweenWasMeantToFinishNow (startTime, TweenDuration));
        }

        [Test]
        [TestCase (TweenType.From, Space.World)]
        [TestCase (TweenType.From, Space.Self)]
        [TestCase (TweenType.To, Space.World)]
        [TestCase (TweenType.To, Space.Self)]
        [TestCase (TweenType.FromTo, Space.World)]
        [TestCase (TweenType.FromTo, Space.Self)]
        public void JumpTween_PassesCorrectPeakAndReachesTarget_InSetDuration (TweenType tweenType, Space space)
        {
            var jumpHeight = 5f;
            var positionOne = space == Space.World ? _testChild.position : _testChild.localPosition;
            var peakPosition = positionOne + new Vector3 (2.5f, 5f, 0f);
            var positionTwo = positionOne + new Vector3 (5f, 0f, 0f);

            var firstStartPosition = tweenType.Matches (TweenType.From) ? positionTwo : positionOne;
            var firstTargetPosition = tweenType.Matches (TweenType.From) ? positionOne : positionTwo;

            Tween = TestUtilities.GetJumpTweenType (
                tweenType, _testChild, firstStartPosition, peakPosition, firstTargetPosition, TweenDuration, space);

            Tween.Play ();
            Tween.Update (TweenDuration / 2f);
            Assert.That ((space == Space.World ? _testChild.position : _testChild.localPosition) == peakPosition);
            Tween.Update (TweenDuration / 2f);
            Assert.That ((space == Space.World ? _testChild.position : _testChild.localPosition) == firstTargetPosition);

            var secondStartPosition = positionTwo;
            var secondTargetPosition = positionOne;

            Tween = TestUtilities.GetJumpTweenType (
                tweenType, _testChild, secondStartPosition, jumpHeight, secondTargetPosition, TweenDuration, Vector3.up, space);

            Tween.Play ();
            Tween.Update (TweenDuration / 2f);
            Assert.That ((space == Space.World ? _testChild.position : _testChild.localPosition) == peakPosition);
            Tween.Update (TweenDuration / 2f);
            Assert.That ((space == Space.World ? _testChild.position : _testChild.localPosition) == secondTargetPosition);
        }

        [Test]
        [TestCase (Space.Self)]
        [TestCase (Space.World)]
        public void PunchPositionTween_ChangesPositionCorrectly_InSetDuration (Space space)
        {
            var startPosition = space == Space.World ? _testChild.position : _testChild.localPosition;
            var targetPosition = new Vector3 (0f, 3f, 0f);
            Tween = _testChild.PunchPosition (targetPosition, TweenDuration, space);
            Tween.Play ();

            Tween.Update (0.5f);
            Debug.Assert ((space == Space.World ? _testChild.position : _testChild.localPosition) == targetPosition);
            Tween.Update (1f);
            Debug.Assert ((space == Space.World ? _testChild.position : _testChild.localPosition) == startPosition);
        }

        [Test]
        [TestCase (Space.Self)]
        [TestCase (Space.World)]
        public void PunchRotationTween_ChangesRotationCorrectly_InSetDuration (Space space)
        {
            var startRotation = space == Space.World ? _testChild.eulerAngles : _testChild.localEulerAngles;
            var targetRotation = new Vector3 (0f, 3f, 0f);
            Tween = _testChild.PunchRotation (targetRotation, TweenDuration, space);
            Tween.Play ();

            Tween.Update (0.5f);
            Debug.Assert ((space == Space.World ? _testChild.eulerAngles : _testChild.localEulerAngles) == targetRotation);
            Tween.Update (1f);
            Debug.Assert ((space == Space.World ? _testChild.eulerAngles : _testChild.localEulerAngles) == startRotation);
        }

        [Test]
        public void PunchScaleTween_ChangesScaleCorrectly_InSetDuration ()
        {
            var startScale = _testChild.localScale;
            var targetScale = new Vector3 (1f, 3f, 1f);
            Tween = _testChild.PunchScale (targetScale, TweenDuration);
            Tween.Play ();

            Tween.Update (0.5f);
            Debug.Assert (_testChild.localScale == targetScale);
            Tween.Update (1f);
            Debug.Assert (_testChild.localScale == startScale);
        }

        [TearDown]
        public void TearDown ()
        {
            _testChild.SetLocalPositionAndRotation (Vector3.zero, Quaternion.identity);
            _testChild.localScale = Vector3.one;

            TweenManager.RemoveTween (Tween);
            Tween = null;
        }
    }
}