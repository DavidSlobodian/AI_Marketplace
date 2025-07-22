using AIMarketplace.BL.Statuses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMarketplace.BL.Services.AdCreationService
{
    public class AdCreationResult
    {
        public object ReturnObject { get; set; }
        public AdCreationStatus Status {  get; set; }
    }
}
