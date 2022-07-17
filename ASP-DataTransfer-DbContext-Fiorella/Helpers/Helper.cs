namespace ASP_DataTransfer_DbContext_Fiorella.Helpers
{
    public class Helper
    {
        public static void DeleteImage(string path)
        {
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }


        public enum UserRoles
        {
            Admin,
            Member
        }
    }
}
