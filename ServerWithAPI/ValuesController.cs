using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Server;
using Microsoft.Win32;
using ServerWithAPI.Models;

namespace ServerWithAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        string connectionString = "workstation id=MyDBForChat.mssql.somee.com;packet size=4096;user id=dro3d_rus;pwd=vbif4fvbif4f;data source=MyDBForChat.mssql.somee.com;persist security info=False;initial catalog=MyDBForChat;Trust Server Certificate=true;Encrypt=False";
        SqlConnection conn;

        [HttpGet("{email}/{password}")]
        public async Task<ActionResult<bool>> Login(string email, string password)
        {
            string sqlQuery;

            conn = new SqlConnection(connectionString);
            try
            {
                await conn.OpenAsync();
            }
            catch (Exception ex)
            {
               // await DisplayAlert("Login failed", ex.Message, "Try again");
            }
            sqlQuery = "SELECT Email, Pass FROM [dbo].[Accounts]";
            SqlCommand cmd = new SqlCommand(sqlQuery, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (await reader.ReadAsync())
            {
                if (reader.GetValue(0).ToString() == email && reader.GetValue(1).ToString() == password)
                {
                    return true;
                }

            }
            await conn.CloseAsync();
            return false;
            }

        [HttpGet("Regist/{email}/{password}/{name}")]
        public async Task <ActionResult<bool>> Register(string email, string password, string name)
        {
            string sqlQuery;

            conn = new SqlConnection(connectionString);
            try
            {
                await conn.OpenAsync();
            }
            catch (Exception ex)
            {
                // await DisplayAlert("Login failed", ex.Message, "Try again");
            }

            sqlQuery = "SELECT Email FROM [dbo].[Accounts]";
            SqlCommand cmd = new SqlCommand(sqlQuery, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            bool sameEmail = false;
            //Проверка наличия почты на сервере
            while (await reader.ReadAsync())
            {
                if (reader.GetValue(0).ToString() == email)
                {
                    sameEmail = true;
                }
            }
            await reader.CloseAsync();
            //Новый аккаунт
            AccountsModel newAcc = new AccountsModel();
            if (!sameEmail)
            {
                newAcc.Email = email;
                newAcc.Pass = password;
                newAcc.Name = name;
                newAcc.Role = "Pupil";
                newAcc.CreationDate = DateTime.Now;

                //

                sqlQuery =
                    $"INSERT INTO [dbo].[Accounts] (Email, Pass, Name, Role, CreationDate)" +
                    $" VALUES ('{newAcc.Email}', '{newAcc.Pass}', '{newAcc.Name}', '{newAcc.Role}', '{newAcc.CreationDate.ToString("MM/dd/yyyy").Replace('/', '.')}')";
                cmd = new SqlCommand(sqlQuery, conn);
                cmd.ExecuteNonQuery();
            }
            else
            {
                await conn.CloseAsync();
                return false;
            }

            await conn.CloseAsync();
            return true;
        }

    }

}
