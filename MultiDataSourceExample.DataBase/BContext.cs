using Microsoft.EntityFrameworkCore;
using MultiDataSourceExample.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiDataSourceExample.DataBase
{
    public class BContext : DbContext
    {
        public BContext(DbContextOptions<BContext> options) : base(options) { }

        public DbSet<EntityB> EntitiesB { get; set; }
        // ... 其他数据集
    }
}
