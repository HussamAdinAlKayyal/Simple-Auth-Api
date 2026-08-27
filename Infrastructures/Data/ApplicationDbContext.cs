using BasicAuthApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BasicAuthApi.Infrastructures.Data;

public class ApplicationDbContext(DbContextOptions options) : IdentityDbContext<User>(options) {}
