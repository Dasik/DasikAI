using System.Collections.Generic;
using System.Linq;
using DasikAI.BT.Attributes;
using DasikAI.BT.CustomTypes;
using DasikAI.BT.Base.Blocks;
using DasikAI.BT.Nodes.DSO;
using DasikAI.Common.Base;

namespace DasikAI.BT.Nodes.Checks
{
    [BTNode("Checks/StateIs")]
    public class StateIs : BTBlockCheck
    {
        [Output(dynamicPortList = true, backingValue = ShowBackingValue.Always,
            connectionType = ConnectionType.Multiple)]
        public StatesEnum[] States;

        public override IEnumerable<AINode> Next(NodeContext nodeContext)
        {
            var stateDSO = ((StateDSO) nodeContext.SharedDSO[typeof(StateDSO)]);
            var currentState = stateDSO.State;
            var result = Enumerable.Empty<AINode>();
            for (var i = 0; i < States.Length; i++)
            {
                var state = States[i].SelectedValue;
                if (state.Equals(currentState))
                    result = result.Union(
                        GetPort(nameof(States) + " " + i)
                            .GetConnections()
                            .Select(port => port.node as AINode));
            }

            return result;
        }
    }
}