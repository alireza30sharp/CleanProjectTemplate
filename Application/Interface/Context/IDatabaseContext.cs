using Domain.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Context
{
    public interface IDatabaseContext
    {
        //DbSet<User> users { get; set; }
        int SaveChanges();
    }
}
