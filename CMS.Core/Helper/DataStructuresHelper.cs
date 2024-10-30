using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweetCMS.Core.Helper
{
    public class DataStructureType
    {
        public const string Product = "Product";
        public const string Article = "Article";
        public const string Service = "Service";
        public const string WebPage = "WebPage";
    }
    public class AggregateRating
    {
        /// <summary>
        /// Type AggregateRating
        /// </summary>
        public string @type { get; set; }

        /// <summary>
        /// Điểm đánh giá
        /// </summary>
        public double ratingValue { get; set; }

        /// <summary>
        /// Số lần đánh giá
        /// </summary>
        public int ratingCount { get; set; }

    }

    public class Offer
    {
        /// <summary>
        /// Type Offer
        /// </summary>
        public string @type { get; set; }

        /// <summary>
        /// Giá trị này được lấy từ danh sách hạn chế gồm các tùy chọn, biểu diễn dưới dạng đánh dấu bằng các liên kết URL
        /// </summary>
        public string availability { get; set; }

        /// <summary>
        /// Giá của . Hãy tuân theo nguyên tắc về cách sử dụng trên schema.org.
        /// </summary>
        /// 
        public decimal price { get; set; }

        /// <summary>
        /// Đơn vị tiền tệ dùng để mô tả giá , ở định dạng ISO 4217 gồm ba chữ cái.
        /// </summary>
        public string priceCurrency { get; set; }

        /// <summary>
        /// Mặt hàng đang được bán. Thông thường, thuộc tính này bao gồm một mục product () lồng ghép, nhưng cũng có thể chứa các loại mục khác hoặc văn bản tự do.
        /// </summary>
        public string itemOffered { get; set; }

        /// <summary>
        /// Ngày (ở định dạng ISO 8601) mà sau đó giá này sẽ hết hiệu lực. Đoạn trích về  của bạn có thể không hiển thị nếu thuộc tính priceValidUtil chỉ định một ngày đã qua.
        /// </summary>
        public string priceValidUntil { get; set; }

        /// <summary>
        /// URL đến trang web của bạn
        /// </summary>
        public string url { get; set; }
    }

    public class Brand
    {
        /// <summary>
        /// Type Brand
        /// </summary>
        public string @type { get; set; }

        public string name { get; set; }
    }

    public class MainEntityOfPage
    {
        public string @type { get; set; }

        public string id { get; set; }
    }
    public class Author
    {
        public string @type { get; set; }

        public string name { get; set; }
        public string url { get; set; }
    }
    public class Publisher
    {
        public string @type { get; set; }

        public string name { get; set; }
        public LogoPublisher logo { get; set; }
    }
    public class LogoPublisher
    {
        public string @type { get; set; }

        public string url { get; set; }
    }
    public class DataStructures
    {
        #region Data property general
        /// <summary>
        /// Context
        /// </summary>
        public string @context { get; set; }

        /// <summary>
        /// Type data
        /// </summary>
        public string @type { get; set; }

        /// <summary>
        /// Tên 
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Hình ảnh 
        /// </summary>
        public List<string> image { get; set; }

        /// <summary>
        /// Mô tả 
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// Đánh giá 
        /// </summary>
        public AggregateRating aggregateRating { get; set; }

        /// <summary>
        /// Đường dẫn nếu có
        /// </summary>
        public string url { get; set; }
        #endregion

        #region Data property product, service
        public string sku { get; set; }
        public string mpn { get; set; }
        /// <summary>
        /// Nhà cung cấp (áp dụng cho product, service)
        /// </summary>
        public Brand brand { get; set; }

        /// <summary>
        /// Đề nghị bán . Bao gồm một mục Offer hoặc AggregateOffer lồng ghép.(áp dụng cho product, service)
        /// </summary>
        public Offer offers { get; set; }
        #endregion

        #region Data property article
        /// <summary>
        /// áp dụng cho bài viết
        /// </summary>
        public Author author { get; set; }

        /// <summary>
        /// áp dụng cho bài viết
        /// </summary>
        public string datePublished { get; set; }

        /// <summary>
        /// áp dụng cho bài viết
        /// </summary>
        public string dateModified { get; set; }

        /// <summary>
        /// áp dụng cho bài viết
        /// </summary>
        public string headline { get; set; }
        /// <summary>
        /// áp dụng cho bài viết
        /// </summary>
        public Publisher publisher { get; set; }
        /// <summary>
        /// áp dụng cho bài viết
        /// </summary>
        public MainEntityOfPage mainEntityOfPage { get; set; }
        #endregion

    }
}
