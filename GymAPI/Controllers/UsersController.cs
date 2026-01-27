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
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService,IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
      
        [HttpGet]

        public async Task<List<UserDTO>> Get()
        {
            var uList = await _userService.GetUsersAsync();
            var uDTOList = _mapper.Map<List<UserDTO>>(uList);
            return uDTOList;
        }

        //הצגת לקוח לפי id
        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var us= await _userService.GetByIdAsync(id);
            var u=_mapper.Map<UserDTO>(us);
            if (us == null)
            {
                return NotFound();
            }
            return Ok(u);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetEmail(string email)
        {
            var us = await _userService.GetByEmailAsync(email);
            var u = _mapper.Map<UserDTO>(us);
            if (us == null)
            {
                return NotFound();
            }
            return Ok(u);
        }
        //רישום לקוח
        // POST api/<UsersController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UserPostModel value)
        {
            
            var us=await _userService.GetByEmailAsync(value.Email);
            if(us == null)
            {
                var u = await _userService.AddUserAsync(us);
                return Ok(value);
            }
            return Conflict();
        }
        //עדכון לקוח
        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Users value)
        {
            var us=await _userService.UpdateUsersAsync(id,value);
            if(us == null)
                return BadRequest();
            return Ok(us);
        }
        //מחיקת לקוח
        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
           await _userService.DeleteUsersAsync(id);   
           
        }
    }
}
