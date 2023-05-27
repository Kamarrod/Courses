using Courses.DAL.Interfaces;
using Courses.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.DAL.Repositories
{
    public class CompletedCourseRepository : IBaseRepository<CompletedCourse>
    {
        private readonly ApplicationDbContext _db;
        public CompletedCourseRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> Create(CompletedCourse entity)
        {
            await _db.CompletedCourse.AddAsync(entity);
            await _db.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(CompletedCourse entity)
        {
            _db.CompletedCourse.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }
        public IQueryable<CompletedCourse> GetAll()
        {
            return _db.CompletedCourse;
        }
        public async Task<CompletedCourse> Update(CompletedCourse entity)
        {
            _db.CompletedCourse.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
