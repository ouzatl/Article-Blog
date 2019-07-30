using Article.Blog.Common.Config;
using Article.Blog.Common.Enum;
using Article.Blog.Common.Enum.Article;
using Article.Blog.Common.NLog;
using Article.Blog.Common.Templates.ArticleTemplates;
using Article.Blog.Repository.Repositories.ArticleRepository;
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
    public class ArticleController : ControllerBase
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly IOptions<SiteConfig> _config;
        private readonly IMemoryCache _memoryCache;

        public ArticleController(
            IArticleRepository articleRepository, 
            IMapper mapper,
            ILog logger,
            IOptions<SiteConfig> config, 
            IMemoryCache memoryCache)
        {
            _articleRepository = articleRepository;
            _mapper = mapper;
            _logger = logger;
            _config = config;
            _memoryCache = memoryCache;
        }

        [HttpGet]
        [Route("GetAllArticle")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ArticleTemplate>))]
        public IActionResult GetAll()
        {
            try
            {
                var allarticle = _articleRepository.GetAll();
                var x = allarticle.ToList();
                var articleTemplate = new List<ArticleTemplate>();
                articleTemplate = _mapper.Map<List<ArticleTemplate>>(allarticle);

                return Ok(articleTemplate);
            }
            catch (System.Exception ex)
            {
                _logger.Error($"Get All Article:{ex}");
                return BadRequest(new ServiceResponse<ArticleServiceResponse, ArticleTemplate>(ArticleServiceResponse.Exception));
            }
        }

        [HttpPost]
        [Route("AddArticle")]
        [ProducesResponseType(typeof(ServiceResponse<ArticleServiceResponse, ArticleTemplate>), 200)]
        public IActionResult Add([FromBody] ArticleTemplate article)
        {
            try
            {
                var articleMap = _mapper.Map<Article.Blog.Data.Models.Article>(article);
                _articleRepository.Add(articleMap);

                return Ok(new ServiceResponse<ArticleServiceResponse, ArticleTemplate>(ArticleServiceResponse.Success));
            }
            catch (System.Exception ex)
            {
                _logger.Error($"Article Add :{ex}");
                return BadRequest(new ServiceResponse<ArticleServiceResponse, ArticleTemplate>(ArticleServiceResponse.Exception));
            }
        }

        [HttpPost]
        [Route("UpdateArticle")]
        [ProducesResponseType(typeof(ServiceResponse<ArticleServiceResponse, ArticleTemplate>), 200)]
        public IActionResult Update([FromBody] ArticleTemplate article)
        {
            try
            {
                var articleMap = _mapper.Map<Article.Blog.Data.Models.Article>(article);
                _articleRepository.Update(articleMap);

                return Ok(new ServiceResponse<ArticleServiceResponse, ArticleTemplate>(ArticleServiceResponse.Success));
            }
            catch (System.Exception ex)
            {
                _logger.Error($"Article Update :{ex}");
                return BadRequest(new ServiceResponse<ArticleServiceResponse, ArticleTemplate>(ArticleServiceResponse.Exception));
            }
        }

        [HttpPost]
        [Route("DeleteArticle")]
        [ProducesResponseType(typeof(ServiceResponse<ArticleServiceResponse, ArticleTemplate>), 200)]
        public IActionResult Delete(int id)
        {
            try
            {
                _articleRepository.Delete(id);

                return Ok(new ServiceResponse<ArticleServiceResponse, ArticleTemplate>(ArticleServiceResponse.Success));
            }
            catch (System.Exception ex)
            {
                _logger.Error($"Article Delete :{ex}");
                return BadRequest(new ServiceResponse<ArticleServiceResponse, ArticleTemplate>(ArticleServiceResponse.Exception));
            }
        }
    }
}