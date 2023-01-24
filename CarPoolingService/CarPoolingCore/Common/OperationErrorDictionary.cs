using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolingCore.Common
{
    public static class OperationErrorDictionary
    {
        public static class CarPoolingErrorMessage
        {
            public static OperationError Failed() =>
               new OperationError("There is a failure in the request format, expected headers, or the payload can't be unmarshalled");

            public static OperationError EntityIsNull() =>
               new OperationError("The group is waiting to be assigned to a car");

            public static OperationError EntityNotFound() =>
               new OperationError("The group is not to be found");

            public static OperationError UnprocessableEntity() =>
               new OperationError("The journey was already cancelled or completed");
        }

        public static class GenericErrorMessage
        {
            public static OperationError Failed() =>
               new OperationError("An unhandled errror has occured while processing your request");

            public static OperationError EntityIsNull() =>
               new OperationError("Supplied entity is null or supplied list of entities is empty");

            public static OperationError EntityNotFound() =>
               new OperationError("The requested resource was not found. Verify that the supplied Id is correct");

            public static OperationError UnprocessableEntity() =>
               new OperationError("The action cannot be processed");
        }
    }
}
