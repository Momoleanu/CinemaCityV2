using Microsoft.EntityFrameworkCore;

//Daca nu va merge dupa clone DBContext are targeted .NET>=-6.0
//Control dreapta pe proiect IP -> properties->Application-> schimbati versiunea sa fie >=6

namespace ProiectIP.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
    }
}
