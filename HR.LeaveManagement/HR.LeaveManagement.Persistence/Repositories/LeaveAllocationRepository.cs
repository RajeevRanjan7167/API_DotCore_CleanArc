using HR.LeaveManagement.Application.Persistence.Contracts;
using HR.LeaveManagement.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Persistence.Repositories
{
    public class LeaveAllocationRepository : GenericRepsitory<LeaveAllocation>, ILeaveAllocationRepository
    {
        private readonly LeaveManagementDbContext _dbContext;

        public LeaveAllocationRepository(LeaveManagementDbContext dbContext) : base(dbContext)
        {
            this._dbContext = dbContext;
        }

        public Task AddAllocations(List<LeaveAllocation> allocations)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AllocationExists(string userId, int leaveTypeId, int period)
        {
            throw new NotImplementedException();
        }

        public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails()
        {
            var leaveAllocations = await _dbContext.LeaveAllocations
                                             .Include(x => x.LeaveType)
                                             .ToListAsync();
            return leaveAllocations;
        }

        public Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id)
        {
            return await _dbContext.LeaveAllocations
                                             .Include(x => x.LeaveType)
                                             .FirstOrDefaultAsync(x => x.Id == id); 
        }

        public Task<LeaveAllocation> GetUserAllocations(string userId, int leaveTypeId)
        {
            throw new NotImplementedException();
        }
    }
}
