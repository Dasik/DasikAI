using System.Collections.Generic;
using DasikAI.BT.Base;
using DasikAI.BT.Base.Blocks;
using DasikAI.Common.DSO;
using DasikAI.BT.Nodes.DSO;
using UnityEditor;
using UnityEngine;
using XNodeEditor;

namespace DasikAI.Common.Controller
{
	public class AIGraphBehaviourController : MonoBehaviour
	{
		[SerializeField] protected AIGraph AiGraph = null;
		[SerializeField] protected AgentController AgentController = null;

		private HashSet<AINode> _activeNodes = new HashSet<AINode>();

		public HashSet<AINode> ActiveNodes
		{
			get { return _activeNodes; }
			private set { _activeNodes = value; }
		}

		private readonly Dictionary<AINode, IDataStoreObject> _dsoDictionary =
			new Dictionary<AINode, IDataStoreObject>();

		public Dictionary<object, IDataStoreObject> SharedStoreObjects { get; } =
			new Dictionary<object, IDataStoreObject>();

		public void Start()
		{
			if (AiGraph.StatesSource != null)
			{
				if (!SharedStoreObjects.ContainsKey(typeof(StateDSO))
				    || ((StateDSO) SharedStoreObjects[typeof(StateDSO)]).State == null)
				{
					SharedStoreObjects.Add(typeof(StateDSO),
						new StateDSO() {State = AiGraph.StatesSource.InitialState.SelectedValue});
				}
			}

			foreach (var node in AiGraph.nodes)
			{
				var aiNode = node as AINode;
				if (aiNode != null)
				{
					var dso = aiNode.Initialize(AgentController);
					_dsoDictionary.Add(aiNode, dso);
				}
			}

			//_activeNodes.Add(_aiGraph.root);
		}

		public void OnEnable()
		{
			foreach (var activeBlock in _activeNodes)
			{
				var dso = _dsoDictionary[activeBlock];
				activeBlock.Enable(dso, AgentController);
			}
		}

		public void OnDisable()
		{
			foreach (var activeBlock in _activeNodes)
			{
				var dso = _dsoDictionary[activeBlock];
				activeBlock.Disable(dso, AgentController);
			}
		}

		public void OnDestroy()
		{
			foreach (var node in AiGraph.nodes)
			{
				var aiNode = node as AINode;
				if (aiNode != null)
				{
					var dso = _dsoDictionary[aiNode];
					aiNode.Dispose(dso);
				}
			}
		}

		public void Update()
		{
			var currentActivatedNodes = new HashSet<AINode>();
			var nodesStack = new Stack<AINode>();
			nodesStack.Push(AiGraph.Root);

			while (nodesStack.Count != 0)
			{
				var currentNode = nodesStack.Pop();
				var dso = _dsoDictionary[currentNode];

				if (_activeNodes.Contains(currentNode))
				{
					_activeNodes.Remove(currentNode);
				}
				else
				{
					dso = currentNode.Enter(dso, AgentController);
				}

				currentActivatedNodes.Add(currentNode);

				if (currentNode is AIBlock)
				{
					var aiBlock = currentNode as AIBlock;
					dso = aiBlock.DoWork(dso, AgentController);
				}

				foreach (var node in currentNode.Next(dso, AgentController))
				{
					if (node == null || currentActivatedNodes.Contains(node))
						continue;

					var nextAiNode = (AINode) node;
					nodesStack.Push(nextAiNode);
				}

				_dsoDictionary[currentNode] = dso;

#if UNITY_EDITOR
				if (NodeEditorWindow.current != null)
				{
					if (Selection.activeGameObject == gameObject)
						NodeEditorWindow.current.Repaint();
				}
#endif
			}

			foreach (var disabledBlock in _activeNodes)
			{
				var dso = _dsoDictionary[disabledBlock];
				disabledBlock.Exit(dso, AgentController);
			}

			_activeNodes = currentActivatedNodes;
		}
	}
}