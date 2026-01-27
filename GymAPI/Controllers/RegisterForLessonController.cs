using AutoMapper;
using GymAPI.Core.DTOs;
using GymAPI.Core.Entities;
using GymAPI.Core.Services;
using GymAPI.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GymAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterForLessonController : ControllerBase
    {
        private readonly IRegisterForLessonService _registerForLessonService;
        private readonly IMapper _mapper;


        public RegisterForLessonController(IRegisterForLessonService registerForLessonService, IMapper mapper)
        {
            _registerForLessonService = registerForLessonService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<List<RegisterForLesson>> Get()
        {
            return await _registerForLessonService.GetRegisterForLessonAsync();
        }
        // GET api/<RegisterForLessonController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var us = await _registerForLessonService.GetByIdAsync(id);
            var u=_mapper.Map<RegisterForLessonDTO>(us);
            if (us == null)
                return NotFound();
            return Ok(u);
        }

        public async Task<ActionResult> GetRegisterForLessonByDTimeAsync(DateTime dtime)
        {
            var us = await _registerForLessonService.GetRegisterForLessonByDTimeAsync(dtime);
            var u = _mapper.Map<RegisterForLessonDTO>(us);
            if (us == null)
                return NotFound();
            return Ok(u);
        }

        // POST api/<RegisterForLessonController>
        [HttpPost]
        public  async Task<ActionResult> Post([FromBody] RegisterForLessonPostModel value)
        {
            var les = new Lessons { LessonName = value.LessonPostModelToRegister.LessonName, TeacherName = value.LessonPostModelToRegister.TeacherName, DTime = value.LessonPostModelToRegister.DTime };
            var us = new Users { Name = value.UserPostModelToRegister.Name, Email = value.UserPostModelToRegister.Email, Password = value.UserPostModelToRegister.Password };
            var re = new RegisterForLesson { User = us, Lesson = les, DTimeRegister = value.DTimeRegister };
            if (re != null)
            {
                var rg =await _registerForLessonService.AddRegisterAsync(re);
                return Ok(rg);

            }
            return Conflict();
        }

        // PUT api/<RegisterForLessonController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] RegisterForLessonPostModel value)
        {
            var les = new Lessons { LessonName = value.LessonPostModelToRegister.LessonName, TeacherName = value.LessonPostModelToRegister.TeacherName, DTime = value.LessonPostModelToRegister.DTime };
            var us = new Users { Name = value.UserPostModelToRegister.Name, Email = value.UserPostModelToRegister.Email, Password = value.UserPostModelToRegister.Password };
            var re = new RegisterForLesson { User = us, Lesson = les, DTimeRegister = value.DTimeRegister };
            var re1 = _registerForLessonService.GetRegisterForLessonByDTimeAsync(value.DTimeRegister);
            if (re1 == null)
                return BadRequest();

            return Ok(await _registerForLessonService.UpdateRegisterAsync(id, re));
            
        }

        // DELETE api/<RegisterForLessonController>/5
        [HttpDelete("{id}")]
        public async void Delete(int id)
        {
            await _registerForLessonService.DeleteRegisterAsync(id);

        }
    }
}
