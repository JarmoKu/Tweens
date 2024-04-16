using NUnit.Framework;
using System;

namespace JK.Tweening.Tests
{
    public class EaseTests
    {
        [Test]
        public void GetEased_ReturnsZero_WhenPassedZeroAsAnArgument ()
        {
            var easeTypes = Enum.GetValues (typeof (EaseType));

            foreach (EaseType easeType in easeTypes)
            {
                Assert.That (0f == Ease.GetEased (0f, easeType));
            }
        }

        [Test]
        public void GetEased_ReturnsOne_WhenPassedOneAsAnArgument ()
        {
            var easeTypes = Enum.GetValues (typeof (EaseType));

            foreach (EaseType easeType in easeTypes)
            {
                Assert.That (1f == Ease.GetEased (1f, easeType));
            }
        }
    }
}