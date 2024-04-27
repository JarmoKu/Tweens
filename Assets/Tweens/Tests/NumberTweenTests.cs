using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

namespace JK.Tweening.Tests
{
    public class NumberTweenTests
    {
        private const float TweenTimeOut = 1.5f;

        [OneTimeSetUp]
        public void OneTimeSetUp ()
        {
            // Makes the tests run faster and deterministic
            // Value needs to be divisor of TweenTimeOut
            Time.captureDeltaTime = TweenTimeOut / 3f;
        }

        [UnityTest]
        [TestCase (2, ExpectedResult = null)]
        [TestCase (-2, ExpectedResult = null)]
        public IEnumerator IntTween_CompletesWithExpectedValue_InSetDuration (int targetValue)
        {
            var startTime = Time.timeAsDouble;
            var result = 0;
            var duration = 2f;
            var tween = new IntTween (0, targetValue, duration, (value) => { result = value; });
            tween.Play ();

            yield return TestUtilities.WaitForTestCondition (() =>
            {
                return result == targetValue;
            },
            "IntTween did not complete in time",
            2.5f);

            Assert.That (TestUtilities.TweenWasMeantToFinishNow (startTime, duration));
        }

        [Test]
        [TestCase (2)]
        [TestCase (-2)]
        public void IntTween_CompletesWithExpectedValue_InSetIntervals (int targetValue)
        {
            var startTime = Time.timeAsDouble;
            var result = 0;
            var duration = 2f;
            var tween = new IntTween (0, targetValue, duration, 1, (value) => { result = value; });
            tween.Play ();

            tween.Update (0.9f);
            Assert.That (result == 0);
            tween.Update (0.1f);
            Assert.That (result == targetValue / 2);
            tween.Update (0.9f);
            Assert.That (result == targetValue / 2);
            tween.Update (0.1f);
            Assert.That (result == targetValue);
        }

        [UnityTest]
        [TestCase (2f, ExpectedResult = null)]
        [TestCase (-2f, ExpectedResult = null)]
        public IEnumerator FloatTween_CompletesWithExpectedValue_InSetDuration (float targetValue)
        {
            var startTime = Time.timeAsDouble;
            var result = 0f;
            var duration = 2f;
            var tween = new FloatTween (0, targetValue, duration, (value) => { result = value; });
            tween.Play ();

            yield return TestUtilities.WaitForTestCondition (() =>
            {
                return result == targetValue;
            },
            "IntTween did not complete in time",
            2.5f);

            Assert.That (TestUtilities.TweenWasMeantToFinishNow (startTime, duration));
        }

        [Test]
        [TestCase (2f)]
        [TestCase (-2f)]
        public void FloatTween_CompletesWithExpectedValue_InSetIntervals (float targetValue)
        {
            var startTime = Time.timeAsDouble;
            var result = 0f;
            var duration = 2f;
            var tween = new FloatTween (0f, targetValue, duration, 1f, (value) => { result = value; });
            tween.Play ();

            tween.Update (0.9f);
            Assert.That (result == 0f);
            tween.Update (0.1f);
            Assert.That (result == targetValue / 2f);
            tween.Update (0.9f);
            Assert.That (result == targetValue / 2f);
            tween.Update (0.1f);
            Assert.That (result == targetValue);
        }

        [OneTimeTearDown]
        public void OneTimeTeardown ()
        {
            Time.captureDeltaTime = 0f;
        }
    }
}