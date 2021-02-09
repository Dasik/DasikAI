using System;
using DasikAI.Common.Attributes;

namespace DasikAI.UAI.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class UAINodeAttribute : AINodeAttribute
    {
        public UAINodeAttribute(string editorName) : base(editorName)
        {
        }
    }
}