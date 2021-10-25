using Commander.Models;
using System.Collections.Generic;

namespace Commander.Data
{
    public class MockCommanderRepo : ICommanderRepo
    {
        public IEnumerable<Command> GetAppCommands()
        {
            var commands = new List<Command>
            {
                new Command{Id=0, HowTo="Find yourself", Line="Graduating", Platform="Ashesi University"},
                new Command{Id=1, HowTo="Register for classes", Line="Graduating", Platform="University of Benin" },
                new Command{Id=2, HowTo="Study hard", Line="Graduating", Platform="Harvard University"}
            };

            return commands;
        }

        public Command GetCommandById(int Id)
        {
            return new Command { Id = 0, HowTo = "Study hard", Line = "Graduating", Platform = "Ashesi University" };
        }

    }
}
