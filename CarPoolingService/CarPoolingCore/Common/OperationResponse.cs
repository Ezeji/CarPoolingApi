using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolingCore.Common
{
    public class OperationResponse
    {
        protected bool _forcedFailedResponse;
        public bool CompletedWithSuccess => OperationError == null && !_forcedFailedResponse;
        public OperationError OperationError { get; set; }

        public OperationResponse SetAsFailureResponse(OperationError operationError)
        {
            OperationError = operationError;
            _forcedFailedResponse = true;
            return this;
        }
    }

    public class OperationResponse<T> : OperationResponse
    {
        public OperationResponse() { }
        public OperationResponse(T result)
        {
            Result = result;
        }

        public T Result { get; set; }

        public new OperationResponse<T> SetAsFailureResponse(OperationError operationError)
        {
            base.SetAsFailureResponse(operationError);
            return this;
        }
    }

}
