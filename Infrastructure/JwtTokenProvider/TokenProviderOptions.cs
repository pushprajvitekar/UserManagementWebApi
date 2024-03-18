namespace JwtTokenProvider
{
    public record TokenProviderOptions(string ValidIssuer, string ValidAudience, byte[] SymmetricSecurityKey);
}
