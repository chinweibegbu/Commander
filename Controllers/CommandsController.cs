using Commander.Data;
using Commander.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Commander.Controllers
{
    /* You can also define a route with [Route("api/[controller]")].
     * This automaically replaces the "[controller]" with the name of your controller class.
     */

    [Route("api/commands")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        /* Alternative (1): Directly interface with an implementation of the repository interface 
        * >> private readonly MockCommanderRepo _repository = new MockCommanderRepo();
        */
        private readonly ICommanderRepo _repository;

        // Alternative (2): Use dependency injection
        public CommandsController(ICommanderRepo repository)
        {
            _repository = repository;
        }

        // GET api/commands
        [HttpGet]
        public ActionResult<IEnumerable<Command>> GetAllCommands()
        {
            var commandItems = _repository.GetAllCommands();

            return Ok(commandItems);
        }

        // GET api/commands/{id}
        [HttpGet("{id}")] 
        public ActionResult<Command> GetCommandById(int id)
        {
            var commandItem = _repository.GetCommandById(id);

            return Ok(commandItem);
        }
    }
}
