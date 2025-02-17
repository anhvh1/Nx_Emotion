namespace Com.Gosol.VHTT.Security
{
    using System;
    using System.Collections;
    using System.Security.Claims;
    using System.Security.Principal;

    public class AVHTTessControlPrincipal : IPrincipal
    {
        private Hashtable _aVHTTessGroups;
        private Hashtable _acl;
        private Hashtable _aclType;
        private AVHTTessControlIdentity _userIdentity;

        private AVHTTessControlPrincipal()
        {
            throw new AVHTTessControlExceptions("CanNotCreateClass");
        }

        private AVHTTessControlPrincipal(Hashtable aVHTTessGroups, Hashtable acl, Hashtable aclType, Hashtable userInfo)
        {
            this._aVHTTessGroups = aVHTTessGroups;
            this._acl = acl;
            this._aclType = aclType;
            this._userIdentity = AVHTTessControlIdentity.CreateInstance(userInfo);
            
        }

        public static AVHTTessControlPrincipal CreateInstance(Hashtable aVHTTessGroups, Hashtable acl, Hashtable aclType, Hashtable userInfo)
        {
            return new AVHTTessControlPrincipal(aVHTTessGroups, acl, aclType, userInfo);
        }

        public bool HasPermission(EntityType entityType, AccessLevel aVHTTessLevel)
        {
            string key = Convert.ToInt32(entityType).ToString();
            if (this._aclType.ContainsKey(key))
            {
                int num = Convert.ToInt32(aVHTTessLevel);
                return (num == (num & ((int) this._aclType[key])));
            }
            return (aVHTTessLevel == AccessLevel.NoAVHTTess);
        }

        public bool HasPermission(object entityType, AccessLevel aVHTTessLevel)
        {
            if (entityType.ToString().Equals("?"))
            {
                return false;
            }
            if (entityType.ToString().Equals("*"))
            {
                return true;
            }
            string key = Convert.ToInt32(entityType).ToString();
            if (this._aclType.ContainsKey(key))
            {
                int num = Convert.ToInt32(aVHTTessLevel);
                return (num == (num & ((int) this._aclType[key])));
            }
            return (aVHTTessLevel == AccessLevel.NoAVHTTess);
        }

        public bool HasPermission(object entityType, string aVHTTessLevel)
        {
            if (entityType.ToString().Equals("?") || aVHTTessLevel.Equals("?"))
            {
                return false;
            }
            if (entityType.ToString().Equals("*") || aVHTTessLevel.Equals("*"))
            {
                return true;
            }
            bool flag = false;
            if (Enum.IsDefined(typeof(AccessLevel), aVHTTessLevel))
            {
                AccessLevel level = (AccessLevel) Enum.Parse(typeof(AccessLevel), aVHTTessLevel);
                //flag = AVHTTessControl.User.HasPermission(entityType, level);
            }
            return true;
        }

        public bool HasPermission(object entityType, int entityID, AccessLevel aVHTTessLevel)
        {
            if (entityType.ToString().Equals("?"))
            {
                return false;
            }
            if (entityType.ToString().Equals("*"))
            {
                return true;
            }
            string key = Convert.ToInt32(entityType).ToString() + "$" + entityID.ToString();
            if (this._acl.ContainsKey(key))
            {
                int num = Convert.ToInt32(aVHTTessLevel);
                return (num == (num & ((int) this._acl[key])));
            }
            return (aVHTTessLevel == AccessLevel.NoAVHTTess);
        }

        public bool IsInRole(int RoleID)
        {
            return this._aVHTTessGroups.ContainsKey(RoleID);
        }
        public string NameGroup()
        {
            return this._aVHTTessGroups["TenNhom"].ToString();
        }
        
        public bool IsInRole(string roleName)
        {
            return this._aVHTTessGroups.ContainsValue(roleName);
        }

        public IIdentity Identity
        {
            get
            {
                return this._userIdentity;
            }
        }
    }
}
