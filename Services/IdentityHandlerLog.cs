using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mtd.OrderMaker.Ids.Entity;
using Mtd.OrderMaker.Ids.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Mtd.OrderMaker.Ids.Services
{
    public partial class IdentityHandler : UserManager<WebAppUser>
    {
        public async Task<LogChangeDoc> GetLogDocLastEditorAsync(Guid storeId)
        {
            MtdLogDocument mtdLog = await context.MtdLogDocuments.Where(x => x.StoreId == storeId).OrderByDescending(x => x.TimeCh).FirstOrDefaultAsync();
            LogChangeDoc logChange = null; 

            if (mtdLog != null)
            {
                WebAppUser user = await FindByIdAsync(mtdLog.UserId.ToString());
                logChange = new LogChangeDoc { UserName = user.Title, TimeCh = mtdLog.TimeCh };

            }
            
            return logChange;
        }

        public async Task<LogChangeDoc> GetLogDocCreatorAsync(Guid storeId)
        {
            MtdLogDocument mtdLog = await context.MtdLogDocuments.Where(x => x.StoreId == storeId).OrderBy(x => x.TimeCh).FirstOrDefaultAsync();
            LogChangeDoc logChange = null;

            if (mtdLog != null)
            {
                WebAppUser user = await FindByIdAsync(mtdLog.UserId.ToString());
                logChange = new LogChangeDoc { UserName = user.Title, TimeCh = mtdLog.TimeCh };

            }

            return logChange;
        }


        public async Task<bool> SaveLogDocumentAsync(Guid storeId, Guid userId)
        {

            MtdLogDocument mtdLog = new MtdLogDocument { Id = Guid.NewGuid(), StoreId = storeId, UserId = userId, TimeCh = DateTime.UtcNow };
            await context.MtdLogDocuments.AddAsync(mtdLog);

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
