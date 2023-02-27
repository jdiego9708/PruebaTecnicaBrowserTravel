using Microsoft.Extensions.Configuration;
using SISBrowserTravelBooks.DataAccess.Interfaces;
using SISBrowserTravelBooks.Entities.ModelsConfiguration;

namespace SISBrowserTravelBooks.DataAccess.Dacs
{
    public class ConnectionDac : IConnectionDac
    {
        private readonly IConfiguration Configuration;
        private readonly ConnectionStrings ConnectionStringsModel;
        public ConnectionDac(IConfiguration IConfiguration)
        {
            this.Configuration = IConfiguration;

            var settings = this.Configuration.GetSection("ConnectionStrings");
            this.ConnectionStringsModel = settings.Get<ConnectionStrings>();

            if (this.ConnectionStringsModel == null)
                this.ConnectionStringsModel = new();
        }
        public string Cn()
        {
            if (this.ConnectionStringsModel.ConexionBDPredeterminada.Equals("ConexionBDAzure"))
                return this.ConnectionStringsModel.ConexionBDAzure;
            else
                return this.ConnectionStringsModel.ConexionBDLocal;
        }
    }
}
