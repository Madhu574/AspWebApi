
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using EmployeeDemoProject.IRepositories;
//using EmployeeDemoProject.Models;
//using EmployeeMCrud.Models;

//namespace EmployeeDemoProject.Repositories
//{
//    public class DesignationRepository : IDesignationRepository
//    {
//        private readonly List<Designation> _designations;

//        //public DesignationRepository()
//        //{ 
//        //    _designations = new List<Designation>
//        //    {
//        //        new Designation { DesignationId = 1, DesignationName = "Manager" },
//        //        new Designation { DesignationId = 2, DesignationName = "Engineer" },
//        //        new Designation { DesignationId = 3, DesignationName = "Analyst" }
//        //    };
//        //}
//              private readonly APIDbContext _appDBContext;
//        public DesignationRepository(APIDbContext context)
//        {
//            _appDBContext = context ??
//                throw new ArgumentNullException(nameof(context));
//        }

//        public Task<IEnumerable<Designation>> GetDesignations()
//        {
//            return Task.FromResult<IEnumerable<Designation>>(_designations);
//        }

//        public Task<Designation> GetDesignationById(int designationId)
//        {
//            var designation = _designations.FirstOrDefault(d => d.DesignationId == designationId);
//            return Task.FromResult(designation);
//        }

//        public Task<Designation> GetDesignationByName(string designationName)
//        {
//            var designation = _designations.FirstOrDefault(d => d.DesignationName == designationName);
//            return Task.FromResult(designation);
//        }

//        public Task<Designation> InsertDesignation(Designation designation)
//        {
//            if (designation == null)
//            {
//                throw new ArgumentNullException(nameof(designation));
//            }

//            // Generate a new designation Id
//            var newId = _designations.Count + 1;
//            designation.DesignationId = newId;

//            _designations.Add(designation);

//            return Task.FromResult(designation);
//        }

//        public Task<Designation> UpdateDesignation(Designation designation)
//        {
//            if (designation == null)
//            {
//                throw new ArgumentNullException(nameof(designation));
//            }

//            var existingDesignation = _designations.FirstOrDefault(d => d.DesignationId == designation.DesignationId);
//            if (existingDesignation != null)
//            {
//                existingDesignation.DesignationName = designation.DesignationName;
//            }

//            return Task.FromResult(existingDesignation);
//        }

//        public Task<bool> DeleteDesignation(int designationId)
//        {
//            var existingDesignation = _designations.FirstOrDefault(d => d.DesignationId == designationId);
//            if (existingDesignation != null)
//            {
//                _designations.Remove(existingDesignation);
//                return Task.FromResult(true);
//            }

//            return Task.FromResult(false);
//        }

//        public Task<bool> DeleteDesignation(string designationName)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeDemoProject.IRepositories;
using EmployeeDemoProject.Models;
using EmployeeMCrud.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDemoProject.Repositories
{
    public class DesignationRepository : IDesignationRepository
    {
        private readonly APIDbContext _appDBContext;
        public DesignationRepository(APIDbContext context)
        {
            _appDBContext = context ??
                throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Designation>> GetDesignations()
        {
            return await _appDBContext.Designations.ToListAsync();
        }


        public async Task<Designation> GetDesignationByName(string designationName)
        {
            return await _appDBContext.Designations.FirstOrDefaultAsync(d => d.DesignationName == designationName);
        }

        public async Task<Designation> InsertDesignation(Designation designation)
        {
            var entry = await _appDBContext.Designations.AddAsync(designation);
            await _appDBContext.SaveChangesAsync();
            return entry.Entity;
        }

        public async Task<Designation> UpdateDesignation(Designation designation)
        {
            _appDBContext.Designations.Update(designation);
            await _appDBContext.SaveChangesAsync();
            return designation;
        }



        public async Task<bool> DeleteDesignation(string designationName)
        {
            var existingDesignation = await GetDesignationByName(designationName);
            if (existingDesignation == null)
            {
                return false; // Designation not found
            }

            _appDBContext.Designations.Remove(existingDesignation);
            await _appDBContext.SaveChangesAsync();
            return true; // Deletion successful
        }

        public Task DeleteDesignation(Designation existingDesignation)
        {
            throw new NotImplementedException();
        }
    }
}

