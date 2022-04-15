using System;
using TheSphinx.TheSphinxConsole.Enums;
using TheSphinx.TheSphinxConsole.Logics;

namespace TheSphinx.TheSphinxConsole
{
    internal class Interpreter
    {
        private InterpreterState state;

        private UserLogic userLogic;
        private AccauntLogic accountLogic;

        internal Interpreter()
        {
            state = InterpreterState.Unknown;

            userLogic = new UserLogic();
            accountLogic = new AccauntLogic();
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
            switch (cmd.Split(' ')[0])
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
            switch (cmd.Split(' ')[0])
            {
                case "chngspass":
                    userLogic.ChangeStoragePassword(cmd.Remove(0, (cmd + " ").Length));
                    return ResultStatus.Ok;
                case "chngfpass":
                    userLogic.ChangeFieldsPassword(cmd.Remove(0, (cmd + " ").Length));
                    return ResultStatus.Ok;
                case "setuserfield":
                    userLogic.ChangeUserField(cmd.Remove(0, (cmd + " ").Length));
                    return ResultStatus.Ok;
                case "deluserfield":
                    userLogic.ChangeUserField(cmd.Remove(0, (cmd + " ").Length));
                    return ResultStatus.Ok;
                case "addacc":
                    string answer = accountLogic.CreateAccaunt(cmd.Remove(0, (cmd + " ").Length));
                    if (answer != "")
                    {
                        Console.WriteLine(answer);
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
    }
}
