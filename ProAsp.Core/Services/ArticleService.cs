using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProAsp.Data.Models;
using ProAsp.Data.Repository;

namespace ProAsp.Core.Services
{
    public interface IArticleService
    {
        IEnumerable<Article> GetAllArticles();

        Article GetArticle(Guid articleId);

        void AddArticle(Article article);

    }

    public class ArticleService : IArticleService
    {
        private readonly IRepository<Article> _articleRepository;
        private readonly IRepository<User> _userRepository;

        public ArticleService(IRepository<Article> articleRepository, IRepository<User> userRepository)
        {
            _articleRepository = articleRepository;
            _userRepository = userRepository;
        }

        public IEnumerable<Article> GetAllArticles()
        {
            Article a = new Article();
            User b = new User();
            yield return a;
        }

        public Article GetArticle(Guid articleId)
        {
            return _articleRepository.GetById(articleId);
        }

        public void AddArticle(Article article)
        {
            _articleRepository.Insert(article);
        }
    }
}
