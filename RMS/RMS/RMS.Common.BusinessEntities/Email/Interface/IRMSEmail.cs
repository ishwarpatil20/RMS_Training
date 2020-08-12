using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace RMS.Common.BusinessEntities.Email
{
    public interface IRMSEmail
    {
        string From { get; set; }
        ArrayList To { get; set; }
        ArrayList CC { get; set; }
        string Subject { get; set; }
        string Body { get; set; }

        string EmailFrom();
        ArrayList EmailReceipientTo();
        ArrayList EmailReceipientCC();
        string EmailSubject();
        string EmailBody();

        void SendEmail(IRMSEmail objEmailConfigEntities);
    }
}
