

namespace PTS.Helpers
{
    public class PagingModel{
        //trang hiện tại
        public int currentpage{set;get;}
        //tổng số trang
        public int countpages{set;get;}
        //phát sinh ra url
        public Func<int?, string> generateUrl{set;get;}
    }
}