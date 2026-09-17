using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Collections.Generic;
using System.Linq;
using webdemo.Models;

namespace webdemo.DAL
{
    public class Data
    {
        private static readonly List<Hanghoa> dsHangHoa = new List<Hanghoa>();
        public List<Hanghoa> GetAll() => dsHangHoa;
        public Hanghoa GetById(string id)
            => dsHangHoa.FirstOrDefault(h => h.MaHangHoa == id);
        public void Add(Hanghoa hh) => dsHangHoa .Add(hh);
        public bool Update(string id, Hanghoa hh)
        {
            var item = GetById(id);
            if (item == null ) return false;

            item.TenHangHoa = hh.TenHangHoa;
            item.DonGia = hh.DonGia;
            item.SoLuong = hh.SoLuong;
            return true;
        }
        public bool Delete(string id)
        {
            var item = GetById(id);
            if (item == null) return false;
            dsHangHoa.Remove(item);
            return true;
        }
    }
}
