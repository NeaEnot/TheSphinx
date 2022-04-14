using TheSphinx.TheSphinxConsole.Enums;
using TheSphinx.TheSphinxConsole.Logics;

namespace TheSphinx.TheSphinxConsole
{
    internal class Interpreter
    {
        private InterpreterState state;

        private UserLogic userLogic;

        internal Interpreter()
        {
            state = InterpreterState.Unknown;

            userLogic = new UserLogic();
        }

        internal ResultStatus Execute(string cmd)
        {
            switch (state)
            {
                case InterpreterState.Unknown:
                    return ExecuteUnknown(cmd);
                case InterpreterState.Authorized:
                    return ExecuteAuthorized(cmd);
                default:
                    return ResultStatus.Error;
            }
        }

        private ResultStatus ExecuteUnknown(string cmd)
        {
            switch (cmd)
            {
                case "enter":
                    if (userLogic.Enter())
                    {
                        state = InterpreterState.Authorized;
                        return ResultStatus.Ok;
                    }
                    else
                    {
                        return ResultStatus.Error;
                    }
                case "exit":
                    return ResultStatus.Exit;
                default:
                    return ResultStatus.Error;
            }
        }

        private ResultStatus ExecuteAuthorized(string cmd)
        {
            switch (cmd)
            {
                case "change spass":
                    userLogic.ChangeStoragePassword();
                    return ResultStatus.Ok;
                case "change fpass":
                    userLogic.ChangeFieldsPassword();
                    return ResultStatus.Ok;
                case "exit":
                    return ResultStatus.Exit;
                default:
                    return ResultStatus.Error;
            }
        }
    }
}
