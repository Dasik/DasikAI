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
using Unity.Profiling;
using UnityEngine;
using CharacterController = DasikAI.Common.Controller.CharacterController;
using Object = UnityEngine.Object;

namespace DasikAI.Example.Data.Graph.Nodes.Blocks
{
    [AINode("Example/Blocks/Move To Position")]
    public class MoveToPosition : BTBlock
    {
        private static ProfilerMarker s_MoveToPositionProfilerMarker =
            new ProfilerMarker(nameof(MoveToPosition));

        private static ProfilerMarker s_GetPathTathTaskProfilerMarker =
            new ProfilerMarker(nameof(MoveToPosition) + "@Get path task");

        [Input(ShowBackingValue.Always, ConnectionType.Override)]
        public Vector2ParamSource ParamSource;

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

        public override void DoWork(NodeContext nodeContext)
        {
            s_MoveToPositionProfilerMarker.Begin((Object) nodeContext.CharacterController);
            try
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

                if (dso.PathEnumerator == null) //character is reached target point or is first call
                {
                    if (dso.PathTask == null)
                        dso.PathTask = GetPathTask(nodeContext);
                }
                else
                {
                    if (dso.PathEnumerator.Current == null && !dso.PathEnumerator.MoveNext()
                    ) //character is reached target point 
                    {
                        dso.PathEnumerator = null;
                        dso.PathTask = GetPathTask(nodeContext);
                    }
                    else
                    {
                        if ((agentPos.Position2d - dso.PathEnumerator.Current.Position).sqrMagnitude <
                            _minDistanceToCell
                        ) // character is around waypoint
                            if (!dso.PathEnumerator.MoveNext())
                            {
                                dso.PathEnumerator = null;
                                dso.PathTask = GetPathTask(nodeContext);
                                return;
                            }

                        if (dso.PathEnumerator.Current != null)
                            agentMov.OnMoveDirectionChanged(dso.PathEnumerator.Current.Position -
                                                            agentPos.Position2d); //move to next point
                        else agentMov.OnMoveDirectionChanged(Vector2.zero);
                    }
                }
            }
            finally
            {
                s_MoveToPositionProfilerMarker.End();
            }
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
            s_GetPathTathTaskProfilerMarker.Begin((Object) nodeContext.CharacterController);
            try
            {
                var param = nodeContext.GetParam(ParamSource);
                return PathFinding.Instance.GetPathAsync(
                    ((IControllerPosition2d) nodeContext.CharacterController).Position2d, param);
            }
            finally
            {
                s_GetPathTathTaskProfilerMarker.End();
            }
        }
    }
}