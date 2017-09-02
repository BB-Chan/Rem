using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rem;
using Windows.UI.Xaml.Controls;
using SQLite.Net.Attributes;

namespace Rem.Models
{
    public class Passwords
    {
        public int ID { get; set; }
        public string Account { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string SQ1 { get; set; }
        public string SQA1 { get; set; }
        public string SQ2 { get; set; }
        public string SQA2 { get; set; }
        public string Code { get; set; }
        public string Accnumber { get; set; }

        public static explicit operator Passwords(ItemCollection v)
        {
            throw new NotImplementedException();
        }
    }

    public class Mail
    {
        //public int ID { get; set; }
        public string Account { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class Wallet
    {
        //public int ID { get; set; }
        public string Account { get; set; }
        public string CardNo { get; set; }
        public string ExpDate { get; set; }
        public string CVC { get; set; }
    }
}
