using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tidred.Repo;
using Tidred.WebApp.Controllers.ApiData.Dto;

namespace Tidred.WebApp.Controllers.ApiData
{
    [Authorize]
    [RoutePrefix("api/Customers")]
    public class CustomersController : ApiController
    {
        public IEnumerable<CustomerData> GetCustomers(int? coId)
        {
            if (!coId.HasValue)
            {
                Request.CreateErrorResponse(HttpStatusCode.BadRequest, "CoId is required.");
                return null;
            }

            var repo = RepoFactory.Instance.CreateCustomerRepo();
            var result = repo.GetAllCustomers(coId.Value).Select(c => 
                new CustomerData { CoId = c.CoID, CustomerId = c.CustomerId, CurrencyId = c.CurrencyId, 
                    CurrencyName = c.Currency.Name, Name = c.Name});

            return result;
        }

        public void PostCustomer(Customer customer)
        {
            var repo = RepoFactory.Instance.CreateCustomerRepo();

            if (customer.CurrencyId <= 0)
            {
                ModelState.AddModelError("CurrencyId", "Currency is required.");
            }

            if (string.IsNullOrEmpty(customer.Name))
            {
                ModelState.AddModelError("Name", "Name is required.");
            }

            if (!ModelState.IsValid)
            {
                return;
            }

            if (customer.CustomerId > 0)
            {
                repo.Update(customer);
                return;
            }

            repo.Create(customer);
        }

        [Route("Currencies")]
        public IEnumerable<CurrencyData> GetCurrencies()
        {
            var repo = RepoFactory.Instance.CreateCustomerRepo();
            var result = repo.GetAllCurrencies().Select(c =>
                            new CurrencyData
                            {
                                CurrencyId  = c.CurrencyId,
                                Name = c.Name
                            });

            return result;
        }
    }
}
