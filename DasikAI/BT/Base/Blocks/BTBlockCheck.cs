using System.Collections.Generic;
using System.Linq;
using DasikAI.Common.Base.DSO;
using DasikAI.Common.Controller;
using DasikAI.BT.Base;
using DasikAI.Common.Base;

namespace DasikAI.BT.Base.Blocks
{
    public abstract class BTBlockCheck : BTNode
    {
        [Input(ShowBackingValue.Always)] public AINode[] Parent = new AINode[1];

        /// <summary>
        /// get next node in tree
        /// </summary>
        /// <param name="nodeContext"></param>
        /// <returns>Next node in tree or null</returns>
        protected virtual AINode NextOne(NodeContext nodeContext)
        {
            return null;
        }

        /// <summary>
        /// get next nodes in tree
        /// </summary>
        /// <param name="nodeContext"></param>
        /// <returns>Next nodes in tree or null</returns>
        public override IEnumerable<AINode> Next(NodeContext nodeContext)
        {
            var nextOne = NextOne(nodeContext);
            return ReferenceEquals(nextOne, null) ? null : Enumerable.Repeat(nextOne, 1);
        }
    }
}