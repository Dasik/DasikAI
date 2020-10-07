using System.Collections.Generic;
using System.Linq;
using DasikAI.Common.Base;
using DasikAI.Common.Base.DSO;
using DasikAI.Common.Base.ParamSources;
using UnityEngine;
using XNode;

namespace DasikAI.Common.Controller
{
    public abstract class AIGraphController : MonoBehaviour
    {
        [SerializeField] protected CharacterController characterController = null;

        protected readonly Dictionary<AINode, NodeContext> Contexts = new Dictionary<AINode, NodeContext>();

        /// <summary>
        /// Shared DSO accessed for all nodes
        /// </summary>
        protected Dictionary<object, IDataStoreObject> SharedDso { get; } =
            new Dictionary<object, IDataStoreObject>();

        protected virtual void Awake()
        {
            if (characterController == null)
                characterController = GetComponent<CharacterController>();
        }

        /// <summary>
        ///  Get param of paramSource
        /// </summary>
        /// <param name="paramSource"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public abstract T GetParam<T>(ParamSource<T> paramSource);

        protected abstract NodeContext CreateNodeContext(AINode node);
    }

    [RequireComponent(typeof(CharacterController))]
    public abstract class AIGraphController<TGraph> : AIGraphController
        where TGraph : NodeGraph
    {
        [SerializeField] protected TGraph graph = null;

        protected virtual void Awake()
        {
            base.Awake();
            foreach (var node in graph.nodes.OfType<AINode>())
            {
                var context = CreateNodeContext(node);
                Contexts.Add(node, context);
                node.OnNodeInitialize(context);
            }
        }

        protected virtual void OnEnable()
        {
            foreach (var node in graph.nodes.OfType<AINode>())
            {
                node.OnNodeEnable(Contexts[node]);
            }
        }

        protected virtual void OnDisable()
        {
            foreach (var node in graph.nodes.OfType<AINode>())
            {
                node.OnNodeDisable(Contexts[node]);
            }
        }

        protected virtual void OnDestroy()
        {
            foreach (var node in graph.nodes.OfType<AINode>())
            {
                node.OnNodeDestroy(Contexts[node]);
            }
        }
    }
}