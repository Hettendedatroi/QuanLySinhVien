using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using webdemo.Models;
using webdemo.BLL;
using webdemo.Models;

namespace webdemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HanghoaController : ControllerBase
    {
        private readonly HanghoaBll bll = new HanghoaBll();
    

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            var hangHoa = bll.GetById(id);
            if (hangHoa == null) return NotFound("Không tìm thấy hàng hóa.");
            return Ok(hangHoa);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Hanghoa hh)
        {
            bll.Add(hh);
            return CreatedAtAction(nameof(GetById), new { id = hh.MaHangHoa }, hh);
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] Hanghoa hh)
        {
            bool result = bll.Update(id, hh);
            if (!result) return NotFound("Không tìm thấy mã hàng hóa để cập nhật.");
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            bool result = bll.Delete(id);
            if (!result) return NotFound("Không tìm thấy mã hàng hóa để xóa.");
            return Ok(new { message = "Xóa thành công" });
        }
    }
}
