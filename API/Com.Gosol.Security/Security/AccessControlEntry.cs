namespace Com.Gosol.VHTT.Security
{
    using System;

    [Serializable]
    internal class AVHTTessControlEntry
    {
        private int _aVHTTessRight;
        private ACLType _aclType;
        private int _entityid;
        private Com.Gosol.VHTT.Security.EntityType _entityType;

        public AVHTTessControlEntry(Com.Gosol.VHTT.Security.EntityType entityType, int entityid, int aVHTTessRight) : this(entityType, entityid, aVHTTessRight, ACLType.ObjectInstance)
        {
        }

        public AVHTTessControlEntry(Com.Gosol.VHTT.Security.EntityType entityType, int entityid, int aVHTTessRight, ACLType aclType)
        {
            this._entityType = entityType;
            if (aclType == ACLType.ObjectClass)
            {
                this._entityid = -1;
            }
            else if (aclType == ACLType.ObjectInstance)
            {
                this._entityid = entityid;
            }
            this._aclType = aclType;
            this._aVHTTessRight = aVHTTessRight;
        }

        public int AVHTTessRight
        {
            get
            {
                return this._aVHTTessRight;
            }
        }

        public ACLType ACEType
        {
            get
            {
                return this._aclType;
            }
        }

        public int Entityid
        {
            get
            {
                return this._entityid;
            }
        }

        public Com.Gosol.VHTT.Security.EntityType EntityType
        {
            get
            {
                return this._entityType;
            }
        }
    }
}
