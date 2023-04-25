using SetlistMaker.Services.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetlistMaker.Services.Interfaces
{
    public interface IBandService
    {
        public void AddBand(Band band);
        public void EditBand(Band band);
        public void RemoveBand(Guid band);
        public Guid GetIdByName(string name);
        public List<Band> GetBands();
    }
}
