using System;
using System.Collections.Generic;
using System.Text;

namespace PTP.Core.Common
{
    public interface ICleintContext
    {
        string Token();
        int? GetUserId();
        string GetUsername();

        string GetLanguage();

        string GetAuthHash();
        string GetPasswordHash();
        string GetFirstNameEn();
        string GetFirstNameAr();
        string GetLastNameAr();
        string GetLastNameEn();
    }
}
