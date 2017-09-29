namespace Logic.interfaces
{
    public interface ITruthTable
    {
        byte[][] GetTable();
        string ToHex();

        IParser GetParser();
    }
}