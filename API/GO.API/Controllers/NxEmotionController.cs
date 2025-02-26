using Com.Gosol.BUS.NghiepVu;
using Com.Gosol.Models.NghiepVu;
using Com.Gosol.VHTT.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GO.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class NxEmotionController : ControllerBase
    {
        private HistoryEmotionBUS _historyEmotionBUS;
        public NxEmotionController()
        {
            _historyEmotionBUS = new HistoryEmotionBUS();
        }
        [HttpPost]
        [Route("Log")]
        public IActionResult Log(EmotionModel model)
        {
            try
            {
                _historyEmotionBUS.Log(model);
                return Ok(new BaseResultModel
                {
                    Status = 1,
                    Message = "Thành công",
                    Data = model
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResultModel
                {
                    Status = -1,
                    Message = ex.Message
                });
            }
        }
        [HttpGet]
        [Route("Data")]
        public IActionResult Data()
        {
            try
            {
                var result = _historyEmotionBUS.Data();
                return Ok(new BaseResultModel
                {
                    Status = 1,
                    Message = "Thành công",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResultModel
                {
                    Status = -1,
                    Message = ex.Message
                });
            }
        }
    }
}
