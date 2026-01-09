namespace Elevate.Services
{
    internal static class UniqueIdGenerator
    {
        static int id = 0;

        static public int GenerateNewId()
        {
            return id++;
        }
    }
}
