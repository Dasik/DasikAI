using System;
using Dasik.PathFinder;
using Dasik.PathFinder.Task;
using DasikAI.Common.Controller;
using DasikAI.Common.Attributes;
using DasikAI.BT.Base.Blocks;
using DasikAI.BT.Controller;
using DasikAI.Common.Base;
using DasikAI.Common.Base.DSO;
using DasikAI.Common.Base.ParamSources;
using DasikAI.Example.Controller;
using DasikAI.Example.Data.Graph.Nodes.DSO;
using UnityEngine;

namespace DasikAI.Example.Data.Graph.Nodes.Blocks
{
    [AINode("Example/Blocks/MoveToPosition")]
    public class MoveToPosition : BTBlock
    {
        [Input(ShowBackingValue.Always, ConnectionType.Override, TypeConstraint.None)]
        public Vector2ParamSource ParamSource;

        private float _minDistanceToCell = 0.75f;

        public override void OnInitialize(Context context)
        {
            context.CurrentDSO = new MoveToPlayerDSO();
            if (!(context.AgentController is IControllerMovement))
                throw new ArgumentException(
                    $"{nameof(AgentController)} must be instance of {nameof(IControllerMovement)}");
            if (!(context.AgentController is IControllerPosition2d))
                throw new ArgumentException(
                    $"{nameof(AgentController)} must be instance of {nameof(IControllerPosition2d)}");
        }

        public override void OnEnter(Context context)
        {
            var dso = (MoveToPlayerDSO) context.CurrentDSO;
            dso.PathTask = GetPathTask(context);
        }

        public override void DoWork(Context context)
        {
            var dso = (MoveToPlayerDSO) context.CurrentDSO;
            var agentMov = (IControllerMovement) context.AgentController;
            var agentPos = (IControllerPosition2d) context.AgentController;
            if (dso.PathTask != null)
            {
                switch (dso.PathTask.Status)
                {
                    case PathTaskStatus.Completed:
                        dso.PathEnumerator = dso.PathTask.Path.GetEnumerator();
                        dso.PathTask = null;
                        break;
                    case PathTaskStatus.Canceled:
                        break;
                    case PathTaskStatus.Faulted:
                        dso.PathTask = GetPathTask(context);
                        break;
                    case PathTaskStatus.WaitingToRun:
                    case PathTaskStatus.Running:
                        break;
                    default:
                        throw new Exception(); //HOW!!!
                }
            }

            if (dso.PathEnumerator == null)
            {
                if (dso.PathTask == null)
                    dso.PathTask = GetPathTask(context);
            }
            else
            {
                if (dso.PathEnumerator.Current == null && !dso.PathEnumerator.MoveNext())
                {
                    dso.PathEnumerator = null;
                    dso.PathTask = GetPathTask(context);
                }
                else
                {
                    if ((agentPos.Position2d - dso.PathEnumerator.Current.Position).sqrMagnitude < _minDistanceToCell)
                        if (!dso.PathEnumerator.MoveNext())
                        {
                            dso.PathEnumerator = null;
                            dso.PathTask = GetPathTask(context);
                            return;
                        }

                    if (dso.PathEnumerator.Current != null)
                        agentMov.OnMoveDirectionChanged(dso.PathEnumerator.Current.Position - agentPos.Position2d);
                    else agentMov.OnMoveDirectionChanged(Vector2.zero);
                }
            }
        }

        public override void OnExit(Context context)
        {
            var dso = (MoveToPlayerDSO) context.CurrentDSO;
            dso.PathTask?.Dispose();
            dso.PathEnumerator?.Dispose();

            dso.PathTask = null;
            dso.PathEnumerator = null;

            ((IControllerMovement) context.AgentController).OnMoveDirectionChanged(Vector2.zero);
        }

        public override void OnDispose(Context context)
        {
            var dso = (MoveToPlayerDSO) context.CurrentDSO;
            dso.PathTask?.Dispose();
            dso.PathEnumerator?.Dispose();

            dso.PathTask = null;
            dso.PathEnumerator = null;
        }

        protected virtual SinglePathTask GetPathTask(Context context)
        {
            var param = context.GetParam(ParamSource);
            return PathFinding.Instance.GetPathAsync(((IControllerPosition2d) context.AgentController).Position2d, param);
        }
    }
}