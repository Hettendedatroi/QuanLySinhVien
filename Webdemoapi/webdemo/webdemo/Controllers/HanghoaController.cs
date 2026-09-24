using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using webdemo.Models;
using webdemo.BLL;
using webdemo.Models;
using webdemo.DTO;

namespace webdemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
            var result = _bll.GetById(id);
            if (result == null) return NotFound("Không tìm thấy hàng hóa.");
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateHanghoaDTO dto)
        {
            _bll.Add(dto);
            return CreatedAtAction(nameof(GetById), new { id = dto.MaHangHoa }, dto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] UpdateHanghoaDTO dto)
        {
            bool result = _bll.Update(id, dto);
            if (!result) return NotFound("Không tìm thấy mã hàng hóa để cập nhật.");
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            if (!_bll.Delete(id)) return NotFound("Không tìm thấy hàng hóa để xóa.");
            return Ok(new { message = "Xóa thành công" });
        }
    }
}
