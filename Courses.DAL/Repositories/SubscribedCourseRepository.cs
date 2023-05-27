using Courses.DAL.Interfaces;
using Courses.Domain.Entity;

namespace Courses.DAL.Repositories
{
    public class SubscribedCourseRepository : IBaseRepository<SubscribedCourse>
    {
        private readonly ApplicationDbContext _db;
        public SubscribedCourseRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> Create(SubscribedCourse entity)
        {
            await _db.SubscribedCourse.AddAsync(entity);
            await _db.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(SubscribedCourse entity)
        {
            _db.SubscribedCourse.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }
        public  IQueryable<SubscribedCourse> GetAll()
        {
            return _db.SubscribedCourse;
        }
        public async Task<SubscribedCourse> Update(SubscribedCourse entity)
        {
            _db.SubscribedCourse.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
