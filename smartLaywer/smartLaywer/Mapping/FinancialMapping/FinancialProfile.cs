namespace smartLaywer.Mapping.FinancialMapping
{
    public class FinancialProfile :Profile
    {
        public FinancialProfile()
        {
            CreateMap<Fee, FeeDetailsDto>()
            .ForMember(dest => dest.CaseNumber, opt => opt.MapFrom(src => src.Case.CaseNumber))
            .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.Client.FullName))
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.CreatedAt))
            .ForMember(dest => dest.CollectedAmount, opt => opt.MapFrom(src => src.ActualPayments.Sum(ap => ap.Amount)))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src =>
                src.ActualPayments.Sum(ap => ap.Amount) >= src.TotalAmount ? "ãßÊãáÉ" : "ãÝÊæÍÉ"))
            .ForMember(dest => dest.Payments, opt => opt.MapFrom(src => src.ActualPayments.OrderByDescending(p => p.CreatedAt)));

            CreateMap<ActualPayment, PaymentLogDto>()
                .ForMember(dest => dest.PaymentDate, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.Method, opt => opt.MapFrom(src => src.Method.ToString()));

            CreateMap<PaymentCreationDto, ActualPayment>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.PaymentDate))
                .ForMember(dest => dest.Method, opt => opt.MapFrom(src => src.PaymentMethod));

            CreateMap<PaymentSchedule, InstallmentDetailDto>()
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.PlannedAmount))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
        }
    }
}
