using Application.Static.Cache;
using Domain.Abstractions;
using Domain.Entities;
using Domain.IRepositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMemoryCacheRepository _memoryCacheRepository;

        public AuthenticationRepository(UserManager<ApplicationUser> userManager, IMemoryCacheRepository memoryCacheRepository)
        {
            _userManager = userManager;
            _memoryCacheRepository = memoryCacheRepository;
            
        }
        public async Task<bool> CheckPasswordAsync(ApplicationUser username, string password)
        {
            return await _userManager.CheckPasswordAsync(username, password);
        }

        public async Task<IdentityResult> CreateAsync(ApplicationUser user, string password)
        {
            await _memoryCacheRepository.RemoveAsync(CacheKeys.User);
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<ApplicationUser> FindByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if(user is null)
            {
                return default!;
            }
            return user;
        }

        public async Task<ApplicationUser> FindByNameAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if(user is null)
            {
                return default!;
            }
            return user;
        }

        public async Task<IList<string>> GetRolesAsync(ApplicationUser username)
        {
            return await _userManager.GetRolesAsync(username);
        }

        public async Task UpdateUserAsync(ApplicationUser user)
        {
            await _userManager.UpdateAsync(user);
        }
    }
}
