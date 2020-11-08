using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Mtd.OrderMaker.Ids.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Mtd.OrderMaker.Ids.Services
{
    public enum RightsType
    {
        View, Create, Edit, Delete, ViewOwn, EditOwn, DeleteOwn, ViewGroup, EditGroup, DeleteGroup, SetOwn, Reviewer
    };

    public partial class IdentityHandler : UserManager<WebAppUser>
    {
        private readonly ILogger logger;
        private readonly IdentityContext context;     
        private readonly SignInManager<WebAppUser> _signInManager;        

        public IdentityHandler(IdentityContext context,
            IUserStore<WebAppUser> store,
            IOptions<IdentityOptions> optionsAccessor, 
            IPasswordHasher<WebAppUser> passwordHasher, 
            IEnumerable<IUserValidator<WebAppUser>> userValidators, 
            IEnumerable<IPasswordValidator<WebAppUser>> passwordValidators, 
            ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, 
            IServiceProvider services, ILogger<IdentityHandler> logger, SignInManager<WebAppUser> signInManager) : 
            base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {

            this.logger = logger;
            this.context = context;
            _signInManager = signInManager;
            
        }

        private string RightTypeToString(RightsType rightsType)
        {
            string value = $"-{rightsType.ToString().ToLower()}";
            value = value.Replace("own", "-own");
            value = value.Replace("group", "-group");
            return value;
        }

        public async Task<List<Guid>> GetFormIdsAsync(WebAppUser user, params RightsType[] rightsTypes)
        {
            List<Guid> formIds = new List<Guid>();
            if (user == null) return formIds;

            foreach (RightsType rightsType in rightsTypes)
            {
                string right = RightTypeToString(rightsType);
                IList<Claim> claims = await GetClaimsAsync(user);
                List<Guid> fids = claims.Where(x => x.Value == right).Select(x => Guid.Parse(x.Type)).ToList();
                formIds.AddRange(fids.Where(x => !formIds.Contains(x)));
            }

            return formIds;
        }

        public async Task<bool> IsRightAsync(WebAppUser user, RightsType rightsType, Guid formId)
        {
            bool result;

            string right = RightTypeToString(rightsType);
            IList<Claim> claims = await GetClaimsAsync(user);
            result = claims.Where(x => x.Type == formId.ToString() && x.Value == right).Any();

            return result;
        }

        public async Task<bool> IsAdmin(WebAppUser user)
        {
            return await IsInRoleAsync(user, "Admin");
        }

        public async Task<bool> IsOwner(WebAppUser user, Guid storeId)
        {
            return await context.MtdStoreOwners.Where(x => x.StoreId == storeId && x.UserId == user.Id).AnyAsync();
        }

        private async Task<bool> IsRights(string right, WebAppUser user, Guid FormId, Guid? storeId = null)
        {

            IList<Claim> claims = await GetClaimsAsync(user);

            bool ownRight = claims.Where(x => x.Type == FormId.ToString() && x.Value == $"{right}-own").Any();
            if (ownRight && storeId != null)
            {
                bool isOkOwner = await context.MtdStoreOwners.Where(x => x.StoreId == storeId && x.UserId == user.Id).AnyAsync();
                return isOkOwner;
            }

            bool groupRight = claims.Where(x => x.Type == FormId.ToString() && x.Value == $"{right}-group").Any();
            if (groupRight && storeId != null)
            {
                List<WebAppUser> users = await GetUsersInGroupsAsync(user);
                List<Guid> userIds = users.Select(x => x.Id).ToList();
                bool isOkGroup = await context.MtdStoreOwners.Where(x => x.StoreId== storeId && userIds.Contains(x.UserId)).AnyAsync();
                return isOkGroup;
            }

            
            bool isOk = claims.Where(x => x.Type == FormId.ToString() && x.Value == right).Any();
            if (isOk) return true;

            return false;
        }


        public async Task<bool> IsCreator(WebAppUser user, Guid formId)
        {
            return await IsRights("-create", user, formId);
        }

        public async Task<bool> IsViewer(WebAppUser user, Guid formId, Guid? idStore = null)
        {            
            return await IsRights("-view", user, formId, idStore);
        }


        public async Task<bool> IsEditor(WebAppUser user, Guid formId, Guid? idStore = null)
        {
            
            return await IsRights("-edit", user, formId, idStore);
        }

        public async Task<bool> IsEraser(WebAppUser user, Guid formId, Guid? idStore = null)
        {
            return  await IsRights("-delete", user, formId, idStore);
        }

        public async Task<bool> IsInstallerOwner(WebAppUser user, Guid formId, Guid storeId)
        {
            bool result = await IsRights("-set-own", user, formId);
            if (!result) return result;

            MtdStoreOwner mtdStoreOwner =  await context.MtdStoreOwners.FindAsync(storeId);
            if (mtdStoreOwner == null) { return await IsAdmin(user); }

            List<WebAppUser> webAppUsers = await GetUsersInGroupsAsync(user);
            if (webAppUsers.Count == 0 ) { return result; }

            List<Guid> userIds = webAppUsers.Select(x => x.Id).ToList();
            return webAppUsers.Where(x => userIds.Contains(mtdStoreOwner.UserId)).Any();            
        }

        public async Task<bool> IsCreatorPartAsync(WebAppUser user, Guid PartId)
        {
            IList<Claim> claims = await GetClaimsAsync(user);
            return claims.Where(x => x.Type == PartId.ToString() && x.Value == "-part-create").Any();
        }

        public async Task<bool> IsEditorPartAsync(WebAppUser user, Guid PartId)
        {
            IList<Claim> claims = await GetClaimsAsync(user);
            return claims.Where(x => x.Type == PartId.ToString() && x.Value == "-part-edit").Any();
        }

        public async Task<bool> IsViewerPartAsync(WebAppUser user, Guid idPart)
        {
            IList<Claim> claims = await GetClaimsAsync(user);
            return claims.Where(x => x.Type == idPart.ToString() && x.Value == "-part-view").Any();
        }

        public async Task<List<Guid>> GetAllowPartsForView(WebAppUser user, List<Guid> partIds)
        {           
            IList<Claim> claims = await GetClaimsAsync(user);
            return claims.Where(x => partIds.Select(x=>x.ToString()).Contains(x.Type) && x.Value == "-part-view").Select(x => Guid.Parse(x.Type)).ToList();
        }
        
        public async Task<List<WebAppUser>> GetUsersInGroupsAsync(WebAppUser webAppUser)
        {
            List<WebAppUser> webAppUsers = new List<WebAppUser>();
            IList<Claim> claims = await GetClaimsAsync(webAppUser);
            IList<Claim> groups = claims.Where(c => c.Value == "-group").ToList();

            foreach (var claim in groups)
            {
                IList<WebAppUser> users = await GetUsersForClaimAsync(claim);
                if (users != null)
                {
                    var temp = users.Where(x => !webAppUsers.Select(w => w.Id).Contains(x.Id)).ToList();
                    if (temp != null)
                    {
                        webAppUsers.AddRange(temp);
                    }
                }
            }

            return webAppUsers;
        }

        public override async Task<WebAppUser> GetUserAsync(ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentNullException(nameof(principal));
            }
            
            var id = GetUserId(principal);
            if (id == null)  { return null; }

            WebAppUser user = await FindByIdAsync(id);
            if (user == null)
            {
                await _signInManager.SignOutAsync();
                return null;
            }

            return user;
        }  
        

    }
}
