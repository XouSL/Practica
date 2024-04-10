using StolAPI.Context;
using StolAPI.Controllers.DTO;
using StolAPI.Models;
using StolAPI.Replicates;
using Microsoft.EntityFrameworkCore;

namespace StolAPI.Managers
{
    public class MagasManager
    {

        protected ApplicationContext ApplicationContext { get; set; }
        protected DBContext DBContext { get; set; }
        public MagasManager (ApplicationContext applicationContext) { ApplicationContext = applicationContext;  DBContext = applicationContext.CreateDbContext(); }

        private List<Maga> _magas { get; set; } = new List<Maga> ();

        public Maga[] Magas { get => _magas.ToArray (); }
        public bool Read()
        {
            try
            {
                DBContext.Magas.Include(it => it.EFStols).ToList();
                foreach (EFMaga item in DBContext.Magas)
                {
                    if(item.IsDeleted != true) _magas.Add(new Maga(item));
                }
                return true;
            }
            catch { throw; }
        }

        public Maga Get(int id) => _magas.FirstOrDefault(it => it.Id == id);

        public Maga Create(MagaModel model)
        {
            try
            {
                EFMaga maga = new EFMaga()
                {
                    Name = model.name,
                    Description = model.description,
                    StartDateWork = model.startDateWork,
                };
                DBContext.Add(maga);
                DBContext.SaveChanges();

                Maga replicate = new Maga(maga);
                _magas.Add(replicate);

                return replicate;
            }
            catch { throw; }
        }

        public Maga Update(MagaModel model)
        {
            try
            {

                EFMaga _maga = DBContext.Magas.FirstOrDefault(it => it.Id == model.id);


                _maga.Name = model.name;
                _maga.Description = model.description;
                _maga.StartDateWork = model.startDateWork;

                DBContext.Update(_maga);
                DBContext.SaveChanges();

                _magas.Remove(_magas.FirstOrDefault(it => it.Id == model.id));
                Maga repl = new Maga(_maga);
                _magas.Add(repl);

                return repl;
            }
            catch { throw; }
        }

        public Stols[] GetStols(int magaId)
        {
            return Get(magaId).Stols.ToArray();
        }

        public Stols[] AttachStol(int magaId, int stolId)
        {
            var table = ApplicationContext.StolsManager.Get(stolId);

            var _maga = DBContext.Magas.FirstOrDefault(it => it.Id == magaId);
            _maga.EFStols.Add(table.Context);

            DBContext.Update(_maga);
            DBContext.SaveChanges();

            var maga = Get(magaId);
            maga.Stols.Add(table);

            return GetStols(magaId);
        }

        public Stols[] DettachStols(int magaId, int stolId)
        {
            var stol = ApplicationContext.StolsManager.Get(stolId);

            var _maga = DBContext.Magas.FirstOrDefault(it => it.Id == magaId);
            _maga.EFStols.Remove(stol.Context);

            DBContext.Update(_maga);
            DBContext.SaveChanges();


            var maga = Get(magaId);
            maga.Stols.Remove(stol);

            return GetStols(magaId);
        }

        public bool Delete(int id)
        {
            try
            {

                EFMaga _maga = DBContext.Magas.FirstOrDefault(it => it.Id == id);
                _maga.IsDeleted = true;
                DBContext.Update(_maga);
                DBContext.SaveChanges();
                _magas.Remove(_magas.FirstOrDefault(it => it.Id == id));
                return true;

            }
            catch { throw; }



        }

    }
}
