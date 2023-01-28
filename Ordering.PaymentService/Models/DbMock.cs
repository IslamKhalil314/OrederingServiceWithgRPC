namespace Ordering.PaymentService.Models
{
    public static class DbMock
    {
        public static List<User> Users = new List<User>
        {
            new User   { Id = 1 , Balance =  10000},
            new User   { Id = 2 , Balance =  100.6m},
            new User   { Id = 3 , Balance =  10643m},
            new User   { Id = 4 , Balance =  0m},
            new User   { Id = 5 , Balance =  15893m},
            new User   { Id = 6 , Balance =  1m},
            new User   { Id = 7 , Balance =  100m},
            new User   { Id = 8 , Balance =  10m},
            new User   { Id = 9 , Balance =  50m},
        };                                     
    }
}
