using Microsoft.EntityFrameworkCore;
using WegaisApp.Core.DB.Entities;

namespace WegaisApp.Core.DB
{
    public class WegaisContext : DbContext
    {
        public DbSet<Producer> Producers => Set<Producer>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<StockPosition> StockPositions => Set<StockPosition>();

        public WegaisContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=WegaisStock.db");
        }

        /// <summary>
        /// Вставить записи, удалить дубликаты
        /// </summary>
        /// <returns>Результат операции - кол-во добавленных записей</returns>
        public int UnionRange(WegaisDataBundle dataToAdd)
        {
            List<Producer> producersToAdd = dataToAdd.Producers.Except(Producers).ToList();
            List<Product> productsToAdd = (List<Product>)dataToAdd.Products.Except(Products).ToList();
            //List<StockPosition> stockPositionsToAdd = (List<StockPosition>)dataToAdd.StockPositions.Except(StockPositions).ToList;

            Producers.AddRange(producersToAdd);
            Products.AddRange(productsToAdd);
            StockPositions.AddRange(dataToAdd.StockPositions);

            return SaveChanges();
        }

        /// <summary>
        /// Вставить уникальные записи
        /// </summary>
        //public void AddRange(WegaisDataBundle dataBundle)
        //{
        //    // Удаление существующих сущностей
        //    if(IsContains(dataBundle))
        //    {
        //        dataBundle.Producers.RemoveAll(p => Producers.Any(dbP => dbP.ClientRegId == p.ClientRegId));
        //        dataBundle.Products.RemoveAll(p => Products.Any(dbP => dbP.AlcCode == p.AlcCode));
        //    }

        //    Producers.AddRange(dataBundle.Producers);
        //    Products.AddRange(dataBundle.Products);
        //    StockPositions.AddRange(dataBundle.StockPositions);

        //    SaveChanges();

        //    Logger.Log("Следующие записи успешно добавлены в БД\n" +
        //        $"Producers: {Producers.Count()}" +
        //        $"Products: {Products.Count()}" +
        //        $"StockPositions: {StockPositions.Count()}");
        //}

        //public bool IsContains(WegaisDataBundle dataBundle)
        //{
        //    bool containsProducer = Producers.ToList().Intersect(dataBundle.Producers).Any();
        //    bool containsProducts = Products.ToList().Intersect(dataBundle.Products).Any();

        //    return containsProducer && containsProducts;
        //}

        //public WegaisDataBundle TryAddRange(WegaisDataBundle dataBundle)
        //{
        //    if (IsContains(dataBundle))
        //    {
        //        dataBundle.Producers.RemoveAll(p => Producers.Any(dbP => dbP.ClientRegId == p.ClientRegId));
        //        dataBundle.Products.RemoveAll(p => Products.Any(dbP => dbP.AlcCode == p.AlcCode));
        //    }

        //    return dataBundle;
        //} 
    }
}