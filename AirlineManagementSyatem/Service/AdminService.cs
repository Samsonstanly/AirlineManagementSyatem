using AirlineManagementSyatem.Models;
using AirlineManagementSyatem.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagementSyatem.Service
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;

        public AdminService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public Task<Admin> LoginAsync(string userName, string password)
        {
            return _adminRepository.LoginAsync(userName, password);
        }
    }
}
