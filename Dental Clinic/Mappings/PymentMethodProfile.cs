
public class PaymentMethodProfile : Profile
{
    public PaymentMethodProfile()
    {
        // PaymentMethod mappings
        CreateMap<PaymentMethod, PaymentMethodDto>();
        CreateMap<CreatePaymentMethodDto, PaymentMethod>();
        CreateMap<UpdatePaymentMethodDto, PaymentMethod>();
    }
}