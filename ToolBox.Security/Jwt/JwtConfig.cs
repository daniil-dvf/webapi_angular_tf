namespace ToolBox.Security.Jwt
{
    public class JwtConfig
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Signature { get; set; }
    }
}