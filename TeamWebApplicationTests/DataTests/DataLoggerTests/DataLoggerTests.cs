using TeamWebApplicationAPI.Data.ExceptionLogger;

namespace TeamWebApplicationTests.DataTests.DataLoggerTests
{
    public class DataLoggerTests : IDisposable
    {
        private const string TestDirectory = @".\Test\";
        private const string relativePath = @".\Test\TestDirectory\";

        public DataLoggerTests()
        {
            if (!Directory.Exists(TestDirectory))
                Directory.CreateDirectory(TestDirectory);
        }

        public void Dispose()
        {
            if (Directory.Exists(TestDirectory))
                Directory.Delete(TestDirectory, true);
        }

        [Fact]
        public void LogException_PassingException_LogsExceptionCorrectly()
        {
            var exception = new Exception();
            var logger = new DataLogger(TestDirectory);
            logger.Log(exception);
            string fileContent = File.ReadAllText(logger.filePath);
            Assert.Contains(exception.GetType().ToString(), fileContent);
            Assert.Contains(exception.ToString(), fileContent);
        }

        [Fact]
        public void LogMessage_PassingException_LogsMessageCorrectly()
        {
            var logMessage = "Log message!";
            var logger = new DataLogger(TestDirectory);
            logger.Log(logMessage);
            string fileContent = File.ReadAllText(logger.filePath);
            Assert.Contains(logMessage, fileContent);
        }

        [Fact]
        public void CheckDirectory_DirectoryDoesntExist_CreatesDirectory()
        {
            var logger = new DataLogger(relativePath);
            logger.CheckDirectory(logger.relativePath);
            Assert.True(Directory.Exists(logger.relativePath));
        }

        [Fact]
        public void CheckPath_FileDoesntExist_CreatesFile()
        {
            var logger = new DataLogger(TestDirectory);
            logger.CheckPath(logger.filePath);
            Assert.True(File.Exists(logger.filePath));
        }
    }
}
