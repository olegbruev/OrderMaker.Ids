using System;
using System.Collections.Generic;
using System.Text;

namespace Mtd.OrderMaker.Ids.Entity
{
    public class MtdPolicyForm
    {
        public Guid Id { get; set; }
        public Guid PolicyId { get; set; }
        public Guid FormId { get; set; }
        public bool Create { get; set; }
        public bool EditAll { get; set; }
        public bool EditGroup { get; set; }
        public bool EditOwn { get; set; }
        public bool ViewAll { get; set; }
        public bool ViewGroup { get; set; }
        public bool ViewOwn { get; set; }
        public bool DeleteAll { get; set; }
        public bool DeleteGroup { get; set; }
        public bool DeleteOwn { get; set; }
        public bool ChangeOwner { get; set; }
        public bool ChangeDate { get; set; }
        public bool ExportToExcel { get; set; }

        public virtual MtdPolicy MtdPolicy { get; set; }
    }
}
