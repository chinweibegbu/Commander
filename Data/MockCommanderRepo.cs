using Commander.Models;
using System.Collections.Generic;

namespace Commander.Data
{
    public class MockCommanderRepo : ICommanderRepo
    {
        public IEnumerable<Command> GetAllCommands()
        {
            var commands = new List<Command>
            {
                new Command{Id=0, HowTo="Find yours elf", Line="Graduating", Platform="Ashesi University"},
                new Command{Id=1, HowTo="Register for classes", Line="Graduating", Platform="University of Benin" },
                new Command{Id=2, HowTo="Study hard", Line="Graduating", Platform="Harvard University"}
            };

            return commands;
        }

        public Command GetCommandById(int Id)
        {
            return new Command { Id = 0, HowTo = "Study hard", Line = "Graduating", Platform = "Ashesi University" };
        }

        void ICommanderRepo.CreateCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }

        void ICommanderRepo.UpdateCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }

        void ICommanderRepo.DeleteCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }

        bool ICommanderRepo.SaveChanges()
        {
            throw new System.NotImplementedException();
        }
    }
}
