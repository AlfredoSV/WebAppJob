﻿using Application.IServices;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;


namespace Application.Services
{
    public class ServiceCatalogEnglishLevel : IServiceCatalog<EnglishLevel>
    {
        private CatalogContext _context;

        public ServiceCatalogEnglishLevel(CatalogContext catalogContext) {
            _context = catalogContext;  
        }

        public async Task Delete(Guid id)
        {
            _context.EnglishLevel.Remove(await GetById(id));
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<EnglishLevel>> GetAllAsync()
        {

            var englishLevels = await  _context.EnglishLevel
                           .ToListAsync();

            if (englishLevels.Count == 0)
                throw new CommonException("Table englishLevel is empty", "GetAllAsync");

            return englishLevels;

        }

        public async Task<EnglishLevel> GetById(Guid id)
        {

            EnglishLevel englishLevel = await _context.EnglishLevel.FirstOrDefaultAsync(ar => ar.Id.Equals(id));

            if (englishLevel == null)
                throw new CommonException("EnglishLevel not exist", "GetById");

            return englishLevel;
        }

        public async Task Save(EnglishLevel entity)
        {
            _context.EnglishLevel.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(EnglishLevel entity)
        {
            EnglishLevel englishLevel = await GetById(entity.Id);
            if (englishLevel != null)
            {
                englishLevel.NameLevel = entity.NameLevel;
                englishLevel.UpdateDate = DateTime.Now;
                englishLevel.IsActive = entity.IsActive;
                _context.EnglishLevel
                        .Update(englishLevel);
                await _context.SaveChangesAsync();
            }
        }
    }
}
