using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Stationery.Common.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public class JwtIssuerOptions
    {
        /// <summary>
        /// Gets the security key.
        /// </summary>
        /// <value>
        /// The security key.
        /// </value>
        public SymmetricSecurityKey SecurityKey => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.SigningKey));

        /// <summary>
        /// Gets or sets the signing key.
        /// </summary>
        /// <value>
        /// The signing key.
        /// </value>
        public string SigningKey { get; set; }

        /// <summary>
        /// 4.1.1.  "iss" (Issuer) Claim - The "iss" (issuer) claim identifies the principal that issued the JWT.
        /// </summary>
        /// <value>
        /// The issuer.
        /// </value>
        public string Issuer { get; set; }

        /// <summary>
        /// 4.1.2.  "sub" (Subject) Claim - The "sub" (subject) claim identifies the principal that is the subject of the JWT.
        /// </summary>
        /// <value>
        /// The subject.
        /// </value>
        public string Subject { get; set; }

        /// <summary>
        /// 4.1.3.  "aud" (Audience) Claim - The "aud" (audience) claim identifies the recipients that the JWT is intended for.
        /// </summary>
        /// <value>
        /// The audience.
        /// </value>
        public string Audience { get; set; }

        /// <summary>
        /// 4.1.4.  "exp" (Expiration Time) Claim - The "exp" (expiration time) claim identifies the expiration time on or after which the JWT MUST NOT be accepted for processing.
        /// </summary>
        /// <value>
        /// The expiration.
        /// </value>
        public DateTime Expiration => IssuedAt.Add(ValidFor);

        /// <summary>
        /// 4.1.5.  "nbf" (Not Before) Claim - The "nbf" (not before) claim identifies the time before which the JWT MUST NOT be accepted for processing.
        /// </summary>
        /// <value>
        /// The not before.
        /// </value>
        public DateTime NotBefore => DateTime.UtcNow;

        /// <summary>
        /// 4.1.6.  "iat" (Issued At) Claim - The "iat" (issued at) claim identifies the time at which the JWT was issued.
        /// </summary>
        /// <value>
        /// The issued at.
        /// </value>
        public DateTime IssuedAt => DateTime.UtcNow;

        /// <summary>
        /// Set the timespan the token will be valid for (default is 120 min)
        /// </summary>
        /// <value>
        /// The valid for.
        /// </value>
        public TimeSpan ValidFor { get; set; } = TimeSpan.FromMinutes(120);


        /// <summary>
        /// "jti" (JWT ID) Claim (default ID is a GUID)
        /// </summary>
        /// <value>
        /// The jti generator.
        /// </value>
        public Func<Task<string>> JtiGenerator =>
          () => Task.FromResult(Guid.NewGuid().ToString());

        /// <summary>
        /// The signing key to use when generating tokens.
        /// </summary>
        /// <value>
        /// The signing credentials.
        /// </value>
        public SigningCredentials SigningCredentials => new SigningCredentials(this.SecurityKey, SecurityAlgorithms.HmacSha256);
    }
}
