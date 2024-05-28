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
    /// <summary>
    /// Get all stored image caches in database
    /// </summary>
    /// <returns>List of ImageCache's</returns>
    public async Task<List<ImageCache>> GetCachedImagesAsync()
    {
        return await _database.ImageCaches.ToListAsync();
    }
    /// <summary>
    /// Gets the concrete ImageUrl from database by ImageName
    /// </summary>
    /// <param name="imageName">ImageName from ImageCache or just string</param>
    /// <returns>ImageUrl</returns>
    public string GetImageUrl(string imageName)
    {
        return _database.ImageCaches
            .Where(i => i.ImageName == imageName)
            .Select(i => i.ImageUrl)
            .First();
    }
    /// <summary>
    /// Gets the concrete ImageUrl from database by ImageName asynchronously
    /// </summary>
    /// <param name="imageName">ImageName from ImageCache or just string</param>
    /// <returns>ImageUrl</returns>
    public async Task<string> GetImageUrlAsync(string imageName)
    {
        return await _database.ImageCaches
            .Where(i => i.ImageName == imageName)
            .Select(i => i.ImageUrl)
            .FirstAsync();
    }
}