using Com.Gosol.VHTT.BUS.HeThong;
using Com.Gosol.VHTT.Models.HeThong;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;


namespace Com.Gosol.VHTT.API.Formats
{
    public interface ILogHelper
    {
        public void Log(int CanBoID, String logIngo, int logType);

        public void Log(int CanBoID, String logInfo, int logType, DateTime logTime);

        // ClaimsPrincipal : chứa thông tin về quyền hạn và xác thực của người dùng
        public ClaimsPrincipal getCurrentUser();
    }

    public class LogHelper : ILogHelper
    {
        private ISystemLogBUS _SystemLogBUS;

        // IHttpContextAccessor : truy cập thông tin về HTTP request hiện tại
        private readonly IHttpContextAccessor _HttpContextAVHTTessor;

        public LogHelper(ISystemLogBUS SystemLogBUS, IHttpContextAccessor HttpContextAcess)
        {
            _SystemLogBUS = SystemLogBUS;
            _HttpContextAVHTTessor = HttpContextAcess;
        }

        public void Log(int CanBoID, String logInfo, int logType)
        {
            SystemLogModel systemLogInfo = new SystemLogModel();
            systemLogInfo.CanBoID = CanBoID;
            systemLogInfo.LogInfo = logInfo;
            systemLogInfo.LogTime = DateTime.Now;
            systemLogInfo.LogType = logType;

            try
            {
                _SystemLogBUS.Insert(systemLogInfo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Log(int CanBoID, String logInfo, int logType, DateTime logTime)
        {
            SystemLogModel systemLogInfo = new SystemLogModel();
            systemLogInfo.CanBoID = CanBoID;
            systemLogInfo.LogInfo = logInfo;
            systemLogInfo.LogTime = logTime;
            systemLogInfo.LogType = logType;

            try
            {
                _SystemLogBUS.Insert(systemLogInfo);
            }
            catch
            {
            }
        }

        public ClaimsPrincipal getCurrentUser()
        {
            // Lấy thông tin User từ Http
            return _HttpContextAVHTTessor.HttpContext.User;
        }
    }
}