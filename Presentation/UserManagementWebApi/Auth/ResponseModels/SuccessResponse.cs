namespace UserManagement.WebApi.Auth.ResponseModels
{
    public class SuccessResponse : GenericResponse
    {
        public override int StatusCode => StatusCodes.Status200OK;
        public override string? Status => "Success";
    }
}
