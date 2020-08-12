using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Rave.HR.BusinessLayer.Interface
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
