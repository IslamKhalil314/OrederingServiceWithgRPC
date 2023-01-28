using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ordering.Inventory.Protos;
using Ordering.PaymentService.Protos;
using Oredering.Dtos;
using Oredering.Services.Inventory;
using Oredering.Services.Payment;
using System.Text;

namespace Oredering.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly IInventoryService _inventoryService;

        public OrderController(IPaymentService paymentService, IInventoryService inventoryService)
        {
            _paymentService = paymentService;
            _inventoryService = inventoryService;
        }


        /// <summary>
        /// HTTP POST endpoint for placing an order.
        /// </summary>
        /// <param name="order">The order to place</param>
        /// <returns>The result message indicating if the order was placed successfully or not</returns>
        [HttpPost]
        public async Task<IActionResult> PlaceOrder(Order order)
        {
            // create payment request with customer id and total charge
            var paymentRequest = new PaymentRequest
            {
                CustId = order.UserId,
                Charge = (double)order.Items.Sum(x => x.Price * (decimal)x.Quantity)
            };

            // create inventory request with the items in the order
            var inventoryRequest = new CheckReq();
            order.Items.ForEach(x =>
            {
                inventoryRequest.Items.Add(new Ordering.Inventory.Protos.Item { ItemId = x.Id, Quantity = (int)x.Quantity });
            });

            // call the payment and inventory services in parallel
            var paymentTask = _paymentService.Charge(paymentRequest);
            var inventoryTask = _inventoryService.CheckInventory(inventoryRequest);
            await Task.WhenAll(paymentTask, inventoryTask);

            // get the results from the services
            var paymentResult = await paymentTask;
            var inventoryResult = await inventoryTask;

            // create a message builder to store the result message
            var messageBuilder = new StringBuilder();
            var isSuccess = true;

            // check the results from the services
            if (paymentResult.Status == Ordering.PaymentService.Protos.Status.Fail)
            {
                messageBuilder.AppendLine(paymentResult.Message);
                isSuccess = false;
            }
            if (inventoryResult.Status == Ordering.Inventory.Protos.Status.Fail)
            {
                messageBuilder.AppendLine(inventoryResult.Message);
                isSuccess = false;
            }

            // if both services returned success, add a success message
            if (isSuccess)
            {
                messageBuilder.AppendLine("Order placed successfully");
            }

            // return the result message
            return Ok(messageBuilder.ToString());
        }

    }
}
