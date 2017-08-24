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
      
        public SqlDataAdapter adapter;
        public DBSQL()
        {
            con = new SqlConnection(ConnectionSreing);
        }

        ///
        /// function Category
        /// 
        // get all category

        public DataTable GetAllCategoryAR()
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
            cmd = new SqlCommand("select IDCategory as 'رقم الصنف',  NameCategory as 'اسم الصنف' from Category where NameCategory like @NameCategory", con);
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
            return ExecuteQury(qury, 0, NameCategory, 1);

        }

        ///
        // Update Category 
        public int UpdateCategory(int id, string name)
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
        public int AddNewTypeQuntity(string name)
        {
            string qury = "insert into TypeQuntity (NameType) values (@name)";
            return ExecuteQury(qury, 0, name, 1);

        }

        ///////////
        /// Updte TypeQuntit
        /// 
        public int UpdateTypeQuntity(int id, string name)
        {
            string qury = "UPDATE [StoreManagement].[dbo].[TypeQuntity]  SET [NameType] = @name WHERE IDType=@id";
            return ExecuteQury(qury, id, name, 2);

        }

        ///
        // delete TypeQuntity
        public int DeleteQuntity(int id)
        {
            string qury = "DELETE FROM [StoreManagement].[dbo].[TypeQuntity]WHERE IDType = @id";
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
            return ExecuteQury(qury, id, null, 3);

        }


        ////////////////////////////////////////////////////////
        //////////
        ///
        /// CHACKE is account is here ro not
        public int CheckAccountIsHere(int IDCategory, int IDType, int price,int idcurrnt)
        {
            int reslt = 0;
            con.Open();
            try
            {
                cmd = new SqlCommand("select IDAccount from Account where IDCategory=@IDCategory and IDType=@IDType and Price=@Price  and IDCurrency=@IDCurrency", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@IDCategory", IDCategory);
                cmd.Parameters.AddWithValue("@IDType", IDType);
                cmd.Parameters.AddWithValue("@Price", price);
                cmd.Parameters.AddWithValue("@IDCurrency", idcurrnt);

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
        public int CheckQuntityISHereInCheckQuntity(int IDCategory, int IDType)
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
        public int UpdateQuntityAccount(int IDAccount, int newquntity)
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
        public int UpadateQintityInchekQuntity(int IDchack, int CurrntQuntity)
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
        public int AddNewAccount(int IDCategory, int IDType, int Quntity, int Price,int idcurrnt)
        {
            int reslt = 0;
            cmd = new SqlCommand("insert into Account (IDCategory,IDType,Quntity,Price,IDCurrency) values(@IDCategory,@IDType,@Quntity,@Price,@IDCurrency)", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@IDCategory", IDCategory);
            cmd.Parameters.AddWithValue("@IDType", IDType);
            cmd.Parameters.AddWithValue("@Quntity", Quntity);
            cmd.Parameters.AddWithValue("@Price", Price);
            cmd.Parameters.AddWithValue("@IDCurrency", idcurrnt);
            con.Open();
            reslt = cmd.ExecuteNonQuery();
            con.Close();
            return reslt;
        }

        //////
        /// add new CheckQuntity
        ///
        public int AddNewCheckQuntity(int IDCategory, int IDType, int LessQuntity, int Quntity)
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
        public int AddNewRequsetSupply(int IDCategory, int IDType, int Quntity, int Price,int idcurrnt, string NameSupply, string DescSupply, DateTime DateSupply)
        {
            int resl = 0;
            cmd = new SqlCommand("insert into RequstSupply(IDCategory,IDType,Quntity,Price,NameSupply,DescSupply,DateSupply,IDCurrency) values(@IDCategory,@IDType,@Quntity,@Price,@NameSupply,@DescSupply,@DateSupply,@IDCurrency)", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@IDCategory", IDCategory);
            cmd.Parameters.AddWithValue("@IDType", IDType);
            cmd.Parameters.AddWithValue("@Quntity", Quntity);
            cmd.Parameters.AddWithValue("@Price", Price);
            cmd.Parameters.AddWithValue("@NameSupply", NameSupply);
            cmd.Parameters.AddWithValue("@DescSupply", DescSupply);
            cmd.Parameters.AddWithValue("@DateSupply", DateSupply);
            cmd.Parameters.AddWithValue("@IDCurrency", idcurrnt);
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
        private int ExecuteQury(string qury, int id, string name, int flag)
        {
            int res = 0;
            cmd = new SqlCommand(qury, con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            if (flag == 1) //insert
            {
                cmd.Parameters.AddWithValue("@name", name);

            }
            if (flag == 2) //update
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@name", name);

            }
            else if (flag == 3)//delete
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






        //////////////////////////////
        ///////////
        /// requst out
        /// 
        public DataTable GetCatagoryInAccount()
        {
            DataTable dt = new DataTable();
            cmd = new SqlCommand("select DISTINCT Category.IDCategory  as 'رقم الصنف',Category.NameCategory as 'اسم الصنف' from Category,Account where Account.IDCategory =Category.IDCategory and Account.Quntity>0", con);
            cmd.CommandType = CommandType.Text;
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt;
        }
        ////////////////////////////////////
        ////////// get
        public DataTable GetTypeInAccount(int IdCate)
        {
            DataTable dt = new DataTable();
            cmd = new SqlCommand("select DISTINCT TypeQuntity.IDType as 'رقم النوع' ,TypeQuntity.NameType as 'اسم النوع' from TypeQuntity,Account where Account.IDType = TypeQuntity.IDType and Account.IDCategory =@IDCategory and Account.Quntity>0", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@IDCategory", IdCate);
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt;

        }
        /////////////////////////////////////
        ////////// get quntity in Account
        public int GetQunitiyinAccount2(int Idcae, int IdType ,int idcurrnt)
        {
            int res = 0;
            con.Open();
            try
            {
                cmd = new SqlCommand("select SUM(Account.Quntity) from Account  where Account.IDCategory = @IDCategory and Account.IDType = @IDType and Account.IDCurrency=@IDCurrency", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@IDCategory", Idcae);
                cmd.Parameters.AddWithValue("@IDType", IdType);
                cmd.Parameters.AddWithValue("@IDCurrency", idcurrnt);
                res = (int)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();

            }
            return res;


        }

        ///////////////////////////
        /////////GetAccountIDs
        public DataTable GetAccountIDs(int IdCAte, int IdTpe,int idcurrnt)
        {
            DataTable dt = new DataTable();
            try
            {

                cmd = new SqlCommand("select Account.IDAccount from Account where Account.IDCategory = @IDCategory and Account.IDType = @IDType and Account.IDCurrency=@IDCurrency and Account.Quntity>0", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@IDCategory", IdCAte);
                cmd.Parameters.AddWithValue("@IDType", IdTpe);
                cmd.Parameters.AddWithValue("@IDCurrency", idcurrnt);
                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }

            return dt;
        }
        ////////////////////////////////////////////////////////////
        //////////////////
        /////////////////  التكد من ان الحساب يغطي الطلب وارجاع صفر في حالة تم الطلب او ارجاع الكمية المتبقة المطلوبه
        public int GetAndCheckQuntityAccountAndAddRqustNew(int IDAccount, int QuntityMust, int IDCategory, int IDType, int idcurrn,int IDPlace, string NameOut, string DesOut, DateTime DateOut, int Chack, string NameSend)
        {
            int r = -1;
            int QuntityOld = GetQuntityInAccount(IDAccount);
            int Price = GetPriceAccount(IDAccount);// دالة جلب السعر

            if (QuntityOld >= QuntityMust)
            {
                int newQuntity = QuntityOld - QuntityMust;
                UpdateQuntityAccount(IDAccount, newQuntity);/// تعديل الحساب بالكمية الجديدة

                AddNewRequstOut(QuntityMust, IDCategory, IDType, idcurrn,IDPlace, NameOut, DesOut, DateOut, Chack, NameSend,Price);// اضافة طلب جديد
              
                r = 0;
            }

            else
            { 


                int newQun = QuntityMust - QuntityOld;
                UpdateQuntityAccount(IDAccount, 0);
                AddNewRequstOut(QuntityOld, IDCategory, IDType,idcurrn, IDPlace, NameOut, DesOut, DateOut, Chack, NameSend,Price);// اضافة طلب جديد
            
                r = QuntityOld;
                
            }
          
            return r;

        }

        private int GetPriceAccount(int iDAccount)
        {
            int re = 0;
            
            con.Open();
            cmd = new SqlCommand("select Price from Account where IDAccount=@IDAccount", con);
            cmd.Parameters.AddWithValue("@IDAccount", iDAccount);
            cmd.CommandType = CommandType.Text;
            re =(int) cmd.ExecuteScalar();
            con.Close();
            return re;

        }


        //////////////
        /////// Add New requstOut
        public int AddNewRequstOut(int Quntity, int IDCategory, int IDType, int idcurrnt,int IDPlace, string NameOut, string DesOut, DateTime DateOut, int Chack, string NameSend,int price)
        {
            int res = 0;
            con.Open();
            try
            {
                cmd = new SqlCommand("insert into RequstOut (Chack,DateOut,DesOut,IDCategory,IDPlace,IDType,NameOut,NameSend,Quntity,Price,IDCurrency) values(@Chack,@DateOut,@DesOut,@IDCategory,@IDPlace,@IDType,@NameOut,@NameSend,@Quntity,@Price,@IDCurrency)", con);
                cmd.Parameters.AddWithValue("@Chack", Chack);
                cmd.Parameters.AddWithValue("@DateOut", DateOut);
                cmd.Parameters.AddWithValue("@DesOut", DesOut);
                cmd.Parameters.AddWithValue("@IDCategory", IDCategory);
                cmd.Parameters.AddWithValue("@IDPlace", IDPlace);
                cmd.Parameters.AddWithValue("@IDType", IDType);
                cmd.Parameters.AddWithValue("@NameOut", NameOut);
                cmd.Parameters.AddWithValue("@NameSend", NameSend);
                cmd.Parameters.AddWithValue("@Quntity", Quntity);
                cmd.Parameters.AddWithValue("@Price", price);
                cmd.Parameters.AddWithValue("@IDCurrency", idcurrnt);
                res = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            con.Close();
            return res;


        }
        /////////////////////
        ///////// get max check in requstOut
        public int GetMaxCheckInRequsetOut()
        {
            int r = 0;
            con.Open();
            try
            {
                cmd = new SqlCommand("select max(Chack) from RequstOut", con);
                r = (int)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                r = 0;
            }
            finally
            {
                con.Close();
            }
            return r;

        }
        ////////////////
        //////// print requst out
        public DataTable PrintRequstOut(int Check)
        {
            DataTable dt = new DataTable();

            cmd = new SqlCommand("select  Category.NameCategory ,TypeQuntity.NameType,PlaceSend.NamePlace,RequstOut.DateOut,RequstOut.DesOut,RequstOut.NameOut,RequstOut.NameSend ,RequstOut.Price,RequstOut.Quntity from RequstOut,Category,TypeQuntity,PlaceSend where RequstOut.IDCategory = Category.IDCategory and RequstOut.IDType = TypeQuntity.IDType and RequstOut.IDPlace = PlaceSend.IDPlace and RequstOut.Chack =@check", con);
            cmd.Parameters.AddWithValue("@check", Check);
            cmd.CommandType = CommandType.Text;
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt;
        }









        //////////////////////////////
        //////////////// search in RequsetSupply
        public DataTable SearchINRequsetSupply(string txt)
        { //
            DataTable dt = new DataTable();
                txt = "%" + txt + "%";
            cmd = new SqlCommand("select IDSupply as 'رقم الطلب' ,Category.NameCategory as 'اسم الصنف', TypeQuntity.NameType as 'نوع الكمية', RequstSupply.Quntity as 'الكمية', RequstSupply.Price as 'سعر الوحدة', RequstSupply.Quntity * RequstSupply.Price as 'الاجمالي',Currency.NameCurrency as 'العملة',RequstSupply.DateSupply as'تاريخ التوريد', RequstSupply.NameSupply as 'اسم المورد', RequstSupply.DescSupply as 'ملاحظات' from Category, TypeQuntity, RequstSupply,Currency where RequstSupply.IDCategory = Category.IDCategory and RequstSupply.IDCurrency=Currency.IDCurrency and RequstSupply.IDType = TypeQuntity.IDType and convert(varchar,IDSupply)+convert(varchar,Quntity)+convert(varchar,Quntity)+convert(varchar,Price)+Category.NameCategory+Currency.NameCurrency+ TypeQuntity.NameType + RequstSupply.NameSupply + RequstSupply.DescSupply  like @txt order by RequstSupply.DateSupply,RequstSupply.IDCurrency,RequstSupply.IDType,RequstSupply.IDCategory ", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@txt", txt);
             adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt;
        }

        //////////////////////////////
        //////////////// search in RequsetSupply ageo Week
        public DataTable SearchINRequsetSupplyDate( DateTime d1,DateTime d2)
        { 
            DataTable dt = new DataTable();
            

            cmd = new SqlCommand("select IDSupply as 'رقم الطلب' ,Category.NameCategory as 'اسم الصنف', TypeQuntity.NameType as 'نوع الكمية', RequstSupply.Quntity as 'الكمية', RequstSupply.Price as 'سعر الوحدة', RequstSupply.Quntity * RequstSupply.Price as 'الاجمالي',Currency.NameCurrency as 'العملة',RequstSupply.DateSupply as'تاريخ التوريد', RequstSupply.NameSupply as 'اسم المورد', RequstSupply.DescSupply as 'ملاحظات'  from Category, TypeQuntity, RequstSupply,Currency where RequstSupply.IDCategory = Category.IDCategory and RequstSupply.IDCurrency=Currency.IDCurrency and RequstSupply.IDType = TypeQuntity.IDType and DateSupply between @d1 and @d2  order by RequstSupply.DateSupply,RequstSupply.IDCurrency,RequstSupply.IDType,RequstSupply.IDCategory ", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@d1", d1);
            cmd.Parameters.AddWithValue("@d2", d2);
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt;
        }
        //////////////////////////////
        //////////////// search in RequsetSupplythwTXT and Date
        public DataTable SearchINRequsetSupplyTxtAndDate(string txt ,DateTime d1,DateTime d2)
        { //
            DataTable dt = new DataTable();
            txt = "%" + txt + "%";
            cmd = new SqlCommand("select IDSupply as 'رقم الطلب' ,Category.NameCategory as 'اسم الصنف', TypeQuntity.NameType as 'نوع الكمية', RequstSupply.Quntity as 'الكمية', RequstSupply.Price as 'سعر الوحدة', RequstSupply.Quntity * RequstSupply.Price as 'الاجمالي',Currency.NameCurrency as 'العملة',RequstSupply.DateSupply as'تاريخ التوريد', RequstSupply.NameSupply as 'اسم المورد', RequstSupply.DescSupply as 'ملاحظات' from Category, TypeQuntity, RequstSupply,Currency where RequstSupply.IDCategory = Category.IDCategory and RequstSupply.IDCurrency=Currency.IDCurrency and  RequstSupply.IDType = TypeQuntity.IDType and convert(varchar,IDSupply)+convert(varchar,Quntity)+convert(varchar,Quntity)+convert(varchar,Price)+ Category.NameCategory + TypeQuntity.NameType + RequstSupply.NameSupply + RequstSupply.DescSupply+Currency.NameCurrency  like @txt and DateSupply between @d1 and @d2 order by RequstSupply.DateSupply,RequstSupply.IDCurrency,RequstSupply.IDType,RequstSupply.IDCategory ", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@txt", txt);
            cmd.Parameters.AddWithValue("@d1", d1);
            cmd.Parameters.AddWithValue("@d2", d2);
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt;
        }









        /////////////////////
        ////////////////////////////////////////////
        /////// GetRequstSupply
        public DataTable PrintRequstSupply(int IDreqSup)
        {
            DataTable dt = new DataTable();
            cmd = new SqlCommand("select IDSupply ,  Category.NameCategory , TypeQuntity.NameType , RequstSupply.Quntity , RequstSupply.Price  , RequstSupply.Quntity * RequstSupply.Price,Category.NameCategory  , RequstSupply.DateSupply , RequstSupply.NameSupply , RequstSupply.DescSupply  from Category, TypeQuntity, RequstSupply,Currency where RequstSupply.IDCategory = Category.IDCategory and RequstSupply.IDType = TypeQuntity.IDType and RequstSupply.IDCurrency=Currency.IDCurrency  and RequstSupply.IDSupply =@id", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@id",IDreqSup);
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt;

        }
        ////////////////////////////////////////////
        /////// GetRequstSupply
        public DataTable GetRequstSupply(int IDreqSup)
        {
            DataTable dt = new DataTable();
            cmd = new SqlCommand("select IDSupply,IDCategory,IDType,Quntity,Price,IDCurrency,DateSupply,NameSupply,DescSupply  from  RequstSupply where IDSupply=@id ", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@id", IDreqSup);
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt;

        }
        ////////////////////////////////////////////////////////////
        //////////// update Requst Supply
        //////////////////////////////////
 
        /// 
        public int UPateRequstSupply(int IDSup, int IDCategory, int IDType, int Quntity, int Price,int idcurrn, string NameSupply, string DescSupply)
        {
            int resl = 0;
            cmd = new SqlCommand("Update RequstSupply set IDCategory=@IDCategory,IDType=@IDType,Quntity=@Quntity,Price=@Price,NameSupply=@NameSupply,DescSupply=@DescSupply,IDCurrency=@idcurrn where IDSupply=@IDSupply", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@IDSupply", IDSup);
            cmd.Parameters.AddWithValue("@IDCategory", IDCategory);
            cmd.Parameters.AddWithValue("@IDType", IDType);
            cmd.Parameters.AddWithValue("@Quntity", Quntity);
            cmd.Parameters.AddWithValue("@Price", Price);
            cmd.Parameters.AddWithValue("@NameSupply", NameSupply);
            cmd.Parameters.AddWithValue("@DescSupply", DescSupply);
            cmd.Parameters.AddWithValue("@idcurrn", idcurrn);
            con.Open();
            resl = cmd.ExecuteNonQuery();
            con.Close();
            return resl;
        }

        ////////////////////////////////////////////////////////////
        //////////// Add New in UPD supply
        //////////////////////////////////

        /// 
        public int ADDNewUPDSupply(int IDSup, int IDCategory, int IDType, int Quntity, int Price,int idcunnt, string NameSupply, string DescSupply, DateTime dateAdd, DateTime dateUpd, string decNew)
        {
            int resl = 0;
            cmd = new SqlCommand("INSERT INTO [StoreManagement].[dbo].[UpdSupply]([IDSupply] ,[IDCategory],[IDType],[Quntity],[Price],[IDCurrency],[NameSupply],[DescSupply],[DateSupply],[DescUpd] ,[dateUpd]) VALUES (@IDSupply,@IDCategory,@IDType,@Quntity,@Price,@IDCurrency,@NameSupply,@DescSupply,@deteAdd,@descup,@dateup)", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@IDSupply", IDSup);
            cmd.Parameters.AddWithValue("@IDCategory", IDCategory);
            cmd.Parameters.AddWithValue("@IDType", IDType);
            cmd.Parameters.AddWithValue("@Quntity", Quntity);
            cmd.Parameters.AddWithValue("@Price", Price);
            cmd.Parameters.AddWithValue("@NameSupply", NameSupply);
            cmd.Parameters.AddWithValue("@DescSupply", DescSupply);
            cmd.Parameters.AddWithValue("@deteAdd", dateAdd);
            cmd.Parameters.AddWithValue("@descup", decNew);
            cmd.Parameters.AddWithValue("@dateup", dateUpd);
            cmd.Parameters.AddWithValue("@IDCurrency", idcunnt);
           

            con.Open();
            resl = cmd.ExecuteNonQuery();
            con.Close();
            return resl;
        }
        //////////////////////////////
        ///////////////////// search in RequstOut
        ///////////
        //////////////// 
        public DataTable SearchINRequstOutDate(DateTime d1, DateTime d2)
        {
            DataTable dt = new DataTable();


            cmd = new SqlCommand("select RequstOut.IDOut as 'رقم الطلب',Category.NameCategory as 'اسم الصنف' ,TypeQuntity.NameType as 'نوع الكمية',PlaceSend.NamePlace as'الجهة المستفيدة' ,RequstOut.Quntity as'الكمية',RequstOut.Price as 'سعر الوحدة',RequstOut.Quntity*RequstOut.Price as'الاجمالي', Currency.NameCurrency as 'العملة',RequstOut.NameOut as'يصرف بامر',RequstOut.NameSend as'باستلام',RequstOut.DateOut as'تاريخ الصرف' ,RequstOut.DesOut as 'ملاحظات',RequstOut.Chack from Category,TypeQuntity,PlaceSend,RequstOut,Currency where RequstOut.IDCategory=Category.IDCategory and RequstOut.IDType=TypeQuntity.IDType and RequstOut.IDPlace =PlaceSend.IDPlace and RequstOut.IDCurrency =Currency.IDCurrency  and DateOut between @d1 and @d2 order by RequstOut.IDCategory,RequstOut.IDPlace,RequstOut.IDType,RequstOut.IDCurrency,RequstOut.Chack,RequstOut.DateOut ", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@d1", d1);
            cmd.Parameters.AddWithValue("@d2", d2);
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt;


        }













        /////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////
        //////// جدول العملاتط
        // 
        public int AddNewCurrency(string name)
        {
            int res = 0;
            cmd = new SqlCommand("INSERT INTO [StoreManagement].[dbo].[Currency]  ([NameCurrency])  VALUES(@txt)", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@txt", name);
            con.Open();
         res=   cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }
        ////////////////////////////////
        //////// get AllCurrency
        public DataTable GetAllCurrency()
        {
            DataTable dt = new DataTable();
            cmd = new SqlCommand("select IDCurrency as'رقم العملة',NameCurrency as 'اسم العملة' from Currency ", con);
            cmd.CommandType = CommandType.Text;
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt;

        }
        /// <summary>
        /// ////////////////////
        /// </summary>
        /// <returns></returns>
        public DataTable GetCurrencyINAccount(int idcat,int idtyp)
        {

            DataTable dt = new DataTable();
            cmd = new SqlCommand("select DISTINCT Currency.IDCurrency as 'رقم العملة', Currency.NameCurrency as 'اسم العملة' from Currency,Account where Account.IDCurrency=Currency.IDCurrency and Account.IDCategory=@IDCategory and Account.IDType=@IDType and Account.Quntity>0", con);
            cmd.Parameters.AddWithValue("@IDCategory", idcat);
            cmd.Parameters.AddWithValue("@IDType", idtyp);
            cmd.CommandType = CommandType.Text;
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt;
        }

        /////////////////////////////////////////////////
        ///// Update Currency
        public int UpdateCurrency(int id, string name)
        {   int res = 0;
            cmd = new SqlCommand("update Currency set NameCurrency=@name where IDCurrency=@id ", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            res=cmd.ExecuteNonQuery();
            con.Close();
            return res;

        }
        ///////////////////////////////////
        ////// delete Currency
        public  int DeleteCurrency(int id)
        {
            int res = 0;
            cmd = new SqlCommand("delete from Currency where IDCurrency=@id", con);
            cmd.CommandType = CommandType.Text;
        
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            res = cmd.ExecuteNonQuery();
            con.Close();
            return res;

        }
    
        ///////////////////////////////////
        ////////
        public DataTable SearchINRequsetOuttxt(string s)
      {
            s = "%" + s + "%";
         DataTable dt = new DataTable();


        cmd = new SqlCommand("select RequstOut.IDOut as 'رقم الطلب',Category.NameCategory as 'اسم الصنف' ,TypeQuntity.NameType as 'نوع الكمية',PlaceSend.NamePlace as'الجهة المستفيدة' ,RequstOut.Quntity as'الكمية',RequstOut.Price as 'سعر الوحدة',RequstOut.Quntity*RequstOut.Price as'الاجمالي', Currency.NameCurrency as 'العملة',RequstOut.NameOut as'يصرف بامر',RequstOut.NameSend as'باستلام',RequstOut.DateOut as'تاريخ الصرف' ,RequstOut.DesOut as 'ملاحظات',RequstOut.Chack from Category,TypeQuntity,PlaceSend,RequstOut,Currency where RequstOut.IDCategory=Category.IDCategory and RequstOut.IDType=TypeQuntity.IDType and RequstOut.IDPlace =PlaceSend.IDPlace and RequstOut.IDCurrency =Currency.IDCurrency  and Category.NameCategory + TypeQuntity.NameType + PlaceSend.NamePlace +RequstOut.DesOut+RequstOut.NameOut+ RequstOut.NameSend+CONVERT(varchar,RequstOut.IDOut)+CONVERT(varchar,RequstOut.Price)+CONVERT(varchar,RequstOut.Quntity) like @txt  order by RequstOut.IDCategory,RequstOut.IDPlace,RequstOut.IDType,RequstOut.IDCurrency,RequstOut.Chack,RequstOut.DateOut", con);
        cmd.CommandType = CommandType.Text;
           
            cmd.Parameters.AddWithValue("@txt", s);
            adapter = new SqlDataAdapter(cmd);
          adapter.Fill(dt);
            return dt;
       
        }
        ///////////////////////////////////
        ////////// 
    public DataTable SearchINRequsetOutTxtAndDate(string s,DateTime d1,DateTime d2)
        {
            s = "%" + s + "%";
            DataTable dt = new DataTable();


            cmd = new SqlCommand("select RequstOut.IDOut as 'رقم الطلب',Category.NameCategory as 'اسم الصنف' ,TypeQuntity.NameType as 'نوع الكمية',PlaceSend.NamePlace as'الجهة المستفيدة' ,RequstOut.Quntity as'الكمية',RequstOut.Price as 'سعر الوحدة',RequstOut.Quntity*RequstOut.Price as'الاجمالي', Currency.NameCurrency as 'العملة',RequstOut.NameOut as'يصرف بامر',RequstOut.NameSend as'باستلام',RequstOut.DateOut as'تاريخ الصرف' ,RequstOut.DesOut as 'ملاحظات',RequstOut.Chack from Category,TypeQuntity,PlaceSend,RequstOut,Currency where RequstOut.IDCategory=Category.IDCategory and RequstOut.IDType=TypeQuntity.IDType and RequstOut.IDPlace =PlaceSend.IDPlace and RequstOut.IDCurrency =Currency.IDCurrency  and Category.NameCategory + TypeQuntity.NameType + PlaceSend.NamePlace +RequstOut.DesOut+RequstOut.NameOut+ RequstOut.NameSend+CONVERT(varchar,RequstOut.IDOut)+CONVERT(varchar,RequstOut.Price)+CONVERT(varchar,RequstOut.Quntity) like @txt and DateOut between d1 and d2 order by RequstOut.IDCategory,RequstOut.IDPlace,RequstOut.IDType,RequstOut.IDCurrency,RequstOut.Chack,RequstOut.DateOut ", con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@txt", s);
            cmd.Parameters.AddWithValue("@d1", d1);
            cmd.Parameters.AddWithValue("@d2", d2);

            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt;


        }






        ////////////////////////////////
        ////////////
        // get user
        public bool CheckUser(string User,string Pass)
        {
            bool check = false;
            try
            {
               
                DataTable dt = new DataTable();
                cmd = new SqlCommand("select * from Setting where UserName=@UserName and PassWord=@PassWord", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@UserName", User);
                cmd.Parameters.AddWithValue("@PassWord", Pass);
                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                    check = true;
                else
                    check = false;
            }
            catch(Exception ex)
            {
                check = false;
                MessageBox.Show(ex.Message);
            }
            return check;
        }
        //////////////
        //////////
        public void SaveImg(byte [] bimg)
        {
            cmd = new SqlCommand("Update Setting set img=@img where IDSetting=1", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@img" , bimg);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public DataTable GetImg()
        {
            DataTable dt = new DataTable();
            cmd = new SqlCommand("select img from Setting where IDSetting =1", con);

            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt;

        }


        //////////////////////////////
        //////////////// search in RequsetSupplythwTXT and Date
        public DataTable PrintRequstRPT(DateTime d1, DateTime d2,string IDCate,string IDType,string IDCurrn ,string txt)
        { //
            IDCate = "%" + IDCate;
            IDType = "%" + IDType;
            IDCurrn = "%" + IDCurrn;
            txt = "%" + txt + "%";
            DataTable dt = new DataTable();
            cmd = new SqlCommand("select IDSupply as 'رقم الطلب' ,Category.NameCategory as 'اسم الصنف', TypeQuntity.NameType as 'نوع الكمية', RequstSupply.Quntity as 'الكمية', RequstSupply.Price as 'سعر الوحدة', RequstSupply.Quntity * RequstSupply.Price as 'الاجمالي',Currency.NameCurrency as 'العملة',RequstSupply.DateSupply as'تاريخ التوريد', RequstSupply.NameSupply as 'اسم المورد', RequstSupply.DescSupply as 'ملاحظات' from Category, TypeQuntity, RequstSupply,Currency where RequstSupply.IDCategory = Category.IDCategory and RequstSupply.IDCurrency=Currency.IDCurrency and  RequstSupply.IDType = TypeQuntity.IDType and convert(varchar,RequstSupply.IDCurrency) like @IDCu and convert(varchar,RequstSupply.IDType) like @IDTy and convert(varchar,RequstSupply.IDCategory)like @IDCa and NameSupply like @txt and DateSupply between @d1 and @d2 order by RequstSupply.DateSupply,RequstSupply.IDCurrency,RequstSupply.IDType,RequstSupply.IDCategory", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@IDCa", IDCate);
            cmd.Parameters.AddWithValue("@IDTy", IDType);
            cmd.Parameters.AddWithValue("@IDCu", IDCurrn);
            cmd.Parameters.AddWithValue("@d1", d1);
            cmd.Parameters.AddWithValue("@d2", d2);
            cmd.Parameters.AddWithValue("@txt", txt);
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt;
        }

        //////////////////////////////
        //////////////// search in RequsetSupplythwTXT and Date
        public DataTable PrintRequstRPT( string IDCate, string IDType, string IDCurrn, string txt)
        { //
            IDCate = "%" + IDCate;
            IDType = "%" + IDType;
            IDCurrn = "%" + IDCurrn;
            txt = "%" + txt + "%";
            DataTable dt = new DataTable();
            cmd = new SqlCommand("select IDSupply as 'رقم الطلب' ,Category.NameCategory as 'اسم الصنف', TypeQuntity.NameType as 'نوع الكمية', RequstSupply.Quntity as 'الكمية', RequstSupply.Price as 'سعر الوحدة', RequstSupply.Quntity * RequstSupply.Price as 'الاجمالي',Currency.NameCurrency as 'العملة',RequstSupply.DateSupply as'تاريخ التوريد', RequstSupply.NameSupply as 'اسم المورد', RequstSupply.DescSupply as 'ملاحظات' from Category, TypeQuntity, RequstSupply,Currency where RequstSupply.IDCategory = Category.IDCategory and RequstSupply.IDCurrency=Currency.IDCurrency and  RequstSupply.IDType = TypeQuntity.IDType and convert(varchar,RequstSupply.IDCurrency) like @IDCu and convert(varchar,RequstSupply.IDType) like @IDTy and convert(varchar,RequstSupply.IDCategory)like @IDCa and NameSupply like @txt  order by RequstSupply.DateSupply,RequstSupply.IDCurrency,RequstSupply.IDType,RequstSupply.IDCategory ", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@IDCa", IDCate);
            cmd.Parameters.AddWithValue("@IDTy", IDType);
            cmd.Parameters.AddWithValue("@IDCu", IDCurrn);
            cmd.Parameters.AddWithValue("@txt", txt);
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt;
        }
        //////////////////////////////////
        /////////// print Account Quntity
        public DataTable PrintAccountQuntity(string idcat,string idtyp,string idcu)
        {
            idcat = "%" + idcat;
            idtyp = "%" + idtyp;
            idcu = "%" + idcu;
            DataTable dt = new DataTable();
            cmd = new SqlCommand("select Category.NameCategory as 'اسم الصنف' ,TypeQuntity.NameType as 'نوع الكمية' ,Account.Quntity as 'الكمية الحالية',Account.Price as 'السعر' ,Currency.NameCurrency as 'العملة' from Account,Category,TypeQuntity,Currency where Account.IDCategory=Category.IDCategory and Account.IDType =TypeQuntity.IDType and Account.IDCurrency=Currency.IDCurrency and Account.Quntity>0 and    convert(varchar,Account.IDCategory) like @idcat and convert(varchar,Account.IDType) like @idtyp and convert(varchar,Account.IDCurrency) like @idcur  order by Account.IDCategory , Account.IDCurrency,Account.IDType", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@idcat", idcat);
            cmd.Parameters.AddWithValue("@idtyp", idtyp);
            cmd.Parameters.AddWithValue("@idcur", idcu);
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt;

        }
        //////////////////////////////////
        ////////////////////



    }

}