using CatalogMicroservice.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogMicroservice.Repository
{
   public interface ICatalogRepository
    {

        IEnumerable<CatalogItem> GetCatalogItems();
        CatalogItem GetCatalogItem(Guid catagItemId);
        void InsertCatalogItem(CatalogItem catalogItem);
        void UpdateCatalogItem(CatalogItem catalogItem);
        void DeleteCatalogItem(Guid catagItemId);
    }
}
