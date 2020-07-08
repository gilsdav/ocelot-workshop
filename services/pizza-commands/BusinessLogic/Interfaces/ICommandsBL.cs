using System.Collections.Generic;

namespace pizza_commands.BusinessLogic.Interfaces
{
    public interface ICommandsBL
    {
        Command AddCommand(Command command);
        List<Command> GetCommands();
        Command GetCommand(int commandId);

    }
}