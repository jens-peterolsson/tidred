using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tidred.Services
{
    public interface IServiceFactory
    {
        IReportService CreateReportService();
    }
}
