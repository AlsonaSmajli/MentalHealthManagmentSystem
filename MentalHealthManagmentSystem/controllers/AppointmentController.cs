using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;
using MentalHealthManagmentSystem.models;

namespace MentalHealthManagmentSystem.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AppointmentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //API method to get all the appointment details
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select AppointmentId, AppointmentName from MentalHealthDB";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PatientAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);

        }

        [HttpPost]
        public JsonResult Post(Appointment app)
        {
            string query = @"insert into dbo.Appointment values ('" + app.AppointmentName + @"') ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PatientAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Added successfully");
        }
        [HttpPut]
        public JsonResult Put(Appointment app)
        {
            string query = @"
                update dbo.Appointment set
                AppointmentName = '" + app.AppointmentName + @" '
                where AppointmentId= " + app.AppointmentId + @" ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PatientAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Added successfully");
        }

        [HttpDelete("{Id}")]
        public JsonResult Delete(int Id)
        {
            string query = @"
                delete from dbo.Appointment 
                where AppointmentId= " + Id + @" ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PatientAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Deleted successfully");
        }
    }
}
