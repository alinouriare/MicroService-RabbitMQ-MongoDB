using CatalogMicroservice.Model;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogMicroservice.Repository
{
    public class CatalogRepository : ICatalogRepository
    {
        private readonly IMongoDatabase _db;
        public CatalogRepository(IMongoDatabase db)
        {
            _db = db;
        }

        public void DeleteCatalogItem(Guid catagItemId)
        {
            var col = _db.GetCollection<CatalogItem>(CatalogItem.DocumentName);
            col.DeleteOne(c => c.Id == catagItemId);
        }

        public CatalogItem GetCatalogItem(Guid catagItemId)
        {
            var col = _db.GetCollection<CatalogItem>(CatalogItem.DocumentName);
            var catalogItem = col.Find(c => c.Id == catagItemId).FirstOrDefault();
            return catalogItem;
        }

        public IEnumerable<CatalogItem> GetCatalogItems()
        {
            var col = _db.GetCollection<CatalogItem>(CatalogItem.DocumentName);
            var catalogItems = col.Find(FilterDefinition<CatalogItem>.Empty).ToEnumerable();
            return catalogItems;
        }

        public void InsertCatalogItem(CatalogItem catalogItem)
        {
            var col = _db.GetCollection<CatalogItem>(CatalogItem.DocumentName);
            col.InsertOne(catalogItem);
        }

        public void UpdateCatalogItem(CatalogItem catalogItem)
        {
            var col = _db.GetCollection<CatalogItem>(CatalogItem.DocumentName);
            var update=Builders<CatalogItem>.Update
                    .Set(c => c.Name, catalogItem.Name)
                    .Set(c => c.Description, catalogItem.Description)
                    .Set(c => c.Price, catalogItem.Price);
            col.UpdateOne(c => c.Id == catalogItem.Id, update);
        }
    }
}
