using Discount.Grpc.Protos.Client;

namespace Basket.GrpcService
{
    public class DiscountGrpcService
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient _discountProtoServiceClient;

        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountProtoServiceClient)
        {
            _discountProtoServiceClient = discountProtoServiceClient;
        }

        public async Task<CouponModel> GetDiscountAsync(string productName)
        {
            var request = new GetDiscountRequest { ProductName = productName };
            var response = await _discountProtoServiceClient.GetDiscountAsync(request);
            return response;
        }
    }
}
