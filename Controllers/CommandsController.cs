using AutoMapper;
using Commander.Data;
using Commander.DTOs;
using Commander.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
        [HttpGet("{id}", Name = "GetCommandById")]
        public ActionResult<Command> GetCommandById(int id)
        {
            var commandItem = _repository.GetCommandById(id);

            // Changes the status code for a command not found from 204 to 404
            if (commandItem == null)
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
            return CreatedAtRoute(nameof(GetCommandById), new { Id = commandReadDto.Id }, commandReadDto);
        }

        // PUT api/commands/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id, CommandUpdateDto commandUpdateDto)
        {
            // Get the command to be updated using repository 
            var commandToUpdate = _repository.GetCommandById(id);

            // Check if the command is null and return NotFound (404) if it is null
            if (commandToUpdate == null)
            {
                return NotFound();
            }

            // Map (Update) the command to update from the DTO input
            _mapper.Map(commandUpdateDto, commandToUpdate);

            // Call the repository UpdateCommand() method
            _repository.UpdateCommand(commandToUpdate);

            // Save Changes
            _repository.SaveChanges();

            // Return NoContent (204)
            return NoContent();
        }

        // PATCH api/commands/{id}
        [HttpPatch("{id}")]
        public ActionResult PartialUpdateCommand(int id, JsonPatchDocument<CommandUpdateDto> patchDoc)
        {
            // Get the command by ID from the repository
            var commandToPatch = _repository.GetCommandById(id);

            // Check if it is null and, if it is null, return a 404
            if (commandToPatch == null)
            {
                return NotFound();
            }

            // Create a CommandUpdateDTO object using mapper
            var commandUpdateDto = _mapper.Map<CommandUpdateDto>(commandToPatch);

            // Apply patch to command update DTO
            patchDoc.ApplyTo(commandUpdateDto, ModelState);

            // Verify model state
            if (!TryValidateModel(commandToPatch))
            {
                return ValidationProblem(ModelState);
            }

            // Update with mapper
            _mapper.Map(commandUpdateDto, commandToPatch);

            // Call repository update method (does nothing)
            _repository.UpdateCommand(commandToPatch);

            // Save changes
            _repository.SaveChanges();

            // Return NoContent (204)
            return NoContent();
        }

        // DELETE api/controllers/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id)
        {
            // Get the command to delete
            var commandToDelete = _repository.GetCommandById(id);

            // Check if it is null
            if (commandToDelete == null)
            {
                return NotFound();
            }

            // Delete using the repository
            _repository.DeleteCommand(commandToDelete);

            // Save changes
            _repository.SaveChanges();

            // Return NoContent (204)
            return NoContent();
        }
    }
}
