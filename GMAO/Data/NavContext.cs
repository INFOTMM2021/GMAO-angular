using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GMAO.Data
{
    public class NavContext : DbContext
    {
        public NavContext(DbContextOptions<NavContext> options) : base(options)
        {      

    }
    }
}
