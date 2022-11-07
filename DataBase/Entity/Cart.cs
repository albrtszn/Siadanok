using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace DataBase.Entity
{
    public class Cart
    {
        public string CartId { get; set; }
        public List<CartItem> ListCartItems { get; set; }

        private EFDBContext context;
        public Cart(EFDBContext context, int id)
        {
            this.context = context;
        }

        /*public static Cart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            string cartId = session.TryGetValue("CartId", Guid.NewGuid().ToString());
        }*/
    }
}
