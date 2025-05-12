using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace FixMyTask_Core_Project.Models
{
    public class DataDB
    {

        SqlConnection con;
        public DataDB()
        {
            con = new SqlConnection(@"server=(localdb)\MSSQLLocalDB;database=FixMyTaskDB;integrated security=true;");
        }

            public string UserinsertDB(UserInsert clsobj)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_userinsert", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@uid", clsobj.User_Id);
                    cmd.Parameters.AddWithValue("@uname", clsobj.User_Name);
                    cmd.Parameters.AddWithValue("@uemail", clsobj.User_Email);
                    cmd.Parameters.AddWithValue("@uphone", clsobj.User_Phone);
                    cmd.Parameters.AddWithValue("@uaddress", clsobj.User_Address);
                    cmd.Parameters.AddWithValue("@uphoto", clsobj.User_Photo);
                    cmd.Parameters.AddWithValue("@ugender", clsobj.User_Gender);
                    cmd.Parameters.AddWithValue("@ucountry", clsobj.User_Country);
                    cmd.Parameters.AddWithValue("@ustate", clsobj.User_State);
                    cmd.Parameters.AddWithValue("@ucity", clsobj.User_City);
                    cmd.Parameters.AddWithValue("@upincode", clsobj.Pincode);
                    cmd.Parameters.AddWithValue("@crtdate", clsobj.CreatedDate);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return ("Insert Successfully");
                }
                catch (Exception ex)
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    return ex.Message.ToString();
                }
            }  

        public string WorkerInsertDB(WorkerInsert clsobj)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_workerInsert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@wid", clsobj.Worker_Id);
                cmd.Parameters.AddWithValue("@cid", clsobj.Category_Id);
                cmd.Parameters.AddWithValue("@wname", clsobj.Worker_Name);
                cmd.Parameters.AddWithValue("@wemail", clsobj.Worker_Email);
                cmd.Parameters.AddWithValue("@wphone", clsobj.Worker_Phone);
                cmd.Parameters.AddWithValue("@wphoto", clsobj.Worker_Photo);
                cmd.Parameters.AddWithValue("@waddress", clsobj.Worker_Address);
                cmd.Parameters.AddWithValue("@wcountry", clsobj.Worker_Country);
                cmd.Parameters.AddWithValue("@wstate", clsobj.Worker_State);
                cmd.Parameters.AddWithValue("@wcity", clsobj.Worker_City);
                cmd.Parameters.AddWithValue("@wexp", clsobj.Worker_Exp);
                cmd.Parameters.AddWithValue("@wprice", clsobj.Worker_Price);
                cmd.Parameters.AddWithValue("@wdescription", clsobj.Worker_Description);
                cmd.Parameters.AddWithValue("@wcredate", clsobj.Worker_CreatedDate);
                cmd.Parameters.AddWithValue("@wgender", clsobj.Worker_Gender);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return ("Insert Successfully");
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return ex.Message.ToString();
            }
        }

        public int LoginId(string username, string password)
        {
            try
            {
                
                SqlCommand cmd = new SqlCommand("sp_loginId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@una", username);
                cmd.Parameters.AddWithValue("@pwd", password);
                con.Open();
                var result = cmd.ExecuteScalar().ToString();
                con.Close();
                
                return Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                throw new Exception("Error in LoginId: " + ex.Message);
            }
        }

        public int LoginCountId(string username, string password)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_loginCountId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@una", username);
                cmd.Parameters.AddWithValue("@pwd", password);
                con.Open();
                var result= cmd.ExecuteScalar().ToString();
                con.Close();
                
                return Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                throw new Exception("Error in LoginCountId: " + ex.Message);
            }
        }

        public string GetUserType(string username, string password)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_loginType", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@una", username);
                cmd.Parameters.AddWithValue("@pwd", password);
                con.Open();
                var result = cmd.ExecuteScalar().ToString();
                con.Close();

                return result.ToString();
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                throw new Exception("Error in LoginCountId: " + ex.Message);
            }
        }

        public int GetMaxId()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_maxIdLogin", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                object result = cmd.ExecuteScalar();
                con.Close();

                if (result != null && result != DBNull.Value)
                {
                    return Convert.ToInt32(result);
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                throw new Exception("Error in GetMaxId: " + ex.Message);
            }
        }


        //public int GetMaxId()
        //{
        //    try
        //    {
        //        SqlCommand cmd = new SqlCommand("sp_maxIdLogin", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        con.Open();
        //        int maxId =Convert.ToInt32(cmd.ExecuteScalar());
        //        con.Close();

        //        return maxId;
        //    }
        //    catch (Exception ex)
        //    {
        //        if (con.State == ConnectionState.Open)
        //        {
        //            con.Close();
        //        }
        //        throw new Exception("Error in UserinsertDB: " + ex.Message);
        //    }
        //}

        public bool LoginCheck(string username)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_checkuser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@una", username);
                con.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();

                return count > 0;
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                throw new Exception("Error checking username: " + ex.Message);
            }
        }

        public string LoginInsert(Login clsobj)
        {
            try
            {

                SqlCommand cmd = new SqlCommand("sp_logInsert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@rid", clsobj.Reg_Id);
                cmd.Parameters.AddWithValue("@una", clsobj.Username);
                cmd.Parameters.AddWithValue("@pwd", clsobj.Password);
                cmd.Parameters.AddWithValue("@utype", clsobj.UserType);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                
                return "Login inserted successfully";
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return ex.Message.ToString();
            }
        }

        public List<Category> GetCategories()
        {
            List<Category> categories = new List<Category>();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_getCategories", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    categories.Add(new Category
                    {
                        Category_Id = Convert.ToInt32(dr["Category_Id"]),
                        Category_Name = dr["Category_Name"].ToString()

                    });
                }
                con.Close();
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                throw new Exception("Error in GetCategories: " + ex.Message);
            }
            return categories;
        }

        public List<Category> GetCategoriesUserPage()
        {
            List<Category> categories = new List<Category>();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_getCategoriesUserPage", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    categories.Add(new Category
                    {
                        Category_Id = Convert.ToInt32(dr["Category_Id"]),
                        Category_Name = dr["Category_Name"].ToString(),
                        Category_Image = dr["Category_Image"].ToString()

                    });
                }
                con.Close();
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                throw new Exception("Error in GetCategories: " + ex.Message);
            }
            return categories;
        }

        public List<WorkerModel> GetWorkersByCategory(int categoryId)
        {
            List<WorkerModel> workers = new List<WorkerModel>();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_getWorkersByCategory", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cid", categoryId);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    workers.Add(new WorkerModel
                    {
                        WorkerId = Convert.ToInt32(dr["Worker_Id"]),
                        CategoryId = Convert.ToInt32(dr["Category_Id"]),
                        WorkerName= dr["Worker_Name"].ToString(),
                        Category_Name = dr["Category_Name"].ToString(),
                        WorkerEmail = dr["Worker_Email"].ToString(),
                        WorkerPhoto = dr["Worker_Photo"].ToString(),
                        WorkerExperience = dr["Exp_Years"].ToString(),
                        WorkerCountry = dr["Worker_Country"].ToString(),
                        WorkerState = dr["Worker_State"].ToString(),
                        WorkerCity = dr["Worker_City"].ToString(),
                        WorkerPhone = dr["Worker_Phone"].ToString(),

                    });
                }
                con.Close();
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                throw new Exception("Error in GetWorkersByCategory: " + ex.Message);
            }
            return workers;
        }
        public WorkerModel ViewWorker(int workerId)
        {
            WorkerModel worker = new WorkerModel();

            try
            {
                SqlCommand cmd = new SqlCommand("sp_viewWorker", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@wid", workerId);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    worker=new WorkerModel
                    {
                        WorkerId = Convert.ToInt32(dr["Worker_Id"]),
                        CategoryId = Convert.ToInt32(dr["Category_Id"]),
                        WorkerName = dr["Worker_Name"].ToString(),
                        Category_Name = dr["Category_Name"].ToString(),
                        WorkerEmail = dr["Worker_Email"].ToString(),
                        WorkerPhoto = dr["Worker_Photo"].ToString(),
                        WorkerExperience = dr["Exp_Years"].ToString(),
                        WorkerCountry = dr["Worker_Country"].ToString(),
                        WorkerState = dr["Worker_State"].ToString(),
                        WorkerCity = dr["Worker_City"].ToString(),
                        WorkerPhone = dr["Worker_Phone"].ToString(),
                        WorkerAddress = dr["Worker_Address"].ToString(),

                    };
                }
                con.Close();
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                throw new Exception("Error in GetWorkersByCategory: " + ex.Message);
            }
            return worker;
        }
        public int SaveBooking(Booking clsobj)
        {
            try
            {

                SqlCommand cmd = new SqlCommand("sp_bookingDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@wid", clsobj.Worker_Id);
                cmd.Parameters.AddWithValue("@uid", clsobj.User_Id);
                cmd.Parameters.AddWithValue("@bookingDate", clsobj.Booking_Date);
                cmd.Parameters.AddWithValue("@serviceDate", clsobj.ServiceDate);
                cmd.Parameters.AddWithValue("@serviceTime", clsobj.ServiceTime);
                cmd.Parameters.AddWithValue("@uAddress", clsobj.User_Address);
                cmd.Parameters.AddWithValue("@remarks", clsobj.Remarks);
                cmd.Parameters.AddWithValue("@paid", clsobj.Paid);
                cmd.Parameters.AddWithValue("@status", clsobj.Booking_Status);

                SqlParameter outId = new SqlParameter("@bid", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outId);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return Convert.ToInt32(outId.Value);
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                throw new Exception("Error while saving booking: " + ex.Message);
            }
        }

        public Booking GetBookingById(int bookingId)
        {
            try
            {

                SqlCommand cmd = new SqlCommand("sp_getBookingDetils", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@bid",bookingId);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                Booking booking = new Booking();
                while (dr.Read())
                {
                    booking = new Booking
                    {
                        Booking_Id = Convert.ToInt32(dr["Booking_Id"]),
                        Worker_Id = Convert.ToInt32(dr["Worker_Id"]),
                        User_Id = Convert.ToInt32(dr["User_Id"]),
                        Booking_Date = Convert.ToDateTime(dr["Booking_Date"]),
                        ServiceDate = Convert.ToDateTime(dr["ServiceDate"]),
                        ServiceTime = dr["ServiceTime"].ToString(),
                        User_Address = dr["User_Address"].ToString(),
                        Remarks = dr["Remarks"].ToString(),
                        Paid = dr["Paid"].ToString(),
                        Booking_Status = dr["Booking_Status"].ToString(),

                    };
                }
                con.Close();
                return booking;
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                throw new Exception("Error: " + ex.Message);
            }
        }

        public string SaveUserReview(UserReview clsobj)
        {
            try
            {

                SqlCommand cmd = new SqlCommand("sp_bookingDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@wid", clsobj.Worker_Id);
                cmd.Parameters.AddWithValue("@uid", clsobj.User_Id);
                cmd.Parameters.AddWithValue("@bid", clsobj.Booking_Id);
                cmd.Parameters.AddWithValue("@review", clsobj.Review);
                cmd.Parameters.AddWithValue("@rating", clsobj.Rating);
                cmd.Parameters.AddWithValue("@reviewDate", clsobj.ReviewDate);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return ("Insert Successfully");
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return ex.Message.ToString();
            }
        }
    }
}
