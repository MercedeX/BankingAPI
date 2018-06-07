using EndPoint.Models;
using NLog;
using System;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace EndPoint.Controllers
{
    [RoutePrefix("Accounts")]
    public class BankController : ApiController
    {
        readonly IAccountService _service;
        readonly ILogger _log;

        public BankController(ILogger log, IAccountService service)
        {
            _log = log;
            _service = service;
        }
        
        private IHttpActionResult Success()
        {
            return Content(HttpStatusCode.OK, "Success");
        }
        private IHttpActionResult Success<T>(T argument)
        {
            return Content<T>(HttpStatusCode.OK, argument);
        }
        private IHttpActionResult Error(Exception ex)
        {
            return Content(HttpStatusCode.InternalServerError, ex.Message);
        }
        private IHttpActionResult Error(string message)
        {
            return Content(HttpStatusCode.InternalServerError, message);
        }

        [HttpPost]
        [Route("Create")]
        public IHttpActionResult CreateAccount([FromBody]AccountCreateRequest model)
        {
            var ret = Success();

            try
            {
                var accountNo = _service.CreateAccount(model.firstName, model.lastName, model.contact, model.title, model.amount, model.notes);
                ret = (accountNo > 0) ? Success(accountNo) : Error("Account could not be created");
            }
            catch (Exception ex)
            {
                ret = Error(ex);
            }

            return ret;
        }

        [HttpPost]
        [Route("Withdraw")]
        public IHttpActionResult Withdraw([FromBody]AccountExternalTransactionRequest model)
        {
            var ret = Success();

            try
            {
                var result = _service.ExternalTransfer(model.accountNo, model.amount * -1, model.notes ?? "");
                ret = result ? Success("withdrawn successfully") : Error("Could not withdraw");
            }
            catch (Exception ex)
            {
                Error(ex);
            }

            return ret;
        }

        [HttpPost]
        [Route("Deposit")]
        public IHttpActionResult Deposit([FromBody]AccountExternalTransactionRequest model)
        {
            var ret = Success();

            try
            {
                var result = _service.ExternalTransfer(model.accountNo, model.amount, model.notes ?? "");
                ret = result ? Success("deposited successfully") : Error("Could not deposit");
            }
            catch (Exception ex)
            {
                Error(ex);
            }

            return ret;
        }

        [HttpPost]
        [Route("Transfer")]
        public IHttpActionResult Transfer([FromBody]AccountInternalTransactionRequest model)
        {
            var ret = Success();

            try
            {
                var result = _service.InternalTransfer(model.sourceAccountNo, model.targetAccountNo, model.amount, model.notes);
                ret = result ? Success("Amount transferred successfully") : Error("Could not trasnfer amount");
            }
            catch (Exception ex)
            {
                Error(ex);
            }

            return ret;
        }

        [HttpGet]
        [Route("Transactions")]
        public IHttpActionResult GetAccountHistory(int accountNo, DateTime startDate, DateTime endDate)
        {
            var ret = Success();

            try
            {
                var list = _service.GetTransactions(accountNo, startDate, endDate);
                ret = list.Any() ? Success(list) : Error("Could not retrieve history");

            }
            catch (Exception ex)
            {
                Error(ex);
            }

            return ret;
        }

        [HttpGet]
        [Route("Customer")]
        public IHttpActionResult GetAll(int id)
        {
            var ret = Success();

            try
            {
                ret = Success(_service.GetAccounts(id));
            }
            catch (Exception ex)
            {
                Error(ex);
            }

            return ret;
        }
    }
}