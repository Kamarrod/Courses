using Courses.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Сourses.Domain.Entity;

namespace Courses.DAL.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext _db;
        public CourseRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<List<PracticalPart>> GetPracticalParts(int id)
        {
            var practicalPart = await _db.PracticalParts.Where(x => x.CourseId == id).ToListAsync();
            return practicalPart;
        }
        public async Task<bool> Create(Course entity)
        {
            await _db.Course.AddAsync(entity);
            await _db.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Course entity)
        {
            _db.Course.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }
        public async Task<Course> Update(Course entity)
        {
            _db.Course.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
        public IQueryable<Course> GetAll()
        {
            return _db.Course;
        }
    }
}
