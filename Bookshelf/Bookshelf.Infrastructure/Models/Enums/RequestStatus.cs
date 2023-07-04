using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshelf.Infrastructure.Models.Enums
{
    public enum RequestStatus
    {
        AwaitingConfirmation,
        InPreparation,
        Delivering,
        Delivered,
        Discarded
    }
}
