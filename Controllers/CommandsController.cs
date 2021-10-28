using AutoMapper;
using Commander.Data;
using Commander.DTOs;
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
        private readonly IMapper _mapper;

        // Alternative (2): Use dependency injection
        public CommandsController(ICommanderRepo repository, IMapper mapper)
        {
            _repository = repository;   // Dependency-injected repository
            _mapper = mapper;           // Dependency-injected mapper
        }

        // GET api/commands
        [HttpGet]
        public ActionResult<IEnumerable<Command>> GetAllCommands()
        {
            var commandItems = _repository.GetAllCommands();

            // Maps the Command model to the CommandReadDto class
            // Original form: Directly returned the commandItems, and not DTOs.
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItems));
        }

        // GET api/commands/{id}
        [HttpGet("{id}")] 
        public ActionResult<Command> GetCommandById(int id)
        {
            var commandItem = _repository.GetCommandById(id);

            // Changes the status code for a command not found from 204 to 404
            if(commandItem == null)
            {
                return NotFound();
            }

            // Returns HTTP 200 status code
            return Ok(_mapper.Map<CommandReadDto>(commandItem));
        }

        // POST api/commands
        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommand(CommandWriteDto commandWriteDto)
        {
            // Map the Create DTO to the Command model
            var commandModel = _mapper.Map<Command>(commandWriteDto);

            // Add the model instance to the DB through the repository
            _repository.CreateCommand(commandModel);

            // Save changes to the DB
            _repository.SaveChanges();

            // Convert the model to a Read DTO to show the user the created model
            var commandReadDto = _mapper.Map<CommandReadDto>(commandModel);

            // Return route at which the object was created in the header
            // Alternative: Return Ok()
            // return CreatedAtRoute(nameof(GetCommandById), new {Id = commandReadDto.Id}, commandReadDto);
            return Ok(commandReadDto);
        }
    }
}
