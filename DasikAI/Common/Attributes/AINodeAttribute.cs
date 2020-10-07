using System;

namespace DasikAI.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class AINodeAttribute : Attribute
    {
        public string EditorName { get; }

        public AINodeAttribute(string editorName)
        {
            this.EditorName = editorName;
        }
    }
}