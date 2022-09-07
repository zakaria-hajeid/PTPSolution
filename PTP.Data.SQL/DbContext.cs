using Microsoft.EntityFrameworkCore;
using PTP.Core.Entitys;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTP.Data.SQL
{
    public class DbContextTest : DbContext
    {
       
        public DbContextTest(DbContextOptions<DbContextTest> options) : base(options)
        {


        }

        public DbSet<Cleint> Cleint { get; set; }

    }
}



