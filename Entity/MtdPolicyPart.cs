using System;
using System.Collections.Generic;
using System.Text;

namespace Mtd.OrderMaker.Ids.Entity
{
    public class MtdPolicyPart
    {
        public Guid Id { get; set; }
        public Guid PolicyId { get; set; }
        public Guid PartId { get; set; }
        public bool Create { get; set; }
        public bool Edit { get; set; }
        public bool View { get; set; }

        public virtual MtdPolicy MtdPolicy { get; set; }
    }
}
