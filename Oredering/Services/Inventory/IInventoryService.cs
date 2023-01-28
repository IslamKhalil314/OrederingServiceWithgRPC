using Ordering.Inventory.Protos;

namespace Oredering.Services.Inventory
{
    public interface IInventoryService
    {
        public Task<CheckRes> CheckInventory(CheckReq req);
    }
}
