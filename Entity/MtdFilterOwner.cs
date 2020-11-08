using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Mtd.OrderMaker.Ids.Entity
{
    public class MtdFilterOwner
    {
        [Key]
        public Guid FilterId { get; set; }
        public Guid UserId { get; set; }
    }
}
