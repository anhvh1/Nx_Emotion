namespace Com.Gosol.VHTT.Security
{
    using System;
    using System.Collections;
    using System.Globalization;
    using System.Security.Claims;
    using System.Security.Principal;

    public class AVHTTessControlIdentity : IIdentity
    {
        private Hashtable _userInfo;

        private AVHTTessControlIdentity()
        {
            throw new ApplicationException("CanNotCreateClass");
        }

        private AVHTTessControlIdentity(Hashtable userInfo)
        {
            this._userInfo = userInfo;
        }

        public static AVHTTessControlIdentity CreateInstance(Hashtable userInfo)
        {
            return new AVHTTessControlIdentity(userInfo);
        }

        public object GetProperty(string key)
        {
            return this._userInfo[key];
        }

        public string[] GetPropertyNames()
        {
            string[] strArray = new string[this._userInfo.Count];
            int num = 0;
            foreach (object obj2 in this._userInfo.Keys)
            {
                strArray[num++] = (string) obj2;
            }
            return strArray;
        }

        public void SetProperty(string key, object propertyValue)
        {
            this._userInfo[key] = propertyValue;
        }

        public string AuthenticationType
        {
            get
            {
                return "Database";
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                return (this._userInfo.Count > 0);
            }
        }

        public string Name
        {
            get
            {
                return Convert.ToString(this._userInfo["UserName"], CultureInfo.InvariantCulture).Trim();
            }
        }

        // anhnt
        //public static AVHTTessControlIdentity CreateUserGroup(Hashtable userGroup)
        //{
        //    return new AVHTTessControlIdentity(userGroup);
        //}
    }
}
