using System;
using System.Collections.Generic;
using UnityEngine;

namespace Extensions
{
    public static class LayerMaskExtensions
    {
        public static List<int> Layers(this LayerMask layerMask)
        {
            var result = new List<int>();
            var value = layerMask.value;
            var mask = 1;
            for (var i = 0; i < 32; i++)
            {
                if ((value & mask) > 0)
                {
                    result.Add(i);
                }

                mask <<= 1;
            }

            return result;
        }
    }
}