using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mtd.OrderMaker.Ids.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mtd.OrderMaker.Ids.Services
{
    public partial class IdentityHandler : UserManager<WebAppUser>
    {
        public async Task<IList<Guid>> GetFilterIdsAsync(Guid userId)
        {
            IList<Guid> guids = await context.MtdFilterOwners.Where(x => x.UserId == userId).Select(x => x.FilterId).ToListAsync();
            return guids;
        }

        public async Task<bool> SaveFilterOwnerAsync(Guid filterId, Guid userId)
        {           
            bool exists = await context.MtdFilterOwners.Where(x => x.FilterId == filterId).AnyAsync();
            MtdFilterOwner mtdFilterOwner = new MtdFilterOwner { FilterId = filterId, UserId = userId };

            if (exists)
            {
                context.MtdFilterOwners.Update(mtdFilterOwner);

            }
            else
            {
                await context.MtdFilterOwners.AddAsync(mtdFilterOwner);
            }

            try
            {
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return false;
            }

            return true;
        }

        public async Task<bool> RemoveFilterOwnerAsync(Guid filterId)
        {
            MtdFilterOwner mtdFilterOwner = new MtdFilterOwner { FilterId = filterId};
            context.MtdFilterOwners.Remove(mtdFilterOwner);
            try
            {
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return false;
            }

            return true;
        }


        public async Task<bool> RemoveFilterOwnerAsync(IList<Guid> filterIds)
        {
            List<MtdFilterOwner> fowners = new List<MtdFilterOwner>();
            filterIds.ToList().ForEach((id) => {
                fowners.Add(new MtdFilterOwner { FilterId = id });
            });

            context.MtdFilterOwners.RemoveRange(fowners);
            try
            {
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return false;
            }

            return true;

        }
    }
}
