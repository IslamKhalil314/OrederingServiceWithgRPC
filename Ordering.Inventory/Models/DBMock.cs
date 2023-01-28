namespace Ordering.Inventory.Models
{
    public static class DBMock
    {
        public static List<Item> Items = new List<Item>()
        {
            new Item { Id = 1 , Quantity = 500},
            new Item { Id = 2 , Quantity = 0},
            new Item { Id = 3 , Quantity = 10},
            new Item { Id = 4 , Quantity = 20},
            new Item { Id = 5 , Quantity = 1000},
            new Item { Id = 6 , Quantity = 600},
            new Item { Id = 7 , Quantity = 60},
            new Item { Id = 8 , Quantity = 50},
            new Item { Id = 9 , Quantity = 5200},
            new Item { Id = 10, Quantity = 2},
        };
    }
}
