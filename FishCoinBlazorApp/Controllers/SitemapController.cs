using FishCoinBlazorApp.Data;
using FishCoinBlazorApp.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace FishCoinBlazorApp.Controllers
{
    [Route("sitemap.xml")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class SitemapController : ControllerBase
    {
        private readonly FishCoinDbContext _context;

        public SitemapController(FishCoinDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            // ჩაანაცვლე შენი რეალური დომენით წარმოებაში (Production)
            var baseUrl = "https://fishcoin.ge";

            // წამოვიღოთ ID და Name, რომ Slug-ები დავაგენერიროთ
            var products = await _context.Products.AsNoTracking()
                .Select(p => new { p.Id, p.Name })
                .ToListAsync();

            var sb = new StringBuilder();
            sb.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            sb.AppendLine("<urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">");

            // 1. მთავარი გვერდი
            AddUrl(sb, baseUrl, "1.0");

            // 2. პროდუქტების კატეგორიები (თუ გაქვს ცალკე გვერდები)
            // მაგ: AddUrl(sb, $"{baseUrl}/products", "0.9");

            // 3. ყველა პროდუქტის "Friendly" ლინკი
            foreach (var p in products)
            {
                var slug = p.Name.ToUrlSlug();
                var productUrl = $"{baseUrl}/product-details/{p.Id}/{slug}";
                AddUrl(sb, productUrl, "0.8");
            }

            sb.AppendLine("</urlset>");

            return Content(sb.ToString(), "application/xml", Encoding.UTF8);
        }

        private void AddUrl(StringBuilder sb, string url, string priority)
        {
            sb.AppendLine("  <url>");
            sb.AppendLine($"    <loc>{url}</loc>");
            sb.AppendLine($"    <lastmod>{DateTime.Now:yyyy-MM-dd}</lastmod>");
            sb.AppendLine($"    <priority>{priority}</priority>");
            sb.AppendLine("  </url>");
        }
    }
}
