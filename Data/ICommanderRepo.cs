using System.Collections.Generic;   // contains IEnumerable
using Commander.Models;             // allows repo to use the Command model

namespace Commander.Data
{
    public interface ICommanderRepo
    {
        IEnumerable<Command> GetAppCommands();
        Command GetCommandById(int Id);
    }
}
