public class InvoiceProfile : Profile
{
    public InvoiceProfile()
    {
        CreateMap<CreateInvoiceDto, Invoice>();
        CreateMap<Invoice, InvoiceDto>();
        CreateMap<UpdateInvoiceDto, Invoice>();
    }
}