using System.Collections.Generic;
using DasikAI.BT.Base;
using DasikAI.BT.Base.Blocks;
using DasikAI.Common.Base;
using DasikAI.Common.Base.ParamSources;
using DasikAI.Common.Controller;
using Unity.Profiling;
using UnityEditor;

namespace DasikAI.BT.Controller
{
    public class BtGraphController : AIGraphController<BTGraph>
    {
        private static ProfilerMarker s_EnterNodeProfilerMarker =
            new ProfilerMarker(nameof(BtGraphController) + "@Enter node");

        private static ProfilerMarker s_DoWorkProfilerMarker =
            new ProfilerMarker(nameof(BtGraphController) + "@Do Work");

        private static ProfilerMarker s_GetNextNodesProfilerMarker =
            new ProfilerMarker(nameof(BtGraphController) + "@Get next nodes");

        private static ProfilerMarker s_DisableNodesProfilerMarker =
            new ProfilerMarker(nameof(BtGraphController) + "@Disable nodes");

        public HashSet<AINode> ActiveNodes { get; private set; } = new HashSet<AINode>();

        public void Update()
        {
            var currentActivatedNodes = new HashSet<AINode>();
            var nodesStack = new Stack<AINode>();
            nodesStack.Push(graph.Root); //start from root
            var deactivateNodesList = ActiveNodes;

            while (nodesStack.Count != 0)
            {
                s_EnterNodeProfilerMarker.Begin(this);
                var currentNode = nodesStack.Pop();
                var currentContext = Contexts[currentNode];

                if (deactivateNodesList.Contains(currentNode)
                ) // if node was previously activated remove it from deactivation list
                {
                    deactivateNodesList.Remove(currentNode);
                }
                else
                {
                    currentNode.OnNodeEnter(currentContext); //is new node. Enter
                }

                currentActivatedNodes.Add(currentNode);
                s_EnterNodeProfilerMarker.End();
                s_DoWorkProfilerMarker.Begin(this);
                if (currentNode is BTBlock aiBlock) //do work on activated node
                    aiBlock.DoWork(currentContext);
                s_DoWorkProfilerMarker.End();

                s_GetNextNodesProfilerMarker.Begin(this);
                if (currentNode is BTNode currentBTNode) //add next nodes to stack
                {
                    var nextNodes = currentBTNode.Next(currentContext);
                    if (nextNodes != null)
                        foreach (var node in nextNodes)
                        {
                            if (ReferenceEquals(node, null) || currentActivatedNodes.Contains(node))
                                continue;
                            nodesStack.Push(node);
                        }
                }

                s_GetNextNodesProfilerMarker.End();
            }

            s_DisableNodesProfilerMarker.Begin(this);
            foreach (var disabledBlock in deactivateNodesList)
            {
                disabledBlock.OnNodeExit(Contexts[disabledBlock]);
            }

            s_DisableNodesProfilerMarker.End();

            ActiveNodes = currentActivatedNodes;
            
#if UNITY_EDITOR
            //redraw graph editor for debug
            if (XNodeEditor.NodeEditorWindow.current != null)
            {
                if (Selection.activeGameObject == gameObject)
                    XNodeEditor.NodeEditorWindow.current.Repaint();
            }
#endif
        }

        public override T GetParam<T>(ParamSource<T> paramSource)
        {
            return paramSource.GetParam(Contexts[paramSource]);
        }

        protected override NodeContext CreateNodeContext(AINode node)
        {
            return new NodeContext(characterController, SharedDso, this);
        }
    }
}