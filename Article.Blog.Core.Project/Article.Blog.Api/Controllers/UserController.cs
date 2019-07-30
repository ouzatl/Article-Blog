using Article.Blog.Common.Enum;
using Article.Blog.Common.Enum.User;
using Article.Blog.Common.NLog;
using Article.Blog.Common.Templates.UserTemplates;
using Article.Blog.Data.Models;
using Article.Blog.Repository.Repositories.UserRepository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Article.Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //private static Ilogger Ilogger;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILog _logger;

        public UserController(IUserRepository userRepository, IMapper mapper, ILog logger)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;

        }

        [HttpGet]
        [Route("GetAllUser")]
        [ProducesResponseType(200,Type = typeof(IEnumerable<UserTemplate>))]
        public IActionResult GetAll()
        {
            try
            {
                var allUser = _userRepository.GetAll();

                return Ok(allUser);
            }
            catch (System.Exception ex)
            {
                _logger.Error($"Get All User:{ex}");
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("AddUser")]
        [ProducesResponseType(typeof(ServiceResponse<UserServiceResponse,UserTemplate>),200)]
        public void Add([FromBody] UserTemplate user)
        {
            try
            {
                var user2 = _mapper.Map<User>(user);
                _userRepository.Add(user2);
            }
            catch (System.Exception ex)
            {
                _logger.Error($"User Add :{ex}");
            }
        }

        [HttpPost]
        [Route("UpdateUser")]
        [ProducesResponseType(typeof(ServiceResponse<UserServiceResponse, UserTemplate>), 200)]
        public void Update([FromBody] UserTemplate user)
        {
            try
            {
                var user2 = _mapper.Map<User>(user);
                _userRepository.Update(user2);
            }
            catch (System.Exception ex)
            {
                _logger.Error($"User Update :{ex}");
            }
        }

        [HttpPost]
        [Route("DeleteUser")]
        [ProducesResponseType(typeof(ServiceResponse<UserServiceResponse, UserTemplate>), 200)]
        public void Delete([FromBody] int id)
        {
            try
            {
                _userRepository.Delete(id);
            }
            catch (System.Exception ex)
            {
                _logger.Error($"User Delete :{ex}");
            }
        }
    }
}