using Grpc.Net.Client;
using Ordering.PaymentService.Protos;

namespace Oredering.Services.Payment
{
    public class PaymentService : IPaymentService
    {
        private ChargingService.ChargingServiceClient _client = null!;
        private ChargingService.ChargingServiceClient Client
        {
            get
            {

                if (_client is null)
                {
                    var channel = GrpcChannel.ForAddress("https://localhost:7146");
                    _client = new ChargingService.ChargingServiceClient(channel);
                }
                return _client;
            }
        }
        public async Task<PaymentResponse> Charge(PaymentRequest request)
        {
            var res = await Client.ChargeAsync(request);
            return res;
        }
    }
}
