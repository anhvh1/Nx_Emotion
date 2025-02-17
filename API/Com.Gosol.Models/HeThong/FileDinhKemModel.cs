﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Gosol.VHTT.Models.HeThong
{
    public class FileDinhKemModel
    {
        public string TenFileGoc;

        public int FileID { get; set; }
        public int? NghiepVuID { get; set; }
        public int? DMTenFileID { get; set; }
        public string TenFile { get; set; }
        public DateTime? NgayCapNhat { get; set; }
        public int? NguoiCapNhat { get; set; }
        public int FileType { get; set; }
        public int? TrangThai { get; set; }
        public string FolderPath { get; set; }
        public string FileUrl { get; set; }
        public string NoiDung { get; set; }
        public bool? IsBaoMat { get; set; }
        public bool? IsMaHoa { get; set; }
        public string GroupUID { get; set; }
        public FileDinhKemModel()
        {

        }
        public FileDinhKemModel(int FileID)
        {
            this.FileID = FileID;
        }
    }

    public class FileLogInfo
    {
        public int FileID { get; set; }
        public int LoaiLog { get; set; }
        public int LoaiFile { get; set; }
        public bool IsMaHoa { get; set; }
        public bool IsBaoMat { get; set; }
    }
}
