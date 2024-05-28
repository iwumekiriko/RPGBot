namespace RPGBot.Utils.Paths
{
    /// <summary>
    /// Easy access to local files
    /// </summary>
    public static class PathHelper
    {
        private static readonly string BasePath = Path.Combine(AppContext.BaseDirectory, "Resources", "Photos");

        /// <summary>
        /// Easy access to all local images in base directory
        /// </summary>
        /// <returns>An array of full names (including paths) of the images in base directory</returns>
        public static string[] GetAllPhotos()
            => Directory.GetFiles(BasePath, "*.png", SearchOption.AllDirectories);
        public static string GetImagePath(string subPath)
            => Path.Combine(BasePath, subPath);
    }
}
