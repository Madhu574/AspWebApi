//// IDesignationRepository.cs
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using EmployeeDemoProject.Models;

//namespace EmployeeDemoProject.IRepositories
//{
//    public interface IDesignationRepository
//    {
//        Task<IEnumerable<Designation>> GetDesignations();
//        Task<Designation> GetDesignationByName(string designationName);
//        Task<Designation> InsertDesignation(Designation designation);
//        Task<Designation> AddDesignation(Designation designation);
//        Task<Designation> UpdateDesignation(Designation designation);
//        Task<bool> DeleteDesignation(string designationName);

//    }
//}
// IDesignationRepository.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeDemoProject.Models;

namespace EmployeeDemoProject.IRepositories
{
    public interface IDesignationRepository
    {
        Task<IEnumerable<Designation>> GetDesignations();
        Task<Designation> GetDesignationByName(string designationName);
        Task<Designation> InsertDesignation(Designation designation);
        Task<Designation> UpdateDesignation(Designation designation);
  
    }
}
