using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JK.Tweening.Tests
{
    public class TweenTestFixture
    {
        protected const float TweenDuration = 1f;
        protected const float TweenTimeOut = 1.5f;
        protected TweenBase Tween;

        [OneTimeSetUp]
        public void OneTimeSetUp ()
        {
            SceneManager.LoadScene ("TestScene");

            // Makes the tests run faster and deterministic
            // Value needs to be divisor of TweenTimeOut
            Time.captureDeltaTime = TweenTimeOut / 3f;
        }

        [OneTimeTearDown]
        public void OneTimeTeardown ()
        {
            Time.captureDeltaTime = 0f;
        }
    }
}