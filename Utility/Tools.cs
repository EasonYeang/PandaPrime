namespace Utility
{
    public class Tools
    {
        public static object Alert(int flag, string msg)
        {
            object result = new { Flag = flag, Message = msg };
            return result;
        }
    }
}
