using Microsoft.AspNetCore.Mvc;

namespace Project.WebApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class HomeController : ControllerBase 
{

	[HttpGet(Name = "GetAllArticle")]
	public IList<Article> GetAllArticle()
	{
		return FileHelper.GetJson();
	} 


	[HttpGet("{idArticle:int}", Name = "GetArticle")]
	public Article? GetArticle(int idArticle)
	{
		IList<Article> articles = FileHelper.GetJson();
		var article = articles.FirstOrDefault(a => a.Id == idArticle);
		return article;
	}


	[HttpPost(Name = "AddArticle")]
	public bool AddArticle(Article art) 
	{
		Article addingArticle = new Article
		{
			Id = art.Id,
			Title = art.Title,
			Author = art.Author,
			Content = art.Content,
			Date = DateOnly.FromDateTime(DateTime.Now),
		};

		IList<Article> articles = FileHelper.GetJson();
		articles.Add(addingArticle);

		return FileHelper.updateJson(articles);
	}


	[HttpPatch("{idArticle:int}", Name = "UpdateArticle")]
	public bool UpdateArticle(int idArticle, Article art) 
	{
		IList<Article> articles = FileHelper.GetJson();
		var oldArticle = articles.FirstOrDefault(a => a.Id == idArticle);
		if (oldArticle == null)
		{
			return false;			
		}

		var updArticle = new Article
		{
			Id = oldArticle.Id,
			Title = !String.IsNullOrWhiteSpace(art.Title) ? art.Title : oldArticle.Title,
			Author = !String.IsNullOrWhiteSpace(art.Author) ? art.Author : oldArticle.Author,
			Content = !String.IsNullOrWhiteSpace(art.Content) ? art.Content : oldArticle.Content,
			Date = DateOnly.FromDateTime(DateTime.Now)
		};

		articles.Remove(oldArticle);
		articles.Add(updArticle);

		return FileHelper.updateJson(articles);
	}


	[HttpDelete("{idArticle:int}", Name = "DeleteArticle")]
	public bool DeleteArticle(int idArticle)
	{
		IList<Article> articles = FileHelper.GetJson();
		var article = articles.FirstOrDefault(a => a.Id == idArticle);
		if (article != null)
		{
			articles.Remove(article);
			return FileHelper.updateJson(articles);
		}
		return false;
	}
}
