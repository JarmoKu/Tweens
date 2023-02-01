# Tweens

## Contents

- [Constructors](#constructors)
- [TweenBase](#tweenbase)
- [Sequence](#sequences)
- [Extension methods](#extensions)

## Constructors

### Transform

- public MoveTween (Transform transform, Vector3 start, Vector3 end, float duration, Space space = Space.Self)
- public RotateTween (Transform transform, Vector3 start, Vector3 end, float duration, Space space = Space.Self)
- public ScaleTween (Transform transform, Vector3 start, Vector3 end, float duration)
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

### Sprite renderer color

- public SpriteColorTween (SpriteRenderer image, Color start, Color end, float duration)
- public SpriteColorTween (SpriteRenderer image, Gradient gradient, float duration)

### Light color

- public LightColorTween (Light image, Color start, Color end, float duration)
- public LightColorTween (Light image, Gradient gradient, float duration)

## Tweenbase class

Is inherited by all tweens and so the public properties and methods can be used through them as well.

### properties:

| Name | Description |
| --- | --- |
| IsCompleted |	Has the tween finished all loops? |
| IsPlaying | Is the tween active but not finished yet? |
| Duration | Length of a single loop in seconds |

### public methods:

| Method | Description |
| --- | --- |
| Play | Start progressing tween |
| Reset | Set tween progress back to zero |
| Pause | Stop tween from progressing temporarily |
| Restart | Start progressing tween from beginning |
| SetEase (EaseType easeType) | Specify the rate of change of over time |
| SetLoops | Set how many times tween is repeated and in what manner |
| SetLoopDelay | Set time between loops in seconds |

### events:

| Method | Description |
| --- | --- |
| OnUpdated | Invoked every frame when tween progresses |
| OnCompleted | Invoked when all loops have finished |

## Sequence Class

### properties:

| Name | Description |
| --- | --- |
| IsPlaying | Is the tween active but not finished yet? |

### public methods:

| Method | Description |
| --- | --- |
| Play | Start progressing tween |
| Reset | Set tween progress back to zero |
| Pause | Stop tween from progressing temporarily |
| Restart | Start progressing tween from beginning |
| Append | Add first tween or new one after the previous one |
| Join | Add tween at specific time of the sequence |
| AddDelay | Add delay in seconds at the end of the sequence |
| Clear | Remove all tweens inside the sequence |

## Extensions

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
	- callback returns the changed value
- public static FloatTween TweenNumber (this float startValue, float endValue, float duration, float increment, Action<float> callback)
	- callback returns the changed value
- public static IntTween TweenNumber (this int startValue, int endValue, float duration, Action<int> callback)
	- callback returns the changed value
- public static IntTween TweenNumber (this int startValue, int endValue, float duration, int increment, Action<int> callback)
	- callback returns the changed value

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

### SpriteRenderer

- public static SpriteColorTween ColorFromTo (this SpriteRenderer renderer, Color start, Color end, float duration)
- public static SpriteColorTween ColorFrom (this SpriteRenderer renderer, Color start, float duration)
- public static SpriteColorTween ColorTo (this SpriteRenderer renderer, Color end, float duration)
- public static SpriteColorTween ColorThroughGradient (this SpriteRenderer renderer, Gradient gradient, float duration)
- public static SpriteColorTween AlphaFromTo (this SpriteRenderer renderer, float start, float end, float duration)
- public static SpriteColorTween AlphaFrom (this SpriteRenderer renderer, float start, float duration)
- public static SpriteColorTween AlphaTo (this SpriteRenderer renderer, float end, float duration)

### Light

- public static LightColorTween ColorFromTo (this Light light, Color start, Color end, float duration)
- public static LightColorTween ColorFrom (this Light light, Color start, float duration)
- public static LightColorTween ColorTo (this Light light, Color end, float duration)
- public static LightColorTween ColorThroughGradient (this Light light, Gradient gradient, float duration)