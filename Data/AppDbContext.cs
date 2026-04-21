using Microsoft.EntityFrameworkCore;
using CogyShift.Models;

namespace CogyShift.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    public DbSet<ShiftRecord> ShiftRecords { get; set; }
}