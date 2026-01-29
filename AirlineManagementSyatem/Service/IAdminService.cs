using AirlineManagementSyatem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagementSyatem.Service
{
    public interface IAdminService
    {
        Task<Admin> LoginAsync(string userName, string password);
    }
}
