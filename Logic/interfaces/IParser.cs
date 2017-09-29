namespace Logic.interfaces
{
    public interface IParser
    {
        IAsciiBaseOperator GetOperator();
        IArgumentController GetArgumentController();
    }
}