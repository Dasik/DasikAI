using System.Collections.Generic;
using DasikAI.BT.Base;
using DasikAI.BT.Base.Blocks;
using DasikAI.Common.Base;
using DasikAI.Common.Controller;
using UnityEditor;
using XNodeEditor;

namespace DasikAI.BT.Controller
{
    public class BtGraphController : AIGraphController<BTGraph>
    {
        public HashSet<AINode> ActiveNodes { get; private set; } = new HashSet<AINode>();

        public void Start()
        {
            base.Start();
            // if (btGraph.StatesSource != null)
            // {
            // 	if (!_sharedStoreObjects.ContainsKey(typeof(StateDSO))
            // 	    || ((StateDSO) _sharedStoreObjects[typeof(StateDSO)]).State == null)
            // 	{
            // 		_sharedStoreObjects.Add(typeof(StateDSO),
            // 			new StateDSO() {State = btGraph.StatesSource.InitialState.SelectedValue});
            // 	}
            // }

            foreach (var node in graph.nodes)
            {
                var aiNode = node as AINode;
                if (aiNode != null)
                {
                    var context = new Context(agentController, SharedDso,Contexts);
                    Contexts.Add(aiNode, context);
                    aiNode.Initialize(context);
                }
            }

            //_activeNodes.Add(_aiGraph.root);
        }

        public void OnEnable()
        {
            foreach (var activeBlock in ActiveNodes)
            {
                activeBlock.Enable(Contexts[activeBlock]);
            }
        }

        public void OnDisable()
        {
            foreach (var activeBlock in ActiveNodes)
            {
                activeBlock.Enable(Contexts[activeBlock]);
            }
        }

        public void OnDestroy()
        {
            foreach (var node in graph.nodes)
            {
                var aiNode = node as AINode;
                if (aiNode != null)
                {
                    aiNode.Dispose(Contexts[aiNode]);
                }
            }
        }

        public void Update()
        {
            var currentActivatedNodes = new HashSet<AINode>();
            var nodesStack = new Stack<AINode>();
            nodesStack.Push(graph.Root);

            while (nodesStack.Count != 0)
            {
                var currentNode = nodesStack.Pop();
                var currentContext = Contexts[currentNode];

                if (ActiveNodes.Contains(currentNode))
                {
                    ActiveNodes.Remove(currentNode);
                }
                else
                {
                    currentNode.Enter(currentContext);
                }

                currentActivatedNodes.Add(currentNode);

                if (currentNode is BTBlock)
                {
                    var aiBlock = currentNode as BTBlock;
                    aiBlock.DoWork(currentContext);
                }

                if (currentNode is BTNode currentBTNode)
                    foreach (var node in currentBTNode.Next(currentContext))
                    {
                        if (node == null || currentActivatedNodes.Contains(node))
                            continue;

                        var nextAiNode = (AINode) node;
                        nodesStack.Push(nextAiNode);
                    }

                Contexts[currentNode] = currentContext;

#if UNITY_EDITOR
                if (NodeEditorWindow.current != null)
                {
                    if (Selection.activeGameObject == gameObject)
                        NodeEditorWindow.current.Repaint();
                }
#endif
            }

            foreach (var disabledBlock in ActiveNodes)
            {
                disabledBlock.Exit(Contexts[disabledBlock]);
            }

            ActiveNodes = currentActivatedNodes;
        }
    }
}