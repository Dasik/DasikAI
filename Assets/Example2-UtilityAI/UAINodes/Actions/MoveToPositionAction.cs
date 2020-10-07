using System;
using Dasik.PathFinder;
using Dasik.PathFinder.Task;
using DasikAI.Common.Attributes;
using DasikAI.Common.Base;
using DasikAI.Common.Base.ParamSources;
using DasikAI.Common.Controller;
using DasikAI.Example.Controller;
using DasikAI.Example.Data.Graph.Nodes.DSO;
using DasikAI.UAI.Nodes.Base.Blocks;
using DasikAI.UAI.Nodes.Blocks;
using UnityEngine;
using XNode;
using CharacterController = DasikAI.Common.Controller.CharacterController;

namespace UAINodes.Actions
{
    [AINode("Example2/Actions/MoveToPosition")]
    public class MoveToPositionAction : UAIAction
    {
        [Node.InputAttribute(Node.ShowBackingValue.Always, Node.ConnectionType.Override, Node.TypeConstraint.None)]
        public Vector2ParamSource PositionPS;

        [Node.Output(backingValue = Node.ShowBackingValue.Unconnected)]
        public UAIAction NextAction;

        private float _minDistanceToCell = 0.75f;

        public override void OnNodeInitialize(NodeContext nodeContext)
        {
            nodeContext.CurrentDSO = new MoveToPlayerDSO();
            if (!(nodeContext.CharacterController is IControllerMovement))
                throw new ArgumentException(
                    $"{nameof(CharacterController)} must be instance of {nameof(IControllerMovement)}");
            if (!(nodeContext.CharacterController is IControllerPosition2d))
                throw new ArgumentException(
                    $"{nameof(CharacterController)} must be instance of {nameof(IControllerPosition2d)}");
        }

        public override void OnNodeEnter(NodeContext nodeContext)
        {
            var dso = (MoveToPlayerDSO) nodeContext.CurrentDSO;
            dso.PathTask = GetPathTask(nodeContext);
        }

        public override void PerformAction(UAINodeContext nodeContext)
        {
            var dso = (MoveToPlayerDSO) nodeContext.CurrentDSO;
            var agentMov = (IControllerMovement) nodeContext.CharacterController;
            var agentPos = (IControllerPosition2d) nodeContext.CharacterController;
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
                        dso.PathTask = GetPathTask(nodeContext);
                        break;
                    case PathTaskStatus.WaitingToRun:
                    case PathTaskStatus.Running:
                        break;
                    default:
                        throw new Exception(); //HOW!!!
                }
            }

            if (dso.PathEnumerator == null)//character is reached target point or is first call
            {
                if (dso.PathTask == null)
                    dso.PathTask = GetPathTask(nodeContext);
            }
            else
            {
                if (dso.PathEnumerator.Current == null && !dso.PathEnumerator.MoveNext())//character is reached target point 
                {
                    dso.PathEnumerator = null;
                    dso.PathTask = GetPathTask(nodeContext);
                }
                else
                {
                    if ((agentPos.Position2d - dso.PathEnumerator.Current.Position).sqrMagnitude < _minDistanceToCell)// character is around waypoint
                        if (!dso.PathEnumerator.MoveNext())
                        {
                            dso.PathEnumerator = null;
                            dso.PathTask = GetPathTask(nodeContext);
                        }

                    if (dso.PathEnumerator?.Current != null)
                        agentMov.OnMoveDirectionChanged(dso.PathEnumerator.Current.Position - agentPos.Position2d);//move to next point
                    else agentMov.OnMoveDirectionChanged(Vector2.zero);
                }
            }

            if (NextAction == null)
                return;
            nodeContext.PerformAction(NextAction);
        }

        public override void OnNodeExit(NodeContext nodeContext)
        {
            var dso = (MoveToPlayerDSO) nodeContext.CurrentDSO;
            dso.PathTask?.Dispose();
            dso.PathEnumerator?.Dispose();

            dso.PathTask = null;
            dso.PathEnumerator = null;

            ((IControllerMovement) nodeContext.CharacterController).OnMoveDirectionChanged(Vector2.zero);
        }

        public override void OnNodeDestroy(NodeContext nodeContext)
        {
            var dso = (MoveToPlayerDSO) nodeContext.CurrentDSO;
            dso.PathTask?.Dispose();
            dso.PathEnumerator?.Dispose();

            dso.PathTask = null;
            dso.PathEnumerator = null;
        }

        protected virtual SinglePathTask GetPathTask(NodeContext nodeContext)
        {
            var param = nodeContext.GetParam(PositionPS);
            return PathFinding.Instance.GetPathAsync(
                ((IControllerPosition2d) nodeContext.CharacterController).Position2d, param);
        }
    }
}