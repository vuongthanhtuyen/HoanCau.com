using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TBDCMS.Core.Helper
{
    public enum CMSImageType
    {
        [Render("SocialNetwork", 560, 292, "pad")]
        SocialNetwork,
        [Render("Videos", 3600, 3600, "max")]
        Video,
        [Render("Article thumbnail", 1600, 900, "pad")]
        Article,
        [Render("Normal thumbnail", 1920, 1080, "max")]
        Normal,
    }
}