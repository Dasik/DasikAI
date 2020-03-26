using XNode;

namespace DasikAI.Common.Base
{
    public abstract class AINode : Node
    {
        public virtual void OnInitialize(Context context)
        {
        }

        protected virtual void OnEnable()
        {
            base.OnEnable();
        }
        
        public virtual void OnEnable(Context context)
        {
        }

        public virtual void OnEnter(Context context)
        {
        }

        public virtual void OnExit(Context context)
        {
        }

        protected virtual void OnDisable()
        {
        }

        public virtual void OnDisable(Context context)
        {
        }

        public virtual void OnDispose(Context context)
        {
        }
    }
}