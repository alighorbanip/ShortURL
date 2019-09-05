using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShortURL.Models;

namespace ShortURL.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UrlController : ControllerBase
    {
        private readonly ShortUrlContext _context;
        public UrlController(ShortUrlContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<string>> SaveUrl([FromBody]string longUrl)
        {
            var urlObj = new Url();
            urlObj.LongUrl = longUrl;
            urlObj.ShortKey = GenerateShortKey(6);// Max 6
            urlObj.CreateDate = DateTime.Now;

            _context.Urls.Add(urlObj);
            await _context.SaveChangesAsync();

            return urlObj.ShortKey;
        }

        [HttpGet]
        public async Task<ActionResult<string>> GetLongUrl(string shortKey)
        {
            var urlObj = await _context.Urls.FirstOrDefaultAsync(u => u.ShortKey == shortKey);
            if (urlObj == null)
            {
                return NotFound();
            }
            urlObj.ViewCount++;
            await _context.SaveChangesAsync();
            return urlObj.LongUrl;
        }

        private string GenerateShortKey(int lenght)
        {
            string key = Guid.NewGuid().ToString().Replace("-", "").Substring(0, lenght);
            while (_context.Urls.Any(u => u.ShortKey == key))
            {
                key = Guid.NewGuid().ToString().Replace("-", "").Substring(0, lenght);
            }
            return key;
        }
    }
}
