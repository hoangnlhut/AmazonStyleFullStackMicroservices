using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Google.Rpc;
using Grpc.Core;

namespace Discount.Extensions
{
    public static class GrpcErrorHelper
    {

        public static RpcException CreateRpcValidationException(Dictionary<string, string> fieldErrors)
        {
            var fieldViolations = new List<BadRequest.Types.FieldViolation>();

            foreach (var error in fieldErrors)
            {
                fieldViolations.Add(new BadRequest.Types.FieldViolation
                {
                    Field = error.Key,
                    Description = error.Value
                });
            }

            //now add bad request
            var badRequest = new BadRequest();
            badRequest.FieldViolations.AddRange(fieldViolations);

            var status = new Google.Rpc.Status()
            {
                Code = (int)StatusCode.InvalidArgument,
                Message = "Validation failed for the request.",
                Details = { Any.Pack(badRequest) }
            };

            var trailers = new Metadata
            {
                { "grpc-status-details-bin", status.ToByteArray() }
            };

            return new RpcException(new Grpc.Core.Status(StatusCode.InvalidArgument, status.Message), trailers);
        }
    }
}
