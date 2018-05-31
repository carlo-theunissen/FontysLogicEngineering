namespace Logic.interfaces
{


    public interface IAsciiBasePropositionalOperator
    {
        bool HasResult();
        bool Result();

        /**
         * Every Operator needs the ability to set the arguments.
         * This way a user can simply call this function and modify the result. 
         */
        char[] GetArguments();
        IAsciiBasePropositionalOperator[] GetChilds();
        ArgumentsManager GetArgumentsManager();
        int GetOperatorNeededArguments();
        void Instantiate(IAsciiBasePropositionalOperator[] arguments);
        string ToLogicString();
    }
}