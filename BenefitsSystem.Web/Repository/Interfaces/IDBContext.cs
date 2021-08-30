using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BenefitsSystem.Web.Repository
{
    public interface IDBContext
    {
        IDbConnection CreateConnection();
    }
}
