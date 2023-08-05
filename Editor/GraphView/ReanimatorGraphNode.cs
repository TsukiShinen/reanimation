using Aarthificial.Reanimation.Nodes;
using UnityEditor.Experimental.GraphView;

namespace Aarthificial.Reanimation.Editor
{
    public class ReanimatorGraphNode : Node
    {
        private string _guid;
        private ReanimatorNode _node;
        private bool _isEntryPoint = false;

        public string Guid { get => _guid; set { _guid = value; } }
        public ReanimatorNode Node { get => _node; set { _node = value; } }
        public bool IsEntryPoint { get => _isEntryPoint; set { _isEntryPoint = value; } }
    }
}
