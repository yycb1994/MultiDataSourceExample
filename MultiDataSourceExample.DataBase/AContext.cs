using Microsoft.EntityFrameworkCore;
using MultiDataSourceExample.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiDataSourceExample.DataBase
{
    public class AContext : DbContext
    {
        public AContext(DbContextOptions<AContext> options) : base(options) { }

        public DbSet<EntityA> EntitiesA { get; set; }
        // ... 其他数据集
    }
}
