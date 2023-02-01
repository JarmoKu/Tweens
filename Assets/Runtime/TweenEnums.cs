namespace JK.Tweening
{
    public enum ColorTarget
    {
        Image,
        Renderer,
        Material,
        SpriteRenderer,
        Light
    }

    public enum LoopType
    {
        None,
        Repeat,
        PingPong,
        Additive
    }

    public enum TweenClass
    {
        Move,
        Rotate,
        Scale,
        Jump,
        PunchPosition,
        PunchRotation,
        PunchScale
    }

    public enum TweenType
    {
        FromTo,
        From,
        To
    }

    public enum ImageTweenType
    {
        FromTo,
        From,
        To,
        Gradient
    }

    public enum NumberTweens
    {
        Float,
        Int
    }

    public enum PlayOn
    {
        None,
        Start,
        OnEnable
    }

    public enum PunchType
    {
        Position,
        Rotation,
        Scale
    }

    public enum EaseType
    {
        Linear,
        EaseInSine,
        EaseOutSine,
        EaseInOutSine,
        EaseInQuad,
        EaseOutQuad,
        EaseInOutQuad,
        EaseInCubic,
        EaseOutCubic,
        EaseInOutCubic,
        EaseInQuart,
        EaseOutQuart,
        EaseInOutQuart,
        EaseInQuint,
        EaseOutQuint,
        EaseInOutQuint,
        EaseInExpo,
        EaseOutExpo,
        EaseInOutExpo,
        EaseInCirc,
        EaseOutCirc,
        EaseInOutCirc,
        EaseInBack,
        EaseOutBack,
        EaseInOutBack,
        EaseInElastic,
        EaseOutElastic,
        EaseInOutElastic,
        EaseInBounce,
        EaseOutBounce,
        EaseInOutBounce,
        Spike,
        InPunch,
        OutPunch,
        Shake
    }
}