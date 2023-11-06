namespace TeamWebApplication.Data.ExceptionLogger
{
    public class DataLogger : IDataLogger
    {
        private string filePath;
        private string relativePath;

        public DataLogger(string relativePath)
        {
            this.relativePath = relativePath;
			filePath = FormatFileName(relativePath);
		}

        public void Log(Exception exception)
        {
            if (!Directory.Exists(relativePath))
                Directory.CreateDirectory(relativePath);

            if (!File.Exists(filePath))
                File.Create(filePath).Dispose();

            using (StreamWriter writer = new StreamWriter(filePath, append: true))
            {
                writer.WriteLine("At {0} {1}", DateTime.Now, exception.GetType().ToString());
				writer.WriteLine(exception.ToString());
                writer.WriteLine(exception.StackTrace);
            }
        }

		public void Log(string message)
		{
			if (!Directory.Exists(relativePath))
				Directory.CreateDirectory(relativePath);

			if (!File.Exists(filePath))
				File.Create(filePath).Dispose();

			using (StreamWriter writer = new StreamWriter(filePath, append: true))
			{
				writer.WriteLine(message);
			}
		}

		public string FormatFileName(string relativePath)
        {
	        string fileName = String.Format("Log_{0:yyyy-MM-dd}_{0:HH-mm-ss}.txt", DateTime.Now);
	        return String.Concat(relativePath, fileName);
        }
    }
}
