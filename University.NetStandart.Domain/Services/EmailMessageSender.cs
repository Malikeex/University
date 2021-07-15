using System;
using System.Collections.Generic;
using System.Text;

namespace University.NetStandart.Domain.Services
{
    public interface IMessageSender
    {
        string Send();
    }
    public class EmailMessageSender : IMessageSender
    {
        public string Send()
        {
            return "Sent by Email";
        }
    }
}
