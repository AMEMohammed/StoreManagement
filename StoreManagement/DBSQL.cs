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

      public  DataTable  GetAllCategoryAR()
        {
            string qury = "SELECT [IDCategory] as 'رقم الصنف',[NameCategory] as 'اسم الصنف' FROM  [StoreManagement].[dbo].[Category]";
            return ExecuteQurySelect(qury);

        }
        public DataTable GetAllCategory()
        {
            string qury = "SELECT [IDCategory] ,[NameCategory] FROM  [StoreManagement].[dbo].[Category]";
            return ExecuteQurySelect(qury);

        }

        public int GetMaxSupplyid()
        {
            int r = 0;
            con.Open();
            cmd = new SqlCommand("select max(IDSupply) from RequstSupply ", con);
            r = (int)cmd.ExecuteScalar();
            con.Close();
            return r;

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
            int resl = 0;
            cmd = new SqlCommand("UPDATE [StoreManagement].[dbo].[Category] SET [NameCategory]=@name1  WHERE  IDCategory=@id1", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@name1", name);
            cmd.Parameters.AddWithValue("@id1", id);
            con.Open();
            resl = cmd.ExecuteNonQuery();
            con.Close();
            return resl;
           

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
        public int AddNewPlaceSend(string name)
        {
            string qury = "INSERT INTO [StoreManagement].[dbo].[PlaceSend] ([NamePlace]) VALUES  (@name)";
            return ExecuteQury(qury, 0, name, 1);

        }


        //
        // update Place send
        public int UpdatePlaceSend(int id, string name)
        {
            string qury = "UPDATE [StoreManagement].[dbo].[PlaceSend] SET [NamePlace] =@name  WHERE IDPlace =@id";
            return ExecuteQury(qury, id, name, 2);
        }

        ////
        // delete PalceSend
        public int DeletePlaceSend(int id)
        {
            string qury = "delete from PlaceSend where IDPlace=@id";
            return ExecuteQury(qury, id,null, 3);

        }
         

        ////////////////////////////////////////////////////////
        //////////
        ///
         /// CHACKE is account is here ro not
          public int CheckAccountIsHere(int IDCategory,int IDType,int price)
        {
            int reslt = 0;
            con.Open();
            try
            {
                cmd = new SqlCommand("select IDAccount from Account where IDCategory=@IDCategory and IDType=@IDType and Price=@Price ", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@IDCategory", IDCategory);
                cmd.Parameters.AddWithValue("@IDType", IDType);
                cmd.Parameters.AddWithValue("@Price", price);
              
                reslt = (int)cmd.ExecuteScalar();
                con.Close();

            }
            catch
            { reslt = 0; }
            finally
            {
                con.Close();
               
            }
            return reslt;
        }
        //////
        //////
        //// Check Qunnty is Here InCheckQuntity
        public int CheckQuntityISHereInCheckQuntity(int IDCategory,int IDType)
        {
            int reslt = 0;
            con.Open();
            try
            {
                cmd = new SqlCommand("select IDCheck from CheckQuntity where IDCategory=@IDCategory and IDType=@IDType", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@IDCategory", IDCategory);
                cmd.Parameters.AddWithValue("@IDType", IDType);


                reslt = (int)cmd.ExecuteScalar();


            }
            catch
            {
                reslt = 0;

            }
            finally
            {
               
                con.Close();
                
            }
            return reslt;
        }
        
        ///
        /// get currentQuntity in Account
        /// 
        public int GetQuntityInAccount(int IDAcount)
        {
            int reslt = 0;
            cmd = new SqlCommand("select Quntity from Account where IDAccount=@IDAccount", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@IDAccount", IDAcount);
            con.Open();
            reslt = (int)cmd.ExecuteScalar();
            con.Close();
            return reslt;

        }
        ////
        // get currnt quntity chack quntity
        ///
        public int GetQuntityInChackQuntity(int IDChack)
        {
            int resl = 0;
            cmd = new SqlCommand("select CurrntQuntity from CheckQuntity where IDCheck=@IDCheck", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@IDCheck", IDChack);
            con.Open();
            resl = (int)cmd.ExecuteScalar();
            con.Close();
            return resl;


        }
        
        ////////
        ////////
        /// Update quntity account
        /// 
        public int UpdateQuntityAccount(int IDAccount ,int newquntity)
        {
            int resl = 0;
            cmd = new SqlCommand("UPDATE [Account]   SET [Quntity] = @newquntity WHERE IDAccount =@IDAccount", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@IDAccount", IDAccount);
            cmd.Parameters.AddWithValue("@newquntity", newquntity);
            con.Open();
            resl = cmd.ExecuteNonQuery();
            con.Close();
            return resl;
        }

        ////////////
        ///////
        /// update quntity chackquntity
        /// 
        public int UpadateQintityInchekQuntity(int IDchack,int CurrntQuntity)
        {
            int resl = 0;
            cmd = new SqlCommand("UPDATE CheckQuntity   SET CurrntQuntity=@CurrntQuntity WHERE IDCheck=@IDCheck", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@IDCheck", IDchack);
            cmd.Parameters.AddWithValue("@CurrntQuntity", CurrntQuntity);
            con.Open();
            resl = cmd.ExecuteNonQuery();
            con.Close();
            return resl;
        }

        //////
        /// add new account
        /// 
        public int AddNewAccount(int IDCategory,int IDType,int Quntity,int Price)
        {
            int reslt = 0;
            cmd = new SqlCommand("insert into Account (IDCategory,IDType,Quntity,Price) values(@IDCategory,@IDType,@Quntity,@Price)", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@IDCategory",IDCategory);
            cmd.Parameters.AddWithValue("@IDType",IDType);
            cmd.Parameters.AddWithValue("@Quntity",Quntity);
            cmd.Parameters.AddWithValue("@Price",Price);
            con.Open();
            reslt = cmd.ExecuteNonQuery();
            con.Close();
            return reslt;
        }
        
        //////
        /// add new CheckQuntity
        ///
        public int AddNewCheckQuntity(int IDCategory,int IDType,int LessQuntity,int Quntity)
        {
            int resl = 0;
            cmd = new SqlCommand("insert into CheckQuntity (IDCategory,IDType,LessQuntity,CurrntQuntity) values (@IDCategory,@IDType,@LessQuntity,@CurrntQuntity)", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@IDCategory", IDCategory);
            cmd.Parameters.AddWithValue("@IDType", IDType);
            cmd.Parameters.AddWithValue("@LessQuntity", LessQuntity);
            cmd.Parameters.AddWithValue("@CurrntQuntity", Quntity);
            con.Open();
            resl = cmd.ExecuteNonQuery();
            con.Close();
            return resl;
        }

        //////////////////////////////////
        /// add new Requst Supply
        /// 
        public int AddNewRequsetSupply(int IDCategory,int IDType,int Quntity,int Price,string NameSupply,string DescSupply,DateTime DateSupply)
        {
            int resl = 0;
            cmd = new SqlCommand("insert into RequstSupply(IDCategory,IDType,Quntity,Price,NameSupply,DescSupply,DateSupply) values(@IDCategory,@IDType,@Quntity,@Price,@NameSupply,@DescSupply,@DateSupply)", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@IDCategory", IDCategory);
            cmd.Parameters.AddWithValue("@IDType", IDType);
            cmd.Parameters.AddWithValue("@Quntity", Quntity);
            cmd.Parameters.AddWithValue("@Price", Price);
            cmd.Parameters.AddWithValue("@NameSupply", NameSupply);
            cmd.Parameters.AddWithValue("@DescSupply", DescSupply);
            cmd.Parameters.AddWithValue("@DateSupply", DateSupply);
            con.Open();
            resl = cmd.ExecuteNonQuery();
            con.Close();
            return resl;
        }
      
        public DataTable RPTRqustSupply(int IDSupply)
        {
            DataTable dt = new DataTable();
       
            cmd = new SqlCommand("SELECT  Category.NameCategory as'اسم الصنف' ,TypeQuntity.NameType as 'نوع الصنف',RequstSupply.DateSupply as 'تاريخ التوريد', RequstSupply.DescSupply as 'وصف التوريد',RequstSupply.NameSupply as 'اسم المورد', RequstSupply.Price as 'السعر', RequstSupply.Quntity as 'الكمية الموردة' from Category, TypeQuntity, RequstSupply where RequstSupply.IDCategory = Category.IDCategory and RequstSupply.IDType = TypeQuntity.IDType and RequstSupply.IDSupply = @idSupply", con);
            cmd.Parameters.AddWithValue("@idSupply", IDSupply);
            cmd.CommandType = CommandType.Text;
            //  DataSet1 ds = new DataSet1();
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt;
        }




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
           else if(flag==3)//delete
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

