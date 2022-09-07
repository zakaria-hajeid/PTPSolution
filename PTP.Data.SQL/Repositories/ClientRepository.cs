using Microsoft.EntityFrameworkCore;
using PTP.Core.Entitys;
using PTP.Core.Repositores;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTP.Data.SQL.Repositories
{
    public class ClientRepository : Repository<Cleint, CleintFilter>, IClientRepository
    {

        public ClientRepository(DbContextTest context) : base(context)
        {
        }
    }
}
