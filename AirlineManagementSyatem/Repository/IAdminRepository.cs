using AirlineManagementSyatem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagementSyatem.Repository
{
    public interface IAdminRepository
    {
        Task<Admin> LoginAsync(string username, string password);
    }
}

