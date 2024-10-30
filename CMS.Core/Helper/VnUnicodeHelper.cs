using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweetCMS.Core.Helper
{
    public class VnUnicodeHelper
    {
        public VnUnicodeHelper()
        {
        }

        #region  Methods

        /// <summary>
        /// Hàm dùng để Tay dau cho Tieng Viet Unicode.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ReplaceVietnameseCharacters(string value)
        {
            string English = "aAeEoOuUiIdDyY";
            string[] Vietnamese = { "áàạảãâấầậẩẫăắằặẳẵ", "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
                    "éèẹẻẽêếềệểễ", "ÉÈẸẺẼÊẾỀỆỂỄ",
                    "óòọỏõôốồộổỗơớờợởỡ", "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
                    "úùụủũưứừựửữ", "ÚÙỤỦŨƯỨỪỰỬỮ",
                    "íìịỉĩ", "ÍÌỊỈĨ",
                    "đ", "Đ",
                    "ýỳỵỷỹ", "ÝỲỴỶỸ" };
            StringBuilder sb = new StringBuilder();
            //duyệt từng ký tự
            foreach (char ch in value.ToCharArray())
            {
                int i;
                //tìm ký tự unicode trong chuỗi và xác định ký tự ascii tương ứng
                for (i = 0; i < Vietnamese.Length; i++)
                    if (Vietnamese[i].Contains(ch)) break;
                if (i < Vietnamese.Length) sb.Append(English[i]);
                else sb.Append(ch);
            }
            return sb.ToString();
        }

        #endregion
    }
}
