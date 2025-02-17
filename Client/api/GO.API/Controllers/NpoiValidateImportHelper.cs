using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System.Globalization;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace GO.API.Controllers
{
    public static class NpoiValidateImportHelper
    {
        private static CultureInfo _cultureInfo => new CultureInfo("vi-VN");
        private static Regex _fullDateRegex => new Regex(@"^([0]?[0-9]|[12][0-9]|[3][01])[./-]([0]?[1-9]|[1][0-2])[./-]([0-9]{4}|[0-9]{2})$");
        private static Regex _monthYearRegex => new Regex(@"^((0[1-9])|(1[0-2]))\/(\d{4})$");
        private static Regex _yearRegex => new Regex(@"^\d{4}$");
        private static Regex _Decimal => new Regex(@"^\d{0,4}(\,\d{0,1})?$");
        private static Regex _Float => new Regex(@"^\d{0,10}(\,\d{0,2})?$");

        public static Tuple<string, bool> GetCell_Ver2(ISheet sheet, int row, int column)
        {
            var cellResult = sheet.GetRow(row).GetCell(column);
            string value = "";
            try
            {
                if (cellResult == null)
                {
                    value = "";
                }
                else if (cellResult.CellType == CellType.Error)
                {
                    value = "";
                    return new Tuple<string, bool>(value, false);
                }
                else if (cellResult.CellType == CellType.Formula)
                {
                    try
                    {
                        value = sheet.GetRow(row).GetCell(column).NumericCellValue.ToString();
                    }
                    catch (Exception)
                    {
                        value = sheet.GetRow(row).GetCell(column).StringCellValue.ToString();
                    }
                }
                else if (cellResult.CellType == CellType.Numeric)
                {
                    value = cellResult.NumericCellValue.ToString();
                }
                else
                {
                    value = cellResult.StringCellValue;
                }
                return new Tuple<string, bool>(value, true);
            }
            catch (Exception ex)
            {
                return new Tuple<string, bool>(value, false);
            }
        }
        public static string GetStringCellValueFromRow(string cellValue, string columnName, int? maxLength, StringBuilder exceptionMessage, bool isRequired = false)
        {
            if (string.IsNullOrWhiteSpace(cellValue))
            {
                if (isRequired)
                    exceptionMessage.Append($"{columnName} không được để trống; ");
            }

            if (maxLength != null && cellValue.Length > maxLength)
                exceptionMessage.Append($"{columnName} không được phép vượt quá {maxLength.Value} ký tự; ");
            return cellValue;
        }
        public static string GetNumberCellValueFromRow(string cellValue, string columnName, int? maxLength, StringBuilder exceptionMessage, bool isRequired = false)
        {
            long number = -1;
            if (string.IsNullOrWhiteSpace(cellValue))
            {
                if (isRequired)
                    exceptionMessage.Append($"{columnName} không được để trống; ");
            }
            else
            {
                if (!long.TryParse(cellValue.Trim(), out number))
                {
                    exceptionMessage.Append($"{columnName} phải là số; ");
                }
                else if (number < 0)
                {
                    exceptionMessage.Append($"{columnName} phải là số nguyên dương; ");
                }
                else if (cellValue.Length > 11)
                {
                    exceptionMessage.Append($"{columnName}  không được quá {maxLength} chữ số; ");
                }
            }
            return cellValue;
        }

        public static string GetMoneyCellValueFromRow(string cellValue, string columnName, int? maxLength, StringBuilder exceptionMessage, bool isRequired = false)
        {
            long number = -1;
            if (string.IsNullOrWhiteSpace(cellValue))
            {
                if (isRequired)
                    exceptionMessage.Append($"{columnName} không được để trống; ");
            }
            else
            {
                cellValue = cellValue.Replace(",", ".").Replace(".", string.Empty);
                if (!long.TryParse(cellValue.Trim(), out number))
                {
                    exceptionMessage.Append($"{columnName} phải là số; ");
                }
                else if (number < 0)
                {
                    exceptionMessage.Append($"{columnName} phải là số dương; ");
                }
                else if (cellValue.Length > maxLength)
                {
                    exceptionMessage.Append($"{columnName}  không được quá {maxLength} chữ số; ");
                }
            }
            return cellValue;
        }


        public static string GetDecimalCellValueFromRow(string cellValue, string columnName, int? maxLength, StringBuilder exceptionMessage, bool isRequired = false)
        {

            if (string.IsNullOrWhiteSpace(cellValue))
            {
                if (isRequired)
                    exceptionMessage.Append($"{columnName} không được để trống; ");
            }
            else
            {
                cellValue = cellValue.Replace(".", string.Empty);
                var isYearRegexValid = _Decimal.Match(cellValue);
                if (!isYearRegexValid.Success)
                {
                    exceptionMessage.Append($"{columnName} phải là số thập phân có 1 chữ số sau dấu phẩy; ");
                }
                else if (cellValue.Length > maxLength)
                {
                    exceptionMessage.Append($"{columnName}  không được quá {maxLength} chữ số; ");
                }
            }

            return cellValue;
        }
        public static string GetFloatCellValueFromRow(string cellValue, string columnName, int? maxLength, StringBuilder exceptionMessage, bool isRequired = false)
        {

            if (string.IsNullOrWhiteSpace(cellValue))
            {
                if (isRequired)
                    exceptionMessage.Append($"{columnName} không được để trống; ");
            }
            else
            {
                cellValue = cellValue.Replace(".", string.Empty);
                var integer = cellValue.Split(',');
                var isYearRegexValid = _Float.Match(cellValue);
                if (!isYearRegexValid.Success)
                {
                    exceptionMessage.Append($"{columnName} chỉ được là số thập phân có 10 chữ số phần nguyên và 2 chữ số sau dấu phẩy; ");
                }
                else if (integer.Count() > 0 && integer[0].Length > maxLength)
                {
                    exceptionMessage.Append($"{columnName}  không được quá {maxLength} chữ số; ");
                }
            }

            return cellValue;
        }

        public static string GetDateFormatCellValueFromRow(string cellValue, string columnName, StringBuilder exceptionMessage, bool isMoreThanNow = false, bool IsFullDate = false, bool isRequired = false, ICell cell = null)
        {
            if (string.IsNullOrWhiteSpace(cellValue))
            {
                if (isRequired)
                    exceptionMessage.Append($"{columnName} không được để trống; ");

                return null;
            }

            if (cell != null)
            {
                if (cell.CellType == CellType.Numeric)
                {
                    cellValue = cell.DateCellValue.ToShortDateString();
                }
                else
                {
                    cell.SetCellType(CellType.String);
                    cellValue = cell.StringCellValue.Trim();
                }
            }

            var isFullDateRegexValid = _fullDateRegex.IsMatch(cellValue);
            var isMonthYearRegexValid = _monthYearRegex.IsMatch(cellValue);
            var isYearRegexValid = _yearRegex.Match(cellValue);

            if (IsFullDate)
            {
                if (isFullDateRegexValid)
                {
                    DateTime validDate;
                    if (!DateTime.TryParseExact(cellValue, "dd/MM/yyyy", _cultureInfo, DateTimeStyles.None, out validDate))
                    {
                        exceptionMessage.Append($"{columnName} không tồn tại; ");
                        return null;
                    }
                    else if (isMoreThanNow)
                    {
                        if (validDate < new DateTime(1753, 1, 1))
                        {
                            exceptionMessage.Append($"{columnName} không được nhỏ hơn 01/01/1753; ");
                            return null;
                        }
                    }
                    else if (validDate < new DateTime(1753, 1, 1) || validDate > DateTime.Now)
                    {
                        exceptionMessage.Append($"{columnName} không được nhỏ hơn 01/01/1753 và không được lớn hơn ngày hiện tại; ");
                        return null;
                    }
                }
                else
                {
                    exceptionMessage.Append($"{columnName} không đúng định dạng; ");
                    return null;
                }
            }
            else
            {
                if (isFullDateRegexValid)
                {
                    DateTime validDate;
                    if (!DateTime.TryParseExact(cellValue, "dd/MM/yyyy", _cultureInfo, DateTimeStyles.None, out validDate))
                    {
                        exceptionMessage.Append($"{columnName} không tồn tại; ");
                        return null;
                    }
                    else
                    if (isMoreThanNow)
                    {
                        if (validDate < new DateTime(1753, 1, 1))
                        {
                            exceptionMessage.Append($"{columnName} không được nhỏ hơn 01/01/1753; ");
                            return null;
                        }
                    }
                    else if (validDate < new DateTime(1753, 1, 1) || validDate > DateTime.Now)
                    {
                        exceptionMessage.Append($"{columnName} không được nhỏ hơn 01/01/1753 và không được lớn hơn ngày hiện tại; ");
                        return null;
                    }
                }
                else if (isMonthYearRegexValid)
                {
                    var monthPart = int.Parse(cellValue.Trim().Split('/')[0]);
                    var yearPart = int.Parse(cellValue.Substring(cellValue.Trim().IndexOf('/') + 1));
                    if (isMoreThanNow)
                    {
                        if (yearPart < 1753)
                        {
                            exceptionMessage.Append($"{columnName} không được nhỏ hơn 01/01/1753; ");
                            return null;
                        }
                    }
                    else
                    if (yearPart > DateTime.Now.Year || yearPart < 1753 || (yearPart == DateTime.Now.Year && monthPart > DateTime.Now.Month))
                    {
                        exceptionMessage.Append($"{columnName} không được nhỏ hơn 01/01/1753 và không được lớn hơn ngày hiện tại; ");
                        return null;
                    }
                }
                else if (isYearRegexValid.Success)
                {
                    if (isMoreThanNow)
                    {
                        if (Convert.ToInt32(cellValue) < 1753)
                        {
                            exceptionMessage.Append($"{columnName} không được nhỏ hơn 01/01/1753; ");
                            return null;
                        }

                    }
                    else
                    if (Convert.ToInt32(cellValue) > DateTime.Now.Year || Convert.ToInt32(cellValue) < 1753)
                    {
                        exceptionMessage.Append($"{columnName} không được nhỏ hơn 01/01/1753 và không được lớn hơn ngày hiện tại; ");
                        return null;
                    }
                }
                else
                {
                    exceptionMessage.Append($"{columnName}  không đúng định dạng; ");
                    return null;
                }
            }
            return cellValue;
        }
        public static Boolean GetMonthDateFormatCellValueFromRow(string cellValue, string columnName, StringBuilder exceptionMessage, bool isMoreThanNow = false, bool IsFullDate = false, bool isRequired = false)
        {
            if (string.IsNullOrWhiteSpace(cellValue))
            {
                if (isRequired)
                    exceptionMessage.Append($"{columnName} không được để trống; ");

                return false;
            }

            var isFullDateRegexValid = _fullDateRegex.IsMatch(cellValue);
            var isMonthYearRegexValid = _monthYearRegex.IsMatch(cellValue);
            var isYearRegexValid = _yearRegex.Match(cellValue);

            if (isFullDateRegexValid)
            {
                DateTime validDate;
                if (!DateTime.TryParseExact(cellValue, "dd/MM/yyyy", _cultureInfo, DateTimeStyles.None, out validDate))
                {
                    exceptionMessage.Append($"{columnName} không tồn tại; ");
                    return false;
                }
                else
                if (isMoreThanNow)
                {
                    if (validDate < new DateTime(1753, 1, 1))
                    {
                        exceptionMessage.Append($"{columnName} không được nhỏ hơn 01/01/1753; ");
                        return false;
                    }
                }
                else if (validDate < new DateTime(1753, 1, 1) || validDate > DateTime.Now)
                {
                    exceptionMessage.Append($"{columnName} không được nhỏ hơn 01/01/1753 và không được lớn hơn ngày hiện tại; ");
                    return false;
                }
            }
            else if (isMonthYearRegexValid)
            {
                var monthPart = int.Parse(cellValue.Trim().Split('/')[0]);
                var yearPart = int.Parse(cellValue.Substring(cellValue.Trim().IndexOf('/') + 1));
                if (isMoreThanNow)
                {
                    if (yearPart < 1753)
                    {
                        exceptionMessage.Append($"{columnName} không được nhỏ hơn 01/01/1753; ");
                        return false;
                    }
                }
                else
                if (yearPart > DateTime.Now.Year || yearPart < 1753 || (yearPart == DateTime.Now.Year && monthPart > DateTime.Now.Month))
                {
                    exceptionMessage.Append($"{columnName} không được nhỏ hơn 01/01/1753 và không được lớn hơn ngày hiện tại; ");
                    return false;
                }
            }
            else
            {
                exceptionMessage.Append($"{columnName}  không đúng định dạng; ");
                return false;
            }
            return true;
        }

        public static string GetNewDateFormatCellValueFromRow(string cellValue, string columnName, StringBuilder exceptionMessage, bool isMoreThanNow = false, bool isRequired = false)
        {
            if (string.IsNullOrWhiteSpace(cellValue))
            {
                if (isRequired)
                    exceptionMessage.Append($"{columnName} không được để trống; ");

                return null;
            }

            var isFullDateRegexValid = _fullDateRegex.IsMatch(cellValue);

            var isMonthYearRegexValid1 = _monthYearRegex.IsMatch(cellValue);
            var isYearRegexValid1 = _yearRegex.Match(cellValue);


            if (isFullDateRegexValid)
            {
                DateTime validDate;
                if (!DateTime.TryParseExact(cellValue, "dd/MM/yyyy", _cultureInfo, DateTimeStyles.None, out validDate))
                {
                    exceptionMessage.Append($"{columnName} không tồn tại; ");
                    return null;
                }
                else if (isMoreThanNow)
                {
                    if (validDate < new DateTime(1753, 1, 1))
                    {
                        exceptionMessage.Append($"{columnName} không được nhỏ hơn 01/01/1753; ");
                        return null;
                    }
                }
                else if (validDate < new DateTime(1753, 1, 1) || validDate > DateTime.Now)
                {
                    exceptionMessage.Append($"{columnName} không được nhỏ hơn 01/01/1753 và không được lớn hơn ngày hiện tại; ");
                    return null;
                }
            }
            else if (isMonthYearRegexValid1)
            {
                var yearPart = int.Parse(cellValue.Substring(cellValue.IndexOf('/') + 1));
                if (yearPart > DateTime.Now.Year || yearPart < 1753)
                {
                    exceptionMessage.Append($"{columnName} không đúng định dạng; ");
                    return null;
                }
            }
            else if (isYearRegexValid1.Success)
            {
                if (Convert.ToInt32(cellValue) > DateTime.Now.Year || Convert.ToInt32(cellValue) < 1753)
                {
                    exceptionMessage.Append($"{columnName} không được nhỏ hơn 01/01/1753 và không được phép vượt quá ngày hiện tại; ");
                    return null;
                }
            }
            else
            {
                exceptionMessage.Append($"{columnName} không đúng định dạng; ");
                return null;
            }
            return cellValue;
        }


        public static byte GetByteValueFromString(string cellValue, string columnName, Dictionary<int, string> keyValues, StringBuilder exceptionMessage, bool isRequired = false)
        {
            byte value = 0;
            try
            {
                if (string.IsNullOrEmpty(cellValue))
                {
                    if (isRequired == true)
                    {
                        exceptionMessage.Append($"{columnName} không được để trống; ");
                    }
                    return value;
                }
                var result = keyValues.FirstOrDefault(x => x.Value.Equals(cellValue, StringComparison.OrdinalIgnoreCase));
                if (result.Equals(default(KeyValuePair<int, string>)))
                    exceptionMessage.Append($"{columnName} không tồn tại trong hệ thống; ");
                else
                    value = Convert.ToByte(result.Key);
            }
            catch
            {
                exceptionMessage.Append($"{columnName} không tồn tại trong hệ thống; ");
            }

            return value;
        }

        public static long GetLongValueFromString(string cellValue, string columnName, Dictionary<long, string> keyValues, StringBuilder exceptionMessage, bool isRequired = false)
        {
            long value = 0;
            try
            {
                if (string.IsNullOrEmpty(cellValue))
                {
                    if (isRequired == true)
                    {
                        exceptionMessage.Append($"{columnName} không được để trống; ");
                    }
                    return value;

                }
                var result = keyValues.FirstOrDefault(x => x.Value.Equals(cellValue, StringComparison.OrdinalIgnoreCase));
                if (result.Equals(default(KeyValuePair<long, string>)))
                    exceptionMessage.Append($"{columnName} không tồn tại trong hệ thống; ");
                else
                    value = Convert.ToInt64(result.Key);
            }
            catch
            {
                exceptionMessage.Append($"{columnName} không tồn tại trong hệ thống; ");
            }

            return value;
        }


        public static int GetIntValueFromString(string cellValue, string columnName, Dictionary<long, string> keyValues, StringBuilder exceptionMessage, bool isRequired = false)
        {
            int value = 0;
            try
            {
                if (string.IsNullOrEmpty(cellValue))
                {
                    if (isRequired == true)
                    {
                        exceptionMessage.Append($"{columnName} không được để trống; ");
                    }
                    return value;

                }
                var result = keyValues.FirstOrDefault(x => x.Value.Equals(cellValue, StringComparison.OrdinalIgnoreCase));
                if (result.Equals(default(KeyValuePair<int, string>)))
                {
                    exceptionMessage.Append($"{columnName} không tồn tại trong hệ thống; ");
                    return value;
                }
                else
                    value = Convert.ToInt32(result.Key);
            }
            catch
            {
                exceptionMessage.Append($"{columnName} không tồn tại trong hệ thống; ");
                return value;
            }

            return value;
        }

        public static long GetLongValueFromStringPart(string cellValue, string columnName, List<long> keys, StringBuilder exceptionMessage, bool isRequired = false)
        {
            long value = 0;
            try
            {
                if (string.IsNullOrEmpty(cellValue))
                {
                    if (isRequired == true)
                    {
                        exceptionMessage.Append($"{columnName} không được để trống; ");
                        return value;
                    }
                    return value;
                }
                if (cellValue.IndexOf(';') < 0)
                {
                    exceptionMessage.Append($"{columnName} không tồn tại trong hệ thống;  ");
                    return value;
                }
                else
                {
                    var result = long.TryParse(cellValue.Substring(0, cellValue.IndexOf(';')), out value);
                    if (result)
                    {
                        if (!keys.Any(x => x == value))
                            exceptionMessage.Append($"{columnName} không tồn tại trong hệ thống; ");
                        return value;
                    }
                }
            }
            catch
            {
                exceptionMessage.Append($"{columnName} không tồn tại trong hệ thống;  ");
                return value;
            }


            return value;
        }


        public static long GetLongValueFromStringPartCascade(string cellValue, string columnName, Dictionary<long, long> keyValues, string dependColumnName, long dependColumnValue, StringBuilder exceptionMessage, bool isRequired = false, string splits = ";")
        {
            long value = 0;
            bool result = false;
            try
            {
                if (string.IsNullOrEmpty(cellValue))
                {
                    if (isRequired == true)
                    {
                        exceptionMessage.Append($"{columnName} không được để trống; ");
                        return value;
                    }
                }
                else
                {
                    if (cellValue.IndexOf(splits) < 0)
                    {
                        exceptionMessage.Append($"{columnName} không tồn tại trong hệ thống; ");
                        return value;
                    }
                    else
                    {
                        var indexOfComma = cellValue.IndexOf(splits) + 1;
                        var lastIndexOfComma = cellValue.LastIndexOf(splits);

                        var countElement = cellValue.Split('.').Count();
                        if (countElement >= 4)
                        {
                            result = long.TryParse(cellValue.Split('.')[1], out value);
                        }
                        else
                            result = long.TryParse(cellValue.Substring(indexOfComma, lastIndexOfComma - indexOfComma), out value);
                        if (result)
                        {
                            var keyExists = keyValues.FirstOrDefault(x => x.Key == value);
                            if (keyExists.Equals(default(KeyValuePair<int, long>)))
                            {
                                exceptionMessage.Append($"{columnName} không tồn tại trong hệ thống; ");
                                return value;
                            }
                            else if (keyExists.Value != dependColumnValue)
                            {
                                exceptionMessage.Append($"{columnName} không có trong {dependColumnName}; ");
                                return value;
                            }
                        }
                    }
                }
            }
            catch
            {
                exceptionMessage.Append($"{columnName} không tồn tại trong hệ thống; ");
                return value;
            }

            return value;
        }
        public static long GetLongValueFromStringPartCascade_String(string cellValue, string columnName, Dictionary<long, string> keyValues, Dictionary<long, long> keyValuesForeignKey, string dependColumnName, long dependColumnValue, StringBuilder exceptionMessage, bool isRequired = false)
        {
            long id = 0;
            try
            {
                string value = "";
                if (string.IsNullOrEmpty(cellValue))
                {
                    if (isRequired == true)
                    {
                        exceptionMessage.Append($"{columnName} không được để trống; ");
                        return id;
                    }
                }
                else
                {
                    if (cellValue.IndexOf(';') < 0)
                    {
                        exceptionMessage.Append($"{columnName} không tồn tại trong hệ thống; ");
                        return id;
                    }
                    else
                    {
                        cellValue = cellValue.Substring(cellValue.IndexOf(';') + 1, cellValue.Length - cellValue.IndexOf(';') - 1);
                        var indexOfComma = cellValue.IndexOf(';') + 1;
                        var lastIndexOfComma = cellValue.LastIndexOf(';');
                        value = cellValue.Substring(indexOfComma, lastIndexOfComma - indexOfComma);
                        if (!string.IsNullOrEmpty(value))
                        {
                            var keyExists = keyValues.FirstOrDefault(x => x.Value == value);

                            if (keyExists.Equals(default(KeyValuePair<int, long>)))
                            {
                                exceptionMessage.Append($"{columnName} không tồn tại trong hệ thống; ");
                                return id;
                            }
                            else if (keyValuesForeignKey.FirstOrDefault(x => x.Key == keyExists.Key).Value != dependColumnValue)
                            {
                                exceptionMessage.Append($"{columnName} không có trong {dependColumnName}; ");
                                return id;
                            }
                            else
                            {
                                id = keyExists.Key;
                            }
                        }
                    }
                }

            }
            catch (Exception e)
            {
                exceptionMessage.Append($"{columnName} không tồn tại trong hệ thống; ");
            }
            return id;

        }

        public static T CheckPositiveIntegerAndMaxLength<T>(string input, string columnName, long maxlength, StringBuilder exceptionMessage)
        {
            var type = typeof(T);
            if (type.FullName.Contains("Decimal"))
            {
                try
                {
                    T parsedValue = (T)Convert.ChangeType(input.Replace(".", ","), typeof(T));
                    if (Convert.ToDouble(parsedValue) < 0 || Convert.ToDouble(parsedValue) > maxlength)
                    {
                        exceptionMessage.Append($"{columnName} là số không âm và không được vượt quá {maxlength.ToString("###,###")}; ");
                    }
                    return parsedValue;
                }
                catch (Exception)
                {
                    exceptionMessage.Append($"{columnName} không đúng định dạng; ");
                    return default(T);
                }
            }
            else if (type.FullName.Contains("Long") || type.FullName.Contains("Byte") || type.FullName.Contains("Int"))
            {
                try
                {
                    T parsedValue = (T)Convert.ChangeType(input, typeof(T));
                    if (Convert.ToDouble(parsedValue) < 0 || Convert.ToDouble(parsedValue) > maxlength)
                    {
                        exceptionMessage.Append($"{columnName} là số không âm và không được vượt quá {maxlength.ToString("###,###")}; ");
                    }
                    return parsedValue;
                }
                catch (Exception)
                {
                    exceptionMessage.Append($"{columnName} không đúng định dạng; ");
                    return default(T);
                }
            }
            return default(T);
        }

        //public static int ValidateCombox(List<EnumModel> listValue, string value, string fieldName, bool isRequired, StringBuilder exceptionMessage)
        //{
        //    int key = 0;
        //    if (string.IsNullOrEmpty(value))
        //    {
        //        if (isRequired == true)
        //        {
        //            exceptionMessage.Append($"{fieldName} không được để trống; ");
        //        }
        //    }
        //    else
        //    {
        //        var result = listValue.FirstOrDefault(x => x.Value.Equals(value, StringComparison.OrdinalIgnoreCase));
        //        if (result == null || result.Equals(default(KeyValuePair<int, string>)))
        //            exceptionMessage.Append($"{fieldName} không tồn tại trong hệ thống;");
        //        else
        //            key = Convert.ToByte(result.Key);

        //        return key;
        //    }

        //    return key;
        //}
        //public static int? ValidateComboxIsNull(List<EnumModel> listValue, string value, string fieldName, bool isRequired, StringBuilder exceptionMessage)
        //{
        //    int key = 0;
        //    if (string.IsNullOrEmpty(value))
        //    {
        //        if (isRequired == true)
        //        {
        //            exceptionMessage.Append($"{fieldName} không được để trống; ");
        //        }
        //    }
        //    else
        //    {
        //        var result = listValue.FirstOrDefault(x => x.Value.Equals(value, StringComparison.OrdinalIgnoreCase));
        //        if (result == null || result.Equals(default(KeyValuePair<int, string>)))
        //        {
        //            exceptionMessage.Append($"{fieldName} không tồn tại trong hệ thống;");
        //            return null;
        //        }
        //        else
        //            key = Convert.ToByte(result.Key);

        //        return key;
        //    }

        //    return key;
        //}
        //public static byte? ValidateComboxBykey(List<EnumModel> listValue, string value, string fieldName, bool isRequired, StringBuilder exceptionMessage)
        //{
        //    byte key = 0;

        //    if (string.IsNullOrEmpty(value))
        //    {
        //        if (isRequired == true)
        //        {
        //            exceptionMessage.Append($"{fieldName} không được để trống; ");
        //        }
        //        return null;
        //    }
        //    else
        //    {
        //        if (!SplitToId(value, out key))
        //        {
        //            exceptionMessage.Append($"{fieldName} không đúng định dạng;");
        //            return null;
        //        }

        //        var result = listValue.Count(x => x.Key == key);
        //        if (result == 0)
        //        {
        //            exceptionMessage.Append($"{fieldName} không tồn tại trong hệ thống;");
        //            return null;
        //        }
        //    }

        //    return key;
        //}
        //public static byte? ValidateComboxBykeyAndCode(List<EnumModel> listValue, string value, string fieldName, bool isRequired, StringBuilder exceptionMessage)
        //{
        //    byte key = 0;

        //    if (string.IsNullOrEmpty(value))
        //    {
        //        if (isRequired == true)
        //        {
        //            exceptionMessage.Append($"{fieldName} không được để trống; ");
        //        }
        //        return null;
        //    }
        //    else
        //    {
        //        if (!SplitToId(value, out key))
        //        {
        //            exceptionMessage.Append($"{fieldName} không tồn tại trong hệ thống;");
        //            return null;
        //        }

        //        var result = listValue.Count(x => x.Key == key);
        //        if (result == 0)
        //        {
        //            exceptionMessage.Append($"{fieldName} không tồn tại trong hệ thống;");
        //            return null;
        //        }
        //    }

        //    return key;
        //}

        private static bool SplitToId(string value, out byte id)
        {
            id = 0;
            var arr = value.Split('.');
            if (arr != null && arr.Length > 0)
            {
                return Byte.TryParse(arr[0], out id);
            }
            return false;
        }
        public static string IsNumber(string pValue, string fieldName, StringBuilder exceptionMessage)
        {
            foreach (Char c in pValue)
            {
                if (!Char.IsDigit(c))
                {
                    exceptionMessage.Append($"{fieldName} chỉ đươc nhập số nguyên dương; ");
                    break;
                }

            }

            return pValue;
        }

        public static bool IsCheckIntegerNumber(string pValue, string fieldName, StringBuilder exceptionMessage, out int number, bool checkRange, int? min, int? max)
        {
            number = 0;
            if (string.IsNullOrEmpty(pValue)) return false;
            var isValid = Int32.TryParse(pValue, out number);
            if (!isValid)
            {
                exceptionMessage.Append($"{fieldName} chỉ được nhập số nguyên dương; ");
                return false;
            }

            if (number < 0)
            {
                exceptionMessage.Append($"{fieldName} chỉ đươc nhập số nguyên dương; ");
                return false;
            }

            if (checkRange && (min != null && max != null) && (number < min || number > max))
            {
                exceptionMessage.Append($"{fieldName} chỉ được nhập giá trị từ {min} đến {max}; ");
                return false;
            }
            return true;
        }
        public static bool IsCheckIntegerNumberMax(string pValue, string fieldName, StringBuilder exceptionMessage, out int number, bool checkRange)
        {
            number = 0;
            if (string.IsNullOrEmpty(pValue)) return false;
            var isValid = Int32.TryParse(pValue, out number);
            if (!isValid)
            {
                exceptionMessage.Append($"{fieldName} chỉ đươc nhập số nguyên dương; ");
                return false;
            }

            if (number < 0)
            {
                exceptionMessage.Append($"{fieldName} chỉ đươc nhập số nguyên dương; ");
                return false;
            }
            return true;
        }
        public static bool IsCheckDecimalNumberMax(string pValue, string fieldName, StringBuilder exceptionMessage, out decimal number, bool checkRange)
        {
            number = 0;
            if (string.IsNullOrEmpty(pValue)) return false;
            var isValid = decimal.TryParse(pValue, out number);
            if (!isValid)
            {
                exceptionMessage.Append($"{fieldName} chỉ đươc nhập số nguyên dương; ");
                return false;
            }

            if (number < 0)
            {
                exceptionMessage.Append($"{fieldName} chỉ đươc nhập số nguyên dương; ");
                return false;
            }
            return true;
        }
        public static bool IsFloat(string pValue, string fieldName, StringBuilder exceptionMessage, out double number, bool checkRange, int? min, int? max)
        {
            number = 0;
            if (string.IsNullOrEmpty(pValue)) return false;
            pValue = pValue.Replace(".", ",");

            var rs = Double.TryParse(pValue, out number);
            if (!rs)
            {
                exceptionMessage.Append($"{fieldName} chỉ được nhập số hoặc số thập phân!; ");
            }

            if (checkRange && (number < min || number > max))
            {
                exceptionMessage.Append($"{fieldName} chỉ được nhập giá trị từ {min} đến {max}; ");
            }
            return true;
        }

        public static bool IsCheckIntegerNumberForMoney(string pValue, string fieldName, StringBuilder exceptionMessage, out long number, bool checkRange, int? min, int? max)
        {
            number = 0;
            if (string.IsNullOrEmpty(pValue)) return false;

            var valueConvert = pValue.Replace(".", "");
            var isValid = Int64.TryParse(valueConvert, out number);
            if (!isValid)
            {
                exceptionMessage.Append($"{fieldName} chưa nhập đúng định dạng số tiền(VD:1.000.000 hoặc 1000000); ");
                return false;
            }

            if (number < 0)
            {
                exceptionMessage.Append($"{fieldName} chỉ đươc nhập số tiền là số nguyên dương; ");
                return false;
            }

            if (checkRange && (min != null && max != null) && (number < min || number > max))
            {
                exceptionMessage.Append($"{fieldName} chỉ được nhập giá trị từ {min} đến {max}; ");
                return false;
            }
            return true;
        }

        public static string IsPhoneNumber(string pText, StringBuilder exceptionMessage)
        {
            if (string.IsNullOrEmpty(pText))
            {
                return pText;
            }
            if (pText.Length > 50)
            {
                exceptionMessage.Append($"Số điện thoại không được phép vượt quá 50 ký tự; ");
                return pText;
            }
            Regex regex = new Regex(@"^((\+[1-9]{1,4}[ \-]*)|(\([0-9]{2,3}\)[ \-]*)|([0-9]{2,4})[ \-]*)*?[0-9]{3,4}?[ \-]*[0-9]{3,4}?$");
            if (!regex.IsMatch(pText))
            {
                exceptionMessage.Append($"Định dạng số điện thoại chưa đúng!; ");

            }
            return pText;
        }
        public static string IsEmail(string pText, StringBuilder exceptionMessage)
        {
            try
            {
                if (!string.IsNullOrEmpty(pText))
                {
                    if (pText.Length > 255)
                    {
                        exceptionMessage.Append($"Email không được phép vượt quá 255 ký tự; ");
                        return pText;
                    }
                    MailAddress m = new MailAddress(pText);
                }
            }
            catch (Exception)
            {
                exceptionMessage.Append($"Định dạng email chưa đúng!; ");
            }
            return pText;
        }

        public static bool CheckNullRow(IRow row, int totalColumn)
        {
            var checkNull = true;

            for (int i = 1; i < totalColumn; i++)
            {
                var cellResult = row.GetCell(i);
                string value = "";
                if (cellResult == null)
                {
                    value = "";
                }
                else if (cellResult.CellType == NPOI.SS.UserModel.CellType.Error)
                {
                    value = cellResult.ErrorCellValue.ToString();
                }
                else if (cellResult.CellType == NPOI.SS.UserModel.CellType.Formula)
                {
                    try
                    {
                        value = row.GetCell(i).NumericCellValue.ToString();
                    }
                    catch (Exception)
                    {
                        value = row.GetCell(i).StringCellValue.ToString();
                    }
                }
                else if (cellResult.CellType == NPOI.SS.UserModel.CellType.Numeric)
                {
                    value = cellResult.NumericCellValue.ToString();
                }
                else
                {
                    value = cellResult.StringCellValue;
                }
                if (!string.IsNullOrEmpty(value))
                {
                    checkNull = false;
                    break;
                }
                if (!string.IsNullOrEmpty(value))
                {
                    checkNull = false;
                    break;
                }
            }
            return checkNull;
        }

        public static string ConvertListToXml<T>(IList<T> lst, string elementName)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();
            StringWriter sw = new StringWriter();
            XElement xmlElements = new XElement("ROOT");
            foreach (var item in lst)
            {
                XElement xmlElementName = new XElement(elementName);
                xmlElements.Add(xmlElementName);
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    var xmlElement = new XElement(pro.Name, pro.GetValue(item));
                    xmlElementName.Add(xmlElement);
                }
            }
            xmlElements.Save(sw);
            return sw.ToString();
        }

        public static CellRangeAddress GetMergedRegionContainingCell(ISheet sheet, int rowNum, int colNum)
        {
            foreach (var mergedRegion in sheet.MergedRegions)
            {
                if (mergedRegion.IsInRange(rowNum, colNum))
                {
                    return mergedRegion;
                }
            }
            return null;
        }
    }
}
