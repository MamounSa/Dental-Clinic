public class PaymentProfile : Profile
{
    public PaymentProfile()
    {
        CreateMap<CreatePaymentDto, Payment>();
        CreateMap<UpdatePaymentDto, Payment>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<Payment, PaymentDto>();
    }
}