using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Gosol.VHTT.Models.HeThong
{
    public class CustomClaim
    {
        public NguoiDung NguoiDung { get; set; }
        public List<ChucNangModel> ChucNang { get; set; }
        public string ExpiresAt { get; set; }
    }
}
