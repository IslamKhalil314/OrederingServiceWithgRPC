using Grpc.Net.Client;
using Ordering.Inventory.Protos;

namespace Oredering.Services.Inventory
{
    public class InventoryService : IInventoryService
    {
        private Ordering.Inventory.Protos.InventoryService.InventoryServiceClient _client = null!;
        private Ordering.Inventory.Protos.InventoryService.InventoryServiceClient Client
        {
            get
            {

                if (_client is null)
                {
                    var channel = GrpcChannel.ForAddress("https://localhost:7040");
                    _client = new Ordering.Inventory.Protos.InventoryService.InventoryServiceClient(channel);
                }
                return _client;
            }
        }
        public async Task<CheckRes> CheckInventory(CheckReq req)
        {
            var res = await Client.CheckQuantityAsync(req);
            return res;
        }
    }
}
