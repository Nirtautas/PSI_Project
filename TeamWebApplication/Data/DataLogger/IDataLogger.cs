namespace TeamWebApplication.Data.ExceptionLogger
{
	public interface IDataLogger
	{
		void Log(Exception ex);
		void Log(string message);
		string FormatFileName(string relativePath);
	}
}
