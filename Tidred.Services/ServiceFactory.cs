using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tidred.Services
{
    public class ServiceFactory : IServiceFactory
    {
        private ServiceFactory()
        {
        }

        private static IServiceFactory _instance;
        public static IServiceFactory Instance
        {
            get { return _instance ?? (_instance = new ServiceFactory()); }
            set { _instance = value; }
        }

        public IReportService CreateReportService()
        {
            return new ReportService();
        }
    }
}
