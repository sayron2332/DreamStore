using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamStore.Core.Entites.Order
{
    public class AppOrder
    {
        public int Id { get; set; }
        public int UserId { get; set; } 
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public ICollection<AppOrderItem> Items { get; set; } = null!;
        public decimal TotalPrice => Items.Sum(x => x.UnitPrice * x.Quantity);
    }
    public enum OrderStatus
    {
        Pending,
        Paid,
        Cancelled,
        Shipped
    }
}
