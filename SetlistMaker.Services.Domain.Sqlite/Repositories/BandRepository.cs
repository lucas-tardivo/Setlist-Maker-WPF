using SetlistMaker.Services.Domain.Interfaces;
using SetlistMaker.Services.Domain.Models;
using SetlistMaker.Services.Domain.Sqlite.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetlistMaker.Services.Domain.Sqlite.Repositories
{
    public class BandRepository : RepositoryEntityBase<Band>, IBandRepository
    {
        public BandRepository(EntityContext context) : base(context)
        {
        }
    }
}
