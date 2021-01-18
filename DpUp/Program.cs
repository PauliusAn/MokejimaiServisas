using Microsoft.EntityFrameworkCore;
using Persistence;

namespace DpUp
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new DatabaseContext();
            context.Database.Migrate();
        }
    }
}
