using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

namespace TBDCMS.Core.Helper
{
    public class FileHelper
    {
        private static string numberPattern = "-{0}";
        public static string ChangeFileName(string path)
        {
            string pathNonUnicode = Regex.Replace(path, @"[^\u0000-\u007F]", string.Empty);
            return Regex.Replace(pathNonUnicode, @"\s+", "-");
        }
        public static string NextAvailableFilename(string path)
        {
            // Short-cut if already available
            if (!File.Exists(path))
                return path;

            // If path has extension then insert the number pattern just before the extension and return next filename
            if (Path.HasExtension(path))
                return GetNextFilename(path.Insert(path.LastIndexOf(Path.GetExtension(path)), numberPattern));

            // Otherwise just append the pattern to the path and return next filename
            return GetNextFilename(path + numberPattern);
        }

        private static string GetNextFilename(string pattern)
        {
            string tmp = string.Format(pattern, 1);
            if (tmp == pattern)
                throw new ArgumentException("The pattern must include an index place-holder", "pattern");

            if (!File.Exists(tmp))
                return tmp; // short-circuit if no matches

            int min = 1, max = 2; // min is inclusive, max is exclusive/untested

            while (File.Exists(string.Format(pattern, max)))
            {
                min = max;
                max *= 2;
            }

            while (max != min + 1)
            {
                int pivot = (max + min) / 2;
                if (File.Exists(string.Format(pattern, pivot)))
                    min = pivot;
                else
                    max = pivot;
            }

            return string.Format(pattern, max);
        }
    }
}
