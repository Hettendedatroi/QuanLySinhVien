using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webdemo.BLL;
using webdemo.DTO;

namespace webdemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HanghoaController : ControllerBase
    {
        private readonly HanghoaBll _bll;
        public HanghoaController(HanghoaBll bll)
        {
            _bll = bll;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_bll.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            if (!IsValidId(id)) return ValidationProblem(ModelState);

            var result = _bll.GetById(id);
            if (result == null) return NotFound("Không tìm thấy hàng hóa.");
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateHanghoaDTO dto)
        {
            if (!_bll.Add(dto))
            {
                return Conflict(new ProblemDetails
                {
                    Status = StatusCodes.Status409Conflict,
                    Title = "Mã hàng hóa đã tồn tại.",
                    Detail = "Vui lòng sử dụng một mã hàng hóa khác."
                });
            }

            return CreatedAtAction(nameof(GetById), new { id = dto.MaHangHoa }, dto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] UpdateHanghoaDTO dto)
        {
            if (!IsValidId(id)) return ValidationProblem(ModelState);

            bool result = _bll.Update(id, dto);
            if (!result) return NotFound("Không tìm thấy mã hàng hóa để cập nhật.");
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            if (!IsValidId(id)) return ValidationProblem(ModelState);

            if (!_bll.Delete(id)) return NotFound("Không tìm thấy hàng hóa để xóa.");
            return Ok(new { message = "Xóa thành công" });
        }

        private bool IsValidId(string id)
        {
            if (!string.IsNullOrWhiteSpace(id) && id.Length <= 50) return true;

            ModelState.AddModelError(
                nameof(id),
                "Mã hàng hóa trên URL không được để trống và không được vượt quá 50 ký tự.");
            return false;
        }
    }
}
