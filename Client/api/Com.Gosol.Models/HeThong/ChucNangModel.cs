﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Com.Gosol.VHTT.Model.HeThong.ChucNangInfo;

namespace Com.Gosol.VHTT.Models.HeThong
{
    public class ChucNangModel
    {
        public string TenChucNang { get; set; }
        public string MaChucNang { get; set; }
        public int ChucNangID { get; set; }
        public int ChucNangChaID { get; set; }
        public string TenChucNangCha { get; set; }
        public int PhanQuyenID { get; set; }
        public int Xem { get; set; }
        public int Them { get; set; }
        public int Sua { get; set; }
        public int Xoa { get; set; }
        public int Quyen { get; set; }
        public bool? HienThi { get; set; }
        public bool? isHover { get; set; }
        public string Icon { get; set; }
        public List<ChucNangModel> Children { get; set; }
        public ChucNangModel()
        {
        }

        public ChucNangModel(int chucNangID, int chucNangChaID, string tenChucNang, string maChucNang, bool? hienThiTrenMenu, string icon, int quyen , string tenChucNangCha = null)
        {
            ChucNangID = chucNangID;
            ChucNangChaID = chucNangChaID;
            TenChucNang = tenChucNang;
            TenChucNangCha = tenChucNangCha;
            MaChucNang = maChucNang;
            HienThi = hienThiTrenMenu;
            Icon = icon;
            Quyen = quyen;
            Xem = Them = Sua = Xoa = 0;
            switch (quyen)
            {
                case 15:
                    Xem = 1;
                    Them = 1;
                    Sua = 1;
                    Xoa = 1;
                    break;
                case 14:
                    Them = 1;
                    Sua = 1;
                    Xoa = 1;
                    break;
                case 13:
                    Xem = 1;
                    Sua = 1;
                    Xoa = 1;
                    break;
                case 12:
                    Sua = 1;
                    Xoa = 1;
                    break;
                case 11:
                    Xem = 1;
                    Them = 1;
                    Xoa = 1;
                    break;
                case 10:
                    Them = 1;
                    Xoa = 1;
                    break;
                case 9:
                    Xem = 1;
                    Xoa = 1;
                    break;
                case 8:
                    Xoa = 1;
                    break;
                case 7:
                    Xem = 1;
                    Them = 1;
                    Sua = 1;
                    break;
                case 6:
                    Them = 1;
                    Sua = 1;
                    break;
                case 5:
                    Xem = 1;
                    Sua = 1;
                    break;
                case 4:
                    Sua = 1;
                    break;
                case 3:
                    Xem = 1;
                    Them = 1;
                    break;
                case 2:
                    Them = 1;
                    break;
                case 1:
                    Xem = 1;
                    break;
            }

        }


    }
}
