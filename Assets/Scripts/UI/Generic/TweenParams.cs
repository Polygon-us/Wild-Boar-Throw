using System;

namespace UI.Generic
{
    [Serializable]
    public struct TweenParams
    {
        public float duration;
        public LeanTweenType inType;
        public LeanTweenType outType;
    }
}