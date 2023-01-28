using Ordering.PaymentService.Protos;

namespace Oredering.Services.Payment
{
    public interface IPaymentService
    {
        public Task<PaymentResponse> Charge(PaymentRequest request);
    }
}
