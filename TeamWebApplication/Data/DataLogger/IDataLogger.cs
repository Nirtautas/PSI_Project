namespace TeamWebApplication.Data.ExceptionLogger
{
	public interface IDataLogger
	{
		void Log(Exception ex);
		void Log(string message);
		public void CheckDirectory(string relativePath);
		public void CheckPath(string filePath);
        string FormatFileName(string relativePath);
	}
}
