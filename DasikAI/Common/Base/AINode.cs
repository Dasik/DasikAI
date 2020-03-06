using XNode;

namespace DasikAI.Common.Base
{
    public abstract class AINode : Node
    {
        public virtual void Initialize(Context context)
        {
        }

        public virtual void Enable(Context context)
        {
        }

        public virtual void Enter(Context context)
        {
        }

        public virtual void Exit(Context context)
        {
        }

        public virtual void Disable(Context context)
        {
        }

        public virtual void Dispose(Context context)
        {
        }
    }
}