using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MVC1001.Models;

namespace MVC1001.Controllers
{
    public class PosLajuParcelController : Controller
    {
        private readonly IConfiguration configuration;
        public PosLajuParcelController(IConfiguration config)
        {
            this.configuration = config;
        }

        // Method to get the database list
        IList<PosLajuParcel> GetDbList()
        {
            IList<PosLajuParcel> dbList = new List<PosLajuParcel>();
            SqlConnection conn = new SqlConnection(configuration.
                GetConnectionString("ParcelConnStr"));

            string sql = @"SELECT * FROM PosLajuParcels";

            SqlCommand cmd = new SqlCommand(sql, conn);
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    dbList.Add(new PosLajuParcel()
                    {
                        ViewId = reader.GetString(0),
                        ViewDateTime = reader.GetDateTime(1),
                        SenderName = reader.GetString(2),
                        SenderAddress = reader.GetString(3),
                        SenderPhone = reader.GetString(4),
                        SenderEmail = reader.GetString(5),
                        ReceiverName = reader.GetString(6),
                        ReceiverAddress = reader.GetString(7),
                        ReceiverPhone = reader.GetString(8),
                        ReceiverEmail = reader.GetString(9),
                        IndexWeight = reader.GetInt32(10),
                        IndexZone = reader.GetInt32(11),
                        Amount = reader.GetDouble(12)
                    });
                }
            }
            catch
            {
                RedirectToAction("Error");
            }
            finally
            {
                conn.Close();
            }
            return dbList;
        }

        public IActionResult Index()
        {
            IList<PosLajuParcel> dbList = GetDbList();
            return View(dbList);
        }

        public IActionResult Error()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ParcelDelivery()
        {
            PosLajuParcel parcel = new PosLajuParcel();
            parcel.IndexWeight = -1;
            parcel.IndexZone = -1;
            return View(parcel);
        }

        [HttpPost]
        public IActionResult ParcelDelivery(PosLajuParcel parcel)
        {
            if (ModelState.IsValid)
            {
                SqlConnection conn = new SqlConnection(configuration.GetConnectionString("ParcelConnStr"));
                SqlCommand cmd = new SqlCommand("spInsertParcel", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@parcelid", parcel.ParcelId);
                cmd.Parameters.AddWithValue("@parceldatetime", parcel.ParcelDateTime);
                cmd.Parameters.AddWithValue("@sendername", parcel.SenderName);
                cmd.Parameters.AddWithValue("@senderaddress", parcel.SenderAddress);
                cmd.Parameters.AddWithValue("@senderphone", parcel.SenderPhone);
                if (parcel.SenderEmail != null)
                    cmd.Parameters.AddWithValue("@senderemail", parcel.SenderEmail);
                else
                    cmd.Parameters.AddWithValue("@senderemail", "");
                cmd.Parameters.AddWithValue("@receivername", parcel.ReceiverName);
                cmd.Parameters.AddWithValue("@receiveraddress", parcel.ReceiverAddress);
                cmd.Parameters.AddWithValue("@receiverphone", parcel.ReceiverPhone);
                if (parcel.ReceiverEmail != null)
                    cmd.Parameters.AddWithValue("@receiveremail", parcel.ReceiverEmail);
                else
                    cmd.Parameters.AddWithValue("@receiveremail", "");
                cmd.Parameters.AddWithValue("@indexweight", parcel.IndexWeight);
                cmd.Parameters.AddWithValue("@indexzone", parcel.IndexZone);
                cmd.Parameters.AddWithValue("@amount", parcel.Amount);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    return View(parcel);
                }
                finally
                {
                    conn.Close();
                }

                return View("ParcelDeliveryInvoice", parcel);
            }
            else
                return View(parcel);
        }

        public IActionResult Details (string id)
        {
            IList<PosLajuParcel> dbList = GetDbList();
            var result = dbList.First(x => x.ViewId == id);
            return View(result);
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            IList<PosLajuParcel> dbList = GetDbList();
            var result = dbList.First(x => x.ViewId == id);
            return View(result);
        }

        [HttpPost]
        public IActionResult Edit(string id, PosLajuParcel parcel)
        {
            SqlConnection conn = new SqlConnection(configuration.
                GetConnectionString("ParcelConnStr"));
            SqlCommand cmd = new SqlCommand("spUpdateParcel", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@receivername", parcel.ReceiverName);
            cmd.Parameters.AddWithValue("@receiveraddress", parcel.ReceiverAddress);
            cmd.Parameters.AddWithValue("@receiverphone", parcel.ReceiverPhone);
            cmd.Parameters.AddWithValue("@receiveremail", parcel.ReceiverEmail);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch
            {
                RedirectToAction("Error");
            }
            finally
            {
                conn.Close();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(string id)
        {
            IList<PosLajuParcel> dbList = GetDbList();
            var result = dbList.First(x => x.ViewId == id);
            return View(result);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult ConfirmDelete(string id)
        {
            SqlConnection conn = new SqlConnection(configuration.
                GetConnectionString("ParcelConnStr"));
            SqlCommand cmd = new SqlCommand("spDeleteParcel", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@id", id);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
            finally
            {
                conn.Close();
            }
        }

        public IActionResult SearchIndex(string searchString = "")
        {
            IList<PosLajuParcel> dbList = GetDbList();
            var result = dbList.Where(x => x.ViewId.ToLower().Contains(searchString.ToLower()) ||
            x.SenderName.ToLower().Contains(searchString.ToLower()))
                .OrderBy(x => x.SenderName).ThenByDescending(x => x.ViewDateTime);

            return View("Index", result);
        }

    }
}
