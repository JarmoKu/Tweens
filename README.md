# Tweens

## Contents

- [Constructors](#constructors)
- [TweenBase](#tweenbase)
- [Sequence](#sequences)
- [Extension methods](#extensions)

## CONSTRUCTORS

### Transform move

- public MoveTween (Transform transform, Vector3 start, Vector3 end, float duration, Space space = Space.Self)

### Transform rotate

- public RotateTween (Transform transform, Vector3 start, Vector3 end, float duration, Space space = Space.Self)

### Transform scale

- public ScaleTween (Transform transform, Vector3 start, Vector3 end, float duration)

### Transform jump

- public JumpTween (Transform transform, Vector3 start, Vector3 peakPosition, Vector3 end, float duration, Space space = Space.Self)

### Float

- public FloatTween (float startingValue, float endValue, float duration)
- public FloatTween (float startingValue, float endValue, float duration, Action<float> callback)
	- callback returns the changed value
- public FloatTween (float startingValue, float endValue, float duration, float increment, Action<float> callback)
	- callback returns the changed value

### Int

- public IntTween (int startingValue, int endValue, float duration, Action<int> callback)
	- callback returns the changed value
- public IntTween (int startingValue, int endValue, float duration, int increment, Action<int> callback)
	- callback returns the changed value

### Image color

- public ImageColorTween (Image image, Color start, Color end, float duration)
- public ImageColorTween (Image image, Gradient gradient, float duration)

### Material color

- public SharedMaterialColorTween (Material material, Color start, Color end, float duration)
- public SharedMaterialColorTween (Material material, Gradient gradient, float duration)

### Renderer color

- public RendererColorTween (Renderer image, int materialIndex, Color start, Color end, float duration)
- public RendererColorTween (Renderer image, int materialIndex, Gradient gradient, float duration)

## TWEENBASE

### properties:

| Name | Description |
| --- | --- |
| IsCompleted |	Has the tween finished all loops? |
| IsPlaying | Is the tween active but not finished yet? |
| Duration | Length of a single loop in seconds |

### public methods:

- public void Reset ();
- public void Play ()
- public void Pause ()
- public void Restart ()
- public void SetEase (EaseType easeType)
- public virtual void SetLoops (int count, LoopType type = LoopType.None)
- public void SetLoopDelay (float delay)

### Events:

- public event TweenEventHandler OnUpdated;
- public event TweenEventHandler OnCompleted;

## SEQUENCES

### properties:

| Name | Description |
| --- | --- |
| IsPlaying | Is the tween active but not finished yet? |

### public methods:

- public void Play ()
- public void Pause ()
- public void Reset ()
- public void Restart ()
- public void Append (TweenBase tween)
- public void Join (TweenBase tween, float startTime)
- public void AddDelay (float duration)
- public void Clear ()

## EXTENSIONS

### Transform Move

- public static MoveTween MoveFromTo (this Transform transform, Vector3 start, Vector3 end, float duration, Space space = Space.Self)
- public static MoveTween MoveTo (this Transform transform, Vector3 end, float duration, Space space = Space.Self)
- public static MoveTween MoveFrom (this Transform transform, Vector3 start, float duration, Space space = Space.Self)

### Transform Scale

- public static ScaleTween ScaleFromTo (this Transform transform, Vector3 start, Vector3 end, float duration)
- public static ScaleTween ScaleTo (this Transform transform, Vector3 end, float duration)
- public static ScaleTween ScaleFrom (this Transform transform, Vector3 start, float duration)

### Transform Rotate

- public static RotateTween RotateFromTo (this Transform transform, Vector3 start, Vector3 end, float duration, Space space = Space.Self)
- public static RotateTween RotateFrom (this Transform transform, Vector3 start, float duration, Space space = Space.Self)
- public static RotateTween RotateTo (this Transform transform, Vector3 end, float duration, Space space = Space.Self)

### Transform Jump

- public static JumpTween JumpFromTo (this Transform transform, Vector3 start, Vector3 peak, Vector3 end, float duration, Space space = Space.Self)
- public static JumpTween JumpFrom (this Transform transform, Vector3 start, Vector3 peak, float duration, Space space = Space.Self)
- public static JumpTween JumpTo (this Transform transform, Vector3 peak, Vector3 end, float duration, Space space = Space.Self)
- public static JumpTween JumpFromTo (this Transform transform, Vector3 start, Vector3 end, float height, float duration, Vector3 up, Space space = Space.Self)
- public static JumpTween JumpFrom (this Transform transform, Vector3 start, float height, float duration, Vector3 up, Space space = Space.Self)
- public static JumpTween JumpTo (this Transform transform, Vector3 end, float height, float duration, Vector3 up, Space space = Space.Self)

### Transform Punch

- public static PunchPositionTween PunchPosition (this Transform transform, Vector3 targetPosition, float duration, Space space = Space.World)
- public static PunchRotationTween PunchRotation (this Transform transform, Vector3 targetRotation, float duration, Space space = Space.World)
- public static PunchScaleTween PunchScale (this Transform transform, Vector3 targetScale, float duration)

### Numbers

- public static FloatTween TweenNumber (this float startValue, float endValue, float duration, Action<float> callback)
- public static FloatTween TweenNumber (this float startValue, float endValue, float duration, float increment, Action<float> callback)
- public static IntTween TweenNumber (this int startValue, int endValue, float duration, Action<int> callback)
- public static IntTween TweenNumber (this int startValue, int endValue, float duration, int increment, Action<int> callback)

### Image

- public static ImageColorTween ColorFrom (this Image image, Color start, float duration)
- public static ImageColorTween ColorTo (this Image image, Color end, float duration)
- public static ImageColorTween ColorThroughGradient (this Image image, Gradient gradient, float duration)
- public static ImageColorTween AlphaFromTo (this Image image, float start, float end, float duration)
- public static ImageColorTween AlphaFrom (this Image image, float start, float duration)
- public static ImageColorTween AlphaTo (this Image image, float end, float duration)

### Material

- public static SharedMaterialColorTween ColorFromTo (this Material image, Color start, Color end, float duration)
- public static SharedMaterialColorTween ColorFrom (this Material image, Color start, float duration)
- public static SharedMaterialColorTween ColorTo (this Material image, Color end, float duration)
- public static SharedMaterialColorTween ColorThroughGradient (this Material image, Gradient gradient, float duration)
- public static SharedMaterialColorTween AlphaFromTo (this Material image, float start, float end, float duration)
- public static SharedMaterialColorTween AlphaFrom (this Material image, float start, float duration)
- public static SharedMaterialColorTween AlphaTo (this Material image, float end, float duration)

### Renderer

- public static RendererColorTween ColorFromTo (this Renderer renderer, int materialIndex, Color start, Color end, float duration)
- public static RendererColorTween ColorFrom (this Renderer renderer, int materialIndex, Color start, float duration)
- public static RendererColorTween ColorTo (this Renderer renderer, int materialIndex, Color end, float duration)
- public static RendererColorTween ColorThroughGradient (this Renderer renderer, int materialIndex, Gradient gradient, float duration)
- public static RendererColorTween AlphaFromTo (this Renderer renderer, int materialIndex, float start, float end, float duration)
- public static RendererColorTween AlphaFrom (this Renderer renderer, int materialIndex, float start, float duration)
- public static RendererColorTween AlphaTo (this Renderer renderer, int materialIndex, float end, float duration)