using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mtd.OrderMaker.Ids.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mtd.OrderMaker.Ids.Services
{
    public partial class IdentityHandler : UserManager<WebAppUser>
    {
        public async Task<IList<Guid>> GetStoreIdsAsync(Guid userId)
        {
            IList<Guid> guids = await context.MtdStoreOwners.Where(x => x.UserId == userId).Select(x => x.StoreId).ToListAsync();
            return guids;
        }

        public async Task<IList<Guid>> GetStoreIdsAsync(IList<Guid> userIds)
        {
            IList<Guid> guids = await context.MtdStoreOwners.Where(x => userIds.Contains(x.UserId)).Select(x => x.StoreId).ToListAsync();
            return guids;
        }

        public async Task<MtdStoreOwner> GetStoreOwnerAsync(Guid storeId)
        {
            MtdStoreOwner storeOwner = await context.MtdStoreOwners.FindAsync(storeId);
            return storeOwner;
        }


        public async Task<bool> SaveStoreOwnerAsync(Guid storeId, Guid userId)
        {

            MtdStoreOwner storeOwner = new MtdStoreOwner { StoreId = storeId, UserId = userId };

            bool exists = await context.MtdStoreOwners.Where(x => x.StoreId == storeId).AnyAsync();

            if (exists)
            {
                context.MtdStoreOwners.Update(storeOwner);

            }
            else
            {
                await context.MtdStoreOwners.AddAsync(storeOwner);
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

    }
}
