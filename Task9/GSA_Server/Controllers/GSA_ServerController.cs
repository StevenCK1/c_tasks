using GSA_Server.Core.utils;
using Microsoft.AspNetCore.Mvc;

namespace GSA_Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GSA_ServerController : ControllerBase
    {
        
        private readonly DbHelpers _dbHelpers;
        private readonly CommandHelpers _commandHelpers;

        public GSA_ServerController(DbHelpers dbHelpers, CommandHelpers commandHelpers)
        {
            _dbHelpers = dbHelpers;
            _commandHelpers = commandHelpers;
        }

       
        [HttpPost]
        [Route("saveDb")]

        public IActionResult ImportDataFromDb(string csvFilePath)
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

        [HttpPost]
        [Route("capital")]

        public List<string> ImportDataFromCsv(string command)
        {
            var strategyReader = _commandHelpers.ProcessCommands(command);

            return strategyReader;
        }
    }
}