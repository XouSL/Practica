using StolAPI.Context;
using StolAPI.Controllers.DTO;
using StolAPI.Models;
using StolAPI.Replicates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace StolAPI.Managers
{
    public class StolsManager
    {

        protected ApplicationContext ApplicationContext { get; set; }
        protected DBContext DBContext { get; set; }
        public StolsManager(ApplicationContext applicationContext) { ApplicationContext = applicationContext; DBContext = applicationContext.CreateDbContext(); }

        private List<Stols> _stols { get; set; } = new List<Stols>();

        public Stols[] Stols { get => _stols.ToArray(); }
        public bool Read()
        {
            try
            {

                DBContext.Stols.Include(it => it.Magas).ToList();
                foreach (EFStols item in DBContext.Stols)
                {
                    if (item.IsDeleted != true) _stols.Add(new Stols(item));
                }
                return true;
            }
            catch { throw; }
        }

        public Stols Get(int id) => _stols.FirstOrDefault(it => it.Id == id);

        public Stols Create(StolsModel model)
        {
            try
            {
                EFStols stols = new EFStols()
                {
                    Name = model.name,
                   
                    Address = model.address,
                };
                DBContext.Add(stols);
                DBContext.SaveChanges();

                Stols replicate = new Stols(stols);
                _stols.Add(replicate);

                return replicate;
            }
            catch { throw; }
        }

        public Stols Update(StolsModel model)
        {
            try
            {

                EFStols _stol = DBContext.Stols.FirstOrDefault(it => it.Id == model.id);


                _stol.Name = model.name;
                _stol.Address = model.address;
                

                DBContext.Update(_stol);

                _stols.Remove(_stols.FirstOrDefault(it => it.Id == model.id));
                Stols repl = new Stols(_stol);
                _stols.Add(repl);

                return repl;
            }
            catch { throw; }
        }

        public bool Delete(int id)
        {
            try
            {

                EFStols _stol = DBContext.Stols.FirstOrDefault(it => it.Id == id);
                _stol.IsDeleted = true;
                DBContext.Update(_stols);

                _stols.Remove(_stols.FirstOrDefault(it => it.Id == id));
                return true;

            }
            catch { throw; }
        }
    }
}
