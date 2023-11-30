namespace TeamWebApplicationAPI.Data.Exceptions
{
	public class SessionCredentialException : Exception
	{
        private string credentialKey { get; }

        public SessionCredentialException(string credentialKey) : base()
        {
            this.credentialKey = credentialKey;
        }

        public SessionCredentialException(string credentialKey, string message) : base(message)
        {
            this.credentialKey = credentialKey;
        }

        public SessionCredentialException(string credentialKey, string message, Exception innerException) : base(message, innerException)
        {
            this.credentialKey = credentialKey;
        }

		public SessionCredentialException()
		{
		}

		public string GetCredentialKey()
        {
            return credentialKey;
        }
    }
}
