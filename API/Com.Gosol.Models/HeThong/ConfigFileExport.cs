using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Gosol.VHTT.Models.HeThong
{
    public class ConfigFileExport
    {
        public ConfigFileExport() { }
        public string CotJson { get; set; }
        public string ChiTieuJson { get; set; }
        public string CotImportJson { get; set; }
        public string ChiTieuImportJson { get; set; }
        public int RowCotImport { get; set; }
        public int RowDataImport { get; set; }
        public int MauPhieuID { get; set; }
        public bool FileBieuMau { get; set; }
        public int TotalColumn { get; set; }
        public int? KyBaoCaoID { get; set; }
        public int? Nam { get; set; }

    }
}
