﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Gosol.VHTT.Models.HeThong
{
    public class HuongDanSuDungModel
    {
        public int HuongDanSuDungID { get; set; }
        public int? ChucNangID { get; set; }
        public string TieuDe { get; set; }
        public string TenFileGoc { get; set; }
        public string TenFileHeThong { get; set; }
        public DateTime NgayCapNhat { get; set; }
        public int NguoiCapNhat { get; set; }
        public int TrangThai { get; set; }
        public string TenChucNang { get; set; }
        public int? ChucNangChaID { get; set; }
        public string MaChucNang { get; set; }
        public string Base64String { get; set; }
        public string UrlFile { get; set; }
        public string Link { get; set; }
        public int? CapID { get; set; }
        public HuongDanSuDungModel()
        {
        }
    }
    public class HuongDanSuDungModel_v2
    {
        public int HuongDanSuDungID { get; set; }
        public int? ChucNangID { get; set; }
        public string TieuDe { get; set; }
        public string TenFileGoc { get; set; }
        public string TenFileHeThong { get; set; }
        public DateTime NgayCapNhat { get; set; }
        public string NguoiCapNhat { get; set; }
        public bool TrangThai { get; set; }
        public string TenChucNang { get; set; }
        public int? ChucNangChaID { get; set; }
        public string MaChucNang { get; set; }
        public string Link { get; set; }
    }

    public class HuongDanSuDungParams : ThamSoLocDanhMuc
    {
        public int? NhomID { get; set; }
    }

    public class UpLoadmodel
    {
        public string model { get; set; }
        public int num { get; set; }
    }

    public class HuongDanSuDungXoa
    {
        public int HuongDanSuDungID { get; set; }
    }
}
