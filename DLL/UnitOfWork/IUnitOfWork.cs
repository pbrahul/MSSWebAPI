using DLL.DBContext;
using DLL.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DLL.UnitOfWork
{
   public interface IUnitOfWork
    {
        //All Repository Added Here

      
        ICustomerBalanceRepository customerBalanceRepository { get; }
        IOrderRepository orderRepository { get; }
        


        //All repository Ended here

        Task<bool> ApplicationSaveChangesAsync();
        void Dispose();
    }

    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        
            public readonly ApplicationDBContext _context;

            private bool disposed = false;

           
             private ICustomerBalanceRepository _customerBalanceRepository;
       
            private IOrderRepository _orderRepository;

            
        public UnitOfWork(ApplicationDBContext context)
            {
                _context = context;
            }

           
            public ICustomerBalanceRepository customerBalanceRepository =>
          _customerBalanceRepository ??= new CustomerRepository(_context);
            public IOrderRepository orderRepository =>
         _orderRepository ??= new OrderRepository(_context);
           
        public async Task<bool> ApplicationSaveChangesAsync()
            {
            if (await _context.SaveChangesAsync() > 0)
            {
                return true;
            }

            return false;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed )
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
            
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
