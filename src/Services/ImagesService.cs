using RPGBot.Database;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using RPGBot.Database.Models;

namespace RPGBot.Services;

public class ImagesHandler
{
    private readonly ILogger _logger;
    private readonly RPGBotEntities _database;

    public ImagesHandler(
        ILogger<StartupService> logger,
        RPGBotEntities database
    )
    {
        _logger = logger;
        _database = database;
    }
    public async Task<List<ImageCache>> GetCachedImagesAsync()
    {
        return await _database.ImageCaches.ToListAsync();
    }
    public string GetImageUrl(string imageName)
    {
        return _database.ImageCaches
            .Where(i => i.ImageName == imageName)
            .Select(i => i.ImageUrl)
            .First();
    }
    public async Task<string> GetImageUrlAsync(string imageName)
    {
        return await _database.ImageCaches
            .Where(i => i.ImageName == imageName)
            .Select(i => i.ImageUrl)
            .FirstAsync();
    }
}