using KWFCI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace KWFCI.Repositories
{
    public class ApplicationDbContext : IdentityDbContext<StaffUser>
    {
    }
}
