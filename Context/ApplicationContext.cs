using StolAPI.Managers;
using Microsoft.EntityFrameworkCore;

namespace StolAPI.Context
{
    public class ApplicationContext
    {

        public ApplicationContext(IConfiguration config)
        {
            Version = "0.1";
            Title = "MAMAAAAAA";
            Configuration = config;
            Initialize();
        }

        public void Initialize()
        {

            /*Инициализация менеджеров*/
            MagaManager = new MagasManager(this);
            StolsManager = new StolsManager(this);

            MagaManager.Read();
            StolsManager.Read();

        }

        public MagasManager MagaManager { get; set; }
        public StolsManager StolsManager { get; set; }
        public string Version { get; set; }
        public string Title { get; set; }
        public IConfiguration Configuration { get; set; }

        /*Здесь указать название подключения из appsettings*/
        public DBContext CreateDbContext() => new DBContext(Configuration.GetConnectionString("DefaultConnection"));

    }
}
