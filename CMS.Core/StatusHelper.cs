using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.Core
{
    public class StaticPageStatusHelper
    {
        public const string Draft = "Draft";
        public const string Published = "Published";
    }
    public class ArticleStatusHelper
    {
        public const string New = "New";
        public const string WaitForApprove = "WaitForApprove";
        public const string Draft = "Draft";
        public const string Published = "Published";
        public const string UnPublished = "UnPublished";
        public const string Deleted = "Deleted";
    }
    public class BannerStatusHelper
    {
        public const string Published = "Published";
        public const string Unpublished = "Unpublished";
        public const string Review = "Review";
    }
    public class BasicStatusHelper
    {
        public const string InActive = "InActive";
        public const string Active = "Active";
        public const string Draft = "Draft";
        public const string NotApproved = "NotApproved";
        public const string Deleted = "Deleted";
    }
    public class StatusSetting
    {
        public const string ValueTrue = "True";
        public const string ValueFalse = "False";
        
    }
   
}
