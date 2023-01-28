namespace Oredering.Dtos
{
    public class Order
    {
        public Order()
        {
            Items = new();
        }
        public int Id { get; set; }
        public int UserId { get; set; }

        public List<Item> Items { get; set; } = null!;
    }
}
