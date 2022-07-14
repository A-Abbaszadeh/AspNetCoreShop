using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using System;

namespace UnitTest.Builders
{
    public class DatabaseBuilder
    {
        internal DatabaseContext GetDbContext()
        {
            var option = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            return new DatabaseContext(option);
        }
    }
}
