using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using webdemo.DBcontext;
using webdemo.Models;


namespace webdemo.DAL
{
    public class Data
    {
        private readonly AppDbContext _context;
        public Data(AppDbContext context)
        {
            _context =context;
        }
        public List<Hanghoa> GetAll() => _context.Hanghoas.AsNoTracking().ToList();
        public Hanghoa GetById(string id) => _context.Hanghoas.AsNoTracking().FirstOrDefault(h => h.MaHangHoa == id);
        public bool Add(Hanghoa hh)
        {
            var existingItem = _context.Hanghoas.Find(hh.MaHangHoa);
            if (existingItem != null) return false;

            _context.Hanghoas.Add(hh);
            _context.SaveChanges();
            return true;
        }
        public bool Update(string id, Hanghoa hh)
        {
            var item = _context.Hanghoas.Find(id);
            if (item == null ) return false;

            item.TenHangHoa = hh.TenHangHoa;
            item.DonGia = hh.DonGia;
            item.SoLuong = hh.SoLuong;
            _context.SaveChanges(); 
            return true;
        }
        public bool Delete(string id)
        {
            var item = _context.Hanghoas.Find(id);
            if (item == null) return false;
            _context.Hanghoas.Remove(item);
            _context.SaveChanges();
            return true;
        }
    }
}
