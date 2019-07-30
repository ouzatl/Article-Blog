using Article.Blog.Common.Config;
using Article.Blog.Common.Enum;
using Article.Blog.Common.Enum.Comment;
using Article.Blog.Common.NLog;
using Article.Blog.Common.Templates.CommentTemplates;
using Article.Blog.Data.Models;
using Article.Blog.Repository.Repositories.CommentRepository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;

namespace Article.Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly IOptions<SiteConfig> _config;
        private readonly IMemoryCache _memoryCache;

        public CommentController(
            ICommentRepository commentRepository,
            IMapper mapper,
            ILog logger,
            IOptions<SiteConfig> config,
            IMemoryCache memoryCache)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
            _logger = logger;
            _config = config;
            _memoryCache = memoryCache;
        }

        [HttpGet]
        [Route("GetAllUser")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CommentTemplate>))]
        public IActionResult GetAll()
        {
            try
            {
                var allComment = _commentRepository.GetAll();
                var commentTemplate = new List<CommentTemplate>();
                commentTemplate = _mapper.Map<List<CommentTemplate>>(allComment);

                return Ok(commentTemplate.Where(x=>x.IsActive==true));
            }
            catch (System.Exception ex)
            {
                _logger.Error($"Get All Comment:{ex}");
                return BadRequest(new ServiceResponse<CommentServiceResponse, CommentTemplate>(CommentServiceResponse.Exception));
            }
        }

        [HttpPost]
        [Route("AddComment")]
        [ProducesResponseType(typeof(ServiceResponse<CommentServiceResponse, CommentTemplate>), 200)]
        public IActionResult Add([FromBody] CommentTemplate comment)
        {
            try
            {
                var commentMap = _mapper.Map<Comment>(comment);
                _commentRepository.Add(commentMap);

                return Ok(new ServiceResponse<CommentServiceResponse, CommentTemplate>(CommentServiceResponse.Success));
            }
            catch (System.Exception ex)
            {
                _logger.Error($"Comment Add :{ex}");
                return BadRequest(new ServiceResponse<CommentServiceResponse, CommentTemplate>(CommentServiceResponse.Exception));
            }
        }

        [HttpPost]
        [Route("UpdateComment")]
        [ProducesResponseType(typeof(ServiceResponse<CommentServiceResponse, CommentTemplate>), 200)]
        public IActionResult Update([FromBody] CommentTemplate comment)
        {
            try
            {
                var commentMap = _mapper.Map<Comment>(comment);
                _commentRepository.Update(commentMap);

                return Ok(new ServiceResponse<CommentServiceResponse, CommentTemplate>(CommentServiceResponse.Success));
            }
            catch (System.Exception ex)
            {
                _logger.Error($"Comment Update :{ex}");
                return BadRequest(new ServiceResponse<CommentServiceResponse, CommentTemplate>(CommentServiceResponse.Exception));
            }
        }

        [HttpPost]
        [Route("DeleteComment")]
        [ProducesResponseType(typeof(ServiceResponse<CommentServiceResponse, CommentTemplate>), 200)]
        public IActionResult Delete(int id)
        {
            try
            {
                _commentRepository.Delete(id);

                return Ok(new ServiceResponse<CommentServiceResponse, CommentTemplate>(CommentServiceResponse.Success));
            }
            catch (System.Exception ex)
            {
                _logger.Error($"Comment Delete :{ex}");
                return BadRequest(new ServiceResponse<CommentServiceResponse, CommentTemplate>(CommentServiceResponse.Exception));
            }
        }
    }
}