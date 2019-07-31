using Article.Blog.Common.Config;
using Article.Blog.Common.Enum;
using Article.Blog.Common.Enum.User;
using Article.Blog.Common.Helpers;
using Article.Blog.Common.NLog;
using Article.Blog.Common.Templates.UserTemplates;
using Article.Blog.Data.Models;
using Article.Blog.Repository.Repositories.UserRepository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Article.Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly IOptions<SiteConfig> _config;
        private readonly IMemoryCache _memoryCache;

        public UserController(
            IUserRepository userRepository,
            IMapper mapper,
            ILog logger,
            IOptions<SiteConfig> config,
            IMemoryCache memoryCache)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
            _config = config;
            _memoryCache = memoryCache;
        }

        [HttpGet]

        [Route("GetAllUser")]
        [ProducesResponseType(200,Type = typeof(IEnumerable<UserTemplate>))]
        public IActionResult GetAll()
        {
            try
            {
                var allUser = _userRepository.GetAll().Where(x => x.IsActive == true);
                var userTemplate = new List<UserTemplate>();
                userTemplate = _mapper.Map<List<UserTemplate>> (allUser);

                return Ok(userTemplate);
            }
            catch (System.Exception ex)
            {
                _logger.Error($"Get All User:{ex}");
                return BadRequest(new ServiceResponse<UserServiceResponse, UserTemplate>(UserServiceResponse.Exception));
            }
        }

        [HttpPost]
        [Route("AddUser")]
        [ProducesResponseType(typeof(ServiceResponse<UserServiceResponse,UserTemplate>),200)]
        public IActionResult Add([FromBody] UserTemplate user)
        {
            try
            {
                UserServiceResponse validation = Validate(user);
                if (validation != UserServiceResponse.Success)
                    return Ok(new ServiceResponse<UserServiceResponse, UserTemplate>(validation));

                user.Password = EncryptionDecryption.EncryptString(_config.Value.HashKey, user.Password);

                var userMap = _mapper.Map<User>(user);
                _userRepository.Add(userMap);

                return Ok(new ServiceResponse<UserServiceResponse, UserTemplate>(UserServiceResponse.Success));
            }
            catch (System.Exception ex)
            {
                _logger.Error($"User Add :{ex}");
                return BadRequest(new ServiceResponse<UserServiceResponse, UserTemplate>(UserServiceResponse.Exception));
            }
        }

        [HttpPost]
        [Route("UpdateUser")]
        [ProducesResponseType(typeof(ServiceResponse<UserServiceResponse, UserTemplate>), 200)]
        public IActionResult Update([FromBody] UserTemplate user)
        {
            try
            {
                var userGetId = _userRepository.GetById(user.Id);
                if (userGetId == null)
                    return Ok(new ServiceResponse<UserServiceResponse, UserTemplate>(UserServiceResponse.NotFound));

                UserServiceResponse validation = Validate(user);
                if (validation != UserServiceResponse.Success)
                    return Ok(new ServiceResponse<UserServiceResponse, UserTemplate>(validation));

                var userMap = _mapper.Map<User>(user);
                _userRepository.Update(userMap);

                return Ok(new ServiceResponse<UserServiceResponse, UserTemplate>(UserServiceResponse.Success));
            }
            catch (System.Exception ex)
            {
                _logger.Error($"User Update :{ex}");
                return BadRequest(new ServiceResponse<UserServiceResponse, UserTemplate>(UserServiceResponse.Exception));
            }
        }

        [HttpPost]
        [Route("DeleteUser")]
        [ProducesResponseType(typeof(ServiceResponse<UserServiceResponse, UserTemplate>), 200)]
        public IActionResult Delete(int id)
        {
            try
            {
                _userRepository.Delete(id);

                return Ok(new ServiceResponse<UserServiceResponse, UserTemplate>(UserServiceResponse.Success));
            }
            catch (System.Exception ex)
            {
                _logger.Error($"User Delete :{ex}");
                return BadRequest(new ServiceResponse<UserServiceResponse, UserTemplate>(UserServiceResponse.Exception));
            }
        }

        private UserServiceResponse Validate(UserTemplate user)
        {
            #region EmailCheck

            if (string.IsNullOrEmpty(user.Email))
            {
                return UserServiceResponse.InvalidEmail;
            }

            #endregion

            #region NameCheck

            if (string.IsNullOrEmpty(user.Name))
            {
                return UserServiceResponse.InvalidName;
            }

            #endregion

            #region SurnameCheck

            if (string.IsNullOrEmpty(user.Surname))
            {
                return UserServiceResponse.InvalidSurname;
            }

            #endregion

            #region PasswordCheck

            if (string.IsNullOrEmpty(user.Password) || user.Password.Length < 2)
            {
                return UserServiceResponse.InvalidPassword;
            }

            #endregion

            return UserServiceResponse.Success;
        }
    }
}