using System;
using System.Collections.Generic;

namespace smartLaywer.Models;

public partial class ActualPayment
{
    public int Id { get; set; }

    public int FeeId { get; set; }

    public decimal Amount { get; set; }

    public DateOnly PaymentDate { get; set; }

    public PaymentMethodEnum Method { get; set; }

    public string? ReceiptNumber { get; set; }

    public int ReceivedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public int? InstallmentId { get; set; }

    public virtual Fee Fee { get; set; } = null!;

    public virtual PaymentSchedule? Installment { get; set; }

    public virtual User ReceivedByNavigation { get; set; } = null!;
}
