using System;
using System.Collections.Generic;

namespace smartLaywer.Models;

public partial class PaymentSchedule
{
    public int Id { get; set; }

    public int FeeId { get; set; }

    public int InstallmentNumber { get; set; }

    public decimal PlannedAmount { get; set; }

    public DateTime DueDate { get; set; }

    public PaymentStatusEnum Status { get; set; }

    public string? Notes { get; set; }

    public virtual ICollection<ActualPayment> ActualPayments { get; set; } = new List<ActualPayment>();

    public virtual Fee Fee { get; set; } = null!;
}
