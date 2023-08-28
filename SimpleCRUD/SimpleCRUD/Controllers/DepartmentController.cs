using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleCRUD.Models;
using System.Data;
using System.Data.SqlClient;

namespace SimpleCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public DepartmentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select DepartmentId, DepartmentName from dbo.Department ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeConnection");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }

            }

            return new JsonResult(table);
        }

        [HttpPost("Insert")]
        public JsonResult InsertDept(Department dept)
        {
            string query = @"insert into dbo.Department 
                            values (@DepartmentName)";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeConnection");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@DepartmentName", dept.DepartmentName);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }

            }

            return new JsonResult("Inserted Successfully.");
        }

        [HttpPost("Update")]
        public JsonResult UpdateDept(Department dept)
        {
            string query = @"update dbo.Department set
                            DepartmentName = @DepartmentName
                            where DepartmentId = @DepartmentId";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeConnection");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@DepartmentId", dept.DepartmentId);
                    myCommand.Parameters.AddWithValue("@DepartmentName", dept.DepartmentName);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }

            }

            return new JsonResult("Updated Successfully.");
        }

        [HttpGet("Delete/{id}")]
        public JsonResult DeleteDept(int id)
        {
            string query = @"delete from dbo.Department where DepartmentId = @DepartmentId";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeConnection");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@DepartmentId", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }

            }

            return new JsonResult("Deleted Successfully.");
        }

    }
}
