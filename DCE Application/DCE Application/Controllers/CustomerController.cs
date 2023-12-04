using DCE_Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace DCE_Application.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CustomerController : Controller
	{
		private IConfiguration Configuration;

		public CustomerController(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		SqlConnection connString;
		SqlCommand cmd;

		//Get All
		[HttpGet]
		public JsonResult  Index()
		{
			List<Customer> cusList = new List<Customer>();
			connString = new SqlConnection(this.Configuration.GetConnectionString("DefaultConnection"));
			DataTable dt = new DataTable();
			cmd = new SqlCommand("select * from Customer", connString);
			connString.Open();
			SqlDataAdapter ad = new SqlDataAdapter(cmd);
			ad.Fill(dt);

			for (int i = 0; i < dt.Rows.Count; i++)
				{
				Customer obj = new Customer();
				obj.UserId = dt.Rows[i]["UserId"].ToString();
				obj.Username = dt.Rows[i]["Username"].ToString();
				obj.Email = dt.Rows[i]["Email"].ToString();
				obj.FirstName = dt.Rows[i]["FirstName"].ToString();
				obj.LastName = dt.Rows[i]["LastName"].ToString();
				obj.CreatedOn = dt.Rows[i]["CreatedOn"].ToString();
				obj.IsActive = Convert.ToInt16(dt.Rows[i]["IsActive"]);
				cusList.Add(obj);
			}
			connString.Close();
			return Json(dt);

		}

		//Insert
		[Route("Create")]
		[HttpPost]
		public ActionResult Create(Customer cus)
		{
			try
			{
				connString = new SqlConnection(this.Configuration.GetConnectionString("DefaultConnection"));
				cmd = new SqlCommand("insert into Customer(UserId, Username, Email, FirstName, LastName, CreatedOn, IsActive) values('" + cus.UserId + "','" + cus.Username + "','" + cus.Email + "','" + cus.FirstName + "','" + cus.LastName + "','" + cus.CreatedOn + "','" + cus.IsActive + "')", connString);

				connString.Open();
				cmd.ExecuteNonQuery();
				connString.Close();
				return Ok(new { Message = "Record Create" });
			}
			catch (Exception ef)
			{
				return BadRequest(ef.Message);
			}
		}

		//Update
		[Route("/{UserId}")]
		[HttpPut]

		public ActionResult Edit(Customer cus)
		{
			try
			{
				connString = new SqlConnection(this.Configuration.GetConnectionString("DefaultConnection"));
				cmd = new SqlCommand("UPDATE Customer SET " +
					  "Username = '" + cus.Username + "', " +
					  "Email = '" + cus.Email + "', " +
					  "FirstName = '" + cus.FirstName + "', " +
					  "LastName = '" + cus.LastName + "', " +
					  "IsActive = '" + cus.IsActive + "' " +
					  "WHERE UserId = '" + cus.UserId + "'", connString);
				connString.Open();
				int x = cmd.ExecuteNonQuery();

				if (x > 0)
				{
					return Ok(new { Message = "Updated" });
				}
				return BadRequest(new { Message = "Record Not Found" });

			}
			catch (Exception ef)
			{
				return BadRequest(ef.Message);
			}
			finally { connString.Close(); }
		}

		//Delete
		[Route("/{UserId}")]
		[HttpDelete]
		public ActionResult Delete(string UserId)
		{
			try
			{
				connString = new SqlConnection(this.Configuration.GetConnectionString("DefaultConnection"));
				cmd = new SqlCommand("delete from Customer where UserId=" + UserId + "", connString);
				connString.Open();
				int x = cmd.ExecuteNonQuery();
				if (x > 0)
				{
					return Ok(new { Message = "Record Deleted" });
				}
				return BadRequest(new { Message = "Record Not Found" });
			}
			catch (Exception ef)
			{
				return BadRequest(ef.Message);
			}
			finally { connString.Close(); }
		}

		//Get Active Orders
		[Route("{UserId}/ActiveOrders")]
		[HttpGet]
		public IActionResult GetActiveOrdersByCustomer(string UserId)
		{
			try
			{
				connString = new SqlConnection(this.Configuration.GetConnectionString("DefaultConnection"));
				cmd = new SqlCommand("GetActiveOrdersByCustomer", connString);
				cmd.CommandType = CommandType.StoredProcedure;

				cmd.Parameters.AddWithValue("@UserId", UserId);

				connString.Open();

				SqlDataAdapter ad = new SqlDataAdapter(cmd);
				DataTable dt = new DataTable();
				ad.Fill(dt);

				connString.Close();

				if (dt.Rows.Count > 0)
				{
					return Ok(new { Message = "Active Orders Found", Orders = dt });
				}

				return BadRequest(new { Message = "No Active Orders Found" });
			}
			catch (Exception ef)
			{
				return BadRequest(new { Message = "An error occurred while processing request.", ErrorDetails = ef.Message });
			}
			finally
			{
				connString.Close();
			}
		}
	}
}
