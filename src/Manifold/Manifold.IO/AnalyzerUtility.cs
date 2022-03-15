namespace Manifold.IO
{
    public static class AnalyzerUtility
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetFileTimestamp()
        {
            return DateTime.Now.ToString("(yyyy-MM-dd) (HH-mm-ss)");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="mode"></param>
        /// <param name="access"></param>
        /// <param name="share"></param>
        /// <returns></returns>
        public static StreamWriter OpenWriter(
            string filePath,
            FileMode mode = FileMode.Create,
            FileAccess access = FileAccess.ReadWrite,
            FileShare share = FileShare.Read
            )
        {
            var fileStream = File.Open(filePath, mode, access, share);
            var writer = new StreamWriter(fileStream);
            return writer;
        }

    }
}