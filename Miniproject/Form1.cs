using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Miniproject
{
    public partial class Form1 : Form
    {
        // DataBase connection.
        SqlConnection connec = new SqlConnection("Data Source=HAIER-PC;Initial Catalog=ProjectB;Integrated Security=True");
        SqlCommand cmmd;
        SqlDataAdapter adapt;
        //ID variable is used for update and delete.
        int ID = 0;

        public Form1()
        {
            InitializeComponent();
        }
        //It will store all studnet information in DB.
        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                
                connec.Open();
                cmmd = new SqlCommand("insert into Student(FirstName, LastName, Contact, Email, RegistrationNumber, Status)values(@FirstName, @LastName, @Contact, @Email, @RegistrationNumber, @Status)", connec);
                cmmd.Parameters.AddWithValue("@FirstName", textBox2.Text);
                cmmd.Parameters.AddWithValue("@LastName", textBox3.Text);
                cmmd.Parameters.AddWithValue("@Contact", textBox4.Text);
                cmmd.Parameters.AddWithValue("@Email", textBox5.Text);
                cmmd.Parameters.AddWithValue("@RegistrationNumber", textBox6.Text);
               var  tem = 0;
                if(Activebtn.Checked)
                {
                    tem = 5;
                }
                else
                {
                    tem = 6;
                }

                cmmd.Parameters.AddWithValue("@Status", tem);
                cmmd.ExecuteNonQuery();
                connec.Close();
                MessageBox.Show("Saved");
                DisplayDataInGridView("select *from Student", dataGridView1);
             


            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connec.Close();
            }
        }
            
   
    //Function used to display data in gridview.
    private void DisplayDataInGridView(string q, DataGridView g)
        {
           // "select *from Student"
            connec.Open();
            DataTable table = new DataTable();
            adapt = new SqlDataAdapter(q, connec);
            adapt.Fill(table);
            g.DataSource = table;
            //dataGridView2.DataSource = table;
            connec.Close();
        }
        //It will clear the data of these textbox while updating. 
        private void ClearData()
        {
            textBox2.Text = "";
            textBox3.Text = "";
            //ID = 0;
            connec.Open();


        }
        //Show data in gridview.
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //dataGridView1.ColumnCount = 6;
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            textBox6.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            //textBox7.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            string a = "select *from Student";
            DisplayDataInGridView(a, dataGridView1);
           int index = dataGridView1.CurrentCell.RowIndex;
            DataGridViewRow r = dataGridView1.Rows[e.RowIndex];
            ID = Convert.ToInt32(r.Cells[0].Value);


           
        }
        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            textBox6.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            //textBox7.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
        }
        //It will update data in student table .
        private void button3_Click(object sender, EventArgs e)
        {try
            {
                cmmd = new SqlCommand("update Student set FirstName=@FirstName,LastName=@ LastName, Contact=@Contact, Email=@Email, RegistrationNumber=@RegistrationNumber, Status=@Status  where Id=@Id ", connec);
                connec.Open();
                cmmd.Parameters.AddWithValue("@FirstName", textBox2.Text);
                cmmd.Parameters.AddWithValue("@LastName", textBox3.Text);
                cmmd.Parameters.AddWithValue("@Contact", textBox4.Text);
                cmmd.Parameters.AddWithValue("@Email", textBox5.Text);
                cmmd.Parameters.AddWithValue("@RegistrationNumber", textBox6.Text);
               // cmmd.Parameters.AddWithValue("@Status", textBox7.Text);
                cmmd.ExecuteNonQuery();
                connec.Close();
                MessageBox.Show("Updated Successfully");
                DisplayDataInGridView("select *from Student", dataGridView1);
               
                ClearData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connec.Close();
            }
        }
        //It will delete the whole tuple .
        private void button2_Click(object sender, EventArgs e)
        {
           // @RegistrationNumber
            try {
                cmmd = new SqlCommand("delete  From Student where Id = " + ID, connec);
                connec.Open();
               
                //cmmd.Parameters.AddWithValue("@Id", connec.ClientConnectionId);
                cmmd.ExecuteNonQuery();
                connec.Close();
                MessageBox.Show("Record Deleted Successfully!");
                DisplayDataInGridView("select *from Student", dataGridView1);
                ClearData();
               }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connec.Close();
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
        //It will show previous data in Db on page loading.
        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'projectBDataSet14.Clo' table. You can move, or remove it, as needed.
            this.cloTableAdapter1.Fill(this.projectBDataSet14.Clo);
            // TODO: This line of code loads data into the 'projectBDataSet13.AssessmentComponent' table. You can move, or remove it, as needed.
            this.assessmentComponentTableAdapter1.Fill(this.projectBDataSet13.AssessmentComponent);
            // TODO: This line of code loads data into the 'projectBDataSet12.Assessment' table. You can move, or remove it, as needed.
            this.assessmentTableAdapter1.Fill(this.projectBDataSet12.Assessment);
            // TODO: This line of code loads data into the 'projectBDataSet11.Rubric' table. You can move, or remove it, as needed.
            this.rubricTableAdapter3.Fill(this.projectBDataSet11.Rubric);
            // TODO: This line of code loads data into the 'projectBDataSet10.Student' table. You can move, or remove it, as needed.
            this.studentTableAdapter1.Fill(this.projectBDataSet10.Student);
            // TODO: This line of code loads data into the 'projectBDataSet9.Lookup' table. You can move, or remove it, as needed.
            this.lookupTableAdapter1.Fill(this.projectBDataSet9.Lookup);
            // TODO: This line of code loads data into the 'projectBDataSet8.Student' table. You can move, or remove it, as needed.
            this.studentTableAdapter.Fill(this.projectBDataSet8.Student);
            // TODO: This line of code loads data into the 'projectBDataSet7.ClassAttendance' table. You can move, or remove it, as needed.
            this.classAttendanceTableAdapter.Fill(this.projectBDataSet7.ClassAttendance);
            // TODO: This line of code loads data into the 'projectBDataSet6.Assessment' table. You can move, or remove it, as needed.
            this.assessmentTableAdapter.Fill(this.projectBDataSet6.Assessment);
            // TODO: This line of code loads data into the 'projectBDataSet5.Rubric' table. You can move, or remove it, as needed.
            this.rubricTableAdapter2.Fill(this.projectBDataSet5.Rubric);
            // TODO: This line of code loads data into the 'projectBDataSet4.Lookup' table. You can move, or remove it, as needed.
            this.lookupTableAdapter.Fill(this.projectBDataSet4.Lookup);
            // TODO: This line of code loads data into the 'projectBDataSet3.Rubric' table. You can move, or remove it, as needed.
            this.rubricTableAdapter1.Fill(this.projectBDataSet3.Rubric);
            // TODO: This line of code loads data into the 'projectBDataSet2.AssessmentComponent' table. You can move, or remove it, as needed.
            this.assessmentComponentTableAdapter.Fill(this.projectBDataSet2.AssessmentComponent);
            // TODO: This line of code loads data into the 'projectBDataSet1.Clo' table. You can move, or remove it, as needed.
            this.cloTableAdapter.Fill(this.projectBDataSet1.Clo);
            // TODO: This line of code loads data into the 'projectBDataSet.Rubric' table. You can move, or remove it, as needed.
            this.rubricTableAdapter.Fill(this.projectBDataSet.Rubric);
            SqlDataAdapter da = new SqlDataAdapter("select *from Student", connec);
            DataSet ds = new DataSet();
            da.Fill(ds,"Student");
            dataGridView1.DataSource = ds.Tables["Student"].DefaultView;

        }
        private void DisplayInGridView()
        {
            connec.Open();
            DataTable table = new DataTable();
            adapt = new SqlDataAdapter("select *from Clo", connec);
            adapt.Fill(table);
            dataGridView2.DataSource = table;
            connec.Close();

        }
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                cmmd = new SqlCommand("insert into Clo (Name,DateCreated,DateUpdated)values(@Name,@DateCreated,@DateUpdated )", connec);
                connec.Open();
                cmmd.Parameters.AddWithValue("@Name", textBox8.Text);
                cmmd.Parameters.AddWithValue("@DateCreated", dateTimePicker1.Value);
                cmmd.Parameters.AddWithValue("@DateUpdated", dateTimePicker2.Value);
                cmmd.ExecuteNonQuery();
                connec.Close();
                
                MessageBox.Show("Saved");
                DisplayDataInGridView("select *from Clo", dataGridView2);
            }

        
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connec.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                cmmd = new SqlCommand("delete Clo where Id =  " + ID, connec);
                connec.Open();
                
               // cmmd.Parameters.AddWithValue("@Name", connec.ClientConnectionId);
                cmmd.ExecuteNonQuery();
                connec.Close();
                MessageBox.Show("Record Deleted Successfully!");
                DisplayDataInGridView("select *from Student", dataGridView2);
                ClearData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connec.Close();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                cmmd = new SqlCommand("update Clo set Name=@Name, DateCreated=@DateCreated,DateUpdated =@DateUpdated  where Id=@Id ", connec);
                connec.Open();
                cmmd.Parameters.AddWithValue("@Name", textBox8.Text);
                cmmd.Parameters.AddWithValue("@DateCreated", dateTimePicker1.Text);
                cmmd.Parameters.AddWithValue("@DateUpdated",dateTimePicker2.Text);
                
                //cmd.ExecuteNonQuery();
                connec.Close();
                MessageBox.Show("Updated Successfully");
                DisplayDataInGridView("select *from Student", dataGridView2);
                ClearData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connec.Close();
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox8.Text = dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
           dateTimePicker1.Text = dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString();
            dateTimePicker2.Text = dataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString();
            DisplayDataInGridView("select *from Clo", dataGridView2);
           
            int index = dataGridView2.CurrentCell.RowIndex;
            DataGridViewRow r = dataGridView2.Rows[index];
             ID = Convert.ToInt32(r.Cells[0].Value);

        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                connec.Open();
                SqlDataReader Reader;

                int Id = 0;
                cmmd = new SqlCommand("SELECT COUNT(*) FROM Rubric", connec);
               // cmmd.ExecuteNonQuery();
                Reader = cmmd.ExecuteReader();
                while (Reader.Read()) {
                    Id = Convert.ToInt32(Reader[0])+1;
                }
                connec.Close();
                    cmmd = new SqlCommand("insert into Rubric(Id,Details,CloId ) values(@Id,@Details, @CloId)", connec);
                
                connec.Open();
                cmmd.Parameters.AddWithValue("@Id", Id);
                cmmd.Parameters.AddWithValue("@Details", textBox13.Text);
                cmmd.Parameters.AddWithValue("@CloId ", comboBox2.Text);
                cmmd.ExecuteNonQuery();
                connec.Close();
                MessageBox.Show("Saved");
                DisplayDataInGridView("select *from Rubric", dataGridView3);
            }


            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connec.Close();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                cmmd = new SqlCommand("delete Rubric where Id=  " + ID, connec);
                connec.Open();
               // cmmd.Parameters.AddWithValue("@Id", connec.ClientConnectionId);
                cmmd.ExecuteNonQuery();
                connec.Close();
                MessageBox.Show("Record Deleted Successfully!");
                DisplayDataInGridView("select *from Rubric", dataGridView3);
             
                ClearData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connec.Close();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                cmmd = new SqlCommand("update Rubric set Details =@Details, CloId = @CloId where Id=@Id ", connec);
                connec.Open();
                cmmd.Parameters.AddWithValue("@Details", textBox13.Text);
                cmmd.Parameters.AddWithValue("@CloId", comboBox2.Text);
                //cmmd.Parameters.AddWithValue("@Id", ID);

                //cmmd.ExecuteNonQuery();
                connec.Close();
                MessageBox.Show("Updated Successfully");
                DisplayDataInGridView("select *from Rubric", dataGridView3);
               
                ClearData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connec.Close();
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                //connec.Open();
                cmmd = new SqlCommand("insert into RubricLevel (RubricId,Details ,MeasurementLevel)values(@RubricId,@Details,  @MeasurementLevel)", connec);
               
                cmmd.Parameters.AddWithValue("@RubricId", comboBox3.Text);
                
                cmmd.Parameters.AddWithValue("@Details ", textBox15.Text);
                cmmd.Parameters.AddWithValue("@MeasurementLevel ", textBox16.Text);
                connec.Open();
                cmmd.ExecuteNonQuery();
                connec.Close();
                DisplayDataInGridView("select *from RubricLevel", dataGridView4);
               
                MessageBox.Show("Saved");
                
            }


            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connec.Close();
            }

        }

        private void button11_Click(object sender, EventArgs e)
        {

            try
            {
                cmmd = new SqlCommand("delete RubricLevel where Id=  " + ID, connec);
                connec.Open();
               // cmmd.Parameters.AddWithValue("@Id", connec.ClientConnectionId);
                cmmd.ExecuteNonQuery();
                connec.Close();
                MessageBox.Show("Record Deleted Successfully!");
                DisplayDataInGridView("select *from RubricLevel", dataGridView4);
              
                ClearData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connec.Close();
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            try
            {
                cmmd = new SqlCommand("update RubricLevel set RubricId = @RubricId,Details =@Details, MeasurementLevel = @MeasurementLevel ", connec);
                connec.Open();
                cmmd.Parameters.AddWithValue("@RubricId", comboBox3.Text);
                cmmd.Parameters.AddWithValue("@Details ", textBox15.Text);
                cmmd.Parameters.AddWithValue("@MeasurementLevel ", textBox16.Text);


               cmmd.ExecuteNonQuery();
                connec.Close();
                MessageBox.Show("Updated Successfully");
                DisplayDataInGridView("select *from RubricLevel", dataGridView4);
               
                ClearData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connec.Close();
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            try
            {/*
                cmmd = new SqlCommand("insert into Lookup (LookupId,Name ,Category)values( @LookupId,@Name, @Category", connec);

                connec.Open();
                cmmd.Parameters.AddWithValue("@LookupId", txtlook.Text);
                cmmd.Parameters.AddWithValue("@Name", txtname.Text);
                cmmd.Parameters.AddWithValue("@Category", txtcat.Text);
                cmmd.ExecuteNonQuery();

                connec.Close();
                MessageBox.Show("Saved");*/
            }


            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connec.Close();
            }

        }

        private void button14_Click(object sender, EventArgs e)
        {
            try
            {
                cmmd = new SqlCommand("delete Lookup where Id = @Id", connec);
                connec.Open();
                cmmd.Parameters.AddWithValue("@Id", connec.ClientConnectionId);
                cmmd.ExecuteNonQuery();
                connec.Close();
                MessageBox.Show("Record Deleted Successfully!");
                //DisplayDataInGridView();
                ClearData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connec.Close();
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            try
            {
               /* cmmd = new SqlCommand("update Lookup set LookupId= @LookupId,Name=@Name, Category = @Category ", connec);
                connec.Open();
                cmmd.Parameters.AddWithValue("@LookupId", txtlook.Text);
                cmmd.Parameters.AddWithValue("@Name", txtname.Text);
                cmmd.Parameters.AddWithValue("@Category", txtcat.Text);


                //cmd.ExecuteNonQuery();
                connec.Close();
                MessageBox.Show("Updated Successfully");
                //DisplayDataInGridView();
                ClearData();*/
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connec.Close();
            }
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            comboBox2.Text = dataGridView3.Rows[e.RowIndex].Cells[2].Value.ToString();
            textBox13.Text = dataGridView3.Rows[e.RowIndex].Cells[1].Value.ToString();
           //comboBox2.Text = dataGridView3.Rows[e.RowIndex].Cells[2].Value.ToString();
            //DisplayDataInGridView("select *from Rubric", dataGridView3);
            string b = "select *from Rubric";
            DisplayDataInGridView(b, dataGridView3);
            int index = dataGridView3.CurrentCell.RowIndex;
            DataGridViewRow r = dataGridView3.Rows[e.RowIndex];
            ID = Convert.ToInt32(r.Cells[0].Value);
        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            comboBox3.Text = dataGridView4.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox15.Text = dataGridView4.Rows[e.RowIndex].Cells[2].Value.ToString();
            textBox16.Text = dataGridView4.Rows[e.RowIndex].Cells[3].Value.ToString();
            
        }

        private void dataGridView5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {/*
            txtlook.Text = dataGridView5.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtname.Text = dataGridView5.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtcat.Text = dataGridView5.Rows[e.RowIndex].Cells[3].Value.ToString();
            //DisplayDataInGridView();*/
        }

        private void Addbtn_Click(object sender, EventArgs e)
        {
            try
            {
                
                cmmd = new SqlCommand("insert into AssessmentComponent (Name,RubricId,TotalMarks,DateCreated,DateUpdated,AssessmentId)values(@Name,@RubricId,@TotalMarks,@DateCreated,@DateUpdated, @AssessmentId)", connec);
                //connec.Open();
                cmmd.Parameters.AddWithValue("@Name", txtname1.Text);
                cmmd.Parameters.AddWithValue("@RubricId", RubricISd.Text);
                cmmd.Parameters.AddWithValue("@TotalMarks", txtmarks.Text);
                cmmd.Parameters.AddWithValue("@DateCreated", dateTimePicker3.Value);
                cmmd.Parameters.AddWithValue("@DateUpdated", dateTimePicker4.Value);
                cmmd.Parameters.AddWithValue("@AssessmentId", comboBox6.Text);
                DisplayDataInGridView("select *from AssessmentComponent", dataGridView6);
                connec.Open();
                cmmd.ExecuteNonQuery();
                connec.Close();

                MessageBox.Show("Saved");
            }


            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connec.Close();
            }
        }

        private void Updatebtn_Click(object sender, EventArgs e)
        {
            try
            {
                cmmd = new SqlCommand("update AssessmentComponent set Name=@Name, TotalMarks = @TotalMarks,DateCreated=@DateCreated, DateUpdated=@DateUpdated ", connec);
                
                cmmd.Parameters.AddWithValue("@Name", txtname1.Text);
                cmmd.Parameters.AddWithValue("@RubricId", RubricISd.Text);
                cmmd.Parameters.AddWithValue("@TotalMarks", txtmarks.Text);
                cmmd.Parameters.AddWithValue("@DateCreated", dateTimePicker3.Value);
                cmmd.Parameters.AddWithValue("@DateUpdated", dateTimePicker4.Value);
                cmmd.Parameters.AddWithValue("@AssessmentId", comboBox6.Text);
                connec.Open();
                cmmd.ExecuteNonQuery();
                connec.Close();
                MessageBox.Show("Updated Successfully");
                DisplayDataInGridView("select *from AssessmentComponent",dataGridView6);
                
                ClearData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connec.Close();
            }
        }

        private void dataGridView6_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtname1.Text = dataGridView6.Rows[e.RowIndex].Cells[1].Value.ToString();
            RubricISd.Text = dataGridView6.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtmarks.Text = dataGridView6.Rows[e.RowIndex].Cells[3].Value.ToString();
            dateTimePicker3.Text = dataGridView6.Rows[e.RowIndex].Cells[4].Value.ToString();
            dateTimePicker4.Text = dataGridView6.Rows[e.RowIndex].Cells[5].Value.ToString();
            comboBox6.Text = dataGridView6.Rows[e.RowIndex].Cells[6].Value.ToString();
            DisplayDataInGridView("select *from AssessmentComponent", dataGridView6);
            /*string a = "select *from AssessmentComponent";
            DisplayDataInGridView(a, dataGridView6);
            int index = dataGridView6.CurrentCell.RowIndex;
            DataGridViewRow r = dataGridView6.Rows[e.RowIndex];
            ID = Convert.ToInt32(r.Cells[2].Value);*/
        }

        private void insertbtn_Click(object sender, EventArgs e)
        {
            try
            {
                connec.Open();
                cmmd = new SqlCommand("insert into ClassAttendance (AttendanceDate)values(@AttendanceDate )", connec);
                //connec.Open();
                cmmd.Parameters.AddWithValue("@AttendanceDate", dateTimePicker5.Value);
                connec.Close();
                DisplayDataInGridView("select *from ClassAttendance", dataGridView7);
                connec.Open();
                cmmd.ExecuteNonQuery();
                connec.Close();

                MessageBox.Show("Saved");
            }


            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connec.Close();
            }
        }

        private void dataGridView7_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dateTimePicker5.Text = dataGridView7.Rows[e.RowIndex].Cells[1].Value.ToString();
            DisplayDataInGridView("select *from ClassAttendance", dataGridView7);
        }

        private void updatebutton_Click(object sender, EventArgs e)
        {
            try
            {
                connec.Open();
                cmmd = new SqlCommand("update  ClassAttendance set AttendanceDate = @AttendanceDate ", connec);
                //connec.Open();
                cmmd.Parameters.AddWithValue("@AttendanceDate", dateTimePicker5.Value);
                connec.Close();
                DisplayDataInGridView("select *from ClassAttendance", dataGridView7);
                connec.Open();
                MessageBox.Show("Updated Successfully");
                cmmd.ExecuteNonQuery();
                connec.Close();
            }


            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connec.Close();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                
                cmmd = new SqlCommand("insert into StudentAttendance (AttendanceId,StudentId,AttendanceStatus)values(@AttendanceId,@StudentId,  @AttendanceStatus)", connec);
                
                cmmd.Parameters.AddWithValue("@AttendanceId", comboBox4.Text);

                cmmd.Parameters.AddWithValue("@StudentId", comboBox5.Text);
                cmmd.Parameters.AddWithValue("@AttendanceStatus",Convert.ToInt32( comboBox7.Text));
                connec.Open();
                cmmd.ExecuteNonQuery();
                connec.Close();
                DisplayDataInGridView("select *from StudentAttendance", dataGridView8);
                
                MessageBox.Show("Saved");
                
            }


            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connec.Close();
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            try
            {
                
                cmmd = new SqlCommand("insert into Assessment(Title,DateCreated,TotalMarks,TotalWeightage)values(@Title,@DateCreated,@TotalMarks,@TotalWeightage)", connec);
                cmmd.Parameters.AddWithValue("@Title", textBox17.Text);
                cmmd.Parameters.AddWithValue("@DateCreated", dateTimePicker6.Value);
                cmmd.Parameters.AddWithValue("@TotalMarks", Convert.ToInt32(textBox19.Text));
                cmmd.Parameters.AddWithValue("@TotalWeightage", Convert.ToInt32(textBox20.Text));
                //cmmd.Parameters.AddWithValue("@DateUpdated", dateTimePicker2.Value);
                DisplayDataInGridView("select *from Assessment", dataGridView9);
                connec.Open();
                cmmd.ExecuteNonQuery();
                connec.Close();

                MessageBox.Show("Saved");
            }


            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connec.Close();
            }
        }

        private void delBtnClick_Click(object sender, EventArgs e)
        {
            try
            {
                cmmd = new SqlCommand("delete Assessment where Id = @ID", connec);
                connec.Open();

                // cmmd.Parameters.AddWithValue("@Name", connec.ClientConnectionId);
                cmmd.ExecuteNonQuery();
                connec.Close();
                MessageBox.Show("Record Deleted Successfully!");
                DisplayDataInGridView("select *from Assessment", dataGridView9);
                //ClearData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connec.Close();
            }

        }

        private void Deletebtn_Click(object sender, EventArgs e)
        {
            try
            {
                cmmd = new SqlCommand("delete AssessmentComponent where Id = @ID", connec);
                connec.Open();

                // cmmd.Parameters.AddWithValue("@Name", connec.ClientConnectionId);
                cmmd.ExecuteNonQuery();
                connec.Close();
                MessageBox.Show("Record Deleted Successfully!");
                DisplayDataInGridView("select *from AssessmentComponent", dataGridView6);
                //ClearData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connec.Close();
            }
        }

        private void UpdateClickBtn_Click(object sender, EventArgs e)
        {
            try
            {
                cmmd = new SqlCommand("update Assessment set Title=@Title,DateCreated =@DateCreated, TotalMarks = @TotalMarks,TotalWeightage=@TotalWeightage ", connec);
               
                cmmd.Parameters.AddWithValue("@Title", textBox17.Text);
                cmmd.Parameters.AddWithValue("@DateCreated", dateTimePicker6.Value);
                cmmd.Parameters.AddWithValue("@TotalMarks", textBox19.Text);
                cmmd.Parameters.AddWithValue("@TotalWeightage",textBox20.Text);
                //cmmd.Parameters.AddWithValue("@DateCreated", dateTimePicker6.Value);
                connec.Open();
                cmmd.ExecuteNonQuery();
                connec.Close();
                MessageBox.Show("Updated Successfully");
                DisplayDataInGridView("select *from Assessment", dataGridView9);
                //connec.Close();
                ClearData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connec.Close();
            }
        }

        private void deleteclickbtn_Click(object sender, EventArgs e)
        {
            try
            {
                cmmd = new SqlCommand("delete StudentAttendance where StudentId= @StudentId", connec);
                connec.Open();
                cmmd.ExecuteNonQuery();
                connec.Close();
                MessageBox.Show("Record Deleted Successfully!");
                DisplayDataInGridView("select *from StudentAttendance", dataGridView8);

                ClearData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connec.Close();
            }
        }

        private void Updatebuton_Click(object sender, EventArgs e)
        {
            try
            {
                cmmd = new SqlCommand("Update StudentAttendance set AttendanceId=@AttendanceId, StudentId= @StudentId, AttendaceStatus=@AttendaceStatus", connec);
                cmmd.Parameters.AddWithValue("@AttendanceId", comboBox4.Text);

                cmmd.Parameters.AddWithValue("@StudentId", comboBox5.Text);
                cmmd.Parameters.AddWithValue("@AttendaceStatus", comboBox7.Text);
                connec.Open();
                cmmd.ExecuteNonQuery();
                connec.Close();
                MessageBox.Show("Update Successfully!");
                DisplayDataInGridView("select *from StudentAttendance", dataGridView8);

                ClearData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connec.Close();
            }
        }

        private void delbtn_Click(object sender, EventArgs e)
        {
            try
            {
                cmmd = new SqlCommand("delete  From ClassAttendance where Id = " + ID, connec);
                connec.Open();
                cmmd.ExecuteNonQuery();
                connec.Close();
                MessageBox.Show("Record Deleted Successfully!");
                DisplayDataInGridView("select *from ClassAttendance", dataGridView7);
                ClearData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connec.Close();
            }
        }

        private void Addtxtbtn_Click(object sender, EventArgs e)
        {
            connec.Open();
            //DateCreated is set datetime .Now because it gets the date when it created 
            //and update date is also using the dateTime.Now because it also gives the date when it is updating 
            //but now the date created remain the same
            //string query3 = "Insert INTO StudentResult(StudentId,AssessmentComponentId,RubricMeasurementId,EvaluationDate) VALUES((select Id from Student where Id='" + Convert.ToInt32( comboBox8.SelectedValue) + "'),(select Id from AssessmentComponent where Id='" + Convert.ToInt32(comboBox11.SelectedValue) + "' ),(select Id from RubricLevel where RubricId='" + textBox7.Text + "'),'" + dateTimePicker7.Value.Date + "')";
            string query3 = "Insert INTO StudentResult(StudentId,AssessmentComponentId,RubricMeasurementId,EvaluationDate) VALUES((select Id from Student where Id='" + comboBox8.SelectedValue + "'),(select Id from AssessmentComponent where Id='" + comboBox11.SelectedValue + "' ),(select Id from RubricLevel where RubricId='" + textBox7.Text + "'),'" + dateTimePicker7.Value.Date + "')";
            SqlCommand cmmd = new SqlCommand(query3, connec);
            cmmd.ExecuteNonQuery();
            connec.Close();


            MessageBox.Show(" Added");/* ass view

            connec.Open();
            string fun3 = "SELECT AssessmentComponent.Name AS Component ,Rubric.Details AS Rubric, AssessmentComponent.TotalMarks AS Component_Marks,((RubricLevel.MeasurementLevel*AssessmentComponent.TotalMarks)/4) AS ObtainedMarks,Student.FirstName,Student.Id  from StudentResult inner join Student on Student.Id=StudentResult.StudentId and Student.Id='" + comboBox8.SelectedValue + "' left join AssessmentComponent on StudentResult.AssessmentComponentId=AssessmentComponent.Id left join Rubric on Rubric.Id=AssessmentComponent.RubricId left join RubricLevel on  RubricLevel.Id=StudentResult.RubricMeasurementId";

            SqlDataAdapter numpy = new SqlDataAdapter(fun3, connec);

            DataTable ta = new DataTable();
            numpy.Fill(design);
            dataGridView8.DataSource = design;
            connec.Close();*/

        }

        private void button18_Click(object sender, EventArgs e)
        {
            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
            PdfWriter pdf = PdfWriter.GetInstance(doc, new FileStream("Assessment.pdf", FileMode.Create));
            doc.Open();


            PdfPTable table = new PdfPTable(dataGridView10.Columns.Count);
            for (int j = 0; j < dataGridView10.Columns.Count; j++)
            {
                table.AddCell(new Phrase(dataGridView10.Columns[j].HeaderText));

            }
            table.HeaderRows = 1;
            for (int k = 0; k < dataGridView10.Rows.Count; k++)
            {
                for (int w = 0; w < dataGridView10.Columns.Count; w++)
                {
                    if (dataGridView10[w, k].Value != null)
                    {
                        table.AddCell(new Phrase(dataGridView10[w, k].Value.ToString()));

                    }

                }
            }
            doc.Add(table);
            doc.Close();




        }

        private void Assreportbtn_Click(object sender, EventArgs e)
        {

        }

        private void cloreportbtn_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
            PdfWriter pdf = PdfWriter.GetInstance(doc, new FileStream("CLO.pdf", FileMode.Create));
            doc.Open();


            PdfPTable table = new PdfPTable(dataGridView11.Columns.Count);
            for (int j = 0; j < dataGridView11.Columns.Count; j++)
            {
                table.AddCell(new Phrase(dataGridView11.Columns[j].HeaderText));

            }
            table.HeaderRows = 1;
            for (int k = 0; k < dataGridView11.Rows.Count; k++)
            {
                for (int w = 0; w < dataGridView11.Columns.Count; w++)
                {
                    if (dataGridView11[w, k].Value != null)
                    {
                        table.AddCell(new Phrase(dataGridView11[w, k].Value.ToString()));

                    }

                }
            }
            doc.Add(table);
            doc.Close();
        }

        private void button16_Click_1(object sender, EventArgs e)
        {
            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
            PdfWriter pdf = PdfWriter.GetInstance(doc, new FileStream("CLO.pdf", FileMode.Create));
            doc.Open();


            PdfPTable table = new PdfPTable(dataGridView2.Columns.Count);
            for (int j = 0; j < dataGridView2.Columns.Count; j++)
            {
                table.AddCell(new Phrase(dataGridView2.Columns[j].HeaderText));

            }
            table.HeaderRows = 1;
            for (int k = 0; k < dataGridView2.Rows.Count; k++)
            {
                for (int w = 0; w < dataGridView2.Columns.Count; w++)
                {
                    if (dataGridView2[w, k].Value != null)
                    {
                        table.AddCell(new Phrase(dataGridView2[w, k].Value.ToString()));

                    }

                }
            }
            doc.Add(table);
            doc.Close();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
            PdfWriter pdf = PdfWriter.GetInstance(doc, new FileStream("Assessment.pdf", FileMode.Create));
            doc.Open();


            PdfPTable table = new PdfPTable(dataGridView9.Columns.Count);
            for (int j = 0; j < dataGridView9.Columns.Count; j++)
            {
                table.AddCell(new Phrase(dataGridView9.Columns[j].HeaderText));

            }
            table.HeaderRows = 1;
            for (int k = 0; k < dataGridView9.Rows.Count; k++)
            {
                for (int w = 0; w < dataGridView9.Columns.Count; w++)
                {
                    if (dataGridView9[w, k].Value != null)
                    {
                        table.AddCell(new Phrase(dataGridView9[w, k].Value.ToString()));

                    }

                }
            }
            doc.Add(table);
            doc.Close();



        }

        private void textBox2_TextChanged(object sender, KeyPressEventArgs e)
        {
            //Validation of the First Name
          /*  if (e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == Keys.Back || e.KeyChar== (char)Keys.Space))
            {
                e.Handled = true;
                base.OnKeyPress(e);
                MessageBox.Show("Only Alphabets are allowed in this field");
            }*/
        }

        private void RubricISd_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
          /*  if (e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space))
            {
                e.Handled = true;
                base.OnKeyPress(e);
                MessageBox.Show("Only Alphabets are allowed in this field");
            }*/
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            //Validation of the Last Name
           /* if (e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space))
            {
                e.Handled = true;
                base.OnKeyPress(e);
                MessageBox.Show("Only Alphabets are allowed in this field");
            }*/
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            //Validation of the PhoneNumber
           /* if (e.Handled = !(char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space))
            {
                e.Handled = true;
                base.OnKeyPress(e);
                MessageBox.Show("Only Digits are allowed in this field");
            }
            if (txt_Contact.Text.Length == 11)
            {
                e.Handled = true;
                base.OnKeyPress(e);
                MessageBox.Show("Phone Number contains only 11 digits");
            }*/
        }
    }
    }
    
    


