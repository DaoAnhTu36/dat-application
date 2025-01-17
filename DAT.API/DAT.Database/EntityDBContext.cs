using DAT.Common.Models.Configs;
using DAT.Database.Entities.WarehouseEntities;
using DAT.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DAT.Database
{
    public class EntityDBContext : DbContext
    {
        private readonly IOptions<AppConfig> _appSetting;

        public EntityDBContext(DbContextOptions<EntityDBContext> options, IOptions<AppConfig> appSetting) : base(options)
        {
            _appSetting = appSetting;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = ServiceExtensions.OverWriteConnectString(_appSetting);
            optionsBuilder.UseSqlServer(connectionString);
        }

        public DbSet<InventoryWhEntity> InventoryWhEntities { get; set; }
        public DbSet<GoodsWhEntity> GoodsWhEntities { get; set; }
        public DbSet<SupplierWhEntity> SupplierWhEntities { get; set; }
        public DbSet<TransactionWhEntity> TransactionWhEntities { get; set; }
        public DbSet<UnitWhEntity> UnitWhEntities { get; set; }
        public DbSet<StockWhEntity> StockWhEntities { get; set; }
        public DbSet<TransactionDetailWhEntity> TransactionDetailWhEntities { get; set; }
        public DbSet<CategoryWhEntity> CategoryWhEntities { get; set; }
        public DbSet<OrderWhEntity> OrderWhEntities { get; set; }
        public DbSet<GoodsRetailWhEntity> GoodsRetailWhEntities { get; set; }
        public DbSet<TransactionRetailWhEntity> TransactionRetailWhEntities { get; set; }
    }
}