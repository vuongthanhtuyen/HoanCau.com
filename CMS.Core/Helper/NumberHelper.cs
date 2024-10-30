using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace SweetCMS.Core.Helper
{
    public class NumberHelper
    {
        public static long LongParse(string value)
        {
            long l = -1;
            long.TryParse(value, out l);
            return l;
        }

        public static int IntParse(string value)
        {
            int i = -1;
            int.TryParse(value, out i);
            return i;
        }

        public static bool BoolParse(string value)
        {
            bool b = false;
            Boolean.TryParse(value, out b);
            return b;
        }

        /// <summary>
        /// Lấy chuỗi tiền theo định dạng (###.###.###)
        /// </summary>
        /// <param name="price">Tiền cần hiển thị</param>
        /// <returns></returns>
        public static string GetPriceWithFormat(object price)
        {
            string result = decimal.Zero.ToString();
            double kq = 0;
            if (price.GetType().Equals(typeof(System.String)))
            {
                if (!double.TryParse(price.ToString(), out kq))
                    kq = 0;

            }
            else
            {

                kq = (double)price;
            }
            if (kq >= 100)
                result = string.Format(CultureInfo.CreateSpecificCulture("vi-VN"), "{0:0,0}", kq);
            else if (kq < 100)
                result = kq.ToString();
            else
                result = "0";
            return result;
        }
        /// <summary>
        /// Lấy số tiền viết bằng chữ (chưa fix)
        /// </summary>
        /// <param name="price"></param >
        /// <returns></returns>
        public static string GetPriceDescriptionString(string price)
        {
            price = price.Trim();
            string result = string.Empty;

            return result;
        }

        private static string[] ChuSo = new string[10] { " không", " một", " hai", " ba", " bốn", " năm", " sáu", " bảy", " tám", " chín" };
        private static string[] Tien = new string[6] { "", " ngàn", " triệu", " tỷ", " ngàn tỷ", " triệu tỷ" };
        // Hàm đọc số thành chữ
        public static string DocTienBangChu(decimal SoTien, string strTail)
        {
            int lan, i;
            decimal so;
            string KetQua = "", tmp = "";
            int[] ViTri = new int[6];
            if (SoTien < 0) return "Số tiền âm !";
            if (SoTien == 0) return "Không đồng";
            if (SoTien > 0)
            {
                so = SoTien;
            }
            else
            {
                so = -SoTien;
            }
            //Kiểm tra số quá lớn
            if (SoTien > 999999999999999999)
            {
                SoTien = 0;
                return "";
            }
            ViTri[5] = (int)(so / 1000000000000000);
            so = so - long.Parse(ViTri[5].ToString()) * 1000000000000000;
            ViTri[4] = (int)(so / 1000000000000);
            so = so - long.Parse(ViTri[4].ToString()) * +1000000000000;
            ViTri[3] = (int)(so / 1000000000);
            so = so - long.Parse(ViTri[3].ToString()) * 1000000000;
            ViTri[2] = (int)(so / 1000000);
            ViTri[1] = (int)((so % 1000000) / 1000);
            ViTri[0] = (int)(so % 1000);
            if (ViTri[5] > 0)
            {
                lan = 5;
            }
            else if (ViTri[4] > 0)
            {
                lan = 4;
            }
            else if (ViTri[3] > 0)
            {
                lan = 3;
            }
            else if (ViTri[2] > 0)
            {
                lan = 2;
            }
            else if (ViTri[1] > 0)
            {
                lan = 1;
            }
            else
            {
                lan = 0;
            }
            for (i = lan; i >= 0; i--)
            {
                tmp = DocSo3ChuSo(ViTri[i]);
                KetQua += tmp;
                if (ViTri[i] != 0) KetQua += Tien[i];
            }
            KetQua = KetQua.Trim() + strTail;
            return KetQua.Substring(0, 1).ToUpper() + KetQua.Substring(1);
        }

        // Hàm đọc số có 3 chữ số
        private static string DocSo3ChuSo(int baso)
        {
            int tram, chuc, donvi;
            string KetQua = "";
            tram = (int)(baso / 100);
            chuc = (int)((baso % 100) / 10);
            donvi = baso % 10;
            if ((tram == 0) && (chuc == 0) && (donvi == 0)) return "";
            if (tram != 0)
            {
                KetQua += ChuSo[tram] + " trăm";
                if ((chuc == 0) && (donvi != 0)) KetQua += " lẻ";
            }
            if ((chuc != 0) && (chuc != 1))
            {
                KetQua += ChuSo[chuc] + " mươi";
                if ((chuc == 0) && (donvi != 0)) KetQua = KetQua + " lẻ";
            }
            if (chuc == 1) KetQua += " mười";
            switch (donvi)
            {
                case 1:
                    if ((chuc != 0) && (chuc != 1))
                    {
                        KetQua += " mốt";
                    }
                    else
                    {
                        KetQua += ChuSo[donvi];
                    }
                    break;
                case 5:
                    if (chuc == 0)
                    {
                        KetQua += ChuSo[donvi];
                    }
                    else
                    {
                        KetQua += " lăm";
                    }
                    break;
                default:
                    if (donvi != 0)
                    {
                        KetQua += ChuSo[donvi];
                    }
                    break;
            }
            return KetQua;
        }
        /// <summary>
        /// Hàm lấy tròn tiền mặc định
        /// </summary>
        /// <param name="input">Số tiền nhận vào</param>
        /// <returns></returns>
        public static decimal GetRoudPrice(decimal input)
        {
            return GetRoudPrice(input, 500, true);
        }
        /// <summary>
        /// Hàm làm tròn số tiền theo công thức
        /// </summary>
        /// <param name="input">Số tiền nhận vào</param>
        /// <param name="modValue">Số phần dư so sánh</param>
        /// <param name="INC">Tự đông tăng 1000/không</param>
        /// <returns></returns>
        public static decimal GetRoudPrice(decimal input, int modValue, bool INC)
        {
            decimal output = input;
            long phannguyen = (long)(output / 1000);
            int phandu = (int)(output % 1000);
            if (phandu >= modValue && INC)
                output = phannguyen + 1;
            else
                output = phannguyen;
            return output * 1000;
        }

        /// <summary>
        /// Chuyển đổi số tiền theo format 
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        public static string CovertCurrencyToString(decimal price)
        {
            CultureInfo cul = new CultureInfo("vi-VN");
            if (price % 1000 == 0)
                return price.ToString("N0", cul);
            return price.ToString("N3", cul);
        }
        /// <summary>
        /// Chuyển đổi số thành dạng: : 100 là A0 tăng đến A9, rồi B0 -> B9, C0 -> C9,..., Z0 -> Z9
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string ConvertCountToHex(int count)
        {
            //string []
            string result = string.Empty;

            return result;
        }

        #region format price
        public static CultureInfo ENCulture
        {
            get { return CultureInfo.CreateSpecificCulture("en-GB"); }
        }
        public static NumberStyles ENNumberStyles
        {
            get { return NumberStyles.Number | NumberStyles.AllowCurrencySymbol; }
        }
        public static CultureInfo Price_Us_Format = new CultureInfo("en-US");
        public static string FormatPrice(string priceText)
        {
            decimal price = 0;
            decimal.TryParse(priceText, ENNumberStyles, ENCulture, out price);
            return FormatPrice(price);
        }

        public static string FormatPrice(decimal price, string format)
        {
            return price.ToString(format, Price_Us_Format);
        }

        public static string FormatPrice(decimal price)
        {
            if (price == 0)
                return "0";
            string p = string.Empty;

            p = price.ToString("C2", Price_Us_Format);
            if (p.EndsWith(".00"))
                p = p.Remove(p.LastIndexOf(".00"), 3);
            return p.TrimStart('$');
        }

        public static string FormatPriceWithUnit(decimal price, string currentCode)
        {
            string formatted = FormatPrice(price);
            return formatted + " " + currentCode;
        }

        public static string FormatPrice(decimal price, bool usePrefix)
        {
            string formatted = FormatPrice(price);
            if (usePrefix)
                //return "$" + FormatPrice(price, "F2");
                return "$" + formatted;
            else
                return formatted;
        }

        public static string FormatPrice(object price, bool usePrefix)
        {
            return FormatPrice((decimal)price, usePrefix);
        }

        public static string FormatPrice(object price)
        {
            if (price != null)
                return FormatPrice(price.ToString());
            else
                return FormatPrice((decimal)0);
        }

        public static string FormatPrice(float price)
        {
            return FormatPrice((decimal)price);
        }

        #endregion


        public static decimal[] CaculatePrice(decimal total, decimal percentDiscount)
        {
            return CaculatePrice(total, percentDiscount, 2);
        }

        public static decimal MakePriceDecrease(decimal price, int place)
        {
            if (price > 0)
            {
                decimal round = Math.Round(price, place);
                if (round < price)
                {
                    string priceDecimalPlaces = price.ToString(System.Globalization.CultureInfo.InvariantCulture);
                    char probablyDot = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0];
                    string[] number = priceDecimalPlaces.Split(probablyDot);
                    if (number.Length > 1 && number[1].TrimEnd('0').EndsWith("5"))
                    {
                        int pow = number[1].TrimEnd('0').Length;
                        if (pow > 5)
                            pow = 5;
                        double divide = 1;
                        if (pow > 0)
                            divide = Math.Pow(10, pow);
                        price += (decimal)(5 / divide);

                        round = Math.Round(price, place);
                    }
                    return round;
                }
                else
                    price = round;
            }
            return price;
        }

        static decimal[] CaculatePrice(decimal total, decimal percentDiscount, int place)
        {
            /*
            decimal newTotal = total * ((decimal)(100 - percentDiscount) / (decimal)100);
            decimal round = Math.Round(newTotal, 2);
            decimal discount = total - round;
            if (percentDiscount <= 50 && discount > round)
                round = discount;
            return new decimal[] { round, total - round };
             */
            decimal discount = total * ((decimal)percentDiscount / (decimal)100);
            decimal round = Math.Round(discount, place);
            if (round < discount)
            {
                string priceDecimalPlaces = discount.ToString(System.Globalization.CultureInfo.InvariantCulture);
                char probablyDot = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0];
                string[] number = priceDecimalPlaces.Split(probablyDot);
                if (number.Length > 1 && number[1].TrimEnd('0').EndsWith("5"))
                {
                    int pow = number[1].TrimEnd('0').Length;
                    if (pow > 5)
                        pow = 5;
                    double divide = 1;
                    if (pow > 0)
                        divide = Math.Pow(10, pow);
                    discount += (decimal)(5 / divide);

                    round = Math.Round(discount, place);
                }
            }

            decimal newTotal = total - round;
            if (percentDiscount == 50 && newTotal > round)
                round = newTotal;
            return new decimal[] { total - round, round };
        }

    }
}
