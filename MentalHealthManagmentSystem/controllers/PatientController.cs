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
    public class PatientController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public PatientController (IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //API method to get all the appointment details
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
             select PatientId, PatientName, PatientSurname, PatientAge, PatientHistory, PatientGender
            from dbo.Patient
               ";
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
        public JsonResult Post(Patient p)
        {
            string query = @"insert into dbo.Patient
                            (PatientName,PatientSurname,PatientAge,PatientHistory,PatientGender )
                            values 
                            
                            (
                            '" + p.PatientName + @"'
                           ,  '" + p.PatientSurname + @"'
                            , '" + p.PatientAge + @"'
                           , '" + p.PatientHistory + @"'
                            ,'" + p.PatientGender + @"'
                            ) ";
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
        public JsonResult Put(Patient p)
        {
            string query = @"
                update dbo.Patient set
                PatientName = '" + p.PatientName + @" '
                ,PatientSurname = '" + p.PatientSurname + @" '
                ,PatientAge = '" + p.PatientAge + @" '
                ,PatientHistory = '" + p.PatientHistory + @" '
                ,PatientGender = '" + p.PatientGender + @" '

                where PatientId= " + p.PatientId + @" ";
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
                delete from dbo.Patient
                where PatientId= " + Id + @" ";
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

        [Route("GetAllAppointmentName")]
        public JsonResult GetAllAppointmentName()
        {
            string query = @"
             select AppointmentName from  dbo.Appointment
               ";
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
    }
}


