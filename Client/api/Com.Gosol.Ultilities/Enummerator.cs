using System;
using System.ComponentModel;
using System.Web;

namespace Com.Gosol.VHTT.Ultilities
{

    #region AnhVH
    public enum EnumLogType
    {
        Error = 0, // lỗi
        //Action = 1, // thực hiện các chức năng
        DangNhap = 100,

        Insert = 101,
        Update = 102,
        Delete = 103,

        GetByID = 201,// lấy dữ liệu theo ID
        GetByName = 202, // lấy dữ liệu theo tên, key
        GetList = 203, // lấy danh sách dữ liệu      

        BackupDatabase = 901,
        RestoreDatabase = 902,

        Other = 500,

    }



    public enum EnumCapCoQuan : Int32
    {
        CapTrungUong = 0,
        CapTinh = 1,
        CapSo = 2,
        CapHuyen = 3,
        CapPhong = 4,
        CapXa = 5,
    }

    public enum EnumCapQuanLyCanBo : Int32
    {
        CapTinh = 1,
        CapHuyen = 2,
        ToanTinh = 3
    }

    public enum EnumTrangThaiCanBo
    {
        DangLamViec = 0,
        NghiHuu = 1,
        ChuyenCongTac = 2,
        NghiViec = 3,
    }


    public enum StatusResult
    {
        // -98 = hết hạn, -99 = không đủ quyền, -1 = lỗi hệ thống, 0 = lỗi validate, 1 = thành công, 2 = đã tồn tại
        HetHan = -98,
        KhongDuQuyen = -99,
        LoiHeThong = -1,
        LoiValidate = 0,
        ThanhCong = 1,
        DaTonTai = 2,
    }
    public enum EnumTrangThaiNhanVien : Int32
    {
        DaNghi = 0,
        DangLam = 1,
    }

    #endregion

    #region VHTT
    public enum CapQuanLy
    {
        CapSoNganh = 1,//
        CapUBNDHuyen = 2,
        CapUBNDXa = 3,//
        CapUBNDTinh = 4,//
        CapTrungUong = 5,
        ToanHuyen = 6, // Toàn huyện= Tổng UBND huyện  +UBND Xã
        CapPhong = 11,
        CapToanTinh = 12,
        ToanHuyenChiTiet = 13,
        CapUBNDXaChiTiet = 14,
        CapUBNDHuyenChiTiet = 15,
        CapUBNDTinhChiTiet = 16,
        CapSoNganhChiTiet = 17

    }
    public enum EnumChucVu
    {
        LanhDao = 1,
        TruongPhong = 2,
        ChuyenVien = 3,
    }
    public enum EnumLoaiFile
    {
        FileAnhDaiDien = 1,
        FileHuongDanSuDung = 2,
        FileCauHinhHeThong = 3,
        FileQuanLyDiTichToanTinh=4,
        FileQuanLyBaoVatQuocGia=5,
        FileQuanLyBaoTang=6,
        FileQuanLyHienVatBaoTang=7,
        FileQuanLyDiSanVanHoaPhiVatThe=8,
    }
    public enum EnumLoaiLog
    {
        Them = 1,
        Sua = 2,
        Xoa = 2,
    }

    public enum RoleEnum
    {
        LanhDao = 1,
        LanhDaoPhong = 2,
        ChuyenVien = 3
    }

    public enum EnumDanhMucChung
    {
        [Description("đơn vị tính")]
        DMDonViTinh = 1,            // Danh mục đơn vị tính
        [Description("kỳ báo cáo")]
        DMKyBaoCao = 2,             // Danh mục kỳ báo cáo (thời gian)
        [Description("loại mẫu phiếu")]
        DMLoaiMauPhieu = 3,             // Danh mục loại mẫu phiếu - loại báo cáo
        [Description("di tích")]
        DMLoaiDiTich = 4,           // Danh mục loại di tích
        [Description("cấp di tích xếp hạng")]
        DMCapDiTichXepHang = 5,     // Danh mục cấp di tích xếp hạng
        [Description("Di sản văn hóa")]
        DMDISANVANHOA = 6,
        [Description("Giải trí")]
        GiaiTri = 7,
        [Description("Pham Vi")]
        PhamVi = 8,
        [Description("Phan Loai")]
        PhanLoai = 9,
        [Description("Quản lý bảo tàng")]
        QuanLyBaoTang = 10,
        [Description("Quản lý bảo vật quốc gia")]
        QUANLYBAOVATQUOCGIA = 11,
        [Description("Di sản tư liệu")]
        DISANTULIEU = 12,
        [Description("Di sản văn hóa phi vật thể")]
        DISANVANHOAPHIVATTHE = 13, 
        [Description("Danh mục thư viện")]
        DMThuVien = 14,
        

    }

    public enum EnumKieuDuLieuCot
    {
        CotInt = 1, // int
        CotDemical = 2, // demical
        CotString = 3, // string
        CotDate = 4, // datetime
        CotBool = 5, // bool
    }

    public enum EnumLoaiCot
    {
        CotHeader = 1,
        CotContent = 2,
        CotFooter = 3,
    }
    public enum LoaiHienThiSTT
    {
        [Description("Hiển thị ngang")]
        HienThiNgang = 1,
        [Description("Hiển thị dọc")]
        HienThiDoc = 2,
    }

    public enum EnumLoaiSTT
    {
        [Description("Số la mã")]
        SoLaMa = 1,
        [Description("Số tự nhiên")]
        SoTuNhien = 2,
        [Description("Chữ cái")]
        ChuCai = 3,
        [Description("Gạch đầu dòng")]
        GachDauDong = 4,
        [Description("Cộng đầu dòng")]
        CongDauDong = 5,
    }

    public enum EnumCodeDanhMucCot
    {
        [Description("Phụ lục")] // bên phải
        TOP_PHULUC = 101,
        [Description("Đơn vị chủ quản")]// bên trái
        TOP_DVCQ = 102,
        [Description("Đơn vị thực hiện báo cáo")] // bên trái
        TOP_DVTHBC = 103,
        [Description("Quốc hiệu tiêu ngữ")]// giữa khi k có đơn vị chủ quản, phải khi có đơn vị chủ quản
        TOP_QHTN = 104,
        [Description("Tiêu đề báo cáo")]// giữa
        TOP_TDBC = 105,
        [Description("Phần ngày tháng")]// phải
        TOP_PHANNGAYTHANG = 106,
        [Description("Đơn vị tính")]// phải
        TOP_DVT = 107,

        [Description("Số Thứ tự")]// đầu tiên nếu có
        BODY_STT = 201,
        [Description("Nội dung")]// đầu tiên hoặc sau stt
        BODY_ND = 202,
        [Description("Ghi chú")]// ở cuối cùng bên phải, chỉ có 1 khi có nhiều tháng
        BODY_GHICHU = 203,
        [Description("tháng/ năm")]//theo biểu mẫu nghiệp vụ
        BODY_THANGNAM = 204,
        
        [Description("Lưu Nhận")]// bên trái
        BOT_LUUNHAN = 301,
        [Description("Phần ngày tháng")]// phải
        BOT_PHANNGAYTHANG = 302,
        [Description("Chức danh")]// phải
        BOT_CHUCDANH = 303,
        [Description("Người ký")]// phải
        BOT_NGUOIKY = 304,
    }

    

    #endregion
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            var type = value.GetType();
            var fieldInfo = type.GetField(value.ToString());
            var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }

        public static T GetEnumValue<T>(int value) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            foreach (T enumValue in Enum.GetValues(typeof(T)))
            {
                if (Convert.ToInt32(enumValue) == value)
                {
                    return enumValue;
                }
            }

            throw new ArgumentException("No enum value corresponds to the specified value");
        }
    }
};