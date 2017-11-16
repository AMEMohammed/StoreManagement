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
       private string ConnectionSreing = @"Data Source="+Properties.Settings.Default.nmserver+";Initial Catalog="+Properties.Settings.Default.nmdatabase+";User ID="+Properties.Settings.Default.UserSql+ ";Password="+Properties.Settings.Default.PassSql;
        private string ConnectionStriingMaster = @"Data Source=" + Properties.Settings.Default.nmserver + ";Initial Catalog=master;Integrated Security=true;";
   //     private string ConnectionSreing = @"Data Source=" + Properties.Settings.Default.nmserver + ";Initial Catalog=" + Properties.Settings.Default.nmdatabase + ";Integrated Security=true;";

        public SqlConnection con;
        public SqlCommand cmd;
      
        public SqlDataAdapter adapter;
        public DBSQL()
        {
            con = new SqlConnection(ConnectionSreing);
        }
        public DBSQL(int i)
        {
            con = new SqlConnection(ConnectionStriingMaster);
        }
        /// restor Buck up database
        public int RestorBackUpDataBase(string path)
        {
            int res = 0;
            cmd = new SqlCommand(" ALTER DATABASE StoreManagement set offline with rollback immediate; Restore Database StoreManagement from Disk =@d", con);
            cmd.Parameters.AddWithValue("@d", path);
            cmd.CommandType = CommandType.Text;
            con.Open();
            res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }
        //// buck up database
        public int BuckUpdatabase(string path)
        {

            int res = 0;
               
                cmd = new SqlCommand("BACKUP DATABASE StoreManagement TO DISK=@d", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@d", path);
                con.Open();
                res = cmd.ExecuteNonQuery();
          
          
          
          
                con.Close();
           
          
            return res;

        }

         //// 
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
        public int AddNewRequsetSupply(int IDCategory, int IDType, int Quntity, int Price,int idcurrnt, string NameSupply, string DescSupply, DateTime DateSupply,int IDuser,int chek,int debi,int cred)
        {
            int resl = 0;
            cmd = new SqlCommand("insert into RequstSupply(IDCategory,IDType,Quntity,Price,NameSupply,DescSupply,DateSupply,IDCurrency,UserId,chek,Debit,Creditor) values(@IDCategory,@IDType,@Quntity,@Price,@NameSupply,@DescSupply,@DateSupply,@IDCurrency,@userId,@chek,@deb,@crd)", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@IDCategory", IDCategory);
            cmd.Parameters.AddWithValue("@IDType", IDType);
            cmd.Parameters.AddWithValue("@Quntity", Quntity);
            cmd.Parameters.AddWithValue("@Price", Price);
            cmd.Parameters.AddWithValue("@NameSupply", NameSupply);
            cmd.Parameters.AddWithValue("@DescSupply", DescSupply);
            cmd.Parameters.AddWithValue("@DateSupply", DateSupply);
            cmd.Parameters.AddWithValue("@IDCurrency", idcurrnt);
            cmd.Parameters.AddWithValue("@userId", IDuser);
            cmd.Parameters.AddWithValue("@chek", chek);
            cmd.Parameters.AddWithValue("@deb", debi);
            cmd.Parameters.AddWithValue("@crd", cred);
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
        public int GetAndCheckQuntityAccountAndAddRqustNew(int IDAccount, int QuntityMust, int IDCategory, int IDType, int idcurrn,int IDPlace, string NameOut, string DesOut, DateTime DateOut, int Chack, string NameSend,int debit,int credi)
        {
            int r = -1;
            int QuntityOld = GetQuntityInAccount(IDAccount);
            int Price = GetPriceAccount(IDAccount);// دالة جلب السعر

            if (QuntityOld >= QuntityMust)
            {
                int newQuntity = QuntityOld - QuntityMust;
                UpdateQuntityAccount(IDAccount, newQuntity);/// تعديل الحساب بالكمية الجديدة
                  AddNewRequstOut(QuntityMust, IDCategory, IDType, idcurrn,IDPlace, NameOut, DesOut, DateOut, Chack, NameSend,Price,Contrl.UserId ,debit,credi);// اضافة طلب جديد
              
                r = 0;
            }

            else
            { 


                int newQun = QuntityMust - QuntityOld;
                UpdateQuntityAccount(IDAccount, 0);
                AddNewRequstOut(QuntityOld, IDCategory, IDType, idcurrn, IDPlace, NameOut, DesOut, DateOut, Chack, NameSend, Price, Contrl.UserId, debit, credi);// اضافة طلب جديد
            
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
        public int AddNewRequstOut(int Quntity, int IDCategory, int IDType, int idcurrnt,int IDPlace, string NameOut, string DesOut, DateTime DateOut, int Chack, string NameSend,int price ,int UserId,int debit,int cred)
        {
            int res = 0;
            con.Open();
            try
            {
                cmd = new SqlCommand("insert into RequstOut (Chack,DateOut,DesOut,IDCategory,IDPlace,IDType,NameOut,NameSend,Quntity,Price,IDCurrency,UserId,Debit,Creditor) values(@Chack,@DateOut,@DesOut,@IDCategory,@IDPlace,@IDType,@NameOut,@NameSend,@Quntity,@Price,@IDCurrency,@userId,@dib,@cred)", con);
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
                cmd.Parameters.AddWithValue("@userId", UserId);
                cmd.Parameters.AddWithValue("@dib", debit);
                cmd.Parameters.AddWithValue("@cred", cred);
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
            catch 
            {
               
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
        public DataTable PrintRequstOut(int Check,int UserId,int user)
        {
            DataTable dt = new DataTable();
            
            cmd = new SqlCommand("select RequstOut.IDOut as  'رقم الطلب',Category.NameCategory as 'اسم الصنف',TypeQuntity.NameType as 'نوع الكمية',PlaceSend.NamePlace as'الجهة المستفيدة' ,RequstOut.Quntity as'الكمية',RequstOut.Price as 'سعر الوحدة',RequstOut.Quntity*RequstOut.Price as'الاجمالي', Currency.NameCurrency as 'العملة',RequstOut.NameOut as'يصرف بامر',RequstOut.NameSend as'باستلام',RequstOut.DateOut as'تاريخ الصرف',Users.Name as 'اسم الموظف',RequstOut.DesOut as 'الملاحظات' ,Debit.NameTypeAccount as 'مدين' ,Creditor.NameTypeAccount as 'دائن'  from Users,RequstOut,Category,TypeQuntity,PlaceSend,Currency,Debit,Creditor where RequstOut.IDCategory = Category.IDCategory and Users.UserID=@idUser and RequstOut.IDType = TypeQuntity.IDType and RequstOut.IDCurrency=Currency.IDCurrency and Debit.IdTypeAccount=RequstOut.Debit and Creditor.IdTypeAccount=RequstOut.Creditor  and RequstOut.IDPlace = PlaceSend.IDPlace and RequstOut.Chack=@check and RequstOut.UserId=@uuu ", con);
            cmd.Parameters.AddWithValue("@check", Check);
            cmd.Parameters.AddWithValue("@idUser", UserId);
            cmd.Parameters.AddWithValue("@uuu", user);
            cmd.CommandType = CommandType.Text;
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);

           
            return dt;
        }
       /// <summary>
       /// ////// print exit Rerquset
       /// </summary>
       /// <param name="Check"></param>
       /// <param name="UserId"></param>
       /// <param name="user"></param>
       /// <returns></returns>
        public DataTable printrequstOutExit(int Check, int UserId, int user)
        {
            DataTable dt = new DataTable();

            cmd = new SqlCommand("select RequstOut.IDOut as  'الرقم المخزني',Category.NameCategory as 'الاسم',TypeQuntity.NameType as 'النوع',RequstOut.Quntity as'الكمية',RequstOut.NameSend as'اسم المستلم',Users.Name as 'اسم الموظف' ,Users.Name as 'العنوان' ,PlaceSend.NamePlace as 'الجهة' from Users,RequstOut,Category,TypeQuntity,PlaceSend,Currency,Debit,Creditor where RequstOut.IDCategory = Category.IDCategory and Users.UserID=@idUser and RequstOut.IDType = TypeQuntity.IDType and RequstOut.IDCurrency=Currency.IDCurrency and Debit.IdTypeAccount=RequstOut.Debit and Creditor.IdTypeAccount=RequstOut.Creditor  and RequstOut.IDPlace = PlaceSend.IDPlace and RequstOut.Chack=@check and RequstOut.UserId=@uuu ", con);
            cmd.Parameters.AddWithValue("@check", Check);
            cmd.Parameters.AddWithValue("@idUser", UserId);
            cmd.Parameters.AddWithValue("@uuu", user);
            cmd.CommandType = CommandType.Text;
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt;
        }
/// <summary>
///
/// </summary>
/// <param name="Check"></param>
/// <param name="UserId"></param>
/// <param name="user"></param>
/// <returns></returns>
        public DataTable printrequstOutExit1(int IDreqSup, int UserId, int user)
        {
            DataTable dt = new DataTable();
            cmd = new SqlCommand("select IDSupply as 'الرقم المخزني' ,  Category.NameCategory  as 'الاسم', TypeQuntity.NameType  as'النوع' , RequstSupply.Quntity  as 'الكمية',    RequstSupply.NameSupply  as'اسم المستلم',Users.Name as 'اسم الموظف' , Users.Name as 'العنوان' ,Users.Name as 'الجهة'  from Debit,Creditor ,Category,Users,TypeQuntity, RequstSupply,Currency where RequstSupply.IDCategory = Category.IDCategory and Debit.IdTypeAccount=RequstSupply.Debit and Creditor.IdTypeAccount=RequstSupply.Creditor and RequstSupply.IDType = TypeQuntity.IDType and RequstSupply.IDCurrency=Currency.IDCurrency and Users.UserID=@UserId  and RequstSupply.chek =@id and RequstSupply.UserId =@uuu ", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@id", IDreqSup);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@uuu", user);
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt;
        }
        /// <summary>
        /// / جلب الاجمالي حق الطلبات المحددة
        /// </summary>
        /// <param name="check"></param>
        /// <returns></returns>
        public int GetPraceInRequstOut(int check)
        {
            int r = 0;
            cmd = new SqlCommand("select  SUM((RequstOut.Price*RequstOut.Quntity)) as totle  from RequstOut where RequstOut.Chack=@check", con);
            cmd.Parameters.AddWithValue("@check", check);
            cmd.CommandType = CommandType.Text;
            con.Open();
            r = (int)cmd.ExecuteScalar();
           
            con.Close();
            return r ;
        }
       






        //////////////////////////////
        //////////////// search in RequsetSupply
        public DataTable SearchINRequsetSupply(string txt)
        { //
            DataTable dt = new DataTable();
                txt = "%" + txt + "%";
            cmd = new SqlCommand("select IDSupply as 'رقم الطلب' ,Category.NameCategory as 'اسم الصنف', TypeQuntity.NameType as 'نوع الكمية', RequstSupply.Quntity as 'الكمية', RequstSupply.Price as 'سعر الوحدة', RequstSupply.Quntity * RequstSupply.Price as 'الاجمالي',Currency.NameCurrency as 'العملة',RequstSupply.DateSupply as'تاريخ التوريد', RequstSupply.NameSupply as 'اسم المورد',Users.Name as 'اسم الموظف',RequstSupply.DescSupply as 'ملاحظات' ,RequstSupply.chek  from Users, Category, TypeQuntity, RequstSupply,Currency where RequstSupply.UserId=Users.UserID and RequstSupply.IDCategory = Category.IDCategory and RequstSupply.IDCurrency=Currency.IDCurrency and RequstSupply.IDType = TypeQuntity.IDType and convert(varchar,IDSupply)+convert(varchar,Quntity)+convert(varchar,Quntity)+convert(varchar,Price)+Category.NameCategory+Currency.NameCurrency+ TypeQuntity.NameType + RequstSupply.NameSupply + RequstSupply.DescSupply +Users.Name  like @txt order by RequstSupply.DateSupply,RequstSupply.IDCurrency,RequstSupply.IDType,RequstSupply.IDCategory ", con);
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
            

            cmd = new SqlCommand("select IDSupply as 'رقم الطلب' ,Category.NameCategory as 'اسم الصنف', TypeQuntity.NameType as 'نوع الكمية', RequstSupply.Quntity as 'الكمية', RequstSupply.Price as 'سعر الوحدة', RequstSupply.Quntity * RequstSupply.Price as 'الاجمالي',Currency.NameCurrency as 'العملة',RequstSupply.DateSupply as'تاريخ التوريد', RequstSupply.NameSupply as 'اسم المورد',Users.Name as 'اسم الموظف', RequstSupply.DescSupply as 'ملاحظات',RequstSupply.chek  from Category, TypeQuntity, RequstSupply,Currency, Users where RequstSupply.UserId=Users.UserID and  RequstSupply.IDCategory = Category.IDCategory and RequstSupply.IDCurrency=Currency.IDCurrency and RequstSupply.IDType = TypeQuntity.IDType and DateSupply between @d1 and @d2  order by RequstSupply.DateSupply,RequstSupply.IDCurrency,RequstSupply.IDType,RequstSupply.IDCategory ", con);
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
            cmd = new SqlCommand("select IDSupply as 'رقم الطلب' ,Category.NameCategory as 'اسم الصنف', TypeQuntity.NameType as 'نوع الكمية', RequstSupply.Quntity as 'الكمية', RequstSupply.Price as 'سعر الوحدة', RequstSupply.Quntity * RequstSupply.Price as 'الاجمالي',Currency.NameCurrency as 'العملة',RequstSupply.DateSupply as'تاريخ التوريد', RequstSupply.NameSupply as 'اسم المورد',Users.Name as 'اسم الموظف', RequstSupply.DescSupply as 'ملاحظات' ,RequstSupply.chek  from Users, Category, TypeQuntity, RequstSupply,Currency where RequstSupply.UserId=Users.UserID and RequstSupply.IDCategory = Category.IDCategory and RequstSupply.IDCurrency=Currency.IDCurrency and  RequstSupply.IDType = TypeQuntity.IDType and convert(varchar,IDSupply)+convert(varchar,Quntity)+convert(varchar,Quntity)+convert(varchar,Price)+ Category.NameCategory + TypeQuntity.NameType + RequstSupply.NameSupply + RequstSupply.DescSupply+Currency.NameCurrency+Users.Name  like @txt and DateSupply between @d1 and @d2 order by RequstSupply.DateSupply,RequstSupply.IDCurrency,RequstSupply.IDType,RequstSupply.IDCategory ", con);
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
        public DataTable PrintRequstSupply(int IDreqSup,int UserId,int user)
        {
            DataTable dt = new DataTable();
            cmd = new SqlCommand("select IDSupply as 'رقم الطلب' ,  Category.NameCategory  as 'الصنف', TypeQuntity.NameType  as'النوع' , RequstSupply.Quntity  as 'الكمية', RequstSupply.Price as 'السعر'  , RequstSupply.Quntity * RequstSupply.Price as 'الاجمالي' ,Currency.NameCurrency as 'العملة', RequstSupply.DateSupply as 'تاريخ' , RequstSupply.NameSupply  as'اسم المورد',Users.Name as 'اسم الموظف', RequstSupply.DescSupply AS 'ملاحظات',Debit.NameTypeAccount as 'مدين' ,Creditor.NameTypeAccount as 'دائن'  from Debit,Creditor ,Category,Users,TypeQuntity, RequstSupply,Currency where RequstSupply.IDCategory = Category.IDCategory and Debit.IdTypeAccount=RequstSupply.Debit and Creditor.IdTypeAccount=RequstSupply.Creditor and RequstSupply.IDType = TypeQuntity.IDType and RequstSupply.IDCurrency=Currency.IDCurrency and Users.UserID=@UserId  and RequstSupply.chek =@id and RequstSupply.UserId =@uuu ", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@id",IDreqSup);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@uuu", user);
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt;

        }
        ////////////////////////////////////////////
        /////// GetRequstSupply
        public DataTable GetRequstSupply(int IDreqSup)
        {
            DataTable dt = new DataTable();
            cmd = new SqlCommand("select IDSupply,IDCategory,IDType,Quntity,Price,IDCurrency,DateSupply,NameSupply,DescSupply,Debit,Creditor  from  RequstSupply where IDSupply=@id ", con);
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
        public int UPateRequstSupply(int IDSup, int IDCategory, int IDType, int Quntity, int Price,int idcurrn, string NameSupply, string DescSupply,int debit,int crd)
        {
            int resl = 0;
            cmd = new SqlCommand("Update RequstSupply set IDCategory=@IDCategory,IDType=@IDType,Quntity=@Quntity,Price=@Price,NameSupply=@NameSupply,DescSupply=@DescSupply,IDCurrency=@idcurrn ,Debit=@debit,Creditor=@crd where IDSupply=@IDSupply", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@IDSupply", IDSup);
            cmd.Parameters.AddWithValue("@IDCategory", IDCategory);
            cmd.Parameters.AddWithValue("@IDType", IDType);
            cmd.Parameters.AddWithValue("@Quntity", Quntity);
            cmd.Parameters.AddWithValue("@Price", Price);
            cmd.Parameters.AddWithValue("@NameSupply", NameSupply);
            cmd.Parameters.AddWithValue("@DescSupply", DescSupply);
            cmd.Parameters.AddWithValue("@idcurrn", idcurrn);
            cmd.Parameters.AddWithValue("@debit", debit);
            cmd.Parameters.AddWithValue("@crd", crd);
                con.Open();
            resl = cmd.ExecuteNonQuery();
            con.Close();
            return resl;
        }

        ////////////////////////////////////////////////////////////
        //////////// Add New in UPD supply
        //////////////////////////////////

        /// 
        public int ADDNewUPDSupply(int IDSup, int IDCategory, int IDType, int Quntity, int Price,int idcunnt, string NameSupply,  DateTime dateAdd, DateTime dateUpd, string decNew,int userid)
        {
            int resl = 0;
            cmd = new SqlCommand("INSERT INTO [UpdSupply]([IDSupply] ,[IDCategory],[IDType],[Quntity],[Price],[IDCurrency],[NameSupply],[DateSupply],[DescUpd] ,[dateUpd],[UserId]) VALUES (@IDSupply,@IDCategory,@IDType,@Quntity,@Price,@IDCurrency,@NameSupply,@deteAdd,@descup,@dateup,@userid)", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@IDSupply", IDSup);
            cmd.Parameters.AddWithValue("@IDCategory", IDCategory);
            cmd.Parameters.AddWithValue("@IDType", IDType);
            cmd.Parameters.AddWithValue("@Quntity", Quntity);
            cmd.Parameters.AddWithValue("@Price", Price);
            cmd.Parameters.AddWithValue("@NameSupply", NameSupply);
         
            cmd.Parameters.AddWithValue("@deteAdd", dateAdd);
            cmd.Parameters.AddWithValue("@descup", decNew);
            cmd.Parameters.AddWithValue("@dateup", dateUpd);
            cmd.Parameters.AddWithValue("@IDCurrency", idcunnt);
            cmd.Parameters.AddWithValue("@userid", userid);

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


            cmd = new SqlCommand("select RequstOut.IDOut as 'رقم الطلب',Category.NameCategory as 'اسم الصنف' ,TypeQuntity.NameType as 'نوع الكمية',PlaceSend.NamePlace as'الجهة المستفيدة' ,RequstOut.Quntity as'الكمية',RequstOut.Price as 'سعر الوحدة',RequstOut.Quntity*RequstOut.Price as'الاجمالي', Currency.NameCurrency as 'العملة',RequstOut.NameOut as'يصرف بامر',RequstOut.NameSend as'باستلام',RequstOut.DateOut as'تاريخ الصرف',Users.Name as 'اسم الموظف' ,RequstOut.DesOut as 'ملاحظات',RequstOut.Chack from Users, Category,TypeQuntity,PlaceSend,RequstOut,Currency where RequstOut.IDCategory=Category.IDCategory and RequstOut.IDType=TypeQuntity.IDType and RequstOut.IDPlace =PlaceSend.IDPlace and RequstOut.UserId=Users.UserID and RequstOut.IDCurrency =Currency.IDCurrency  and DateOut between @d1 and @d2 order by RequstOut.IDCategory,RequstOut.IDPlace,RequstOut.IDType,RequstOut.IDCurrency,RequstOut.Chack,RequstOut.DateOut ", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@d1", d1);
            cmd.Parameters.AddWithValue("@d2", d2);
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt;


        }
        /////////////////////
        ////// 
        // Delete RequstSupply
        public int DeleteRequstSupply (int Id)
        {
            int res = 0;
            cmd = new SqlCommand("delete from RequstSupply where IDSupply=@id", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@id", Id);
            try
            { con.Open();
                res = cmd.ExecuteNonQuery();

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            { con.Close(); }

            return res;






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


        cmd = new SqlCommand("select RequstOut.IDOut as 'رقم الطلب',Category.NameCategory as 'اسم الصنف' ,TypeQuntity.NameType as 'نوع الكمية',PlaceSend.NamePlace as'الجهة المستفيدة' ,RequstOut.Quntity as'الكمية',RequstOut.Price as 'سعر الوحدة',RequstOut.Quntity*RequstOut.Price as'الاجمالي', Currency.NameCurrency as 'العملة',RequstOut.NameOut as'يصرف بامر',RequstOut.NameSend as'باستلام',RequstOut.DateOut as'تاريخ الصرف' ,Users.Name as 'اسم الموظف',RequstOut.DesOut as 'ملاحظات',RequstOut.Chack from Users,Category,TypeQuntity,PlaceSend,RequstOut,Currency where RequstOut.UserId=Users.UserID and RequstOut.IDCategory=Category.IDCategory and RequstOut.IDType=TypeQuntity.IDType and RequstOut.IDPlace =PlaceSend.IDPlace and RequstOut.IDCurrency =Currency.IDCurrency  and Category.NameCategory + TypeQuntity.NameType + PlaceSend.NamePlace +RequstOut.DesOut+RequstOut.NameOut+ RequstOut.NameSend+CONVERT(varchar,RequstOut.IDOut)+CONVERT(varchar,RequstOut.Price)+CONVERT(varchar,RequstOut.Quntity) like @txt  order by RequstOut.IDCategory,RequstOut.IDPlace,RequstOut.IDType,RequstOut.IDCurrency,RequstOut.Chack,RequstOut.DateOut", con);
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


            cmd = new SqlCommand("select RequstOut.IDOut as 'رقم الطلب',Category.NameCategory as 'اسم الصنف' ,TypeQuntity.NameType as 'نوع الكمية',PlaceSend.NamePlace as'الجهة المستفيدة' ,RequstOut.Quntity as'الكمية',RequstOut.Price as 'سعر الوحدة',RequstOut.Quntity*RequstOut.Price as'الاجمالي', Currency.NameCurrency as 'العملة',RequstOut.NameOut as'يصرف بامر',RequstOut.NameSend as'باستلام',RequstOut.DateOut as'تاريخ الصرف' ,Users.Name as 'اسم الموظف',RequstOut.DesOut as 'ملاحظات',RequstOut.Chack from Users, Category,TypeQuntity,PlaceSend,RequstOut,Currency where RequstOut.IDCategory=Category.IDCategory and RequstOut.IDType=TypeQuntity.IDType and RequstOut.IDPlace =PlaceSend.IDPlace and RequstOut.IDCurrency =Currency.IDCurrency  and RequstOut.UserId =Users.UserID and Category.NameCategory + TypeQuntity.NameType + PlaceSend.NamePlace +RequstOut.DesOut+RequstOut.NameOut+ RequstOut.NameSend+CONVERT(varchar,RequstOut.IDOut)+CONVERT(varchar,RequstOut.Price)+CONVERT(varchar,RequstOut.Quntity) like @txt and DateOut between @d1 and @d2 order by RequstOut.IDCategory,RequstOut.IDPlace,RequstOut.IDType,RequstOut.IDCurrency,RequstOut.Chack,RequstOut.DateOut ", con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@txt", s);
            cmd.Parameters.AddWithValue("@d1", d1);
            cmd.Parameters.AddWithValue("@d2", d2);

            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt;


        }






        ////////////////////////////////
      
        //////////////
        ////////// 
        /// <summary>
        ///  get user in setting
        /// </summary>
        /// <returns></returns>
            public DataTable GetUser(int id)
        {


            DataTable dt = new DataTable();
            try
            {

                cmd = new SqlCommand("select Name,UserName,PassWord from Users where UserID=@id", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@id", id);

                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
              
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message);
            }
            return dt;
        }
        ///////////////
        ////////////
        ///updet user in setting
        ///
        /// 
        /// 
      
        /// <summary>
        /// /////
        /// </summary>
        /// <param name="bimg"></param>
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
        public DataTable PrintRequstRPT(DateTime d1, DateTime d2,string IDCate,string IDType,string IDCurrn ,string txt,string iduser)
        { //
            IDCate = "%" + IDCate;
            IDType = "%" + IDType;
            IDCurrn = "%" + IDCurrn;
            txt = "%" + txt + "%";
            iduser = "%" + iduser;
            DataTable dt = new DataTable();
            cmd = new SqlCommand("select IDSupply as 'رقم الطلب' ,Category.NameCategory as 'اسم الصنف', TypeQuntity.NameType as 'نوع الكمية', RequstSupply.Quntity as 'الكمية', RequstSupply.Price as 'سعر الوحدة', RequstSupply.Quntity * RequstSupply.Price as 'الاجمالي',Currency.NameCurrency as 'العملة',RequstSupply.DateSupply as'تاريخ التوريد', RequstSupply.NameSupply as 'اسم المورد', RequstSupply.DescSupply as 'ملاحظات' from Category, TypeQuntity, RequstSupply,Currency where RequstSupply.IDCategory = Category.IDCategory and RequstSupply.IDCurrency=Currency.IDCurrency and  RequstSupply.IDType = TypeQuntity.IDType and convert(varchar,RequstSupply.IDCurrency) like @IDCu and convert(varchar,RequstSupply.IDType) like @IDTy and convert(varchar,RequstSupply.IDCategory)like @IDCa and convert(varchar,RequstSupply.UserId)like @iduser and  NameSupply like @txt and DateSupply between @d1 and @d2 order by RequstSupply.IDSupply", con);
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
        public DataTable PrintRequstRPT( string IDCate, string IDType, string IDCurrn, string txt,string iduser)
        { //
            IDCate = "%" + IDCate;
            IDType = "%" + IDType;
            IDCurrn = "%" + IDCurrn;
            txt = "%" + txt + "%";
            iduser = "%" + iduser;
            DataTable dt = new DataTable();
            cmd = new SqlCommand("select IDSupply as 'رقم الطلب' ,Category.NameCategory as 'اسم الصنف', TypeQuntity.NameType as 'نوع الكمية', RequstSupply.Quntity as 'الكمية', RequstSupply.Price as 'سعر الوحدة', RequstSupply.Quntity * RequstSupply.Price as 'الاجمالي',Currency.NameCurrency as 'العملة',RequstSupply.DateSupply as'تاريخ التوريد', RequstSupply.NameSupply as 'اسم المورد', RequstSupply.DescSupply as 'ملاحظات' from Category, TypeQuntity, RequstSupply,Currency where RequstSupply.IDCategory = Category.IDCategory and RequstSupply.IDCurrency=Currency.IDCurrency and  RequstSupply.IDType = TypeQuntity.IDType and convert(varchar,RequstSupply.IDCurrency) like @IDCu and convert(varchar,RequstSupply.IDType) like @IDTy and convert(varchar,RequstSupply.IDCategory)like @IDCa and convert(varchar,RequstSupply.UserId)like @iduser and NameSupply like @txt  order by RequstSupply.IDSupply", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@IDCa", IDCate);
            cmd.Parameters.AddWithValue("@IDTy", IDType);
            cmd.Parameters.AddWithValue("@IDCu", IDCurrn);
            cmd.Parameters.AddWithValue("@iduser", iduser);
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
            cmd = new SqlCommand("select  Category.NameCategory as 'اسم الصنف' ,TypeQuntity.NameType as 'نوع الكمية' ,Account.Quntity as 'الكمية الحالية',Account.Price as 'السعر' ,Currency.NameCurrency as 'العملة' from Account,Category,TypeQuntity,Currency where Account.IDCategory=Category.IDCategory and Account.IDType =TypeQuntity.IDType and Account.IDCurrency=Currency.IDCurrency and Account.Quntity>0 and    convert(varchar,Account.IDCategory) like @idcat and convert(varchar,Account.IDType) like @idtyp and convert(varchar,Account.IDCurrency) like @idcur  order by Account.IDCategory,Account.IDType ,Account.IDCurrency", con);
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

        public DataTable PrintOutAllwithDate(string idca,string idtyp,string idpalce,string idcurrnt,string name,DateTime d1,DateTime d2,string iduser)
        {
            DataTable dt = new DataTable();
            idca = "%" + idca;
            idtyp = "%" + idtyp;
            idpalce = "%" + idpalce;
            idcurrnt = "%" + idcurrnt;
            name = "%" + name + "%";
            iduser = "%" + iduser;
            cmd = new SqlCommand("select RequstOut.IDOut as'رقم الطلب' ,Category.NameCategory as 'اسم الصنف' ,TypeQuntity.NameType as 'نوع الكمية',PlaceSend.NamePlace AS 'الجهة المستفيدة' ,RequstOut.Quntity as 'الكمية',RequstOut.Price AS 'سعر الوحدة',RequstOut.Quntity*RequstOut.Price as 'الاجمالي' ,Currency.NameCurrency AS 'العملة',RequstOut.NameOut AS 'يصرف بامر',RequstOut.NameSend as 'باستلام',RequstOut.DateOut as'تاريخ الصرف' ,RequstOut.DesOut as 'الملاحظات' from RequstOut,Category,TypeQuntity,Currency,PlaceSend where RequstOut.IDCategory=Category.IDCategory and RequstOut.IDType=TypeQuntity.IDType and RequstOut.IDPlace=PlaceSend.IDPlace and RequstOut.IDCurrency=Currency.IDCurrency and convert(varchar,RequstOut.IDCategory) like @idca and convert(varchar,RequstOut.IDType) like @idty and convert(varchar,RequstOut.UserId) like @iduser and convert(varchar,RequstOut.IDCurrency) like @idcurr and convert(varchar,RequstOut.IDPlace) like @idplac and RequstOut.NameSend like @nmsend and RequstOut.DateOut between @d1 and @d2 order by RequstOut.IDOut", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@idca",idca);
            cmd.Parameters.AddWithValue("@idty",idtyp);
            cmd.Parameters.AddWithValue("@idcurr",idcurrnt);
            cmd.Parameters.AddWithValue("@idplac",idpalce);
            cmd.Parameters.AddWithValue("@nmsend",name);
            cmd.Parameters.AddWithValue("@d1",d1);
            cmd.Parameters.AddWithValue("@d2",d2);
            cmd.Parameters.AddWithValue("@iduser", iduser);
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt;
        }


        //////////////////////////////////
        ////////////////////

        public DataTable PrintOutAll(string idca, string idtyp, string idpalce, string idcurrnt, string name ,string iduser)
        {
            DataTable dt = new DataTable();
            idca = "%" + idca;
            idtyp = "%" + idtyp;
            idpalce = "%" + idpalce;
            idcurrnt = "%" + idcurrnt;
            name = "%" + name + "%";
            iduser = "%" + iduser;
            cmd = new SqlCommand("select RequstOut.IDOut as'رقم الطلب' ,Category.NameCategory as 'اسم الصنف' ,TypeQuntity.NameType as 'نوع الكمية',PlaceSend.NamePlace AS 'الجهة المستفيدة' ,RequstOut.Quntity as 'الكمية',RequstOut.Price AS 'سعر الوحدة',RequstOut.Quntity*RequstOut.Price as 'الاجمالي' ,Currency.NameCurrency AS 'العملة',RequstOut.NameOut AS 'يصرف بامر',RequstOut.NameSend as 'باستلام',RequstOut.DateOut as'تاريخ الصرف' ,RequstOut.DesOut as 'الملاحظات' from RequstOut,Category,TypeQuntity,Currency,PlaceSend where RequstOut.IDCategory=Category.IDCategory and RequstOut.IDType=TypeQuntity.IDType and RequstOut.IDPlace=PlaceSend.IDPlace and RequstOut.IDCurrency=Currency.IDCurrency and convert(varchar,RequstOut.IDCategory) like @idca and convert(varchar,RequstOut.IDType) like @idty and convert(varchar,RequstOut.IDCurrency) like @idcurr and convert(varchar,RequstOut.UserId) like @iduser and convert(varchar,RequstOut.IDPlace) like @idplac and RequstOut.NameSend like @nmsend order by RequstOut.IDOut", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@idca", idca);
            cmd.Parameters.AddWithValue("@idty", idtyp);
            cmd.Parameters.AddWithValue("@idcurr", idcurrnt);
            cmd.Parameters.AddWithValue("@idplac", idpalce);
            cmd.Parameters.AddWithValue("@nmsend", name);
            cmd.Parameters.AddWithValue("@iduser", iduser);
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt;
        }



        

        

        /////////////////////////////// 
        ///////////Get form Update supply
        //// get by id supply
        public DataTable GetUpdateSupplyByIDSupply(int Id)
        {
            DataTable dt = new DataTable();
            cmd = new SqlCommand("SELECT  dbo.UpdSupply.IDUpt as 'رقم' , dbo.UpdSupply.IDSupply as 'رقم الطلب', dbo.Category.NameCategory as 'اسم الصنف' , dbo.TypeQuntity.NameType as 'نوع الكمية', dbo.UpdSupply.Quntity as 'الكمية', dbo.UpdSupply.Price as 'سعر الوحدة', dbo.Currency.NameCurrency as 'العملة',dbo.UpdSupply.NameSupply as 'اسم المورد' , dbo.UpdSupply.dateUpd as 'تاريخ التعديل', dbo.UpdSupply.DescUpd  as 'التعديل',Users.Name as 'اسم الموظف'  FROM Users, dbo.TypeQuntity CROSS JOIN   dbo.Category INNER JOIN  dbo.UpdSupply ON dbo.Category.IDCategory = dbo.UpdSupply.IDCategory CROSS JOIN   dbo.Currency  where UpdSupply.IDCategory=Category.IDCategory and UpdSupply.IDType=TypeQuntity.IDType and UpdSupply.IDCurrency =Currency.IDCurrency and UpdSupply.UserId = Users.UserID and dbo.UpdSupply.IDSupply=@id ", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@id", Id);
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt;
               
      }
        // get by Date
        public DataTable GetUpdateSupplyByDate(DateTime d1,DateTime d2)
        {
            DataTable dt = new DataTable();
            cmd = new SqlCommand("SELECT  dbo.UpdSupply.IDUpt as 'رقم', dbo.UpdSupply.IDSupply as 'رقم الطلب', dbo.Category.NameCategory as 'اسم الصنف' , dbo.TypeQuntity.NameType as 'نوع الكمية', dbo.UpdSupply.Quntity as 'الكمية', dbo.UpdSupply.Price as 'سعر الوحدة', dbo.Currency.NameCurrency as 'العملة',dbo.UpdSupply.NameSupply as 'اسم المورد', dbo.UpdSupply.dateUpd as 'تاريخ التعديل', dbo.UpdSupply.DescUpd  as 'التعديل' ,Users.Name as 'اسم الموظف' FROM Users, dbo.TypeQuntity CROSS JOIN   dbo.Category INNER JOIN  dbo.UpdSupply ON dbo.Category.IDCategory = dbo.UpdSupply.IDCategory CROSS JOIN   dbo.Currency  where UpdSupply.IDCategory=Category.IDCategory and UpdSupply.IDType=TypeQuntity.IDType and UpdSupply.IDCurrency =Currency.IDCurrency and UpdSupply.UserId = Users.UserID and dbo.UpdSupply.dateUpd between @d1 and @d2  ", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@d1", d1);
            cmd.Parameters.AddWithValue("@d2", d2);
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt;
        }
        ////////////////////////////////////////////////////////
        // get by Date
        public DataTable GetUpdateSupplyByDate1()
        {
            DataTable dt = new DataTable();
            cmd = new SqlCommand("SELECT  dbo.UpdSupply.IDUpt as 'رقم', dbo.UpdSupply.IDSupply as 'رقم الطلب', dbo.Category.NameCategory as 'اسم الصنف' , dbo.TypeQuntity.NameType as 'نوع الكمية', dbo.UpdSupply.Quntity as 'الكمية', dbo.UpdSupply.Price as 'سعر الوحدة', dbo.Currency.NameCurrency as 'العملة',dbo.UpdSupply.NameSupply as 'اسم المورد', dbo.UpdSupply.dateUpd as 'تاريخ التعديل', dbo.UpdSupply.DescUpd  as 'التعديل' ,Users.Name as 'اسم الموظف' FROM Users, dbo.TypeQuntity CROSS JOIN   dbo.Category INNER JOIN  dbo.UpdSupply ON dbo.Category.IDCategory = dbo.UpdSupply.IDCategory CROSS JOIN   dbo.Currency  where UpdSupply.IDCategory=Category.IDCategory and UpdSupply.IDType=TypeQuntity.IDType and UpdSupply.IDCurrency =Currency.IDCurrency and UpdSupply.UserId = Users.UserID  ", con);
            cmd.CommandType = CommandType.Text;
          
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt;
        }
        /// <summary>
        /// ///
        /// 
        public DataTable GetUpdateSupplyByDateUpdateWithDate(DateTime d1, DateTime d2)
        {
            DataTable dt = new DataTable();
            cmd = new SqlCommand("SELECT  dbo.UpdSupply.IDUpt as 'رقم', dbo.UpdSupply.IDSupply as 'رقم الطلب', dbo.Category.NameCategory as 'اسم الصنف' , dbo.TypeQuntity.NameType as 'نوع الكمية', dbo.UpdSupply.Quntity as 'الكمية', dbo.UpdSupply.Price as 'سعر الوحدة', dbo.Currency.NameCurrency as 'العملة',dbo.UpdSupply.NameSupply as 'اسم المورد', dbo.UpdSupply.dateUpd as 'تاريخ التعديل', dbo.UpdSupply.DescUpd  as 'التعديل' ,Users.Name as 'اسم الموظف' FROM Users, dbo.TypeQuntity CROSS JOIN   dbo.Category INNER JOIN  dbo.UpdSupply ON dbo.Category.IDCategory = dbo.UpdSupply.IDCategory CROSS JOIN   dbo.Currency  where UpdSupply.IDCategory=Category.IDCategory and UpdSupply.IDType=TypeQuntity.IDType and UpdSupply.IDCurrency =Currency.IDCurrency and UpdSupply.UserId = Users.UserID and dbo.UpdSupply.dateUpd between @d1 and @d2  and UpdSupply.DescUpd !=N'تم حذف الطلب' ", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@d1", d1);
            cmd.Parameters.AddWithValue("@d2", d2);
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt;
        }
        ////////////////////////////////////////////////////////
        // get by Date
        public DataTable GetUpdateSupplyByDate1Update()
        {
            DataTable dt = new DataTable();
            cmd = new SqlCommand("SELECT  dbo.UpdSupply.IDUpt as 'رقم', dbo.UpdSupply.IDSupply as 'رقم الطلب', dbo.Category.NameCategory as 'اسم الصنف' , dbo.TypeQuntity.NameType as 'نوع الكمية', dbo.UpdSupply.Quntity as 'الكمية', dbo.UpdSupply.Price as 'سعر الوحدة', dbo.Currency.NameCurrency as 'العملة',dbo.UpdSupply.NameSupply as 'اسم المورد', dbo.UpdSupply.dateUpd as 'تاريخ التعديل', dbo.UpdSupply.DescUpd  as 'التعديل' ,Users.Name as 'اسم الموظف' FROM Users, dbo.TypeQuntity CROSS JOIN   dbo.Category INNER JOIN  dbo.UpdSupply ON dbo.Category.IDCategory = dbo.UpdSupply.IDCategory CROSS JOIN   dbo.Currency  where UpdSupply.IDCategory=Category.IDCategory and UpdSupply.IDType=TypeQuntity.IDType and UpdSupply.IDCurrency =Currency.IDCurrency and UpdSupply.UserId = Users.UserID and UpdSupply.DescUpd !=N'تم حذف الطلب' ", con);
            cmd.CommandType = CommandType.Text;

            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt;
        }
        /// </summary>
        /// 
        /// 
        public DataTable GetUpdateSupplyByDateDeleteWithDate(DateTime d1, DateTime d2)
        {
            DataTable dt = new DataTable();
            cmd = new SqlCommand("SELECT  dbo.UpdSupply.IDUpt as 'رقم', dbo.UpdSupply.IDSupply as 'رقم الطلب', dbo.Category.NameCategory as 'اسم الصنف' , dbo.TypeQuntity.NameType as 'نوع الكمية', dbo.UpdSupply.Quntity as 'الكمية', dbo.UpdSupply.Price as 'سعر الوحدة', dbo.Currency.NameCurrency as 'العملة',dbo.UpdSupply.NameSupply as 'اسم المورد', dbo.UpdSupply.dateUpd as 'تاريخ التعديل', dbo.UpdSupply.DescUpd  as 'التعديل' ,Users.Name as 'اسم الموظف' FROM Users, dbo.TypeQuntity CROSS JOIN   dbo.Category INNER JOIN  dbo.UpdSupply ON dbo.Category.IDCategory = dbo.UpdSupply.IDCategory CROSS JOIN   dbo.Currency  where UpdSupply.IDCategory=Category.IDCategory and UpdSupply.IDType=TypeQuntity.IDType and UpdSupply.IDCurrency =Currency.IDCurrency and UpdSupply.UserId = Users.UserID and dbo.UpdSupply.dateUpd between @d1 and @d2  and UpdSupply.DescUpd =N'تم حذف الطلب' ", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@d1", d1);
            cmd.Parameters.AddWithValue("@d2", d2);
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt;
        }
        ////////////////////////////////////////////////////////
        // get by Date
        public DataTable GetUpdateSupplyByDate1delete()
        {
            DataTable dt = new DataTable();
            cmd = new SqlCommand("SELECT  dbo.UpdSupply.IDUpt as 'رقم', dbo.UpdSupply.IDSupply as 'رقم الطلب', dbo.Category.NameCategory as 'اسم الصنف' , dbo.TypeQuntity.NameType as 'نوع الكمية', dbo.UpdSupply.Quntity as 'الكمية', dbo.UpdSupply.Price as 'سعر الوحدة', dbo.Currency.NameCurrency as 'العملة',dbo.UpdSupply.NameSupply as 'اسم المورد', dbo.UpdSupply.dateUpd as 'تاريخ التعديل', dbo.UpdSupply.DescUpd  as 'التعديل' ,Users.Name as 'اسم الموظف' FROM Users, dbo.TypeQuntity CROSS JOIN   dbo.Category INNER JOIN  dbo.UpdSupply ON dbo.Category.IDCategory = dbo.UpdSupply.IDCategory CROSS JOIN   dbo.Currency  where UpdSupply.IDCategory=Category.IDCategory and UpdSupply.IDType=TypeQuntity.IDType and UpdSupply.IDCurrency =Currency.IDCurrency and UpdSupply.UserId = Users.UserID and UpdSupply.DescUpd =N'تم حذف الطلب' ", con);
            cmd.CommandType = CommandType.Text;

            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt;
        }
        /// <param name="IDOutRequst"></param>
        /// <returns></returns>
        /////////////////////////
        /////
        //
        public DataTable GetRequstOutSngle(int IDOutRequst)
        {
            DataTable dt = new DataTable();
            cmd = new SqlCommand("select * from RequstOut where IDOut=@id", con);
            cmd.Parameters.AddWithValue("@id", IDOutRequst);
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt;

        }
        /// upd Out
        /// Add in Upd Out
        public int AddNewUpdOut(int IDOut, int IdCate, int IdType, int IdPlace, int Quntity, string NameOUt, string NameSend, int Price, int IdCurrent, string TxtReson, DateTime DateUpdate,int UserId)
        {
            int res = 0;
           try
            {
                cmd = new SqlCommand("insert into UpdateOut(IDOut,IdCate,IdType,IdPlace,Quntity,NameOUt,DateUpdate,NameSend,Price,IdCurrent,TxtReson,UserId) values(@IDOut,@IdCate,@IdType,@IdPlace,@Quntity,@NameOUt,@Date,@NameSend,@Price,@IdCurrent,@TxtReson,@UserId)", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@IDOut", IDOut);
                cmd.Parameters.AddWithValue("@IdCate", IdCate);
                cmd.Parameters.AddWithValue("@IdType", IdType);
                cmd.Parameters.AddWithValue("@IdPlace", IdPlace);
                cmd.Parameters.AddWithValue("@Quntity", Quntity);
                cmd.Parameters.AddWithValue("@NameOUt", NameOUt);
                cmd.Parameters.AddWithValue("@NameSend", NameSend);
                cmd.Parameters.AddWithValue("@Price", Price);
                cmd.Parameters.AddWithValue("@IdCurrent", IdCurrent);
                cmd.Parameters.AddWithValue("@TxtReson", TxtReson);
                cmd.Parameters.AddWithValue("@Date", DateUpdate);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                con.Open();
                res = cmd.ExecuteNonQuery();

            }
           catch(Exception ex)
            {
               MessageBox.Show(ex.Message);
            }
           finally
            {
                con.Close();
            }
            return res;
        }
        ///////////////////////////////////////////////////
        ////////////////
        ///// delete from requst out
        public int DeleteRqustOut(int IdRequstOut,int IdUser)
        {
            int res = 0;
            DataTable dt = new DataTable();
            // جلب معلومات عن الطلب المراد حذفه
            dt = GetRequstOutSngle(IdRequstOut);
            // اضافة البيانات الى جدول التعديلات 

            AddNewUpdOut(IdRequstOut, Convert.ToInt32(dt.Rows[0]["IDCategory"].ToString()), Convert.ToInt32(dt.Rows[0]["IDType"].ToString()), Convert.ToInt32(dt.Rows[0]["IDPlace"].ToString()), Convert.ToInt32(dt.Rows[0]["Quntity"].ToString()), dt.Rows[0]["NameOut"].ToString(), dt.Rows[0]["NameSend"].ToString(), Convert.ToInt32(dt.Rows[0]["Price"].ToString()), Convert.ToInt32(dt.Rows[0]["IDCurrency"].ToString()), "تم حذف الطلب", DateTime.Now,IdUser);

            // التعديل في جدول الحسابات
          // ارجاع الكمية الى المخزون
            int IdAccount = CheckAccountIsHere(Convert.ToInt32(dt.Rows[0]["IDCategory"].ToString()), Convert.ToInt32(dt.Rows[0]["IDType"].ToString()), Convert.ToInt32(dt.Rows[0]["Price"].ToString()), Convert.ToInt32(dt.Rows[0]["IDCurrency"].ToString()));
            int QuntOld = GetQuntityInAccount(IdAccount);
            int QuntNew = QuntOld + Convert.ToInt32(dt.Rows[0]["Quntity"].ToString());
            UpdateQuntityAccount(IdAccount, QuntNew);
            // حذف الطلب من جدول طلبات الصرف
            cmd = new SqlCommand("delete from RequstOut where IDOut =@id", con);
            cmd.Parameters.AddWithValue("@id", IdRequstOut);
            con.Open();
            res = cmd.ExecuteNonQuery();
            con.Close();
            return res;

        }
        /////////////
        ////////
        /// upadte Rqust oU
        /// 
      public  int UpdateRequstOut(int IDOut,int IdPlace,string NameOut,string NameSend,string Reson,DateTime d1,int UserId,int debt,int crd)
        {
            int res = 0;
            /// اضافة التعديل الى جدول التعديلات
            /// 
            DataTable dt = new DataTable();
            // جلب معلومات عن الطلب المراد تعديله
            dt = GetRequstOutSngle(IDOut);
            // اضافة البيانات الى جدول التعديلات 

            AddNewUpdOut(IDOut, Convert.ToInt32(dt.Rows[0]["IDCategory"].ToString()), Convert.ToInt32(dt.Rows[0]["IDType"].ToString()), Convert.ToInt32(dt.Rows[0]["IDPlace"].ToString()), Convert.ToInt32(dt.Rows[0]["Quntity"].ToString()), dt.Rows[0]["NameOut"].ToString(), dt.Rows[0]["NameSend"].ToString(), Convert.ToInt32(dt.Rows[0]["Price"].ToString()), Convert.ToInt32(dt.Rows[0]["IDCurrency"].ToString()), Reson, DateTime.Now,UserId);
            //////////////////////////////////
            //التعديل في جدول التعديلات
            cmd = new SqlCommand("Update RequstOut set IDPlace=@idplace , NameOut=@nameout ,NameSend=@namesend,Creditor=@crd,Debit=@dibt where IDOut=@idOut ", con);
            cmd.Parameters.AddWithValue("@idplace", IdPlace);
            cmd.Parameters.AddWithValue("@nameout", NameOut);
            cmd.Parameters.AddWithValue("@namesend", NameSend);
            cmd.Parameters.AddWithValue("@idOut", IDOut);
            cmd.Parameters.AddWithValue("@dibt", debt);
            cmd.Parameters.AddWithValue("@crd", crd);
            con.Open();
            res=     cmd.ExecuteNonQuery();
            con.Close();
            return res;
           
        }
        //////////////
         /// get of UpdtOut Uing IdOut
       public DataTable GetUpdtOutByIDOut(int idOUt)
        {
            DataTable dt = new DataTable();
            cmd = new SqlCommand("SELECT     dbo.UpdateOut.IdUpdate as 'رقم', dbo.UpdateOut.IDOut as 'رقم الطلب', dbo.Category.NameCategory as 'اسم الصنف', dbo.TypeQuntity.NameType as 'نوع الكمية', dbo.UpdateOut.Quntity as 'الكمية', dbo.UpdateOut.Price as 'سعر الوحدة', dbo.Currency.NameCurrency as 'العملة',  dbo.PlaceSend.NamePlace as 'الجهة المستفيدة', dbo.UpdateOut.NameOUt as 'بامر من ', dbo.UpdateOut.NameSend as 'اسم المستلم', dbo.UpdateOut.TxtReson as 'سبب التعديل', dbo.UpdateOut.DateUpdate as 'تاريخ التعديل' ,Users.Name as 'اسم الموظف' FROM Users,   dbo.UpdateOut INNER JOIN   dbo.PlaceSend ON dbo.UpdateOut.IdPlace = dbo.PlaceSend.IDPlace CROSS JOIN   dbo.TypeQuntity CROSS JOIN  dbo.Category CROSS JOIN   dbo.Currency where UpdateOut.IdCate = Category.IDCategory and UpdateOut.IdCurrent = Currency.IDCurrency and UpdateOut.IdPlace = PlaceSend.IDPlace and UpdateOut.UserId=Users.UserID and UpdateOut.IdType = TypeQuntity.IDType and UpdateOut.IDOut=@id", con);
            cmd.Parameters.AddWithValue("@id", idOUt);
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt;
        }
        /// <summary>
        /// // update from updout
        /// </summary>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <returns></returns>
        public DataTable GetUpdOutByDate(DateTime d1,DateTime d2)
        {
            DataTable dt = new DataTable();
            cmd = new SqlCommand("SELECT    dbo.UpdateOut.IdUpdate as 'رقم', dbo.UpdateOut.IDOut as 'رقم الطلب', dbo.Category.NameCategory as 'اسم الصنف', dbo.TypeQuntity.NameType as 'نوع الكمية', dbo.UpdateOut.Quntity as 'الكمية', dbo.UpdateOut.Price as 'سعر الوحدة', dbo.Currency.NameCurrency as 'العملة',  dbo.PlaceSend.NamePlace as 'الجهة المستفيدة', dbo.UpdateOut.NameOUt as 'بامر من ', dbo.UpdateOut.NameSend as 'اسم المستلم', dbo.UpdateOut.TxtReson as 'سبب التعديل', dbo.UpdateOut.DateUpdate as 'تاريخ التعديل' ,Users.Name as 'اسم الموظف' FROM  Users, dbo.UpdateOut INNER JOIN   dbo.PlaceSend ON dbo.UpdateOut.IdPlace = dbo.PlaceSend.IDPlace CROSS JOIN   dbo.TypeQuntity CROSS JOIN  dbo.Category CROSS JOIN   dbo.Currency where UpdateOut.IdCate = Category.IDCategory and UpdateOut.IdCurrent = Currency.IDCurrency and UpdateOut.IdPlace = PlaceSend.IDPlace and UpdateOut.UserId=Users.UserID and  UpdateOut.IdType = TypeQuntity.IDType and UpdateOut.DateUpdate between @d1 and @d2", con);
            cmd.Parameters.AddWithValue("@d1",d1);
            cmd.Parameters.AddWithValue("@d2", d2);
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt;
        }
        /// <summary>
        /// ///////
        /// 
          public DataTable GetUpdOutByDate1()
        {
            DataTable dt = new DataTable();
            cmd = new SqlCommand("SELECT    dbo.UpdateOut.IdUpdate as 'رقم', dbo.UpdateOut.IDOut as 'رقم الطلب', dbo.Category.NameCategory as 'اسم الصنف', dbo.TypeQuntity.NameType as 'نوع الكمية', dbo.UpdateOut.Quntity as 'الكمية', dbo.UpdateOut.Price as 'سعر الوحدة', dbo.Currency.NameCurrency as 'العملة',  dbo.PlaceSend.NamePlace as 'الجهة المستفيدة', dbo.UpdateOut.NameOUt as 'بامر من ', dbo.UpdateOut.NameSend as 'اسم المستلم', dbo.UpdateOut.TxtReson as 'سبب التعديل', dbo.UpdateOut.DateUpdate as 'تاريخ التعديل' ,Users.Name as 'اسم الموظف' FROM  Users, dbo.UpdateOut INNER JOIN   dbo.PlaceSend ON dbo.UpdateOut.IdPlace = dbo.PlaceSend.IDPlace CROSS JOIN   dbo.TypeQuntity CROSS JOIN  dbo.Category CROSS JOIN   dbo.Currency where UpdateOut.IdCate = Category.IDCategory and UpdateOut.IdCurrent = Currency.IDCurrency and UpdateOut.IdPlace = PlaceSend.IDPlace and UpdateOut.UserId=Users.UserID and  UpdateOut.IdType = TypeQuntity.IDType  ", con);

            adapter = new SqlDataAdapter(cmd);
        adapter.Fill(dt);
            return dt;
        }
    /// </summary>
    /// <param name="d1"></param>
    /// <param name="d2"></param>
    /// <returns></returns>
    public DataTable GetUpdOutByDateUpdte()
        {
            DataTable dt = new DataTable();
            cmd = new SqlCommand("SELECT    dbo.UpdateOut.IdUpdate as 'رقم', dbo.UpdateOut.IDOut as 'رقم الطلب', dbo.Category.NameCategory as 'اسم الصنف', dbo.TypeQuntity.NameType as 'نوع الكمية', dbo.UpdateOut.Quntity as 'الكمية', dbo.UpdateOut.Price as 'سعر الوحدة', dbo.Currency.NameCurrency as 'العملة',  dbo.PlaceSend.NamePlace as 'الجهة المستفيدة', dbo.UpdateOut.NameOUt as 'بامر من ', dbo.UpdateOut.NameSend as 'اسم المستلم', dbo.UpdateOut.TxtReson as 'سبب التعديل', dbo.UpdateOut.DateUpdate as 'تاريخ التعديل' ,Users.Name as 'اسم الموظف' FROM  Users, dbo.UpdateOut INNER JOIN   dbo.PlaceSend ON dbo.UpdateOut.IdPlace = dbo.PlaceSend.IDPlace CROSS JOIN   dbo.TypeQuntity CROSS JOIN  dbo.Category CROSS JOIN   dbo.Currency where UpdateOut.IdCate = Category.IDCategory and UpdateOut.IdCurrent = Currency.IDCurrency and UpdateOut.IdPlace = PlaceSend.IDPlace and UpdateOut.UserId=Users.UserID and  UpdateOut.IdType = TypeQuntity.IDType  and UpdateOut.TxtReson !=N'تم حذف الطلب'", con);

            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt;
        }
        /// <summary>
        /// ///////
        /// </summary>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <returns></returns>
        public DataTable GetUpdOutByDateUpdtewithdate(DateTime d1, DateTime d2)
        {
            DataTable dt = new DataTable();
            cmd = new SqlCommand("SELECT    dbo.UpdateOut.IdUpdate as 'رقم', dbo.UpdateOut.IDOut as 'رقم الطلب', dbo.Category.NameCategory as 'اسم الصنف', dbo.TypeQuntity.NameType as 'نوع الكمية', dbo.UpdateOut.Quntity as 'الكمية', dbo.UpdateOut.Price as 'سعر الوحدة', dbo.Currency.NameCurrency as 'العملة',  dbo.PlaceSend.NamePlace as 'الجهة المستفيدة', dbo.UpdateOut.NameOUt as 'بامر من ', dbo.UpdateOut.NameSend as 'اسم المستلم', dbo.UpdateOut.TxtReson as 'سبب التعديل', dbo.UpdateOut.DateUpdate as 'تاريخ التعديل' ,Users.Name as 'اسم الموظف' FROM  Users, dbo.UpdateOut INNER JOIN   dbo.PlaceSend ON dbo.UpdateOut.IdPlace = dbo.PlaceSend.IDPlace CROSS JOIN   dbo.TypeQuntity CROSS JOIN  dbo.Category CROSS JOIN   dbo.Currency where UpdateOut.IdCate = Category.IDCategory and UpdateOut.IdCurrent = Currency.IDCurrency and UpdateOut.IdPlace = PlaceSend.IDPlace and UpdateOut.UserId=Users.UserID and  UpdateOut.IdType = TypeQuntity.IDType and UpdateOut.DateUpdate between @d1 and @d2 and UpdateOut.TxtReson !=N'تم حذف الطلب'", con);
            cmd.Parameters.AddWithValue("@d1", d1);
            cmd.Parameters.AddWithValue("@d2", d2);
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt;
        }
        ////////////////////////////////////////
        /////////////////

        /// </summary>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <returns></returns>
        public DataTable GetUpdOutByDatedelte()
        {
            DataTable dt = new DataTable();
            cmd = new SqlCommand("SELECT    dbo.UpdateOut.IdUpdate as 'رقم', dbo.UpdateOut.IDOut as 'رقم الطلب', dbo.Category.NameCategory as 'اسم الصنف', dbo.TypeQuntity.NameType as 'نوع الكمية', dbo.UpdateOut.Quntity as 'الكمية', dbo.UpdateOut.Price as 'سعر الوحدة', dbo.Currency.NameCurrency as 'العملة',  dbo.PlaceSend.NamePlace as 'الجهة المستفيدة', dbo.UpdateOut.NameOUt as 'بامر من ', dbo.UpdateOut.NameSend as 'اسم المستلم', dbo.UpdateOut.TxtReson as 'سبب التعديل', dbo.UpdateOut.DateUpdate as 'تاريخ التعديل' ,Users.Name as 'اسم الموظف' FROM  Users, dbo.UpdateOut INNER JOIN   dbo.PlaceSend ON dbo.UpdateOut.IdPlace = dbo.PlaceSend.IDPlace CROSS JOIN   dbo.TypeQuntity CROSS JOIN  dbo.Category CROSS JOIN   dbo.Currency where UpdateOut.IdCate = Category.IDCategory and UpdateOut.IdCurrent = Currency.IDCurrency and UpdateOut.IdPlace = PlaceSend.IDPlace and UpdateOut.UserId=Users.UserID and  UpdateOut.IdType = TypeQuntity.IDType  and UpdateOut.TxtReson =N'تم حذف الطلب'", con);

            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt;
        }
        /// <summary>
        /// ///////
        /// </summary>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <returns></returns>
        public DataTable GetUpdOutByDateDetle2tewithdate(DateTime d1, DateTime d2)
        {
            DataTable dt = new DataTable();
            cmd = new SqlCommand("SELECT    dbo.UpdateOut.IdUpdate as 'رقم', dbo.UpdateOut.IDOut as 'رقم الطلب', dbo.Category.NameCategory as 'اسم الصنف', dbo.TypeQuntity.NameType as 'نوع الكمية', dbo.UpdateOut.Quntity as 'الكمية', dbo.UpdateOut.Price as 'سعر الوحدة', dbo.Currency.NameCurrency as 'العملة',  dbo.PlaceSend.NamePlace as 'الجهة المستفيدة', dbo.UpdateOut.NameOUt as 'بامر من ', dbo.UpdateOut.NameSend as 'اسم المستلم', dbo.UpdateOut.TxtReson as 'سبب التعديل', dbo.UpdateOut.DateUpdate as 'تاريخ التعديل' ,Users.Name as 'اسم الموظف' FROM  Users, dbo.UpdateOut INNER JOIN   dbo.PlaceSend ON dbo.UpdateOut.IdPlace = dbo.PlaceSend.IDPlace CROSS JOIN   dbo.TypeQuntity CROSS JOIN  dbo.Category CROSS JOIN   dbo.Currency where UpdateOut.IdCate = Category.IDCategory and UpdateOut.IdCurrent = Currency.IDCurrency and UpdateOut.IdPlace = PlaceSend.IDPlace and UpdateOut.UserId=Users.UserID and  UpdateOut.IdType = TypeQuntity.IDType and UpdateOut.DateUpdate between @d1 and @d2 and UpdateOut.TxtReson =N'تم حذف الطلب'", con);
            cmd.Parameters.AddWithValue("@d1", d1);
            cmd.Parameters.AddWithValue("@d2", d2);
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt;
        }
        ////////////////////////////////////////
        /////////////////
        ////// users
        // add new Users
        public int AddNewUser( string name,string user,string pass,bool supply,bool outt,bool upd,bool printr,bool userAdd,bool Active)
        {
            int res = 0;

            cmd = new SqlCommand("INSERT INTO [Users]([Name],[UserName],[Password],[Supply],[Out],[UpdteDe],[PrintRE],[UserAdd],[Active]) VALUES(@Name,@UserName,@Password,@Supply,@Out,@UpdteDe,@PrintRE,@UserAdd,@Active)", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@UserName", user);
            cmd.Parameters.AddWithValue("@Password", pass);
            cmd.Parameters.AddWithValue("@Supply", supply);
            cmd.Parameters.AddWithValue("@Out", outt);
            cmd.Parameters.AddWithValue("@UpdteDe", upd);
            cmd.Parameters.AddWithValue("@PrintRE", printr);
            cmd.Parameters.AddWithValue("@UserAdd", userAdd);
            cmd.Parameters.AddWithValue("@Active", Active);

            try
            {
                con.Open();
                res = cmd.ExecuteNonQuery();
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

 


    /////
    ////////Updte Users
  public  int UpdUsers(int IdUser, string name, string user, string pass, bool supply, bool outt, bool upd, bool printr, bool userAdd, bool Active)
    {
        
        int res = 0;

        cmd = new SqlCommand("Update Users set Name=@Name,UserName=@UserName,Password=@Password,Supply=@Supply,Out=@Out,UpdteDe=@UpdteDe,PrintRE=@PrintRE,UserAdd=@UserAdd,Active=@Active where UserID=@id", con);
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.AddWithValue("@Name", name);
        cmd.Parameters.AddWithValue("@UserName", user);
        cmd.Parameters.AddWithValue("@Password", pass);
        cmd.Parameters.AddWithValue("@Supply", supply);
        cmd.Parameters.AddWithValue("@Out", outt);
        cmd.Parameters.AddWithValue("@UpdteDe", upd);
        cmd.Parameters.AddWithValue("@PrintRE", printr);
        cmd.Parameters.AddWithValue("@UserAdd", userAdd);
        cmd.Parameters.AddWithValue("@Active", Active);
        cmd.Parameters.AddWithValue("@id", IdUser);

        try
        {
            con.Open();
            res = cmd.ExecuteNonQuery();
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
        ///////// Delete User
       public int DeleteUser(int Iduser)
        {
            int res = 0;
            cmd = new SqlCommand("delete from Users where UserID= @id", con);

                cmd.Parameters.AddWithValue("@id", Iduser);
            try
            {
                con.Open();
              res=  cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                con.Close();
            }
            return res;

        }

        /////////////
        //// Get All Users
       public  DataTable  GetAllUser()
        {
            DataTable dt = new DataTable();
            cmd = new SqlCommand("select Users.UserID as 'رقم' ,Users.Name as 'اسم الموظف',Users.UserName as 'اسم المستخدم',Users.Password as 'كلمة المرور',Users.Supply as 'امر توريد',Users.Out as 'امر صرف',Users.PrintRE as 'طباعة تقارير',Users.UpdteDe as'تعديل / حذف' ,Users.Active as 'تفعيل',Users.UserAdd as'اضافة مستخدمين' from Users ", con);
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt;
        }
        //////////////////
        ///////
        ///////////
        // get user
        public bool CheckUser(string User, string Pass)
        {
            bool check = false;
           try
            {

                DataTable dt = new DataTable();
                cmd = new SqlCommand("select Active from Users where UserName=@UserName and Password=@PassWord", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@UserName", User);
                cmd.Parameters.AddWithValue("@PassWord", Pass);
                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    bool ch = Convert.ToBoolean(dt.Rows[0][0].ToString());
                    check = ch;
                }
               else
                {
                    check = false;
                }
                    
               
            }
           catch (Exception ex)
            {
                check = false;
                MessageBox.Show(ex.Message);
            }
            return check;
        }
        ////////
        /////
        ///////////
        // get user
        public int CheckUser2(string User, string Pass)
        {
            int check = 0;
            try
            {

                DataTable dt = new DataTable();
                cmd = new SqlCommand("select UserID from Users where UserName=@UserName and Password=@PassWord", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@UserName", User);
                cmd.Parameters.AddWithValue("@PassWord", Pass);
                con.Open();
                check =(int) cmd.ExecuteScalar();
             
            }
            catch (Exception ex)
            {
                check = 0;
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
                
            }  
            return check;

        }
        ////////////////
        //////// updat password
    public    int UpdateUserPassword(int IdUser,string pass)
        {
            int res = 0;
            cmd = new SqlCommand("update Users set Password=@pass where UserId=@id", con);
            cmd.Parameters.AddWithValue("@pass", pass);
            cmd.Parameters.AddWithValue("@id", IdUser);
            try
            {
                con.Open();
                res = cmd.ExecuteNonQuery();
            }
            catch(Exception ex)

            { MessageBox.Show(ex.Message); }
            finally { con.Close();
            }
            return res;
        }

        ///////////////////////////////
        /////// Get Gernd Users
       public DataTable  GetGrendUsers(int IDUser)
        {
            DataTable dt = new DataTable();
            cmd = new SqlCommand(" Select * from Users where UserID=@id ", con);
            cmd.Parameters.AddWithValue("@id", IDUser);
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt;
        }
        /// GetUserNameById
        /// 
        public string GetUserNameBYIdUser(int IdUser)
        {
            string s = "";
            cmd = new SqlCommand("select Name from Users where UserID=@userid", con);
            cmd.Parameters.AddWithValue("@userid", IdUser);
            try
            {
                con.Open();
                s =(string) cmd.ExecuteScalar();

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return s;
        }

        ///////////////////////////
        /////////////////
        ////////////
        /// Get Max ChechSupply
        /// 
        public int GetMaxCheckSupply()
        {
            int res = 0;
            cmd = new SqlCommand("select max(chek) from RequstSupply ", con);
            try
            {
                con.Open();
                res = (int)cmd.ExecuteScalar();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
               
            }
            finally
            {
                con.Close();
            }
            return res;
        }
      
       
        //////////////////////////////////////////////////////
        //////////////////////////////////
        ////////////
        ////// typeDebit
        // add new Debit
        public int AddNewDebit(string nameType)
        {
            int reslt = 0;
            cmd = new SqlCommand("insert into Debit (NameTypeAccount) values(@name) ", con);
            cmd.Parameters.AddWithValue("@name", nameType);
            con.Open();
            try
            {
                reslt = cmd.ExecuteNonQuery();

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                reslt = 0;

            }
            finally
            {
                con.Close();
            }
            return reslt;
        }
        ////////////
        //// update Debit
        public int UpdateDebit(int idtypeAccount,string nameType)
        {
            int reslt = 0;
            cmd = new SqlCommand("update  Debit set NameTypeAccount = @name where IdTypeAccount=@id ", con);
            cmd.Parameters.AddWithValue("@name", nameType);
            cmd.Parameters.AddWithValue("@id", idtypeAccount);
            con.Open();
            try
            {
                reslt = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                reslt = 0;

            }
            finally
            {
                con.Close();
            }
            return reslt;
        }
        ///////////////////////////////////////////
        /////////////////////
        //// delete Debit

        public int DeleteDebit(int idtypeAccount)
        {
            int reslt = 0;
            cmd = new SqlCommand("delete from Debit where IdTypeAccount=@id ", con);
         
            cmd.Parameters.AddWithValue("@id", idtypeAccount);
            con.Open();
            try
            {
                reslt = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                reslt = 0;

            }
            finally
            {
                con.Close();
            }
            return reslt;
        }
        //////////////////////////////////////////
        ///////////////
        /// GetAllDebit
        /// 
         public    DataTable GetAllDebit()
        {
            DataTable dt = new DataTable();
            cmd = new SqlCommand("select IdTypeAccount as 'الرقم' ,NameTypeAccount as 'نوع الحساب' from Debit ", con);
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt;

        }



        //////////////////////////////////////////////////////
        //////////////////////////////////
        ////////////
        ////// typeCreditor
        // add new Creditor
        public int AddNewCreditor(string nameType)
        {
            int reslt = 0;
            cmd = new SqlCommand("insert into Creditor (NameTypeAccount) values(@name) ", con);
            cmd.Parameters.AddWithValue("@name", nameType);
            con.Open();
            try
            {
                reslt = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                reslt = 0;

            }
            finally
            {
                con.Close();
            }
            return reslt;
        }
        ////////////
        //// update Creditor
        public int UpdateCreditor(int idtypeAccount, string nameType)
        {
            int reslt = 0;
            cmd = new SqlCommand("update  Creditor set NameTypeAccount = @name where IdTypeAccount=@id ", con);
            cmd.Parameters.AddWithValue("@name", nameType);
            cmd.Parameters.AddWithValue("@id", idtypeAccount);
            con.Open();
            try
            {
                reslt = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                reslt = 0;

            }
            finally
            {
                con.Close();
            }
            return reslt;
        }
        ///////////////////////////////////////////
        /////////////////////
        //// delete Creditor

        public int DeleteCreditor(int idtypeAccount)
        {
            int reslt = 0;
            cmd = new SqlCommand("delete from Creditor where IdTypeAccount=@id ", con);

            cmd.Parameters.AddWithValue("@id", idtypeAccount);
            con.Open();
            try
            {
                reslt = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                reslt = 0;

            }
            finally
            {
                con.Close();
            }
            return reslt;
        }
        //////////////////////////////////////////
        ///////////////
        /// GetAllCreditor
        /// 
        public DataTable GetAllCreditor()
        {
            DataTable dt = new DataTable();
            cmd = new SqlCommand("select  IdTypeAccount as 'الرقم' ,NameTypeAccount as 'نوع الحساب' from Creditor ", con);
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt;

        }
        public int GetIdUser(string NameUser)
        {
            int reslt = 0;
       
            cmd = new SqlCommand("select UserID from Users where Name=N'"+NameUser+"' ", con);

        
            con.Open();
            try
           {
                reslt =(int) cmd.ExecuteScalar();
             
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                reslt = 0;

            }
            finally
            {
                con.Close();
            }
            return reslt;
        }
        public int delteAccount(int id)
        {

            int res = 0;
            cmd = new SqlCommand("delete from Account where IDAccount=@id", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@id", id);
            try
            {
                con.Open();
                res = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            { con.Close(); }

            return res;
        }
    }

}