namespace AuthTestApi
{
    internal class SecurityTokenDescriptor
    {
        public object Subject { get; set; }
        public object Expires { get; set; }
        public object SigningCredentials { get; set; }
    }
}