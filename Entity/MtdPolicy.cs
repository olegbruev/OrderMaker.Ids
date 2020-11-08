using System;
using System.Collections.Generic;
using System.Text;

namespace Mtd.OrderMaker.Ids.Entity
{
    public class MtdPolicy
    {
        public MtdPolicy()
        {
            MtdPolicyForms = new HashSet<MtdPolicyForm>();
            MtdPolicyParts = new HashSet<MtdPolicyPart>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }


        public virtual ICollection<MtdPolicyForm> MtdPolicyForms { get; set; }
        public virtual ICollection<MtdPolicyPart> MtdPolicyParts { get; set; }

    }
}
