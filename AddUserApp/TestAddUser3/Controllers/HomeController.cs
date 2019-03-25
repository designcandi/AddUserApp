using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestAddUser3.Models; 
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace TestAddUser3.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(User user)
        {                    
        

            if (!ModelState.IsValid)
            {
                ViewData["status"] = "Not Valid";
            }
            else
            {
                ViewData["status"] = AddUser(user);

            }
            return View();
        }

        public String AddUser(User user)
        {
                var password = user.Password;
                var passwordHashed = string.Empty;
                var salt = GetRandomSalt();
                passwordHashed = HashPassword(password, salt);

                var status = string.Empty;

                using (SqlConnection con = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=TestAddUser;Trusted_Connection=True;"))
                {

                using (SqlCommand cmd = new SqlCommand("select * from Users where Email = '"+user.Email+"'"))
                {
                    var IsEmailExist = false;
                    cmd.Connection = con;
                    con.Open();
                    // Retrieving Record from datasource  
                    SqlDataReader sdr = cmd.ExecuteReader();
                    // Reading and Iterating Records  
                    if (sdr.HasRows)
                    {
                        IsEmailExist = true;
                       
                    }

                    sdr.Close();
                    con.Close();

                    if (IsEmailExist)
                    {
                        status = ("Email already exists");
                        return (status);
                    }
                }



                // Insert query  
                string query = "INSERT INTO Users(Email,Password,Salt) VALUES(@email, @password, @salt)";
                    using (SqlCommand cmd = new SqlCommand(query))
                    {
                        cmd.Connection = con;
                        // opening connection  
                        con.Open();
                        // Passing parameter values  

                        cmd.Parameters.AddWithValue("@email", user.Email);
                        cmd.Parameters.AddWithValue("@password", passwordHashed);
                        cmd.Parameters.AddWithValue("@salt", salt);
                        // Executing insert query  
                        status = (cmd.ExecuteNonQuery() >= 1) ? "Record is saved Successfully!" : "Record is not saved";
                      
                    }
                   

                }
                return(status);
            
        }
        public String GetRandomSalt(Int32 size = 12)
        {
            var random = new RNGCryptoServiceProvider();
            var salt = new Byte[size];
            random.GetBytes(salt);
            return Convert.ToBase64String(salt);
        }

        public String HashPassword(String password, String salt)
        {
            var combinedPassword = String.Concat(password, salt);
            var sha256 = new SHA256Managed();
            var bytes = System.Text.UTF8Encoding.UTF8.GetBytes(combinedPassword);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }


        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
