using FluentValidation;

namespace UserManagement.Application.SortingPaging
{
    public class SortingPagingValidator : AbstractValidator<SortingPagingDto>
    {
        private readonly string[] allowedSortOptions = { "name", "description", "id" };

        public SortingPagingValidator()
        {
            When(c => !string.IsNullOrEmpty(c.SortBy), () =>
            RuleFor(x => x.SortBy)
                .Must(BeAValidSortOption)
                .WithMessage($"Sort option must be one of: {string.Join(", ", allowedSortOptions)}"));
        }

        private bool BeAValidSortOption(string? sortOption)
        {
            return allowedSortOptions.Contains(sortOption?.ToLower());
        }
    }

}
