using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using razorweb.models;

namespace razorweb.Pages_Blog
{
    public class IndexModel : PageModel
    {
        private readonly razorweb.models.MyBlogContext _context;

        public IndexModel(razorweb.models.MyBlogContext context)
        {
            _context = context;
        }

        public IList<Article> Article { get;set; } = default!;

        //khởi tạo hằng số hiển thị bao nhiêu mục trên 1 trang
        public const int ITEMS_PER_PAGE = 15;
        //property trang hiện tại
        //sử dụng BindProperty để nó tự động Bind từ qr vào currentPage
        [BindProperty(SupportsGet =true, Name ="p")]
        public int currentPage{set;get;}
        //property tổng số bài viết trong bảng
        public int countPages{set;get;}
        

        public async Task OnGetAsync(string SearchString)
        {


            if (_context.articles != null)
            {
                //tổng số bài viết của articles
                var totalArticle = await _context.articles.CountAsync();
                //tìm được có bao nhiêu trang
                countPages = (int)Math.Ceiling((double)totalArticle/ITEMS_PER_PAGE);
 
                if(currentPage < 1)
                   currentPage=1;
                if(currentPage > countPages)
                   countPages = countPages;

                // Article = await _context.articles.ToListAsync();
                //khi thực hiện câu truy vấn
                //Skip không lấy những phần tử đầu tiên nào
                //Skip((currentPage -1) * 10) trang hiện tại là trang 1 sẽ Skip 0 phần tử đầu tiên
                //Take(ITEMS_PER_PAGE) khi lấy ra chỉ lấy ra bao nhiêu phần tử k lấy hết
                var qr = (from a in _context.articles
                     orderby a.Created descending
                     select a)
                     .Skip((currentPage -1) * ITEMS_PER_PAGE)
                     .Take(ITEMS_PER_PAGE);

                if(!string.IsNullOrEmpty(SearchString)){
                    Article = qr.Where(a=> a.Title.Contains(SearchString)).ToList();
                }
                else{
                    Article = await qr.ToListAsync();
                }
                
                
            }
        }
    }
}
