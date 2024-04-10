using StolAPI.Context;
using StolAPI.Controllers.DTO;
using StolAPI.Replicates;
using Microsoft.AspNetCore.Mvc;

namespace StolAPI.Controllers
{
    public class MagasController:BaseController
    {
        public MagasController(ApplicationContext _appContext):base(_appContext) { }

        [HttpGet("[controller]/[action]")]
        public JsonResult Init()
        {
            var res = GetCommon();
            return Send(true, res);
        }

        [HttpGet("[controller]/[action]")]
        public JsonResult GetAll()
        {
            var res = GetCommon();
            res.maga = ApplicationContext.MagaManager.Magas.Select(it => new MagaModel(it));
            return Send(true, res);
        }


        [HttpGet("[controller]/[action]")]
        public JsonResult Get(int id)
        {
            var res = GetCommon();
            res.magas = new MagaModel(ApplicationContext.MagaManager.Magas.FirstOrDefault(it => it.Id == id));
            return Send(true, res);
        }

        [HttpPost("[controller]/[action]")]
        public JsonResult Create([FromBody] MagaModel model)
        {
            Maga maga = ApplicationContext.MagaManager.Create(model);

            var res = GetCommon();
            res.magas = new MagaModel(maga);
            return Send(true, res);
        }

        [HttpPut("[controller]/[action]")]
        public JsonResult Update([FromBody] MagaModel model)
        {

            Maga maga = ApplicationContext.MagaManager.Update(model);

            var res = GetCommon();
            res.maga = new MagaModel(maga);
            return Send(true, res);
        }

        [HttpGet("[controller]/[action]")]
        public JsonResult GetStols(int magaId)
        {

            Stols[] stols = ApplicationContext.MagaManager.GetStols(magaId);

            var res = GetCommon();
            res.stols = stols.Select(it => new StolsModel(it));
            return Send(true, res);
        }
        [HttpPost("[controller]/[action]")]
        public JsonResult AttachStol(int magaId, int stolId)
        {

            Stols[] stols = ApplicationContext.MagaManager.AttachStol(magaId, stolId);

            var res = GetCommon();
            res.stols = stols.Select(it => new StolsModel(it));
            return Send(true, res);
        }
        [HttpPost("[controller]/[action]")]
        public JsonResult DettachStols(int magaId, int stolId)
        {

            Stols[] stol = ApplicationContext.MagaManager.DettachStols(magaId, stolId);

            var res = GetCommon();
            res.stols = stol.Select(it => new StolsModel(it));
            return Send(true, res);
        }

        [HttpDelete("[controller]/[action]")]
        public JsonResult Delete(int id)
        {
            ApplicationContext.MagaManager.Delete(id);
            var res = GetCommon();
            res.magas = ApplicationContext.MagaManager.Magas.Select(it => new MagaModel(it));
            return Send(true, res);
        }
    }
}
