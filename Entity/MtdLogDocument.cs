using System;
using System.Collections.Generic;
using System.Text;

namespace Mtd.OrderMaker.Ids.Entity
{
    public class MtdLogDocument
    {
        public Guid Id { get; set; }
        public Guid StoreId { get; set; }
        public Guid UserId { get; set; }
        public DateTime TimeCh { get; set; }
    }
}
