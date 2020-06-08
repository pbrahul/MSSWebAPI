using DLL.DBContext;
using DLL.Model;
using DLL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Repository
{
    public interface IOrderRepository : IRepositoryBase<Order>
    {
      
    }

    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {

        public OrderRepository(ApplicationDBContext context) : base(context)
        {

        }

    }

 }
