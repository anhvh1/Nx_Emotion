using Com.Gosol.VHTT.Ultilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Gosol.Models.NghiepVu;
using Com.Gosol.VHTT.Models.HeThong;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;

namespace Com.Gosol.DAL.NghiepVu
{
    public class HistoryEmotionDAL
    {
        private readonly string _connectionString;
        public HistoryEmotionDAL()
        {
            _connectionString = SQLHelper.appConnectionStrings;
        }
        public void Log(EmotionModel emotion)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("NV_HistoryEmotion_Insert", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    var parameters = new Dictionary<string, object>
                    {
                        {"IdCamera",emotion.IdCamera },
                        {"TypeEmotion",emotion.TypeEmotion },
                        {"ThoiGianInt",ConvertDateTimeToInt32(DateTime.Now)},
                    };

                    DataAccessHelper.SetSqlCommandParameters(cmd, parameters);

                    var result = Convert.ToInt32(cmd.ExecuteNonQuery());
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<EmotionData> Data()
        {
            var data = new EmotionData();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("NV_HistoryEmotion_GetList", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                var parameters = new Dictionary<string, object>
                {
                    {"StartDateInt",ConvertDateTimeToInt32( DateTime.Now.AddSeconds(-10) )},
                    {"EndDateInt",ConvertDateTimeToInt32(DateTime.Now) },
                    //{"IdCamera",idCamera },
                };

                DataAccessHelper.SetSqlCommandParameters(cmd, parameters);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    var result = DataAccessHelper.MapToList<EmotionData>(reader);
                    //var statisticalData = result.Count > 0 ? result[0] : null;
                    if (result != null)
                    {
                        foreach (var item in result)
                        {
                            item.TypeEmotion = GetMaxEmotion(item);
                        }
                    }
                    return result;
                }
            }
        }
        public int GetMaxEmotion(EmotionData model)
        {
            // Kiểm tra nếu tất cả đều bằng nhau
            if (model.CountHappy == model.CountAngry && model.CountAngry == model.CountSad && model.CountSad == model.CountNone)
                return 4; // Trả về 4 nếu tất cả bằng nhau

            // Tìm giá trị lớn nhất trong 4 trạng thái
            int max = Math.Max(Math.Max(model.CountHappy, model.CountAngry), Math.Max(model.CountSad, model.CountNone));

            // Xác định cảm xúc có giá trị lớn nhất
            if (max == model.CountHappy) return 1;
            if (max == model.CountAngry) return 2;
            if (max == model.CountSad) return 3;
            if (max == model.CountNone) return 4;

            return 4; // Mặc định nếu không rơi vào các trường hợp trên
        }
        public int ConvertDateTimeToInt32(DateTime dateTime)
        {
            DateTime epoch = new DateTime(1970, 1, 1);  // Unix epoch
            TimeSpan timeSpan = dateTime - epoch;
            return (int)timeSpan.TotalSeconds;
        }

    }
}
