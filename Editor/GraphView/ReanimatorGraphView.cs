using Aarthificial.Reanimation.Nodes;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Aarthificial.Reanimation.Editor
{
    public class ReanimatorGraphView : GraphView
    {
        private readonly Vector2 _defaultNodeSize;

        public ReanimatorGraphView() 
        {
            styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Reanimator/Editor/GraphView/UI Builder/USS/ReanimatorGraph.uss"));
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

            var grid = new GridBackground();
            Insert(0, grid);
            grid.StretchToParentSize();

            AddElement(GenerateEntryPoint());
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            var compatiblePorts = new List<Port>();
            ports.ForEach(port =>
            {
                if (startPort != port && startPort.node != port.node) 
                {
                    compatiblePorts.Add(port);
                }
            });

            return compatiblePorts;
        }

        private Port GeneratePort(ReanimatorGraphNode pNode, Direction pDirection, Port.Capacity pCapacity = Port.Capacity.Single)
        {
            return pNode.InstantiatePort(Orientation.Horizontal, pDirection, pCapacity, typeof(float));
        }

        private ReanimatorGraphNode GenerateEntryPoint()
        {
            var node = new ReanimatorGraphNode()
            {
                title = "Root",
                Guid = Guid.NewGuid().ToString(),
                Node = new SwitchNode(),
                IsEntryPoint = true,
            };

            var port = GeneratePort(node, Direction.Output, Port.Capacity.Multi);
            port.portName = "Output";
            node.outputContainer.Add(port);

            node.RefreshExpandedState();
            node.RefreshPorts();

            node.SetPosition(new Rect(100, 200, 100, 150));
            return node;
        }

        public void CreateNode(string nodeName)
        {
            var node = new ReanimatorGraphNode()
            {
                title = nodeName,
                Guid = Guid.NewGuid().ToString(),
                Node = new SwitchNode(),
            };

            var inputPort = GeneratePort(node, Direction.Input);
            inputPort.portName = "Input";
            node.inputContainer.Add(inputPort);

            node.RefreshExpandedState();
            node.RefreshPorts();

            node.SetPosition(new Rect(Vector2.zero, _defaultNodeSize));

            AddElement(node);
        }
    }
}
