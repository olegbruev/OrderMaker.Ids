using Microsoft.AspNetCore.Http;
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

        public async Task<IList<MtdGroup>> GetGroupListAsync(string textFilter = null)
        {
            var query = context.MtdGroups.AsQueryable();
            if (textFilter != null)
            {
                string normText = textFilter.ToUpper();
                query = query.Where(x => x.Name.ToUpper().Contains(normText) || x.Description.ToUpper().Contains(normText));
            }

            return await query.ToListAsync();
        }

        public async Task<MtdGroup> GetGroupAsync(Guid id)
        {
            if (id == null) { return null; }
            MtdGroup group = await context.MtdGroups.FindAsync(id);
            return group;
        }

        public async Task<bool> SaveGroupAsync(MtdGroup group)
        {
            if (group == null) { return false; }
            bool exists = await context.MtdGroups.Where(x => x.Id == group.Id).AnyAsync();

            if (exists)
            {
                context.MtdGroups.Update(group);

            }
            else
            {
                await context.MtdGroups.AddAsync(group);
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

        public async Task<bool> RemoveGroupAsync(MtdGroup group)
        {

            try
            {
                context.MtdGroups.Remove(group);
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
