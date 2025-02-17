using System;
using System.Web;
namespace Com.Gosol.VHTT.Security
{
   
    public enum AccessLevel
    {
        Create = 2,
        Delete = 8,
        Edit = 4,
        FullAVHTTess = 0x1f,
        NoAVHTTess = 0,
        Publish = 0x10,
        Read = 1
    }
   
}
