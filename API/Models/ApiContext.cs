using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class ApiContext:DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options)
            :base(options)
        {

        }

        public DbSet<ApiItem> ApiItems { get; set; }
    }
}
