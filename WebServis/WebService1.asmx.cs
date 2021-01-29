using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using System.Windows;
using WebServis.Models;

namespace WebServis
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {
        DatabaseContext db = new DatabaseContext();

      
        [WebMethod]
        public void saveUsers(string username,string email,string password,string rePassword)
        {
            var numberList = db.Users.ToList().LastOrDefault();
            User user = new User();

            var accountControlList = db.Users.ToList();
            int flag = 0;
         
            foreach (var control in accountControlList)
            {
                if (control.Username != username && control.Mail != email)
                {
                    flag = 0;
                }
                else
                {
                    flag = 1;
                    goto Etiket;
                }
            }
        Etiket:
            if (flag == 0)
            {
                if (password == rePassword)
                {
                    if (numberList == null)
                    {
                        user.HomepageNu = 1;
                        user.ProfileNu = 1;
                    }
                    else
                    {
                        user.HomepageNu = numberList.HomepageNu + 1;
                        user.ProfileNu = numberList.ProfileNu + 1;
                    }
                    user.Username = username;
                    user.Mail = email;
                    user.Password = password;
                   
                    user.IsOnline = false;
                    user.ProfileImage = "";

                    db.Users.Add(user);
                    db.SaveChanges();
                    MessageBox.Show("KAYIT BAŞARILI");
                }
                else
                {
                    MessageBox.Show("ŞİFRELERİNİZ EŞLEŞMEDİ,TEKRAR DENEYİNİZ");
                }
            }
            else if (flag == 1)
            {
                MessageBox.Show("KULLANICI ADINIZ VEYA MAİL ADRESİNİZ DAHA ÖNCE KULLANILMIŞ,KAYIT BAŞARISIZ ");

            }
        }
        [WebMethod]
        public List<User> getUsers()
        {
            List<User> userList = db.Users.ToList();
            return  userList;
        }
    }
}
