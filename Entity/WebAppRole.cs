using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mtd.OrderMaker.Ids.Entity
{
    public class WebAppRole : IdentityRole<Guid>
    {
        public string Title { get; set; }
        public int Seq { get; set; }

    }
}
