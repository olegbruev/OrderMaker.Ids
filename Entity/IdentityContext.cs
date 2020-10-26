using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Mtd.OrderMaker.Ids.Entity
{
    public class IdentityContext : IdentityDbContext<WebAppUser,WebAppRole,Guid>
    {        

        public IdentityContext(DbContextOptions<IdentityContext> options)
            : base(options)
        {            
        
        }

    }
}
