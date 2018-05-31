namespace Logic.interfaces
{
    public interface IParser
    {
        IAsciiBasePropositionalOperator GetOperator();
        IArgumentController GetArgumentController();
    }
}