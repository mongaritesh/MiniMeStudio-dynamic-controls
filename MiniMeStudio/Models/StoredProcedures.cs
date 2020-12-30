using System;
using System.Collections.Generic;
using System.Text;

namespace MiniMeStudio.Models
{
    public static class StoredProcedures
    {
        public const string USER_Exists = "USER_Exists";
        public const string USER_UpdateEncryptedEmailAddress = "User_UpdateEncryptedEmailAddress";
        public const string USER_SelectOne = "User_SelectOne";
        public const string USER_UpdatePasswordAndFirstTimeLogin = "User_UpdatePasswordAndFirstTimeLogin";
        public const string USER_EncryptedEmailAddress = "User_EncryptedEmailAddress";

    }
}
