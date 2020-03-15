using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace HIUConsole.Model
{
    public class HisEntities : DbContext
    {
        public HisEntities(string nameOrConnectionString)
           : base(nameOrConnectionString)
        {
        }
    }
}
