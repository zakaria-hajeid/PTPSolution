using System;
using System.Collections.Generic;
using System.Text;
using PTP.Core.Common.DI;
namespace PTP.Core.Common.Utilities
{
    public static class IdentityManager
    {
        private static readonly ICleintContext CleintProvider;

        static IdentityManager()
        {
            CleintProvider = IoC.Instance.Reslove<ICleintContext>();
        }

        public static string Token
        {
            get => CleintProvider.Token();
        }


        public static int? UserId
        {
            get => CleintProvider.GetUserId();
        }




        //public static List<string> Permissions
        //{
        //    get => UserProvider.GetPermissions();
        //}

    }

}
