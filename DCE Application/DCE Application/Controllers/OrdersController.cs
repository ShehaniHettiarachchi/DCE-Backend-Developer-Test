using DCE_Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace DCE_Application.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrdersController : Controller
	{
		private IConfiguration Configuration;

		public OrdersController(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		SqlConnection connString;
		SqlCommand cmd;

		//Get All
		[HttpGet]
		public JsonResult Index()
		{
			List<Orders> orderList = new List<Orders>();
			connString = new SqlConnection(this.Configuration.GetConnectionString("DefaultConnection"));
			DataTable dt = new DataTable();
			cmd = new SqlCommand("select * from Orders", connString);
			connString.Open();
			SqlDataAdapter ad = new SqlDataAdapter(cmd);
			ad.Fill(dt);

			for (int i = 0; i < dt.Rows.Count; i++)
			{
				Orders obj = new Orders();
				obj.OrderId = dt.Rows[i]["OrderId"].ToString();
				obj.ProductId = dt.Rows[i]["ProductId"].ToString();
				obj.OrderStatus = Convert.ToInt16(dt.Rows[i]["OrderStatus"]);
				obj.OrderType = Convert.ToInt16(dt.Rows[i]["OrderType"]);
				obj.OrderBy = dt.Rows[i]["OrderBy"].ToString();
				obj.OrderedOn = dt.Rows[i]["OrderedOn"].ToString();
				obj.ShippedOn = dt.Rows[i]["ShippedOn"].ToString();
				obj.IsActive = Convert.ToInt16(dt.Rows[i]["IsActive"]);
				orderList.Add(obj);
			}
			connString.Close();
			return Json(dt);
		}

	}
}
