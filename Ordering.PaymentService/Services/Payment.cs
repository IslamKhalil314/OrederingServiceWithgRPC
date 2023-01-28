using Grpc.Core;
using Ordering.PaymentService.Models;
using Ordering.PaymentService.Protos;

namespace Ordering.PaymentService.Services
{
    public class Payment : ChargingService.ChargingServiceBase
    {
        /// <summary>
        /// Charges a customer for a payment
        /// </summary>
        /// <param name="request">The payment request containing customer ID and charge amount</param>
        /// <param name="context">The server call context</param>
        /// <returns>A PaymentResponse indicating the status of the charge</returns>
        public override Task<PaymentResponse> Charge(PaymentRequest request, ServerCallContext context)
        {
            PaymentResponse res = new PaymentResponse();
            var id = request.CustId;
            var user = DbMock.Users.FirstOrDefault(u => u.Id == id);

            // Check if user exists
            if (user == null)
            {
                res.Status = Protos.Status.Fail;
                res.Message = $"there is no such a user";
                return Task.FromResult(res);
            }

            // Check if user has enough balance
            if (user.Balance >= (decimal)request.Charge)
            {
                // Charge user and set response to success
                user.Balance -= (decimal)request.Charge;
                res.Status = Protos.Status.Success;
                res.Message = $"Charged Successfully";
                return Task.FromResult(res);
            }

            // Set response to fail if user does not have enough balance
            res.Status = Protos.Status.Fail;
            res.Message = $"User Does not have enough balance";
            return Task.FromResult(res);
        }
    }
}
