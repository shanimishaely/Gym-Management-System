using GymAPI.Core.Repositories;
using GymAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GymAPI.Data.Repositories
{
    public class LessonRepository:ILessonRepository
    {
        private readonly DataContext _context;
        public LessonRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Lessons>> GetLessonsAsync()
        {
            return await _context.lessons.ToListAsync();


        }

        public async Task<Lessons> GetLessonsByIdAsync(int id)
        {
            return await _context.lessons.FirstAsync(l=> l.Id==id);
        }
        
        public async Task<Lessons> GetLessonsByNameAsync(string name)
        {
            return await _context.lessons.FirstAsync(l => l.LessonName == name);
        }
        public Lessons AddLesson(Lessons lesson) 
        { 
            _context.lessons.Add(lesson); 
            return lesson;
        }

        public async Task<Lessons> UpdateLessonsAsync(int id)
        {

            return await _context.lessons.FirstAsync(x=> x.Id==id);
            
        }
        public async Task DeleteLessonsAsync(int id) 
        {
            _context.lessons.Remove(await _context.lessons.FirstAsync(x=> x.Id==id));
        }
        public async Task SaveAsync()
        {
           await _context.SaveChangesAsync();
        }


    }

}
    
