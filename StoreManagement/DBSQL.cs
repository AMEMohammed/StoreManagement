using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using System.Data;

namespace StoreManagement
{
    class DBSQL
    {
       private string ConnectionSreing = @"Data Source=.\S2008;Initial Catalog=StoreManagement;Integrated Security=True";
        public SqlConnection con;
        public SqlCommand cmd;
        public SqlDataReader reader;
        public SqlDataAdapter adapter;
        public DBSQL()
        {
            con = new SqlConnection(ConnectionSreing);
        }

        ///
        /// function Category
        /// 
        // get all category

      public  DataTable  GetAllCategory()
        {
            string qury = "SELECT [IDCategory] as 'رقم الصنف',[NameCategory] as 'اسم الصنف' FROM  [StoreManagement].[dbo].[Category]";
            return ExecuteQurySelect(qury);

        }

        ///
        ////  Search Category
        public DataTable SearchCategory(string nameCatagory)
        {
            nameCatagory = "%" + nameCatagory + "%";
            DataTable dt = new DataTable(); 
            cmd=new SqlCommand("select IDCategory as 'رقم الصنف',  NameCategory as 'اسم الصنف' from Category where NameCategory like @NameCategory", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@NameCategory", nameCatagory);
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt;

          }

    /// <summary>
    ///  Add New Category
    /// </summary>
    /// <param name="NameCategory"></param> this name a category
    /// <returns></returns>
        public int AddNewCategory(string NameCategory)
        {
            string qury = "INSERT INTO [StoreManagement].[dbo].[Category]([NameCategory])VALUES(@name)";
            return ExecuteQury(qury, 0, NameCategory,1);
         
        }

        ///
        // Update Category 
        public int UpdateCategory(int id,string name)
        {
            string qury = " UPDATE [StoreManagement].[dbo].[Category]  SET[NameCategory] = @name  WHERE IDCategory=@id";
            return ExecuteQury(qury, id, name, 2);

       }

        //
        // delete Category
        public int DeleteCategory(int id)
        {
            string qury = "DELETE FROM [StoreManagement].[dbo].[Category] WHERE IDCategory=@id";
            return ExecuteQury(qury, id, null, 3);
        }

        ////////////////////////////////////////////////////////////////////////////////////////
        ///////// TypeQuntity
        // 

///    /////
///    //Get All TypeQuntity
///    
        public DataTable GetAllTypeQuntity()
        {
            string qury = "SELECT [IDType] as 'رقم النوع' ,[NameType] as 'اسم النوع' FROM [StoreManagement].[dbo].[TypeQuntity]";
            return ExecuteQurySelect(qury);
        }


        //add new TypeQuntity
        public int AddNewTypeQuntity( string name)
        {
            string qury = "insert into TypeQuntity (NameType) values (@name)";
            return ExecuteQury(qury, 0, name, 1);

        }
        
        ///////////
        /// Updte TypeQuntit
        /// 
        public int UpdateTypeQuntity(int id,string name)
        {
            string qury = "UPDATE [StoreManagement].[dbo].[TypeQuntity]  SET [NameType] = @name WHERE IDType=@id";
            return ExecuteQury(qury, id, name, 2);

        }

        ///
        // delete TypeQuntity
        public int DeleteQuntity(int id)
        {
            string qury= "DELETE FROM [StoreManagement].[dbo].[TypeQuntity]WHERE IDType = @id";
            return ExecuteQury(qury, id, null, 3);
        }

        ///////
        /////////
        ///////////
        //PlaceSend
        //


        //Get All Place send
        public DataTable GetAllPlace()
        {
            string qury = "SELECT [IDPlace]as 'رقم الجهة' ,[NamePlace] as 'اسم الجهة'  FROM [StoreManagement].[dbo].[PlaceSend]";
            return ExecuteQurySelect(qury);

        }



        // add new PlaceSend
        public int AddPlaceSend(string name)
        {
            string qury = "INSERT INTO [StoreManagement].[dbo].[PlaceSend] ([NamePlace]) VALUES  (@id)";
            return ExecuteQury(qury, 0, name, 1);

        }


        //
        // update Place send
        public int UpdtePlaceSend(int id, string name)
        {
            string qury = "UPDATE [StoreManagement].[dbo].[PlaceSend] SET [NamePlace] =@name  WHERE IDPlace =@id";
            return ExecuteQury(qury, id, name, 2);
        }

        ////
        // delete PalceSend
        public int DeletePlaceSend(int id)
        {
            string qury = "";
            return ExecuteQury(qury, id,null, 3);

        }
         

        ////////////////////////////////////////////////////////
        //////////
        ///
         /// CHACKE is account is here ro not
          public int CheckAccountIsHere(int IDCategory,int IDType,int price)
        {
            int reslt = 0;
            try
            {
                cmd = new SqlCommand("select IDAccount from Account where IDCategory=@IDCategory and IDType=@IDType and Price=@Price ", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@IDCategory", IDCategory);
                cmd.Parameters.AddWithValue("@IDType", IDType);
                cmd.Parameters.AddWithValue("@Price", price);
                con.Open();
                reslt = (int)cmd.ExecuteScalar();
                con.Close();

            }
            catch
            { reslt = 0; }

            return reslt;
        }
        //////
        //////
        //// Check Qunnty is Here InCheckQuntity
        public int CheckQuntityISHereInCheckQuntity(int IDCategory,int IDType)
        {
            int reslt = 0;
            try
            {
                cmd = new SqlCommand("select IDCheck from CheckQuntity where IDCategory=@IDCategory and IDType=@IDType", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@IDCategory", IDCategory);
                cmd.Parameters.AddWithValue("@IDType", IDType);
               
                con.Open();
                reslt = (int)cmd.ExecuteScalar();
                con.Close();

            }
            catch
            { reslt = 0; }

            return reslt;

        }
        
        ///
        ///














        /// <summary>
        ///  exuectue  qury ansert  and delete and update
        /// </summary>
        /// <param name="qury"></param>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private int ExecuteQury(string qury,int id,string name ,int flag)
        {
            int res = 0;
            cmd=new SqlCommand(qury,con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            if (flag == 1) //insert
            {
                cmd.Parameters.AddWithValue("@name", name);

            }
           if(flag==2) //update
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@name", name);

            }
           else(flag==3)//delete
            {
                cmd.Parameters.AddWithValue("@id", id); 

            }
            res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }

        //////////////
        /// exuctte qury return datatable 
        /// 
         
        private DataTable ExecuteQurySelect(string qury)
        { 
              
            DataTable dt = new DataTable();
            cmd = new SqlCommand(qury, con);
            cmd.CommandType = CommandType.Text;
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt;
        }

        }
        
         

    }

