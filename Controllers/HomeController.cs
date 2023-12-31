using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.WebApi.Entities.Models;
using Project.WebApi.Services;

namespace Project.WebApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
// [Authorize]
public class HomeController : ControllerBase 
{
	private ArticleService _articleService;

	public HomeController(ArticleService service) {
		_articleService = service;
	}

	[HttpGet(Name = "GetAllArticle")]
    public async Task<ActionResult<IEnumerable<Article>>> GetAllArticle() 
    {
        var articles = await _articleService.GetAll();
        return Ok(articles);
    }


	[HttpGet("{idArticle:int}", Name = "GetArticle")]
	public async Task<ActionResult<Article>> GetArticle(int idArticle)
	{
        var article = await _articleService.GetById(idArticle);
        if (article == null) 
        {
            return NotFound();
        }
        return Ok(article);
	}


	[HttpPost(Name = "AddArticle")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
	public async Task<ActionResult> AddArticle(Article art) 
	{
		await _articleService.Create(art);
		return Ok(new { message = "Article created" });
	}


	[HttpPut("{idArticle:int}", Name = "UpdateArticle")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
	public async Task<ActionResult> UpdateArticle(int idArticle, Article art) 
	{
        if (idArticle != art.Id) 
        {
            return BadRequest();
        }
        await _articleService.Update(art);
        return NoContent();
	}

	[HttpDelete("{idArticle:int}", Name = "DeleteArticle")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
	public async Task<ActionResult> DeleteArticle(int idArticle)
	{
        await _articleService.Delete(idArticle);
        return NoContent();
	}
}
