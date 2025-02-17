using Com.Gosol.VHTT.Models.HeThong;
using Com.Gosol.VHTT.Ultilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Com.Gosol.VHTT.DAL.HeThong
{
    public class NguoiDungDAL
    {
        public NguoiDungModel GetInfoByLogin(string UserName, string Password)
        {
            NguoiDungModel user = null;
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
            {
                return user;
            }
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter(@"TenNguoiDung",SqlDbType.NVarChar),
                new SqlParameter(@"MatKhau",SqlDbType.NVarChar),
            };

            parameters[0].Value = UserName.Trim();
            parameters[1].Value = Password.Trim();
            try
            {

                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.appConnectionStrings, CommandType.StoredProcedure, "v2_HT_NguoiDung_GetByLogin", parameters))
                {
                    if (dr.Read())
                    {
                        user = new NguoiDungModel();
                        user.TenNguoiDung = Utils.ConvertToString(dr["TenNguoiDung"], "");
                        user.TenCanBo = Utils.ConvertToString(dr["TenCanBo"], "");
                        user.NguoiDungID = Utils.ConvertToInt32(dr["NguoiDungID"], 0);
                        user.CanBoID = Utils.ConvertToInt32(dr["CanBoID"], 0);
                        user.TenCoQuan = Utils.ConvertToString(dr["TenCoQuan"], "");
                        user.CoQuanID = Utils.ConvertToInt32(dr["CoQuanID"], 0);
                        user.PhongBanID = Utils.ConvertToInt32(dr["PhongBanID"], 0);
                        user.TrangThai = Utils.ConvertToInt32(dr["State"], 0);
                        user.RoleID = Utils.ConvertToInt32(dr["RoleID"], 0);
                        user.XemTaiLieuMat = Utils.ConvertToBoolean(dr["XemTaiLieuMat"], false);
                        user.CapID = Utils.ConvertToInt32(dr["CapID"], 0);
                        user.CapUBND = Utils.ConvertToInt32(dr["CapUBND"], 0);
                        user.TinhID = Utils.ConvertToInt32(dr["TinhID"], 0);
                        user.HuyenID = Utils.ConvertToInt32(dr["HuyenID"], 0);
                        user.XaID = Utils.ConvertToInt32(dr["XaID"], 0);
                        user.TenHuyen = Utils.ConvertToString(dr["TenHuyen"], string.Empty);
                        user.TenXa = Utils.ConvertToString(dr["TenXa"], string.Empty);
                        user.MaCoQuan = Utils.ConvertToString(dr["MaCQ"], string.Empty);
                        user.SuDungQuyTrinhPhucTap = Utils.ConvertToBoolean(dr["SuDungQuyTrinh"], false);
                        user.SuDungQuyTrinhGQPhucTap = Utils.ConvertToBoolean(dr["SuDungQuyTrinhGQ"], false);
                        user.SuDungQTVanThuTiepDan = Utils.ConvertToBoolean(dr["QTVanThuTiepDan"], false);
                        user.SuDungQTVanThuTiepNhanDon = Utils.ConvertToBoolean(dr["QTVanThuTiepNhanDon"], false);
                        user.BanTiepDan = Utils.ConvertToBoolean(dr["BanTiepDan"], false);
                        user.CapThanhTra = Utils.ConvertToBoolean(dr["CapThanhTra"], false);
                        user.QuyTrinhGianTiep = Utils.ConvertToInt32(dr["QuyTrinhGianTiep"], 0);
                        user.ChuTichUBND = Utils.ConvertToInt32(dr["ChuTichUBND"], 0);
                        //user.AnhHoSo = Utils.ConvertToString(dr["AnhHoSo"], "");
                        //user.QuanLyThanNhan = user.VaiTro;

                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return user;
        }

        public NguoiDung UserInfo(string UserName, string Password)
        {
            NguoiDung user = null;
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
            {
                return user;
            }
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter(@"TenNguoiDung",SqlDbType.NVarChar),
                new SqlParameter(@"MatKhau",SqlDbType.NVarChar),
            };

            parameters[0].Value = UserName.Trim();
            parameters[1].Value = Password.Trim();
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.appConnectionStrings, CommandType.StoredProcedure, "v2_HT_NguoiDung_GetByLogin", parameters))
                {
                    if (dr.Read())
                    {
                        user = new NguoiDung();
                        user.NguoiDungID = Utils.ConvertToInt32(dr["NguoiDungID"], 0);
                        user.TenNguoiDung = Utils.ConvertToString(dr["TenNguoiDung"], "");
                        user.CanBoID = Utils.ConvertToInt32(dr["CanBoID"], 0);
                        user.TenCanBo = Utils.ConvertToString(dr["TenCanBo"], "");
                        user.CoQuanID = Utils.ConvertToInt32(dr["CoQuanID"], 0);
                        user.TenCoQuan = Utils.ConvertToString(dr["TenCoQuan"], "");
                        user.CapID = Utils.ConvertToInt32(dr["CapID"], 0);
                        //user.AnhHoSo = Utils.ConvertToString(dr["AnhHoSo"], "");
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return user;
        }

        public NguoiDung UserInfoForRefreshToken(int nguoiDungID)
        {
            NguoiDung user = null;
            if (nguoiDungID < 1)
            {
                return user;
            }
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter(@"NguoiDungID",SqlDbType.Int),
            };

            parameters[0].Value = nguoiDungID;
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.appConnectionStrings, CommandType.StoredProcedure, "v2_HT_NguoiDung_RefreshToken", parameters))
                {
                    if (dr.Read())
                    {
                        user = new NguoiDung();
                        user.NguoiDungID = Utils.ConvertToInt32(dr["NguoiDungID"], 0);
                        user.TenNguoiDung = Utils.ConvertToString(dr["TenNguoiDung"], "");
                        user.CanBoID = Utils.ConvertToInt32(dr["CanBoID"], 0);
                        user.TenCanBo = Utils.ConvertToString(dr["TenCanBo"], "");
                        user.CoQuanID = Utils.ConvertToInt32(dr["CoQuanID"], 0);
                        user.TenCoQuan = Utils.ConvertToString(dr["TenCoQuan"], "");
                        //user.AnhHoSo = Utils.ConvertToString(dr["AnhHoSo"], "");
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return user;
        }
        // đng không dùng
        public NguoiDungModel GetInfoByLoginCAS(string Email)
        {
            NguoiDungModel user = null;
            if (string.IsNullOrEmpty(Email))
            {
                return user;
            }
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter(@"Email",SqlDbType.NVarChar)
            };

            parameters[0].Value = Email.Trim();

            try
            {

                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.appConnectionStrings, CommandType.StoredProcedure, "v1_HT_NguoiDung_GetByLogin_CAS", parameters))
                {
                    if (dr.Read())
                    {
                        user = new NguoiDungModel();
                        user.TenNguoiDung = Utils.ConvertToString(dr["TenNguoiDung"], "");
                        user.TenCanBo = Utils.ConvertToString(dr["TenCanBo"], "");
                        user.NguoiDungID = Utils.ConvertToInt32(dr["NguoiDungID"], 0);
                        user.CanBoID = Utils.ConvertToInt32(dr["CanBoID"], 0);
                        //user.TenNguoiDung = Utils.ConvertToString(dr["TenNguoiDung"], "");
                        user.CoQuanID = Utils.ConvertToInt32(dr["CoQuanID"], 0);
                        user.TrangThai = Utils.ConvertToInt32(dr["TrangThai"], 0);
                        user.CapCoQuan = Utils.ConvertToInt32(dr["CapID"], 0);
                        user.VaiTro = Utils.ConvertToInt32(dr["VaiTro"], 0);
                        user.AnhHoSo = Utils.ConvertToString(dr["AnhHoSo"], "");
                        user.QuanLyThanNhan = user.VaiTro;
                        // nếu người dùng có quyền quản lý cán bộ thì có quyền quản lý thân nhân
                        var QuyenCuaCanBo = new ChucNangDAL().GetListChucNangByNguoiDungID(user.NguoiDungID);
                        user.DoiMatKhauLanDau = Utils.ConvertToBoolean(dr["PasswordChanged"], false);
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return user;
        }

        public NguoiDungModel GetByTenNguoiDung(string TenNguoiDung)
        {
            var NguoiDungModel = new NguoiDungModel();
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter(@"TenNguoiDung",SqlDbType.NVarChar),
            };

            parameters[0].Value = TenNguoiDung.ToLower().Trim();
            try
            {

                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.appConnectionStrings, CommandType.StoredProcedure, "v1_HT_NguoiDung_GetByTenNguoiDung", parameters))
                {
                    if (dr.Read())
                    {
                        NguoiDungModel.NguoiDungID = Utils.ConvertToInt32(dr["NguoiDungID"], 0);
                        NguoiDungModel.TenNguoiDung = Utils.ConvertToString(dr["TenNguoiDung"], "");
                        NguoiDungModel.TrangThai = Utils.ConvertToInt32(dr["TrangThai"], 0);
                        NguoiDungModel.ThoiGianLogin = Utils.ConvertToNullableDateTime(dr["ThoiGianLogin"], null);
                        NguoiDungModel.SoLanLogin = Utils.ConvertToInt32(dr["SoLanLogin"], 0);
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return NguoiDungModel;
        }

        public int UpdateThoiGianlogin(NguoiDungModel nguoiDungModel, ref string message)
        {
            var Result = 0;
            SqlParameter[] parameters = new SqlParameter[]
              {
                            new SqlParameter("@NguoiDungID", SqlDbType.Int),
                            new SqlParameter("@ThoiGianLogin", SqlDbType.DateTime),
                            new SqlParameter("@SoLanLogin", SqlDbType.Int),
                            new SqlParameter("@TrangThai", SqlDbType.TinyInt),

              };
            parameters[0].Value = nguoiDungModel.NguoiDungID;
            parameters[1].Value = nguoiDungModel.ThoiGianLogin ?? Convert.DBNull;
            parameters[2].Value = nguoiDungModel.SoLanLogin ?? Convert.DBNull;
            parameters[3].Value = nguoiDungModel.TrangThai;
            using (SqlConnection conn = new SqlConnection(SQLHelper.appConnectionStrings))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        Result = SQLHelper.ExecuteNonQuery(trans, System.Data.CommandType.StoredProcedure, @"v1_HT_NguoiDung_UpdateThoiGianLogin", parameters);
                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        message = ex.Message;
                        trans.Rollback();
                        throw ex;
                    }
                }
            }
            return Result;
        }

        #region login SSO vĩnh phúc

        public NguoiDungModel GetInfoByLoginSSO(string UserName)
        {
            NguoiDungModel user = null;
            if (string.IsNullOrEmpty(UserName))
            {
                return user;
            }
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter(@"TenNguoiDung",SqlDbType.NVarChar),
            };

            parameters[0].Value = UserName.Trim();
            try
            {

                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.appConnectionStrings, CommandType.StoredProcedure, "v2_HT_NguoiDung_GetByLoginSSO", parameters))
                {
                    if (dr.Read())
                    {
                        user = new NguoiDungModel();
                        user.TenNguoiDung = Utils.ConvertToString(dr["TenNguoiDung"], "");
                        user.TenCanBo = Utils.ConvertToString(dr["TenCanBo"], "");
                        user.NguoiDungID = Utils.ConvertToInt32(dr["NguoiDungID"], 0);
                        user.CanBoID = Utils.ConvertToInt32(dr["CanBoID"], 0);
                        user.TenCoQuan = Utils.ConvertToString(dr["TenCoQuan"], "");
                        user.CoQuanID = Utils.ConvertToInt32(dr["CoQuanID"], 0);
                        user.TrangThai = Utils.ConvertToInt32(dr["State"], 0);
                        user.RoleID = Utils.ConvertToInt32(dr["RoleID"], 0);
                        user.XemTaiLieuMat = Utils.ConvertToBoolean(dr["XemTaiLieuMat"], false);
                        user.CapID = Utils.ConvertToInt32(dr["CapID"], 0);
                        user.TinhID = Utils.ConvertToInt32(dr["TinhID"], 0);
                        user.HuyenID = Utils.ConvertToInt32(dr["HuyenID"], 0);
                        user.MaCoQuan = Utils.ConvertToString(dr["MaCQ"], string.Empty);
                        user.SuDungQuyTrinhPhucTap = Utils.ConvertToBoolean(dr["SuDungQuyTrinh"], false);
                        user.SuDungQuyTrinhGQPhucTap = Utils.ConvertToBoolean(dr["SuDungQuyTrinhGQ"], false);
                        user.SuDungQTVanThuTiepDan = Utils.ConvertToBoolean(dr["QTVanThuTiepDan"], false);
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return user;
        }

        public List<NguoiDungModel> GetByListCoQuan(string coQuanIds)
        {
            var listNguoiDung = new List<NguoiDungModel>();
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter(@"CoQuanIds",SqlDbType.NVarChar),
            };

            parameters[0].Value = coQuanIds;
            try
            {

                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.appConnectionStrings, CommandType.StoredProcedure, "v1_HT_NguoiDung_GetByCoQuanIds", parameters))
                {
                    while (dr.Read())
                    {
                        var nguoidung = new NguoiDungModel();
                        nguoidung.NguoiDungID = Utils.ConvertToInt32(dr["NguoiDungID"], 0);
                        nguoidung.TenNguoiDung = Utils.ConvertToString(dr["TenNguoiDung"], "");
                        nguoidung.TrangThai = Utils.ConvertToInt32(dr["TrangThai"], 0);
                        nguoidung.ThoiGianLogin = Utils.ConvertToNullableDateTime(dr["ThoiGianLogin"], null);
                        nguoidung.SoLanLogin = Utils.ConvertToInt32(dr["SoLanLogin"], 0);
                        listNguoiDung.Add(nguoidung);
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listNguoiDung;
        }
        #endregion
    }
}
