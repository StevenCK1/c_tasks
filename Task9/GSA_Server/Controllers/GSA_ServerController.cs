using GSA_Server.Core.utils;
using Microsoft.AspNetCore.Mvc;

namespace GSA_Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GSA_ServerController : ControllerBase
    {
        
        private readonly DbHelpers _dbHelpers;

        public GSA_ServerController(DbHelpers dbHelpers)
        {
            _dbHelpers = dbHelpers;
        }

       
        [HttpPost]
        [Route("saveDb")]

        public IActionResult ImportDataFromCsv(string csvFilePath)
        {
            var strategyReader = new StrategyReader(new MyFileReader());
            var result = strategyReader.Execute();
            foreach (var item in result)
            {
                Console.WriteLine(item);
            }
             _dbHelpers.SaveToDatabase(result);

            return Ok();
        }
    }
}