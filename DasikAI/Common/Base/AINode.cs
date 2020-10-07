using System;
using System.Linq;
using System.Reflection;
using XNode;

namespace DasikAI.Common.Base
{
    public abstract class AINode : Node
    {
        public override void OnCreateConnection(NodePort from, NodePort to) //like dependency injection
        {
            base.OnCreateConnection(from, to);
            // SetFieldValue(from.node == this ? from : to);
            if (from.node == this)
                SetFieldValue(from, to.node);
            else
                SetFieldValue(to, from.node);
        }

        public override void OnRemoveConnection(NodePort port) //like dependency injection
        {
            base.OnRemoveConnection(port);
            SetFieldValue(port, null);
        }

        protected void SetFieldValue(NodePort port, Node value)
        {
            var spaceIndex = port.fieldName.IndexOf(" ");
            var field = GetType().GetField(port.fieldName.Contains(" ")
                    ? port.fieldName.Substring(0, spaceIndex)
                    : port.fieldName,
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (field.FieldType.IsArray)
            {
                Array fieldValue = (Array) field.GetValue(this);

                if (port.IsInput)
                {
                    var connectedNodes = port.GetConnections().Select(nodePort => nodePort.node).ToArray();

                    if (!fieldValue.Length.Equals(connectedNodes.Length))
                        Resize(ref fieldValue, connectedNodes.Length);

                    Array.Copy(connectedNodes, fieldValue, connectedNodes.Length);

                    field.SetValue(this, fieldValue);
                }
                else
                {
                    var elementIndex =  int.Parse(port.fieldName.Substring(spaceIndex));
                    fieldValue.SetValue(value, elementIndex);
                }
            }
            else
            {
                field.SetValue(this, value);
            }
        }

        // protected void SetFieldValue(NodePort port)
        // {
        //     var spaceIndex = port.fieldName.IndexOf(" ");
        //     var field = GetType().GetField(port.fieldName.Contains(" ")
        //             ? port.fieldName.Substring(0, spaceIndex)
        //             : port.fieldName,
        //         BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        //     if (field.FieldType.IsArray)
        //     {
        //         if (port.IsInput)
        //         {
        //             var connectedNodes = port.GetConnections().Select(nodePort => nodePort.node).ToArray();
        //
        //             Array fieldValue = (Array) field.GetValue(this);
        //             if (!fieldValue.Length.Equals(connectedNodes.Length))
        //                 Resize(ref fieldValue, connectedNodes.Length);
        //
        //             Array.Copy(connectedNodes, fieldValue, connectedNodes.Length);
        //
        //             field.SetValue(this, fieldValue);
        //         }
        //         else
        //         {
        //             Array fieldValue = (Array) field.GetValue(this);
        //             if (fieldValue.GetType().GetElementType().IsAssignableFrom(port.ValueType.GetElementType()))
        //             {
        //                 var result = port.GetConnections().Select(nodePort => nodePort.node).FirstOrDefault();
        //                 var portIndex = int.Parse(port.fieldName.Substring(spaceIndex));
        //
        //                 var portsCount = port.node.Ports.Count(nodePort => nodePort.fieldName.StartsWith(field.Name)) -
        //                                  1;
        //                 if (!fieldValue.Length.Equals(portsCount))
        //                     Resize(ref fieldValue, portsCount);
        //
        //                 fieldValue.SetValue(result, portIndex);
        //                 field.SetValue(this, fieldValue);
        //             }
        //         }
        //     }
        //     else
        //     {
        //         var result = port.GetConnections().Select(nodePort => nodePort.node).FirstOrDefault();
        //         field.SetValue(this, result);
        //     }
        // }

        private static void Resize(ref Array array, int newSize)
        {
            Type elementType = array.GetType().GetElementType();
            Array newArray = Array.CreateInstance(elementType, newSize);
            Array.Copy(array, newArray, Math.Min(array.Length, newArray.Length));
            array = newArray;
        }

        /// <summary>
        /// Called in Awake method of graph controller. Use it for create DataStoreObject
        /// </summary>
        /// <param name="nodeContext"></param>
        public virtual void OnNodeInitialize(NodeContext nodeContext)
        {
        }

        /// <summary>
        /// Called in OnEnable method of graph controller
        /// </summary>
        /// <param name="nodeContext"></param>
        public virtual void OnNodeEnable(NodeContext nodeContext)
        {
        }

        /// <summary>
        /// Called when graph controller enter in node. Use it for prepare data for calculations.
        /// </summary>
        /// <param name="nodeContext"></param>
        public virtual void OnNodeEnter(NodeContext nodeContext)
        {
        }

        /// <summary>
        /// Called when graph controller exit from node. Use it for cleanup data and stop actions
        /// </summary>
        /// <param name="nodeContext"></param>
        public virtual void OnNodeExit(NodeContext nodeContext)
        {
        }

        /// <summary>
        /// Called in OnDisable method of graph controller
        /// </summary>
        /// <param name="nodeContext"></param>
        public virtual void OnNodeDisable(NodeContext nodeContext)
        {
        }

        /// <summary>
        /// Called in OnDestroyMethod of graph controller
        /// </summary>
        /// <param name="nodeContext"></param>
        public virtual void OnNodeDestroy(NodeContext nodeContext)
        {
        }
    }
}