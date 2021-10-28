using Commander.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Commander.Data
{
    public class SqlCommanderRepo : ICommanderRepo
    {
        private readonly CommanderContext _context;

        public SqlCommanderRepo(CommanderContext context)
        {
            _context = context;
        }

        IEnumerable<Command> ICommanderRepo.GetAllCommands()
        {
            var commandItems = _context.Commands.ToList();
            return commandItems;
        }

        Command ICommanderRepo.GetCommandById(int id)
        {
            var commandItem = _context.Commands.FirstOrDefault(p => p.Id == id);
            return commandItem;
        }

        void ICommanderRepo.CreateCommand(Command cmd)
        {
            if(cmd == null)
            {
                throw new ArgumentNullException(nameof(cmd));
            }

            _context.Commands.Add(cmd);
        }
        void ICommanderRepo.UpdateCommand(Command cmd)
        {
            // This function does nothing because the _mapper.Map() function in the controller carries out the update in place
        }

        void ICommanderRepo.DeleteCommand(Command cmd)
        {
            if (cmd == null)
            {
                throw new ArgumentNullException(nameof(cmd));
            }

            _context.Commands.Remove(cmd);
        }

        bool ICommanderRepo.SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
