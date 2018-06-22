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
        void UpdateChild(int index, IAsciiBasePropositionalOperator baseOperator);
        string ToLogicString();
        string ToName();
        bool IsAdvanced();
        
        IAsciiBasePropositionalOperator ToNandify();
        IAsciiBasePropositionalOperator Negate();
        IAsciiBasePropositionalOperator ToAndOrNot();

        void ChangeArgument(char from, char to);
    }
}