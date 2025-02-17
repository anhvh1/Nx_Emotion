//using Com.Gosol.VHTT.Models.HeThong;
using Com.Gosol.VHTT.Models.HeThong;
using Com.Gosol.VHTT.Security;
using GO.API.Formats;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Com.Gosol.VHTT.API.Authorization
{
    public static class PermissionLevel
    {
        public const string READ = "Read";

        public const string CREATE = "Create";

        public const string EDIT = "Edit";

        public const string DELETE = "Delete";

        public const string FULLACCESS = "FullAccess";
    }
    public class CustomAuthAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        public ChucNangEnum ChucNangID { get; set; }
        public AccessLevel Quyen { get; set; }

        public CustomAuthAttribute() { }
        public CustomAuthAttribute(ChucNangEnum chucNangID, AccessLevel quyen)
        {
            ChucNangID = chucNangID;
            Quyen = quyen;
        }

        // Hàm check quyền 
        // AuthorizationFilterContext là ngữ cảnh
        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            if (CheckAVHTTessLevelByClaims(filterContext))
            {

            }
            else
            {
                filterContext.Result =
                  new ObjectResult(new { Status = -98, Message = "Người dùng không có quyền sử dụng chức năng này." });
            }
        }

        public bool CheckAVHTTessLevelByClaims(AuthorizationFilterContext filterContext)
        {
            try
            {
                // Lấy thông tin user từ claims
                var user = filterContext.HttpContext.User as ClaimsPrincipal;

                string type = "";
                string value = "," + (int)ChucNangID + ",";

                // Lấy thông tin quyền
                switch (Quyen)
                {
                    case AccessLevel.Read:
                        type = PermissionLevel.READ;
                        break;
                    case AccessLevel.Create:
                        type = PermissionLevel.CREATE;
                        break;
                    case AccessLevel.Edit:
                        type = PermissionLevel.EDIT;
                        break;
                    case AccessLevel.Delete:
                        type = PermissionLevel.DELETE;
                        break;
                    case AccessLevel.FullAVHTTess:
                        type = PermissionLevel.FULLACCESS;
                        break;
                }

                // Kiểm tra trong Claims của token có quyền sử dụng có IDUser đó không
                //var nd = JsonConvert.DeserializeObject<NguoiDung>(user.Claims.FirstOrDefault(c => c.Type == "NguoiDung").Value);
                //var chucNang = JsonConvert.DeserializeObject<List<ChucNangModel>>(user.Claims.FirstOrDefault(c => c.Type == "ChucNang").Value);

                //if (user.Claims.Where(c => c.Type.Equals(type) || c.Type.Equals(PermissionLevel.FULLACCESS)).Any(x => x.Value.Contains(value)))
                //{
                //    return true;
                //}
                var dsChucNang = new List<ChucNangModel>();
                try
                {
                    //dsChucNang = JsonConvert.DeserializeObject<List<ChucNangModel>>(user.Claims.FirstOrDefault(c => c.Type == "ChucNang").Value);

                    var customClaim = JsonConvert.DeserializeObject<CustomClaim>(GzipHelper.Decompress(user.Claims.FirstOrDefault(c => c.Type == "CustomClaim").Value));
                    dsChucNang = customClaim.ChucNang;
                }
                catch (Exception)
                {
                    return false;
                }
                if (dsChucNang != null && dsChucNang.Any(x => x.ChucNangID == ChucNangID.GetHashCode()))
                {
                    return true;
                }
                else if (ChucNangID == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }



        // Hàm check quyền 
        // AuthorizationFilterContext là ngữ cảnh
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            // Task.Run cho phép chạy không đồng bộ để không ảnh hưởng đến quá trình đăng nhập 
            var isAuth = await Task.Run(() => CheckAVHTTessLevelByClaims(context)); // check quyền

            if (!isAuth)
            {
                context.Result = new ObjectResult(new { Status = -98, Message = "Người dùng không có quyền sử dụng chức năng này." });
            }
        }
        
    }
}
