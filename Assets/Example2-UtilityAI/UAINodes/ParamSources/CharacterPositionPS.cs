using System;
using DasikAI.Common.Attributes;
using DasikAI.Common.Base;
using DasikAI.Common.Base.ParamSources;
using DasikAI.Example.Controller;
using UnityEngine;

namespace DasikAI.Example.UAINodes.ParamSources
{
    [AINode("Example2/Param Sources/Character Position")]
    public class CharacterPositionPS : Vector2ParamSource
    {
        public override void OnNodeInitialize(NodeContext nodeContext)
        {
            if (!(nodeContext.CharacterController is IControllerPosition2d))
                throw new ArgumentException($"{nameof(CharacterController)} must be instance of {nameof(IControllerPosition2d)}");
        }

        public override Vector2 GetParam(NodeContext nodeContext)
        {
            var characterPosController = (IControllerPosition2d) nodeContext.CharacterController;
            return characterPosController.Position2d;
        }
    }
}