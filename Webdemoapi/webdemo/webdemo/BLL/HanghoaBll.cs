using System.Collections.Generic;
using webdemo.Models;
using webdemo.DAL;
using webdemo.Models;

namespace webdemo.BLL
{
    public class HanghoaBll
    {
        private readonly Data dal = new Data();

        public List<Hanghoa> GetAll() =>dal.GetAll();

        public Hanghoa GetById(string id) => dal.GetById(id);

        public void Add(Hanghoa hh) => dal.Add(hh);

        public bool Update(string id, Hanghoa hh) => dal.Update(id, hh);

        public bool Delete(string id) => dal.Delete(id);
    }
}
