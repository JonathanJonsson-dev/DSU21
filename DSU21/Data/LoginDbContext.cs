using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSU21.Data
{
    public class LoginDbContext : IdentityDbContext
    {
        private readonly DbContextOptions _options;

        public LoginDbContext(DbContextOptions options) : base(options)
        {
            _options = options;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
