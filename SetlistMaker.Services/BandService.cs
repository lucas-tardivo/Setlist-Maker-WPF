using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SetlistMaker.Framework.Exceptions;
using SetlistMaker.Services.Domain.Interfaces;
using SetlistMaker.Services.Domain.Models;
using SetlistMaker.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetlistMaker.Services
{
    public class BandService : IBandService
    {
        private readonly IBandRepository _bandRepository;
        private readonly IMapper _mapper;

        public BandService(IBandRepository bandRepository,
                           IMapper mapper)
        {
            _bandRepository = bandRepository;
            _mapper = mapper;
        }

        private void ValidateBand(Band band)
        {
            if (band.Name == null || band.Name.Length < 3)
            {
                throw new BusinessException("Band name requires 3 characters at least");
            }
        }

        public void AddBand(Band band)
        {
            ValidateBand(band);

            var existingBand = _bandRepository.GetAllAsQueryable().Any(x => x.Name.ToLower() == band.Name.ToLower());

            if (existingBand)
            {
                throw new BusinessException(string.Format("Band {0} already exists", band.Name));
            }

            _bandRepository.Insert(band);
            _bandRepository.SaveChanges();
        }

        public List<Band> GetBands()
        {
            return _bandRepository.GetAllAsQueryable().AsNoTracking().ToList();
        }

        public void EditBand(Band band)
        {
            ValidateBand(band);

            var existingBand = _bandRepository.GetByIdAsNoTracking(band.Id);

            if (existingBand == null)
            {
                throw new BusinessException(string.Format("No band was found with this Id"));
            }

            existingBand = _mapper.Map<Band>(band);

            _bandRepository.Update(existingBand);
            _bandRepository.SaveChanges();
        }

        public void RemoveBand(Guid id)
        {
            var existingBand = _bandRepository.GetById(id);

            if (existingBand == null)
            {
                throw new BusinessException(string.Format("No band was found with this Id"));
            }

            _bandRepository.Delete(existingBand);
            _bandRepository.SaveChanges();
        }

        public Guid GetIdByName(string name)
        {
            var band = _bandRepository.GetAllAsQueryable().AsNoTracking().FirstOrDefault(x => x.Name.ToLower() == name.ToLower());

            return band?.Id ?? Guid.Empty;
        }
    }
}
