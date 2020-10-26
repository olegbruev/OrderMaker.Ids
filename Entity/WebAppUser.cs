using Microsoft.AspNetCore.Identity;
using System;

namespace Mtd.OrderMaker.Ids.Entity
{
    public class WebAppUser : IdentityUser<Guid>
    {
        [PersonalData]
        public string Title { get; set; }
    }
}
