using Sirenix.OdinInspector.Editor;
using Unity.Netcode;
using UnityEditor;

namespace DP.Editor
{
    [CustomEditor(typeof(NetworkBehaviour), true)]
    public class OdinNetworkBehaviourEditor : OdinEditor { }
}
