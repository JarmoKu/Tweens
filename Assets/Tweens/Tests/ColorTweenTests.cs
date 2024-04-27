using NUnit.Framework;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace JK.Tweening.Tests
{
    public class ColorTweenTests : TweenTestFixture
    {
        private readonly Color DefaultColor = Color.white;
        private Image _testImage;
        private SpriteRenderer _testSprite;
        private Material _testMaterial;
        private Light _testLight;
        private Renderer _testRenderer;

        [UnitySetUp]
        public IEnumerator UnitySetUp ()
        {
            yield return TestUtilities.WaitForTestCondition (() =>
            {
                _testSprite = GameObject.Find ("TestSprite").GetComponent<SpriteRenderer> ();

                return _testSprite != null;
            }, context: "Find TestSprite", TweenTimeOut);

            _testSprite.color = DefaultColor;
            Assert.That (_testSprite != null);

            yield return TestUtilities.WaitForTestCondition (() =>
            {
                _testImage = GameObject.Find ("TestImage").GetComponent<Image> ();

                return _testImage != null;
            }, context: "Find TestImage", TweenTimeOut);

            _testImage.color = DefaultColor;
            Assert.That (_testImage != null);

            yield return TestUtilities.WaitForTestCondition (() =>
            {
                _testLight = GameObject.Find ("TestLight").GetComponent<Light> ();

                return _testLight != null;
            }, context: "Find TestLight", TweenTimeOut);

            _testLight.color = DefaultColor;
            Assert.That (_testLight != null);

            yield return TestUtilities.WaitForTestCondition (() =>
            {
                _testRenderer = GameObject.Find ("TestChild").GetComponent<Renderer>();

                return _testRenderer != null;
            }, context: "Find TestRenderer", TweenTimeOut);

            _testRenderer.material.color = DefaultColor;
            Assert.That (_testRenderer != null);

            _testMaterial = new Material (Shader.Find ("Standard"));
        }

        [Test]
        [TestCase (ColorTarget.Image, ImageTweenType.From)]
        [TestCase (ColorTarget.Image, ImageTweenType.FromTo)]
        [TestCase (ColorTarget.Image, ImageTweenType.To)]
        [TestCase (ColorTarget.SpriteRenderer, ImageTweenType.From)]
        [TestCase (ColorTarget.SpriteRenderer, ImageTweenType.FromTo)]
        [TestCase (ColorTarget.SpriteRenderer, ImageTweenType.To)]
        [TestCase (ColorTarget.Material, ImageTweenType.From)]
        [TestCase (ColorTarget.Material, ImageTweenType.FromTo)]
        [TestCase (ColorTarget.Material, ImageTweenType.To)]
        [TestCase (ColorTarget.Light, ImageTweenType.From)]
        [TestCase (ColorTarget.Light, ImageTweenType.FromTo)]
        [TestCase (ColorTarget.Light, ImageTweenType.To)]
        [TestCase (ColorTarget.Renderer, ImageTweenType.From)]
        [TestCase (ColorTarget.Renderer, ImageTweenType.FromTo)]
        [TestCase (ColorTarget.Renderer, ImageTweenType.To)]
        public void ColorTween_ReachestTargetColor_InSetTime (ColorTarget targetType, ImageTweenType tweenType) 
        {
            var startColor = tweenType.Matches (ImageTweenType.From) ? Color.blue : DefaultColor;
            var endColor = tweenType.Matches (ImageTweenType.From) ? DefaultColor : Color.blue;

            Tween = targetType switch
            {
                ColorTarget.Image => TestUtilities.GetImageColorTween (
                    _testImage, tweenType, startColor, endColor, TweenDuration),
                ColorTarget.SpriteRenderer => TestUtilities.GetSpriteColorTween (
                    _testSprite, tweenType, startColor, endColor, TweenDuration),
                ColorTarget.Material => TestUtilities.GetSharedMaterialColorTween (
                    _testMaterial, tweenType, startColor, endColor, TweenDuration),
                ColorTarget.Light => TestUtilities.GetLightColorTween (
                    _testLight, tweenType, startColor, endColor, TweenDuration),
                ColorTarget.Renderer => TestUtilities.GetRendererColorTween (
                    _testRenderer, tweenType, 0, startColor, endColor, TweenDuration),
                _ => throw new NotImplementedException (),
            };

            Tween.Play ();
            Assert.That (GetCurrentColor (targetType) == (tweenType.Matches (ImageTweenType.From) ? endColor : startColor));
            Tween.Update (TweenDuration);
            Assert.That (GetCurrentColor (targetType) == endColor);
        }

        [Test]
        [TestCase (ColorTarget.Image, ImageTweenType.From)]
        [TestCase (ColorTarget.Image, ImageTweenType.FromTo)]
        [TestCase (ColorTarget.Image, ImageTweenType.To)]
        [TestCase (ColorTarget.SpriteRenderer, ImageTweenType.From)]
        [TestCase (ColorTarget.SpriteRenderer, ImageTweenType.FromTo)]
        [TestCase (ColorTarget.SpriteRenderer, ImageTweenType.To)]
        [TestCase (ColorTarget.Material, ImageTweenType.From)]
        [TestCase (ColorTarget.Material, ImageTweenType.FromTo)]
        [TestCase (ColorTarget.Material, ImageTweenType.To)]
        [TestCase (ColorTarget.Renderer, ImageTweenType.From)]
        [TestCase (ColorTarget.Renderer, ImageTweenType.FromTo)]
        [TestCase (ColorTarget.Renderer, ImageTweenType.To)]
        public void SpriteColorTween_ReachestTargetAlpha_InSetTime (ColorTarget targetType, ImageTweenType tweenType)
        {
            var startAlpha = tweenType.Matches (ImageTweenType.From) ? 0f : _testSprite.color.a;
            var endAlpha = tweenType.Matches (ImageTweenType.From) ? _testSprite.color.a : 0f;

            Tween = targetType switch
            {
                ColorTarget.Image => TestUtilities.GetImageAlphaTween (
                    _testImage, tweenType, startAlpha, endAlpha, TweenDuration),
                ColorTarget.SpriteRenderer => TestUtilities.GetSpriteAlphaTween (
                    _testSprite, tweenType, startAlpha, endAlpha, TweenDuration),
                ColorTarget.Material => TestUtilities.GetSharedMaterialAlphaTween (
                    _testMaterial, tweenType, startAlpha, endAlpha, TweenDuration),
                ColorTarget.Renderer => TestUtilities.GetRendererAlphaTween (
                    _testRenderer, tweenType, 0, startAlpha, endAlpha, TweenDuration),
                _ => throw new NotImplementedException (),
            };

            Tween.Play ();
            Assert.That (GetCurrentColor (targetType).a == (tweenType.Matches (ImageTweenType.From) ? endAlpha : startAlpha));
            Tween.Update (TweenDuration);
            Assert.That (GetCurrentColor (targetType).a == endAlpha);
        }

        [Test]
        [TestCase (ColorTarget.Image)]
        [TestCase (ColorTarget.Image)]
        [TestCase (ColorTarget.Image)]
        [TestCase (ColorTarget.SpriteRenderer)]
        [TestCase (ColorTarget.SpriteRenderer)]
        [TestCase (ColorTarget.SpriteRenderer)]
        [TestCase (ColorTarget.Material)]
        [TestCase (ColorTarget.Material)]
        [TestCase (ColorTarget.Material)]
        [TestCase (ColorTarget.Light)]
        [TestCase (ColorTarget.Light)]
        [TestCase (ColorTarget.Light)]
        [TestCase (ColorTarget.Renderer)]
        [TestCase (ColorTarget.Renderer)]
        [TestCase (ColorTarget.Renderer)]
        public void SpriteColorTween_GoesThroughGradient_InSetTime (ColorTarget targetType)
        {
            var targetColor = Color.blue;
            targetColor.a = 1f;

            var gradient = new Gradient ();
            var colors = new GradientColorKey[2] { new (Color.red, 0f), new (targetColor, 1f), };
            var alphas = new GradientAlphaKey[2] { new (1f, 0f), new (1f, 1f), };
            gradient.SetKeys (colors, alphas);

            Tween = targetType switch
            {
                ColorTarget.Image => _testImage.ColorThroughGradient (gradient, TweenDuration),
                ColorTarget.SpriteRenderer => _testSprite.ColorThroughGradient (gradient, TweenDuration),
                ColorTarget.Material => _testMaterial.ColorThroughGradient (gradient, TweenDuration),
                ColorTarget.Light => _testLight.ColorThroughGradient (gradient, TweenDuration),
                ColorTarget.Renderer => _testRenderer.ColorThroughGradient (materialIndex: 0, gradient, TweenDuration),
                _ => throw new NotImplementedException (),
            };

            Tween.Play ();
            Tween.Update (TweenDuration / 2f);
            Assert.That (GetCurrentColor (targetType) == Color.Lerp (Color.red, targetColor, 0.5f));
            Tween.Update (TweenDuration / 2f);
            Assert.That (GetCurrentColor (targetType) == targetColor);
        }

        private Color GetCurrentColor (ColorTarget targetType)
        {
            return targetType switch
            {
                ColorTarget.SpriteRenderer => _testSprite.color,
                ColorTarget.Renderer => _testRenderer.material.color,
                ColorTarget.Material => _testMaterial.color,
                ColorTarget.Image => _testImage.color,
                ColorTarget.Light => _testLight.color,
                _ => throw new NotImplementedException (),
            };
        }

        [TearDown]
        public void TearDown ()
        {
            _testImage.color = DefaultColor;
            _testSprite.color = DefaultColor;
            _testMaterial.color = DefaultColor;
            _testRenderer.material.color = DefaultColor;
            _testMaterial = null;

            TweenManager.RemoveTween (Tween);
            Tween = null;
        }
    }
}