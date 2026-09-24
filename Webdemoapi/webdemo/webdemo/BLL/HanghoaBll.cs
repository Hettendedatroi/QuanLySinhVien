using System.Collections.Generic;
using System.Linq;
using webdemo.DAL;
using webdemo.DBcontext;
using webdemo.DTO;
using webdemo.Models;
using webdemo.Models;

namespace webdemo.BLL
{
    public class HanghoaBll
    {
        private readonly Data _dal;
        public HanghoaBll(Data dal)
        {
            _dal= dal;
        }

        public List<HanghoaDTO> GetAll()
        {
            var entities = _dal.GetAll();
            return entities.Select(h => new HanghoaDTO
            {
                MaHangHoa = h.MaHangHoa,
                TenHangHoa = h.TenHangHoa,
                DonGia = h.DonGia,
                SoLuong = h.SoLuong
            }).ToList();
        }

        public HanghoaDTO GetById(string id)
        {
            var h = _dal.GetById(id);
            if (h == null) return null;
            return new HanghoaDTO
            {
                MaHangHoa = h.MaHangHoa,
                TenHangHoa = h.TenHangHoa,
                DonGia = h.DonGia,
                SoLuong = h.SoLuong
            };
        }

        public void Add(CreateHanghoaDTO dto)
        {
            var entity = new Hanghoa
            {
                MaHangHoa = dto.MaHangHoa,
                TenHangHoa = dto.TenHangHoa,
                DonGia = dto.DonGia,
                SoLuong = dto.SoLuong
            };
            _dal.Add(entity);
        }

        public bool Update(string id, UpdateHanghoaDTO dto)
        {
            var entity = new Hanghoa
            {
                TenHangHoa = dto.TenHangHoa,
                DonGia = dto.DonGia,
                SoLuong = dto.SoLuong
            };
            return _dal.Update(id, entity);
        }


        public bool Delete(string id) => _dal.Delete(id);
    }
}
