using System;
using UnityEngine;

namespace DP.Runtime
{
    [Serializable]
    public struct AnimatorParams
    {
        [field: SerializeField]
        public string WalkBool { get; private set; }
    }
}
