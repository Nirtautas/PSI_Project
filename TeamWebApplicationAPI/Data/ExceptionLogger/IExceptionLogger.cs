namespace TeamWebApplicationAPI.Data.ExceptionLogger
{
	public interface IExceptionLogger
	{
		void Log(Exception ex);
		void LogMessage(string message);
		string FormatFileName(string relativePath);
	}
}
