using UnityEngine;

namespace Utils
{
    public static class LayerExtensions
    {
        public static bool IsInLayerMask(this LayerMask mask, int layer)
        {
            return (mask.value & (1 << layer)) != 0;
        }
    }
}