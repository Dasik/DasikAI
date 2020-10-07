using System.Collections.Generic;
using DasikAI.Common.Base;
using DasikAI.Common.Base.DSO;
using DasikAI.Common.Controller;
using DasikAI.UAI.Controller;
using DasikAI.UAI.Nodes.Base.Blocks;

namespace DasikAI.UAI.Nodes.Blocks
{
    public class UAINodeContext : NodeContext
    {
        public UAINodeContext(ICharacterController characterController, Dictionary<object, IDataStoreObject> sharedDso,
            AIGraphController graphController) : base(characterController, sharedDso, graphController)
        {
        }

        public virtual void PerformAction(UAIAction action)
        {
            ((UAIGraphController) GraphController).PerformAction(action);
        }
    }
}