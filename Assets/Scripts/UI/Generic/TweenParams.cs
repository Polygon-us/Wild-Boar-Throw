using System;

namespace UI.Generic
{
    [Serializable]
    public class TweenParams
    {
        public float duration = 0.2f;
        public LeanTweenType inType = LeanTweenType.easeInCubic;
        public LeanTweenType outType = LeanTweenType.easeOutCubic;
    }
}