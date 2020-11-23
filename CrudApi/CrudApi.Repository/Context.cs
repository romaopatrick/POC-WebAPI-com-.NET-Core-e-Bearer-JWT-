using CrudApi.Domain.Models;
using System;
using System.Data.SqlClient;
using System.Net;
using System.Web.Http;

namespace CrudApi.Repository
{
    public class Context
    {
        public string _login;
        public ClientModel _client;
        private static string strcon = @"Data Source=LAPTOP-G44R5A85\SQLEXPRESS;Initial Catalog=CrudApiDB;Integrated Security=True";
        SqlConnection conn = new SqlConnection(strcon);
        public Context(ClientModel client)
        {
            _client = client;
        }
        public Context(string login)
        {
            _login = login;
        }
        public bool InsertClient()
        {
            try
            {
                string insert = "INSERT INTO Clients(Login, Password) VALUES ('" + _client.Login + "', '" + _client.Password + "')";
                SqlCommand cmd = new SqlCommand(insert,conn);
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                if(i>0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }
        }
        public bool CheckInDb()
        {
            string select = "SELECT * FROM Clients WHERE Login = '" + _client.Login + "' and Password = '" + _client.Password + "'";
            SqlConnection conn = new SqlConnection(strcon);
            try
            {
                SqlCommand cmd = new SqlCommand(select,conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if(reader.HasRows == false)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }
        public bool Update(ClientModel client)
        {
            string update = "UPDATE Clients SET Login ='" + client.Login + "', Password = '" + client.Password + "' WHERE Login = '"
                + _login + "';" ;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(update, conn);
                int i = cmd.ExecuteNonQuery();
                if (i>0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }
        }
        public bool DeleteClient()
        {
            string delete = "DELETE FROM Clients WHERE Login = '" + _login + "'";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(delete, conn);
                int i = cmd.ExecuteNonQuery();
                if(i>0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
