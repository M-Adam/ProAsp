using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProAsp.Core.Services;
using ProAsp.Data.Models;
using ProAsp.Viewmodels;

namespace ProAsp.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly IUserService _userService;
        private readonly ILoggerService _loggerService;

        public ArticleController(IArticleService articleService, ILoggerService loggerService, IUserService userService)
        {
            _articleService = articleService;
            _loggerService = loggerService;
            _userService = userService;
        }

        public ViewResult Index()
        {
            IEnumerable<Article> articles = _articleService.GetAllArticles();
            HashSet<ArticleViewmodel> toView = new HashSet<ArticleViewmodel>();
            foreach (var article in articles)
            {
                toView.Add(AutoMapper.Mapper.Map<ArticleViewmodel>(article));
            }
            
            return View(toView);
        }

        public ViewResult Create()
        {
            _loggerService.LogInfo("aaa");
            return View();
        }

        [HttpPost]
        public ViewResult Create(ArticleViewmodel article)
        {
            if (ModelState.IsValid)
            {
                _articleService.AddArticle(new Article()
                {
                    Creator = _userService.GetUser(article.CreatorName),
                    Content = article.Content,
                    CreateDate = article.CreateDate,
                    Title = article.Title
                });
            }
            return View();
        }

        public ViewResult Details(Guid articleId)
        {
            Article art = _articleService.GetArticle(articleId);
            

            return View();
        }

        
    }
}