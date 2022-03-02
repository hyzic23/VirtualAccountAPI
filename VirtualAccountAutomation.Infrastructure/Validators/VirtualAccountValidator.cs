using FluentValidation;
using VirtualAccountAutomation.Infrastructure.Dtos;

namespace VirtualAccountAutomation.Infrastructure.Validators
{
    public class VirtualAccountValidator : AbstractValidator<VirtualAccountRequestDto>
    {
        public VirtualAccountValidator()
        {
            RuleFor(P => P.AggregatorName)
                .NotEmpty().WithMessage("{PropertyName} is required.");

            // RuleFor(P => P.MerchantId)
            //     .NotEmpty().WithMessage("{PropertyName} is required.");

    
            RuleFor(P => P.NoOfAcctRequested)
                .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(P => P.BVNAcct)
                .NotEmpty().WithMessage("{PropertyName} is required.");



        }
    }
}