using jCtrl.Services.Core.Domain;
using jCtrl.Services.Core.Domain.Advert;
using jCtrl.Services.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using jCtrl.Services.Core.Domain.Catalogue;

namespace jCtrl.Infrastructure.Repositories
{
    public class CatalogueAssemblyRepository : Repository<CatalogueAssembly>, ICatalogueAssemblyRepository
    {
        public CatalogueAssemblyRepository(jCtrlContext context) : base(context)
        {
        }

        public jCtrlContext jCtrlContext
        {
            get { return Context as jCtrlContext; }
        }

        public async Task<CatalogueAssembly> GetCatalogueAssembly(int assemblyId)
        {
            return await jCtrlContext.CatalogueAssemblies
                .Where(a => a.Id == assemblyId)
                .Include(a => a.Titles)
                .Include(a => a.Nodes.Select(n => n.Titles))
                .Include(a => a.Nodes.Select(n => n.Products.Select(p => p.ProductDetails)))
                .Include(a => a.Nodes.Select(n => n.Products.Select(p => p.ProductDetails.DiscountLevel)))
                .Include(a => a.Nodes.Select(n => n.Products.Select(p => p.ProductDetails.TextInfo)))
                .Include(a => a.Nodes.Select(n => n.Products.Select(p => p.ProductDetails.Images)))
                .Include(a => a.Nodes.Select(n => n.Products.Select(p => p.ProductDetails.BranchProducts)))
                .Include(a => a.Nodes.Select(n => n.Products.Select(p => p.ProductDetails.BranchProducts.Select(b => b.SpecialOffers))))
                .Include(a => a.Nodes.Select(n => n.Products.Select(p => p.ProductDetails.BranchProducts.Select(b => b.Branch.TaxRates))))
                .Include(a => a.Nodes.Select(n => n.Children.Select(c => c.Titles)))
                .Include(a => a.Nodes.Select(n => n.Children.Select(c => c.Products)))
                .Include(a => a.Nodes.Select(n => n.Children.Select(c => c.Products.Select(p => p.ProductDetails))))
                .Include(a => a.Nodes.Select(n => n.Children.Select(c => c.Products.Select(p => p.ProductDetails.DiscountLevel))))
                .Include(a => a.Nodes.Select(n => n.Children.Select(c => c.Products.Select(p => p.ProductDetails.TextInfo))))
                .Include(a => a.Nodes.Select(n => n.Children.Select(c => c.Products.Select(p => p.ProductDetails.Images))))
                .Include(a => a.Nodes.Select(n => n.Children.Select(c => c.Products.Select(p => p.ProductDetails.BranchProducts))))
                .Include(a => a.Nodes.Select(n => n.Children.Select(c => c.Products.Select(p => p.ProductDetails.BranchProducts.Select(b => b.SpecialOffers)))))
                .Include(a => a.Nodes.Select(n => n.Children.Select(c => c.Products.Select(p => p.ProductDetails.BranchProducts.Select(b => b.Branch.TaxRates)))))
                .Include(a => a.Illustration)
                .SingleOrDefaultAsync();
        }
    }
}
