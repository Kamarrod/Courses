﻿using Courses.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Сourses.Domain.Entity;

namespace Courses.DAL.Repositories
{
    public class PracticalPartRepository : IBaseRepository<PracticalPart>
    {
        private readonly ApplicationDbContext _db;

        public PracticalPartRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> Create(PracticalPart entity)
        {
            await _db.PracticalParts.AddAsync(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(PracticalPart entity)
        {
            _db.PracticalParts.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public IQueryable<PracticalPart> GetAll()
        {
            return _db.PracticalParts;
        }

        ////public async Task<PracticalPart> GetAsync(int id)
        ////{
        ////    return await _db.PracticalParts.FirstOrDefaultAsync(x => x.Id == id);
        ////}


        //public async Task<PracticalPart>? GetAsync(int number, int courseId )
        //{
        //    return await _db.PracticalParts.FirstOrDefaultAsync(x => x.Number == number && courseId == x.CourseId);
        //}

        //public async Task<List<PracticalPart>> Select()
        //{
        //    return await _db.PracticalParts.ToListAsync();
        //}

        public async Task<PracticalPart> Update(PracticalPart entity)
        {
            _db.PracticalParts.Update(entity);
            await _db.SaveChangesAsync();
            return entity; throw new NotImplementedException();
        }
    }
}
