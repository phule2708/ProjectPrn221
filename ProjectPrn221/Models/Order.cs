using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectPrn221.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int OrderId { get; set; }
        public string? CustomerId { get; set; }
        public int? EmployeeId { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyy}")]
        public DateTime? OrderDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyy}")]
        public DateTime? RequiredDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyy}")]
        public DateTime? ShippedDate { get; set; }
        public decimal? Freight { get; set; }
        public string? ShipName { get; set; }
        public string? ShipAddress { get; set; }
        public string? ShipCity { get; set; }
        public string? ShipRegion { get; set; }
        public string? ShipPostalCode { get; set; }
        public string? ShipCountry { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual Employee? Employee { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
