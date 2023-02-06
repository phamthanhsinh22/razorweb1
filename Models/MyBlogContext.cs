
using Microsoft.EntityFrameworkCore;

namespace razorweb.models
{
    public class MyBlogContext : DbContext{
        //phương thức khởi tạo MyBlogContext, đăng kí nó như là dịch vụ trong hệ thống DJ
        //options được hệ thống DJ khi MyBlogContext tạo ra
        public MyBlogContext(DbContextOptions<MyBlogContext> options) : base(options)
        {

        }
        //nạp chồng phương thức OnConfiguring và OnModelCreating
        //2 phương thức này tự động chạy khi MyBlogContext tạo ra
        protected override void OnConfiguring(DbContextOptionsBuilder builder){
            base.OnConfiguring(builder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder){
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Article> articles{set;get;}
    }
}