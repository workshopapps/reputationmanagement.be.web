using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using src.Data;
using src.Entities;
using src.Models.Dtos;

namespace src.Services
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public AdminRepository(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _context= context;
            _userManager = userManager;
            _mapper= mapper;
        }
        async Task<IEnumerable<ApplicationUser>> IAdminRepository.GetAllLawyers()
        {
            var lawyers = await _userManager.GetUsersInRoleAsync("Lawyer");
            return _mapper.Map<IEnumerable<ApplicationUser>>(lawyers).ToList();
        }

        async Task<IEnumerable<ApplicationUser>> IAdminRepository.GetAllCustomers()
        {
            var businessUsers = await _userManager.GetUsersInRoleAsync("Customer");
            return _mapper.Map<IEnumerable<ApplicationUser>>(businessUsers).ToList();
        }

        public async Task<ApplicationUser> GetUserById(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return user;
        }
    }
}
