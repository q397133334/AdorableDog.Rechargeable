using System;

namespace AdorableDog.Rechargeable.Permissions
{
    public static class RechargeablePermissions
    {
        public const string GroupName = "Rechargeable";

        //Add your own permission names. Example:
        //public const string MyPermission1 = GroupName + ".MyPermission1";


        public static string[] GetAll()
        {
            //Return an array of all permissions
            return Array.Empty<string>();
        }
    }
}