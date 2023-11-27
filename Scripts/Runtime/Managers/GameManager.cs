using UnityEngine;
using DP.Tools;

namespace DP.Runtime
{
    [RequireComponent(typeof(IInputProvider))]
    public class GameManager : PersistantSingleton<GameManager>
    {
        private IInputProvider _inputProvider;
        public IInputProvider InputProvider => _inputProvider ??= GetComponent<IInputProvider>();
    }
}
