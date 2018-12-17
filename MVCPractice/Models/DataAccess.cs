using MVCPractice.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCPractice.Models
{
    public static class DataAccess
    {
        private static string _connectionString = "Server=localhost;Database=MVCPractice;Trusted_Connection=True;";

        public static IEnumerable<User> GetAllUsers()
        {
            List<User> outUsers = new List<User>();

            using(SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(@"
SELECT u.id, 
u.first_name, 
u.last_name, 
u.username, 
c.id,
c.name,
c.address,
c.phone
FROM Users u
JOIN Companies c ON c.id = u.company_id", conn);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    User newUser = new User();
                    Company newCompany = new Company();


                    newUser.Id = reader.GetInt32(0);
                    newUser.FirstName = reader.GetString(1);
                    newUser.LastName = reader.GetString(2);
                    newUser.Username = reader.GetString(3);

                    newCompany.Id = reader.GetInt32(4);
                    newCompany.Name = reader.GetString(5);
                    newCompany.Address = reader.GetString(6);
                    newCompany.Phone = reader.GetString(7);

                    newUser.Company = newCompany;

                    outUsers.Add(newUser);
                }
            }

            return outUsers;
        }

        public static IEnumerable<SelectListItem> GetAllCompanyData()
        {
            List<SelectListItem> outItems = new List<SelectListItem>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(@"SELECT id, name FROM Companies", conn);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    SelectListItem newCompany = new SelectListItem();

                    newCompany.Value = reader.GetInt32(0).ToString();
                    newCompany.Text = reader.GetString(1);

                    outItems.Add(newCompany);
                }
            }

            return outItems;
        }

        public static void UpdateUser(UserCompanyViewModel user)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand($@"UPDATE Users SET first_name = '{user.FirstName}', last_name = '{user.LastName}', username = '{user.Username}', company_id = {user.CompanyId} WHERE id = {user.UserId}", conn);

                conn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public static void CreateUser(UserCompanyViewModel user)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand($@"INSERT INTO Users (first_name, last_name, username, company_id) VALUES ('{user.FirstName}','{user.LastName}','{user.Username}',{user.CompanyId})", conn);

                conn.Open();

                cmd.ExecuteNonQuery();
            }
        }
    }
}