using System.Collections.Generic;   // contains IEnumerable
using Commander.Models;             // allows repo to use the Command model

namespace Commander.Data
{
    public interface ICommanderRepo
    {
        IEnumerable<Command> GetAllCommands();
        Command GetCommandById(int Id);
        void CreateCommand(Command cmd);
        void UpdateCommand(Command cmd);
        bool SaveChanges();
    }
}
