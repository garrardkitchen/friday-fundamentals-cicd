using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Web.Data
{
    public class PolicyContext : DbContext
    {
        public PolicyContext (DbContextOptions<PolicyContext> options)
            : base(options)
        {
        }

        public DbSet<PolicyModel> PolicyModel { get; set; } = default!;
    }
}
