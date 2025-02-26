using Com.Gosol.DAL.NghiepVu;
using Com.Gosol.Models.NghiepVu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Gosol.BUS.NghiepVu
{
    public class HistoryEmotionBUS
    {
        private HistoryEmotionDAL _historyEmotionDAL;
        public HistoryEmotionBUS()
        {
            _historyEmotionDAL = new HistoryEmotionDAL();
        }
        public void Log(EmotionModel emotion)
        {
            _historyEmotionDAL.Log(emotion);    
        }
        public List<EmotionData> Data()
        {
           return _historyEmotionDAL.Data();    
        }
    }
}
