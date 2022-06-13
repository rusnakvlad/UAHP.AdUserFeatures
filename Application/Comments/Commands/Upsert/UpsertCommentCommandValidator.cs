using Application.Comments.Commands.Delete;
using FluentValidation;

namespace Application.Comments.Commands.Upsert;
public class UpsertCommentCommandValidator : AbstractValidator<UpsertCommentCommand>
{
    public UpsertCommentCommandValidator()
    {
        RuleFor(x => x.Text).Length(1, 3000);
    }
}
