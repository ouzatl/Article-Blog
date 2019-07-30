using Article.Blog.Common.Config;
using Article.Blog.Common.Enum;
using Article.Blog.Common.Enum.User;
using Article.Blog.Common.NLog;
using Article.Blog.Common.Templates.UserTemplates;
using Article.Blog.Repository.Repositories.UserRepository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace Article.Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ILog _logger;
        private readonly IOptions<SiteConfig> _config;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;

        public AuthenticationController(
             IUserRepository userRepository,
             ILog logger,
             IOptions<SiteConfig> config,
             IMapper mapper,
             IMemoryCache memoryCache
            )
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
            _config = config;
            _memoryCache = memoryCache;
        }

        [HttpPost]
        [Route("Register")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserTemplate>))]
        public IActionResult Register()
        {
            try
            {

                return Ok();
            }
            catch (System.Exception ex)
            {
                _logger.Error($"Tegister User:{ex}");
                return BadRequest(new ServiceResponse<UserServiceResponse, UserTemplate>(UserServiceResponse.Exception));
            }
        }

        [HttpPost]
        [Route("Login")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserTemplate>))]
        public IActionResult Login()
        {
            try
            {
                return Ok();
            }
            catch (System.Exception ex)
            {
                _logger.Error($"Login User:{ex}");
                return BadRequest(new ServiceResponse<UserServiceResponse, UserTemplate>(UserServiceResponse.Exception));
            }
        }

        [HttpPost]
        [Route("LogOut")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserTemplate>))]
        public IActionResult LogOut()
        {
            try
            {
                return Ok();
            }
            catch (System.Exception ex)
            {
                _logger.Error($"LogOut User:{ex}");
                return BadRequest(new ServiceResponse<UserServiceResponse, UserTemplate>(UserServiceResponse.Exception));
            }
        }
    }
}