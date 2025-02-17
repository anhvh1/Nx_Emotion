namespace Com.Gosol.VHTT.Security
{
    using System;

    internal class AVHTTessControlExceptions : DatabaseProxyException
    {
        public AVHTTessControlExceptions(string errorMessage) : base(errorMessage)
        {
        }
    }
}
