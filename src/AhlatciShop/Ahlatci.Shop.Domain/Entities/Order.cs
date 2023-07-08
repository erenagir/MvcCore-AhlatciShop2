﻿using Ahlatci.Shop.Domain.Common;

namespace Ahlatci.Shop.Domain.Entities
{
    public class Order:AuditableEntity
    {
        public int CustomerId { get; set; }
        public int AddressId { get; set; }
        public DateTime? OrderDate { get; set; }
        public OrderStatus Status { get; set; }
        public Customer Customer { get; set; }
        public Address Address { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }

    }

    public enum OrderStatus
    {
        orderCreated=1,
        PaymentComplated,
        Pedding,
        OrderDelivering,
        CargoDelivered,
        Complated
    }
}