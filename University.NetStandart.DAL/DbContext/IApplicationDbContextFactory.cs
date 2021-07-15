using System;
using System.Collections.Generic;
using System.Text;

namespace University.NetStandart.DAL.DbContext
{
    public interface IApplicationDbContextFactory
    {
        ApplicationDbContext Create();
    }
}
