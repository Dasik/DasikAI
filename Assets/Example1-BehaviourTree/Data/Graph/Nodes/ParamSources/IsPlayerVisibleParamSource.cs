using System;
using DasikAI.Common.Attributes;
using DasikAI.Common.Base;
using DasikAI.Common.Base.DSO;
using DasikAI.Common.Base.ParamSources;
using DasikAI.Example.Controller;
using UnityEngine;
using CharacterController = DasikAI.Common.Controller.CharacterController;

namespace DasikAI.Example.Data.Graph.Nodes.ParamSources
{
    [AINode("Example/Params/Is player visible")]
    public class IsPlayerVisibleParamSource : BooleanParamSource
    {
        public override void OnNodeInitialize(NodeContext nodeContext)
        {
            if (!(nodeContext.CharacterController is IControllerPosition2d))
                throw new ArgumentException(
                    $"{nameof(CharacterController)} must be instance of {nameof(IControllerPosition2d)}");
        }

        public override bool GetParam(NodeContext nodeContext)
        {
            var characterController = nodeContext.CharacterController as IControllerPosition2d;

            var hit = Physics2D.Raycast(characterController.Position2d,
                PlayerController.Instance.Position2d - characterController.Position2d);
            return hit.transform == PlayerController.Instance.transform;
        }
    }
}