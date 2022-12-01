using FluentValidation;

namespace FluentAPIDemo;

public class CalcRequestValidator : AbstractValidator<CalcRequest>
{
    public CalcRequestValidator()
    {
        RuleFor(x => x.A).NotEmpty();
        RuleFor(x => x.B).NotEmpty();
    }
}

public class AddRequestValidator : AbstractValidator<AddRequest>
{
    public AddRequestValidator()
    {
        Include(new CalcRequestValidator());
        //my dummy rule, just fo demo purposes
        RuleFor(x => x.B).GreaterThan(10);
    }
}


public class SubRequestValidator : AbstractValidator<SubRequest>
{
    public SubRequestValidator()
    {
        Include(new CalcRequestValidator());
        //my dummy rule, just fo demo purposes
        RuleFor(x => x.B).GreaterThanOrEqualTo(-10);
    }
}

public class DivRequestValidator : AbstractValidator<DivRequest>
{
    public DivRequestValidator()
    {
        Include(new CalcRequestValidator());
        //my dummy rule, just fo demo purposes
        RuleFor(x => x.B).NotEqual(0);
    }
}