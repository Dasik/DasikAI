using System.Collections.Generic;
using DasikAI.Scripts.Data.Graph.Base;
using UnityEditor;
using UnityEngine;
using XNodeEditor;

namespace DasikAI.Scripts.Controller
{
	public class AIGraphBehaviourController : MonoBehaviour
	{
		[SerializeField]
		private AIGraph _aiGraph;

		[SerializeField]
		private AgentController _agentController;

		private HashSet<AINode> _activeNodes = new HashSet<AINode>();
		public HashSet<AINode> ActiveNodes
		{
			get { return _activeNodes; }
			private set { _activeNodes = value; }
		}
		private readonly Dictionary<AINode, IDataStoreObject> _dsoDictionary = new Dictionary<AINode, IDataStoreObject>();
		public Dictionary<object, IDataStoreObject> SharedStoreObjects { get; } = new Dictionary<object, IDataStoreObject>();

		public void Start()
		{
			foreach (var node in _aiGraph.nodes)
			{
				var aiNode = node as AINode;
				if (aiNode != null)
				{
					var dso = aiNode.Initialize(_agentController);
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
				activeBlock.Enable(dso, _agentController);
			}
		}

		public void OnDisable()
		{
			foreach (var activeBlock in _activeNodes)
			{
				var dso = _dsoDictionary[activeBlock];
				activeBlock.Disable(dso, _agentController);
			}
		}

		public void OnDestroy()
		{
			foreach (var node in _aiGraph.nodes)
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
			nodesStack.Push(_aiGraph.root);

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
					dso = currentNode.Enter(dso, _agentController);
				}

				currentActivatedNodes.Add(currentNode);

				if (currentNode is AIBlock)
				{
					var aiBlock = currentNode as AIBlock;
					dso = aiBlock.DoWork(dso, _agentController);
				}
				foreach (var node in currentNode.Next(dso, _agentController))
				{
					if (node == null || currentActivatedNodes.Contains(node))
						continue;

					var nextAiNode = (AINode)node;
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
				disabledBlock.Exit(dso, _agentController);
			}

			_activeNodes = currentActivatedNodes;
		}
	}
}