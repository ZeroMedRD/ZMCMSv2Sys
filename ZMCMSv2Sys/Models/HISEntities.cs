using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace ZMCMSv2Sys.Models
{
    public partial class HISEntities : DbContext
    {
        public HISEntities(string nameOrConnectionString)
           : base(nameOrConnectionString)
        {
        }
    }
}