using System;
using System.Collections.Generic;
using System.Text;
using System.Management;
using System.Security.Cryptography;

namespace OpenUDIDCSharp
{
    public static class OpenUDID
    {
        public enum OpenUDIDErrors
        {
            None = 0,
            OptedOut = 1,
            Compromised = 2
        }
        private static String _cachedValue;
        private static OpenUDIDErrors _lastError;
        private static String _getOpenUDID()
        {
            _lastError = OpenUDIDErrors.None;
            if (_cachedValue == null)
            {
                MD5CryptoServiceProvider _md5 = new MD5CryptoServiceProvider();
                ManagementObjectSearcher _searcher = new ManagementObjectSearcher("SELECT ProcessorId FROM Win32_Processor");
                int i = 0;
                foreach (ManagementObject mo in _searcher.Get())
                {
                    Console.WriteLine("CPU:{0} Info:\t{1}" ,i++, mo["ProcessorId"].ToString());
                    byte[] bs = System.Text.Encoding.UTF8.GetBytes(mo["ProcessorId"].ToString());
                    bs = _md5.ComputeHash(bs);
                    System.Text.StringBuilder s = new System.Text.StringBuilder();
                    foreach (byte b in bs)
                    {
                        s.Append(b.ToString("x2").ToLower());
                    }
                    _cachedValue = s.ToString();
                }

            }
            return _cachedValue;
        }

        public static String value
        {
            get
            {
                return _getOpenUDID();
            }
        }
        public static String valueWithError(out OpenUDIDErrors error)
        {
            String v = value;
            error = _lastError;
            return v;
        }
    }
}
