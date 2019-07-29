using Article.Blog.Common.NLog;
using Article.Blog.Common.Templates.UserTemplates;
using Article.Blog.Data.Models;
using Article.Blog.Repository.Repositories.UserRepository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost]
        [Route("AddUser")]
        public void Add([FromBody] UserTemplate user)
        {
            try
            {
                var user2 = _mapper.Map<User>(user);
                _userRepository.Add(user2);
            }
            catch (System.Exception ex)
            {
                _logger.Error("User Add :" + ex);
            }

        }
    }
}