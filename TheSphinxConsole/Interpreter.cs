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
            switch (cmd)
            {
                case "enter":
                    if (state == InterpreterState.Unknown)
                    {
                        if (userLogic.Enter())
                        {
                            state = InterpreterState.Authorized;
                            return ResultStatus.Ok;
                        }
                        else
                        {
                            return ResultStatus.Error;
                        }
                    }
                    else
                    {
                        return ResultStatus.Error;
                    }
                case "exit":
                    return ResultStatus.Exit;
            }

            return ResultStatus.Error;
        }
    }
}
