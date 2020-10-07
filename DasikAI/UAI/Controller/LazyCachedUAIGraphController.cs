using System.Collections.Generic;
using System.Linq;
using DasikAI.Common.Base;
using DasikAI.Common.Base.ParamSources;
using DasikAI.UAI.Nodes.Base.Blocks;
using DasikAI.UAI.Nodes.Blocks;
using Unity.Profiling;
#if UNITY_EDITOR
using UnityEditor;
using XNodeEditor;

#endif

namespace DasikAI.UAI.Controller
{
    public class LazyCachedUAIGraphController : UAIGraphController
    {
        private static ProfilerMarker s_ScoreActionsProfilerMarker =
            new ProfilerMarker(nameof(LazyCachedUAIGraphController) + "@Calculate scores");

        private static ProfilerMarker s_PerformBestActionProfilerMarker =
            new ProfilerMarker(nameof(LazyCachedUAIGraphController) + "@Perform best action");

        private static ProfilerMarker s_UpdateEditorProfilerMarker =
            new ProfilerMarker(nameof(LazyCachedUAIGraphController) + "@Update editor");

        private static ProfilerMarker s_CleanupProfilerMarker =
            new ProfilerMarker(nameof(LazyCachedUAIGraphController) + "@Cleanup");

        private static ProfilerMarker s_GetParamProfilerMarker =
            new ProfilerMarker(nameof(LazyCachedUAIGraphController) + "@Get param");

        private UAIAction _activeAction;
        private readonly List<UAIAction> _actions = new List<UAIAction>();
        private readonly Dictionary<ParamSource, object> _cachedValues = new Dictionary<ParamSource, object>();

        protected override void Awake()
        {
            base.Awake();
            foreach (var actionNode in graph.nodes.OfType<UAIAction>())
            {
                if (_actions.Contains(actionNode))
                    continue;
                if (actionNode.Inputs.Any(port => port.Connection?.node is UAIAction))//get all actions without parent actions
                    continue;
                _actions.Add(actionNode);
            }
        }

        protected virtual void Update()
        {
            UAIAction bestAction;
            using (s_ScoreActionsProfilerMarker.Auto())
            {
                bestAction = _actions[0];
                var bestScore = float.MinValue;
                foreach (var action in _actions)
                {
                    var score = this.GetParam(action.Scorer);
                    if (float.IsNaN(score))
                        continue;
                    if (score < bestScore)
                        continue;
                    bestAction = action;
                    bestScore = score;
                }
            }

            using (s_PerformBestActionProfilerMarker.Auto())
            {
                if (bestAction != _activeAction)
                {
                    if (!ReferenceEquals(_activeAction, null))
                        _activeAction.OnNodeExit(Contexts[_activeAction]);
                    _activeAction = bestAction;
                    _activeAction.OnNodeEnter(Contexts[_activeAction]);
                }

                _activeAction.PerformAction((UAINodeContext) Contexts[_activeAction]);
            }

#if UNITY_EDITOR
            using (s_UpdateEditorProfilerMarker.Auto())
            {
                if (NodeEditorWindow.current != null)
                {
                    if (Selection.activeGameObject == gameObject)
                        NodeEditorWindow.current.Repaint();
                }
            }
#endif
            using (s_CleanupProfilerMarker.Auto())
            {
                foreach (var node in _cachedValues.Keys)
                {
                    var context = Contexts[node];
                    node.OnNodeExit(context);
                }

                _cachedValues.Clear();
            }
        }

        public override T GetParam<T>(ParamSource<T> paramSource)
        {
            using (s_GetParamProfilerMarker.Auto())
            {
                if (_cachedValues.ContainsKey(paramSource))
                    return (T) _cachedValues[paramSource];

                var context = Contexts[paramSource];
                paramSource.OnNodeEnter(context);
                var value = paramSource.GetParam(context);
                _cachedValues.Add(paramSource, value);
                return value;
            }
        }

        protected override NodeContext CreateNodeContext(AINode node)
        {
            return new UAINodeContext(characterController, SharedDso, this);
        }

        public override bool IsActionActive(UAIAction action)
        {
            return Equals(action, _activeAction);
        }
    }
}