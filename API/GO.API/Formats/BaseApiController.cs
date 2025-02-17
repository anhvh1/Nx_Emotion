using Com.Gosol.VHTT.Ultilities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
//using Com.Gosol.VHTT.Models.HeThong;
using Newtonsoft.Json;
using GO.API.Formats;

namespace Com.Gosol.VHTT.API.Formats
{
    public class BaseApiController : ControllerBase
    {
        //private string _Message;
        //protected string Message { get { return string.IsNullOrEmpty(_Message) ? Constant.GetUserMessage(Status) : _Message; } set { _Message = value; } }
        //protected object Data { get; set; }
        //protected int Status { get; set; }
        //protected int TotalRow { get; set; }
        //protected int CanBoID { get; }



        //// create by AnhVH - 16/02/2024
        //// lấy thông tin người dùng, chức năng trong token

        //protected TokenModel? tokenData;


        /////----------

        //private ILogHelper _LogHelper;
        //private readonly ILogger _BugLogger;
        //public BaseApiController(ILogHelper logHelper)
        //{
        //    _LogHelper = logHelper;
        //    //_BugLogger = this._BugLogger;
        //    ClaimsPrincipal User = _LogHelper.getCurrentUser();
        //    //if (User.Claims.Any(t => t.Type == "CanBoID"))
        //    //    CanBoID = Utils.ConvertToInt32(User.Claims.FirstOrDefault(t => t.Type == "CanBoID").Value, 0);
        //    //else CanBoID = 0; // trường hợp không đăng nhập - test
        //    //if (User.Claims.Any(t => t.Type == "NguoidungID")) NguoidungID = Utils.ConvertToInt32(User.Claims.FirstOrDefault(t => t.Type == "NguoidungID").Value, 0);
        //    tokenData = new TokenModel();

        //    try
        //    {
        //        tokenData.NguoiDung = new NguoiDung();
        //        tokenData.ChucNang = new List<ChucNangModel>();

        //        var customClaim = JsonConvert.DeserializeObject<CustomClaim>(GzipHelper.Decompress(User.Claims.FirstOrDefault(c => c.Type == "CustomClaim").Value));
        //        tokenData.NguoiDung = customClaim.NguoiDung;
        //        tokenData.ChucNang = customClaim.ChucNang;
        //        var dt = DateTime.Now;
        //        if (DateTime.TryParse(customClaim.ExpiresAt, out dt))
        //        {
        //            tokenData.expires_at = dt;
        //        }

        //        //tokenData.NguoiDung = JsonConvert.DeserializeObject<NguoiDung>(User.Claims.FirstOrDefault(c => c.Type == "NguoiDung").Value);
        //        //tokenData.ChucNang = JsonConvert.DeserializeObject<List<ChucNangModel>>(User.Claims.FirstOrDefault(c => c.Type == "ChucNang").Value);
        //        //tokenData.expires_at= JsonConvert.DeserializeObject<DateTime>(User.Claims.FirstOrDefault(c => c.Type == "ExpiresAt").Value);
        //    }
        //    catch (Exception)
        //    {
        //        tokenData =null;
        //    }
        //    var _BugLogger = this._BugLogger;
        //}
        
        //public BaseApiController(ILogHelper logHelper, ILogger bugLogger)
        //{
        //    _LogHelper = logHelper;
        //    //_BugLogger = this._BugLogger;
        //    ClaimsPrincipal User = _LogHelper.getCurrentUser();
        //    //if (User.Claims.Any(t => t.Type == "CanBoID")) CanBoID = Utils.ConvertToInt32(User.Claims.FirstOrDefault(t => t.Type == "CanBoID").Value, 0);
        //    //else CanBoID = 0; // trường hợp không đăng nhập - test
        //    //if (User.Claims.Any(t => t.Type == "NguoidungID")) NguoidungID = Utils.ConvertToInt32(User.Claims.FirstOrDefault(t => t.Type == "NguoidungID").Value, 0);
        //    tokenData = new TokenModel();

        //    try
        //    {
        //        tokenData.NguoiDung = new NguoiDung();
        //        tokenData.ChucNang = new List<ChucNangModel>();

        //        var customClaim = JsonConvert.DeserializeObject<CustomClaim>(GzipHelper.Decompress(User.Claims.FirstOrDefault(c => c.Type == "CustomClaim").Value));
        //        tokenData.NguoiDung = customClaim.NguoiDung;
        //        tokenData.ChucNang = customClaim.ChucNang;
        //        var dt = DateTime.Now;
        //        if (DateTime.TryParse(customClaim.ExpiresAt, out dt))
        //        {
        //            tokenData.expires_at = dt;
        //        }
        //        //tokenData.NguoiDung = JsonConvert.DeserializeObject<NguoiDung>(User.Claims.FirstOrDefault(c => c.Type == "NguoiDung").Value);
        //        //tokenData.ChucNang = JsonConvert.DeserializeObject<List<ChucNangModel>>(User.Claims.FirstOrDefault(c => c.Type == "ChucNang").Value);
        //        //tokenData.expires_at = JsonConvert.DeserializeObject<DateTime>(User.Claims.FirstOrDefault(c => c.Type == "ExpiresAt").Value);
        //    }
        //    catch (Exception)
        //    {
        //        tokenData = null;
        //    }
        //    _BugLogger = bugLogger;
        //}
        //protected IActionResult GetActionResult()
        //{
        //    return Ok(new
        //    {
        //        Status = Status,
        //        Message = Message,
        //        Data = Data,
        //        TotalRow = TotalRow,
        //    });
        //}
        //protected IActionResult GetActionResult(int status, string message, object data = null, int totalRow = 0)
        //{
        //    if (data == null)
        //        return Ok(new
        //        {
        //            Status = status,
        //            Message = message,
        //        });
        //    else
        //        return Ok(new
        //        {
        //            Status = status,
        //            Message = message,
        //            Data = data,
        //            TotalRow = totalRow,
        //        });
        //}
        //protected IActionResult CreateActionResult(string LogString, EnumLogType Action, Func<IActionResult> funct)
        //{
        //    try
        //    {
        //        WriteLog(LogString, (int)Action);
        //        return funct.Invoke();
        //    }
        //    catch (Exception ex)
        //    {
        //        WriteLog(ex.Message, (int)EnumLogType.Error);
        //        if (_BugLogger != null)
        //            _BugLogger.LogInformation(ex.Message, LogString);
        //        Status = -1;
        //        return Ok(new
        //        {
        //            Status = -1,
        //            Message = ex.Message,
        //        });
        //    }
        //}
        //protected IActionResult CreateActionResult(bool logging, string LogString, EnumLogType Action, Func<IActionResult> funct)
        //{
        //    try
        //    {
        //        if (logging)
        //            WriteLog(LogString, (int)Action);
        //        return funct.Invoke();
        //    }
        //    catch (Exception ex)
        //    {
        //        WriteLog(ex.Message, (int)EnumLogType.Error);
        //        if (_BugLogger != null)
        //            _BugLogger.LogInformation(ex.Message, LogString);
        //        Status = -1;
        //        return Ok(new
        //        {
        //            Status = -1,
        //            Message = ex.Message,
        //        });
        //    }
        //}
        //protected IActionResult CreateActionResultAsync(string LogString, EnumLogType Action, Func<IActionResult> funct)
        //{
        //    try
        //    {
        //        WriteLog(LogString, (int)Action);
        //        return funct.Invoke();
        //    }
        //    catch (Exception ex)
        //    {
        //        WriteLog(ex.Message, (int)EnumLogType.Error);
        //        if (_BugLogger != null)
        //            _BugLogger.LogInformation(ex.Message, LogString);
        //        Status = -1;
        //        return Ok(new
        //        {
        //            Status = -1,
        //            Message = ex.Message,
        //        });
        //    }
        //}

        //protected IActionResult CreateActionResultNew(int Status, string LogString, EnumLogType Action, Func<IActionResult> funct)
        //{
        //    try
        //    {
        //        if (Status == 1)
        //        {
        //            WriteLog(LogString, (int)Action);
        //        }
        //        return funct.Invoke();
        //    }
        //    catch (Exception ex)
        //    {
        //        WriteLog(ex.Message, (int)EnumLogType.Error);
        //        Status = -1;
        //        return Ok(new
        //        {
        //            Status = -1,
        //            Message = ex.Message,
        //        });
        //    }
        //}

        //protected async Task WriteLog(string message, int logType)
        //{
        //    _LogHelper.Log(tokenData.NguoiDung.CanBoID, message, logType);
        //}

    }
}
