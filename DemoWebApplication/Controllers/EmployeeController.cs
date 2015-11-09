using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using DemoWebApplication.Domain.Model;
using DemoWebApplication.Domain.Service;

namespace DemoWebApplication.Controllers
{
   
    [RoutePrefix("api/employee")]
    public class EmployeeController : ApiController
    {
        private EmployeeDeductionCalculator calculator;

        public EmployeeController()
        {
            var defaultSettings = new DeductionCalculatorSettings();
            calculator = new EmployeeDeductionCalculator(defaultSettings);
        }

        [Route("calculatePaycheck")]
        [HttpPost]
        public async Task<IHttpActionResult> Calculate(Employee empl)
        {
            if (empl == null)
            {                
                return BadRequest("Invalid data");
            }

            return Ok(calculator.CalculatePaycheckDeduction(empl));
        }
    }
}