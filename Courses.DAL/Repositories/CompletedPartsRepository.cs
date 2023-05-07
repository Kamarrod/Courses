using Courses.DAL.Interfaces;
using Courses.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Сourses.Domain.Entity;

namespace Courses.DAL.Repositories
{
    public class CompletedPartsRepository : IBaseRepository<CompletedPart>
    {
        private readonly ApplicationDbContext _db;

        public CompletedPartsRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(CompletedPart entity)
        {
            await _db.CompletedParts.AddAsync(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(CompletedPart entity)
        {
            _db.CompletedParts.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public IQueryable<CompletedPart> GetAll()
        {
            return _db.CompletedParts;
        }

        public async Task<CompletedPart> Update(CompletedPart entity)
        {
            _db.CompletedParts.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
