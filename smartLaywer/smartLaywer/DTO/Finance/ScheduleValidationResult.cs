using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartLaywer.DTO.Finance
{
    public class ScheduleValidationResult
    {
        public bool CanAdd { get; set; }        
        public decimal CaseTotalFee { get; set; }  
        public decimal AlreadyScheduled { get; set; } 
        public decimal RemainingToSchedule { get; set; } 
        public string Status { get; set; }
    }
}
