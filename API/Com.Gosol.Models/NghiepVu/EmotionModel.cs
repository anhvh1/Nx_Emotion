using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Gosol.Models.NghiepVu
{
    public class EmotionModel
    {
        public int IdCamera { get; set; }
        public int TypeEmotion { get; set; }
    }
    public class EmotionData
    {
        public int IdCamera { get; set;}
        public int CountHappy { get; set; }
        public int CountAngry { get; set; }
        public int CountSad { get; set; }
        public int CountNone { get; set; }
        public int TypeEmotion { get; set; }
        public float KinhDo { get; set; }
        public float ViDo { get; set; }
        public string MaCamera { get; set; }
        public string TenCamera { get; set; }
        public string ViTri { get; set; }
        public string DiaChiIP { get; set; }
    }
    //public class EmotionData 
    //{
    //    public int IdCamera { get; set; }
    //    public int TypeEmotion { get; set; }
    //    public float KinhDo { get; set; }
    //    public float ViDo { get; set; }
    //    public string MaCamera { get; set; }
    //    public string TenCamera { get; set; }
    //    public string ViTri { get; set; }
    //    public string DiaChiIP { get; set; }

    //}
    
}
