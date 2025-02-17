using Com.Gosol.VHTT.DAL.HeThong;
using Com.Gosol.VHTT.Models.HeThong;

namespace Com.Gosol.VHTT.BUS.HeThong
{
    public class NguoiDungBUS
    {
        private NguoiDungDAL _NguoiDungDAL;
        public NguoiDungBUS()
        {
            _NguoiDungDAL = new NguoiDungDAL();
        }

        private NguoiDungModel GetInfoByLogin(string UserName, string Password)
        {
            return _NguoiDungDAL.GetInfoByLogin(UserName, Password);
        }
        public NguoiDung UserInfo(string UserName, string Password)
        {
            return _NguoiDungDAL.UserInfo(UserName, Password);
        }
        public NguoiDung UserInfoForRefreshToken(int nguoiDungID)
        {
            return _NguoiDungDAL.UserInfoForRefreshToken(nguoiDungID);  
        }
        private NguoiDungModel GetInfoByLoginCAS(string Mail)
        {
            return _NguoiDungDAL.GetInfoByLoginCAS(Mail);
        }

        public bool VerifyUser(string UserName, string Password, string Email, ref NguoiDungModel NguoiDung)
        {
            NguoiDung = GetInfoByLogin(UserName, Password);
            if (NguoiDung != null && (NguoiDung.TrangThai == 1 || NguoiDung.TrangThai == 0))
            {
                return true;
            }
            return false;
        }
        public bool VerifyUser(string UserName, string Password, ref NguoiDungModel NguoiDung)
        {
            NguoiDung = GetInfoByLogin(UserName, Password);
            if (NguoiDung != null && (NguoiDung.TrangThai == 1 || NguoiDung.TrangThai == 0))
            {
                return true;
            }
            return false;
        }

        public bool VerifyUser(string UserName, string Password, ref NguoiDung NguoiDung)
        {
            NguoiDung = UserInfo(UserName, Password);
            if (NguoiDung != null)
            {
                return true;
            }
            return false;
        }
        public bool VerifyUserForRefreshToken(int NguoiDungID, ref NguoiDung NguoiDung)
        {
            NguoiDung = UserInfoForRefreshToken(NguoiDungID);
            if (NguoiDung != null)
            {
                return true;
            }
            return false;
        }
        public NguoiDungModel GetByTenNguoiDung(string TenNguoiDung)
        {
            return _NguoiDungDAL.GetByTenNguoiDung(TenNguoiDung);
        }

        public int UpdateThoiGianlogin(NguoiDungModel nguoiDungModel, ref string message)
        {
            return _NguoiDungDAL.UpdateThoiGianlogin(nguoiDungModel, ref message);
        }


        public NguoiDungModel GetInfoByLoginSSO(string UserName)
        {
            return _NguoiDungDAL.GetInfoByLoginSSO(UserName);
        }

        public bool VerifyUserSSO(string UserName, ref NguoiDungModel NguoiDung)
        {
            NguoiDung = GetInfoByLoginSSO(UserName);
            if (NguoiDung != null && (NguoiDung.TrangThai == 1 || NguoiDung.TrangThai == 0))
            {
                return true;
            }
            return false;
        }
    }
}
