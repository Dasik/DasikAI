using System.Collections.Generic;
using DasikAI.Common.Controller;
using DasikAI.Common.Base.DSO;
using DasikAI.Common.Base.ParamSources;

namespace DasikAI.Common.Base
{
    public class NodeContext
    {
        public ICharacterController CharacterController { get; }
        public IDataStoreObject CurrentDSO { get; set; }
        public Dictionary<object, IDataStoreObject> SharedDSO { get; }
        protected AIGraphController GraphController { get; }

        public NodeContext(ICharacterController characterController, Dictionary<object, IDataStoreObject> sharedDso,
            AIGraphController graphController)
        {
            SharedDSO = sharedDso;
            GraphController = graphController;
            CharacterController = characterController;
        }

        /// <summary>
        ///  Get param of paramSource
        /// </summary>
        /// <param name="paramSource"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetParam<T>(ParamSource<T> paramSource)
        {
            return GraphController.GetParam(paramSource);
        }
    }
}