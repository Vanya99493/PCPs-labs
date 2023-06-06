namespace Laba_1
{
    public static class IdManager
    {
        private static int _id = 1;

        public static int GetId() => _id++;
    }
}