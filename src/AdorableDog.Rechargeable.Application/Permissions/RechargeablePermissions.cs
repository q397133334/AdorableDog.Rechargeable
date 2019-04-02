using System;
using System.Collections;
using System.Collections.Generic;

namespace AdorableDog.Rechargeable.Permissions
{
    public static class RechargeablePermissions
    {
        public const string GroupName = "Rechargeable";

        //Add your own permission names. Example:
        //public const string MyPermission1 = GroupName + ".MyPermission1";
        public const string ShowAllMachine = GroupName + ".ShowMallAachine";


        public static string[] GetAll()
        {
            //Return an array of all permissions
            return new List<string>() {
                ShowAllMachine
            }.ToArray();
        }
    }
}