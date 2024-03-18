namespace UserManagement.WebApi.Auth.ResponseModels
{
    public class InternalServerErrorResponse :GenericResponse
    {
        public override int StatusCode => StatusCodes.Status500InternalServerError;
        public override string? Status => "Internal Server Error";
    }
}
