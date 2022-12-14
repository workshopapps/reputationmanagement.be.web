using System.Collections;
using src.Entities;
using src.Models.Dtos;

namespace src.Services
{
    public interface IAdminRepository
    {
        Task<IEnumerable<ApplicationUser>> GetAllLawyers();   
        Task<IEnumerable<ApplicationUser>> GetAllCustomers();
    }
}
