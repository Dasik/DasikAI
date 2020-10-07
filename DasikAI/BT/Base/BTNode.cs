using System.Collections.Generic;
using DasikAI.Common.Base;
using XNode;

namespace DasikAI.BT.Base
{
    public abstract class BTNode : AINode
    {
        public override object GetValue(NodePort port) //like dependency injection
        {
            return this;
        }

        /// <summary>
        /// get next nodes in tree
        /// </summary>
        /// <param name="nodeContext"></param>
        /// <returns>Next nodes in tree or null</returns>
        public abstract IEnumerable<AINode> Next(NodeContext nodeContext);
    }
}