using StolAPI.Context;
using StolAPI.Controllers.DTO;
using StolAPI.Replicates;
using Microsoft.AspNetCore.Mvc;

namespace StolAPI.Controllers
{
    public class StolsController:BaseController
    {
        public StolsController(ApplicationContext _appContext):base(_appContext) { }


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
            res.stol = ApplicationContext.StolsManager.Stols.Select(it => new StolsModel(it));
            return Send(true, res);
        }


        [HttpGet("[controller]/[action]")]
        public JsonResult Get(int id)
        {
            var res = GetCommon();
            res.tables = new StolsModel (ApplicationContext.StolsManager.Stols.FirstOrDefault(it => it.Id == id));
            return Send(true, res);
        }

        [HttpPost("[controller]/[action]")]
        public JsonResult Create([FromBody] StolsModel model)
        {
            Stols maga = ApplicationContext.StolsManager.Create(model);

            var res = GetCommon();
            res.tables = new StolsModel(maga);
            return Send(true, res);
        }

        [HttpPut("[controller]/[action]")]
        public JsonResult Update([FromBody] StolsModel model)
        {

            Stols maga = ApplicationContext.StolsManager.Update(model);

            var res = GetCommon();
            res.tables = new StolsModel(maga);
            return Send(true, res);
        }

        [HttpDelete("[controller]/[action]")]
        public JsonResult Delete(int id)
        {
            ApplicationContext.StolsManager.Delete(id);
            var res = GetCommon();
            res.tables = ApplicationContext.StolsManager.Stols.Select(it => new StolsModel(it));
            return Send(true, res);
        }
    }
}
