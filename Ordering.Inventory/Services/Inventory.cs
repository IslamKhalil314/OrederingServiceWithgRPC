using Grpc.Core;
using Ordering.Inventory.Models;
using Ordering.Inventory.Protos;
using System.Text;
using Status = Ordering.Inventory.Protos.Status;

namespace Ordering.Inventory.Services
{
    public class Inventory : InventoryService.InventoryServiceBase
    {

        /// <summary>
        /// Check the availability of a list of items
        /// </summary>
        /// <param name="request">A request object containing a list of items to check</param>
        /// <param name="context">The context of the gRPC call</param>
        /// <returns>A task that contains a CheckRes object with the status of the availability check and a message</returns>
        public override Task<CheckRes> CheckQuantity(CheckReq request, ServerCallContext context)
        {
            // Initialize the response object with success status
            var res = new CheckRes { Status = Status.Success };
            // Initialize a StringBuilder to store the message
            var messageBuilder = new StringBuilder();
            // Get the list of items from the request object
            var items = request.Items;

            // Iterate through each item in the list
            foreach (var item in items)
            {
                // Check if the item exists in the inventory
                var itemInInventory = DBMock.Items.FirstOrDefault(x => x.Id == item.ItemId);

                // If the item does not exist in the inventory
                if (itemInInventory == null)
                {
                    // Update the response status to fail
                    res.Status = Status.Fail;
                    // Append a message to the message builder
                    messageBuilder.AppendLine($"We don't have the item with id : {item.ItemId}");
                }
                // If the item exists but is out of stock
                else if (itemInInventory.Quantity == 0)
                {
                    // Update the response status to fail
                    res.Status = Status.Fail;
                    // Append a message to the message builder
                    messageBuilder.AppendLine($"item with id : {item.ItemId} Out of Stock");
                }
                // If the item exists but the requested quantity is more than the available quantity
                else if (itemInInventory.Quantity < item.Quantity)
                {
                    // Update the response status to fail
                    res.Status = Status.Fail;
                    // Append a message to the message builder
                    messageBuilder.AppendLine($"there is only {itemInInventory.Quantity} of item {itemInInventory.Id}");
                }
                else
                {
                    // Reduce the amount in inventory
                    itemInInventory.Quantity -= item.Quantity;
                }
            }

            // If the status is not fail, append a message indicating all items are available
            if (res.Status != Status.Fail)
                messageBuilder.AppendLine($"All items available");

            // Set the message of the response object
            res.Message = messageBuilder.ToString();
            // Return the response object as a task
            return Task.FromResult(res);
        }


    }
}
