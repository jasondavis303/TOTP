using BasicOTP;

namespace TOTP
{
    public static class Extensions
    {
        public static void CopyTo(this OtpKey src, OtpKey dest)
        {
            dest.Account = src.Account;
            dest.Algorithm = src.Algorithm;
            dest.AuthType = src.AuthType;
            dest.Counter = src.Counter;
            dest.Digits = src.Digits;
            dest.Issuer = src.Issuer;
            dest.Period = src.Period;
            dest.Secret = src.Secret;
        }
    }
}
