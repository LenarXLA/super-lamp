using Microsoft.EntityFrameworkCore;
using Project.WebApi.Helpers;

namespace Project.WebApi.Services;

public class ArticleService
{
    private DataContext _context;

    public ArticleService(DataContext context) {
        _context = context;
    }

    public async Task<IEnumerable<Article>> GetAll() 
    {
        return await _context.Articles.ToListAsync();
    }

    public async Task<Article> GetById(int id) {
        return await _context.Articles.FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task Create(Article article) {
        article.Date = DateTime.Now;
        _context.Articles.Add(article);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Article article) {
        article.Date = DateTime.Now;
        _context.Articles.Update(article);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id) {
        var article = await GetById(id);
        _context.Articles.Remove(article);
        await _context.SaveChangesAsync();
    }
}