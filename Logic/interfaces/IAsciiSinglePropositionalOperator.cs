namespace Logic.interfaces
{
    public interface IAsciiSinglePropositionalOperator : IAsciiBasePropositionalOperator
    {
        char GetAsciiSymbol();
        char GetLogicSymbol();
    }
}