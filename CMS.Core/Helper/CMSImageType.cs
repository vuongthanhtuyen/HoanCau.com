using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SweetCMS.Core.Helper
{
    public enum CMSImageType
    {
        //mode=max: hinh se khong bao gio vuot qua kich thuoc cho san

        //mode=pad hoac mode=defaut hoac scale=both hoac scale=canvas: hinh luon duoc fix cung voi kich thuoc cho san
        //,neu khong bang, se tu dong tao background mau trang

        //mode=stretch: gian anh cho fix theo kich thuoc cho san

        //mode=crop: resize theo kich thuoc cho san, sau do cat anh theo kich thuoc da resize
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