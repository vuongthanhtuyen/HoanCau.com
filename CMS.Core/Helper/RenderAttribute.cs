using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace TBDCMS.Core.Helper
{
    [AttributeUsage(AttributeTargets.Field)]
    public class RenderAttribute : Attribute
    {
        private readonly string _renderAs;
        private readonly int _Width;
        private readonly int _Height;
        private readonly string _Mode = "pad";

        CultureInfo currentLanguage = CultureInfo.GetCultureInfo("en-US");

        public RenderAttribute(string renderAs, int width, int height, string mode)
        {
            this._renderAs = renderAs;
            _Width = width;
            _Height = height;
            _Mode = mode;
        }

        public override string ToString()
        {
            return _renderAs;
        }
    
        public string ToImageResizeString()
        {
            string settings = string.Empty;
            if (_Width > 0)
                settings += "w=" + _Width;
            if (_Height > 0)
                settings += "&h=" + _Height;
            if (string.IsNullOrEmpty(_Mode) == false)
            {
                if (_Mode.ToLower() == "pad")
                    settings += "&scale=canvas";
                else
                    settings += "&mode=" + _Mode;
            }

            return settings;
        }

        public int Width
        {
            get
            {
                return _Width;
            }
        }

        public int Height
        {
            get
            {
                return _Height;
            }
        }

    }
}