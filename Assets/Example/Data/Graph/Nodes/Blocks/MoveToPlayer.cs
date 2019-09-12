using System;
using Dasik.PathFinder;
using Dasik.PathFinder.Task;
using DasikAI.Example.Controller;
using DasikAI.Example.Data.Graph.Nodes.DSO;
using DasikAI.Scripts.Controller;
using DasikAI.Scripts.Data.Graph.Attributes;
using DasikAI.Scripts.Data.Graph.Base;
using UnityEngine;

namespace DasikAI.Example.Data.Graph.Nodes.Blocks
{
	[AINode("Blocks/MoveToPlayer")]
	public class MoveToPlayer : AIBlock
	{
		[SerializeField] private float _minDistanceToCell;
		public override IDataStoreObject Initialize(AgentController controller)
		{
			base.Initialize(controller);
			return new MoveToPlayerDSO();
		}

		public override IDataStoreObject Enter(IDataStoreObject dataStoreObject, AgentController controller)
		{
			base.Enter(dataStoreObject, controller);
			if (!(controller is IMovementController))
			{
				throw new ArgumentException("AgentController must be instance of IMovementController");
			}

			var dso = (MoveToPlayerDSO)dataStoreObject;
			dso.PathTask = GetPathTask(dso, controller);
			return base.Enter(dataStoreObject, controller);
		}

		public override IDataStoreObject DoWork(IDataStoreObject dataStoreObject, AgentController controller)
		{
			if (!(controller is IMovementController))
			{
				Debug.LogError("AgentController must be instance of IMovementController", controller);
			}

			var dso = (MoveToPlayerDSO)dataStoreObject;
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
						dso.PathTask = GetPathTask(dso, controller);
						break;
					case PathTaskStatus.WaitingToRun:
					case PathTaskStatus.Running:
						break;
					default:
						throw new Exception();//HOW!!!
				}
			}

			if (dso.PathEnumerator == null)
			{
				if (dso.PathTask == null)
					dso.PathTask = GetPathTask(dso, controller);
			}
			else
			{
				if (dso.PathEnumerator.Current == null && !dso.PathEnumerator.MoveNext())
				{
					dso.PathEnumerator = null;
					dso.PathTask = GetPathTask(dso, controller);
				}
				else
				{
					if ((controller.Position2d - dso.PathEnumerator.Current.Position).sqrMagnitude < _minDistanceToCell)
						if (!dso.PathEnumerator.MoveNext())
						{
							dso.PathEnumerator = null;
							dso.PathTask = GetPathTask(dso, controller);
							return dso;
						}

					if (dso.PathEnumerator.Current != null) ((IMovementController)controller).OnMoveDirectionChanged(dso.PathEnumerator.Current.Position - controller.Position2d);
					else ((IMovementController)controller).OnMoveDirectionChanged(Vector2.zero);
				}
			}

			return dso;
		}

		public override IDataStoreObject Exit(IDataStoreObject dataStoreObject, AgentController controller)
		{
			base.Exit(dataStoreObject, controller);
			if (!(controller is IMovementController))
			{
				Debug.LogError("AgentController must be instance of IMovementController", controller);
			}

			var dso = (MoveToPlayerDSO)dataStoreObject;
			dso.PathTask?.Dispose();
			dso.PathEnumerator?.Dispose();

			dso.PathTask = null;
			dso.PathEnumerator = null;

			((IMovementController)controller).OnMoveDirectionChanged(Vector2.zero);
			return base.Exit(dataStoreObject, controller);
		}

		public override void Dispose(IDataStoreObject dataStoreObject)
		{
			var dso = (MoveToPlayerDSO)dataStoreObject;
			dso.PathTask?.Dispose();
			dso.PathEnumerator?.Dispose();

			dso.PathTask = null;
			dso.PathEnumerator = null;
		}

		protected virtual SinglePathTask GetPathTask(IDataStoreObject dataStoreObject, AgentController controller)
		{
			return PathFinding.Instance.GetPathAsync(controller.Transform.position, PlayerController.Instance.Position2d);
		}
	}
}
