using FluentValidation;
using Rusty.Template.Contracts.Dtos;

namespace Rusty.Template.Contracts.SubTypes;

/// <summary>
///     The filter data
/// </summary>
public sealed record FilterData(DateTime DateFrom, DateTime DateTo);

/// <summary>
///     The filter data validator class
/// </summary>
/// <seealso cref="BaseValidator{T}" />
public sealed class FilterDataValidator : BaseValidator<FilterData>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="FilterDataValidator" /> class
    /// </summary>
    public FilterDataValidator()
    {
        RuleFor(d => d.DateFrom)
            .LessThanOrEqualTo(DateTime.Now)
            .LessThanOrEqualTo(d => d.DateTo.Date)
            .GreaterThanOrEqualTo(DateTime.MinValue);
        RuleFor(d => d.DateTo.Date)
            .GreaterThanOrEqualTo(DateTime.MinValue);
    }
}