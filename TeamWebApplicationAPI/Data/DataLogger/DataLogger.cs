namespace TeamWebApplicationAPI.Data.ExceptionLogger
{
    public class DataLogger : IDataLogger
    {
        public string filePath { get; }
        public string relativePath { get; }

        public DataLogger(string relativePath = @".\Logs\")
        {
            this.relativePath = relativePath;
            filePath = FormatFileName(relativePath);
        }

        public void Log(Exception exception)
        {
            CheckDirectory(relativePath);
            CheckPath(filePath);
            using (StreamWriter writer = new StreamWriter(filePath, append: true))
            {
                writer.WriteLine("At {0} {1}", DateTime.Now, exception.GetType().ToString());
                writer.WriteLine(exception.ToString());
                writer.WriteLine(exception.StackTrace);
            }
        }

        public void Log(string message)
        {
            CheckDirectory(relativePath);
            CheckPath(filePath);
            using (StreamWriter writer = new StreamWriter(filePath, append: true))
            {
                writer.WriteLine("{0} : ", DateTime.Now);
                writer.WriteLine(message);
            }
        }

        public void CheckDirectory(string relativePath)
        {
            if (!Directory.Exists(relativePath))
                Directory.CreateDirectory(relativePath);
        }

        public void CheckPath(string filePath)
        {
            if (!File.Exists(filePath))
                File.Create(filePath).Dispose();
        }

        public string FormatFileName(string relativePath)
        {
            string fileName = String.Format("Log_{0:yyyy-MM-dd}_{0:HH-mm-ss}.txt", DateTime.Now);
            return String.Concat(relativePath, fileName);
        }
    }
}
