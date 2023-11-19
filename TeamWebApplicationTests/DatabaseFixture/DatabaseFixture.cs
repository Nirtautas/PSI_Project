using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TeamWebApplication.Data.Database;

namespace TeamWebApplicationTests.DatabaseFixture
{
    public class DBFixture : IDisposable
    {
        public ApplicationDBContext Context { get; private set; }

        public DBFixture()
        {
            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase("Test")
                .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            Context = new ApplicationDBContext(options);
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
