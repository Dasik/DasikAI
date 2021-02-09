using System;
using DasikAI.Common.Attributes;

namespace DasikAI.BT.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class BTNodeAttribute : AINodeAttribute
    {
        public BTNodeAttribute(string editorName) : base(editorName)
        {
        }
    }
}