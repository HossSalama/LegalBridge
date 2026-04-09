using System.Collections;

namespace ExaminationSystem_API.Repository.UnitWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LegalManagementContext _context;
        private Hashtable _repositories;
        //public IBranchRepository Branches { get; private set; }

        public UnitOfWork(LegalManagementContext context)
        {
            _context = context;
            //Branches = new BranchRepository(_context);
  
        }
       
        public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();

    }
}
