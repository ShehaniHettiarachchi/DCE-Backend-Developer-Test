using DCE_Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace DCE_Application.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : Controller
	{
		private IConfiguration Configuration;

		public ProductController(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		SqlConnection connString;
		SqlCommand cmd;

		//Get All
		[HttpGet]
		public JsonResult Index()
		{
			List<Product> productList = new List<Product>();
			connString = new SqlConnection(this.Configuration.GetConnectionString("DefaultConnection"));
			DataTable dt = new DataTable();
			cmd = new SqlCommand("select * from Product", connString);
			connString.Open();
			SqlDataAdapter ad = new SqlDataAdapter(cmd);
			ad.Fill(dt);

			for (int i = 0; i < dt.Rows.Count; i++)
			{
				Product obj = new Product();
				obj.ProductId = dt.Rows[i]["ProductId"].ToString();
				obj.ProductName = dt.Rows[i]["ProductName"].ToString();
				obj.UnitPrice = Convert.ToDouble(dt.Rows[i]["UnitPrice"]);
				obj.SupplierId = dt.Rows[i]["SupplierId"].ToString();
				obj.CreatedOn = dt.Rows[i]["CreatedOn"].ToString();
				obj.IsActive = Convert.ToInt16(dt.Rows[i]["IsActive"]);
				productList.Add(obj);
			}
			connString.Close();
			return Json(dt);
		}
	}
}
