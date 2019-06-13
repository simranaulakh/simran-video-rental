using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VideoRentalSystemY
{
        //public class using all the functions 
    public partial class Form1 : Form
    {
        CustomerRecord Customer = new CustomerRecord();
        Rental rent = new Rental();
        video video = new video();
        private int CustomerID = 0, RentID = 0, VideoID = 0,RentalID=0,VideoCopies=0,VideoPrice=0;

        public Form1()
        {
            InitializeComponent();
        }

        //this validation is for checking taht value is filled in every textbox so that no text box 
        //remain empty and with this fake record cannot be added 
        private void CustomerInsert_Click(object sender, EventArgs e)
        {
            if (CustomerName.Text.ToString().Equals("") && CustomerAddress.Text.ToString().Equals("") && CustomerContact.Text.ToString().Equals(""))
            {
                MessageBox.Show("Fill all the Text Box First");
            }
            else
            {
                var confirmResult = MessageBox.Show("Are you sure to Save this item ??",
                                         "Confirm Save!!",
                                         MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    // If 'Yes', do something here.
                    // if the above validation come true then data will be succesfuly added to the database 
                    
                    String query = "insert into Customer(CustomerName,CustomerAddress,CustomerContact) values('" + CustomerName.Text.ToString() + "','" + CustomerAddress.Text.ToString() + "','" + CustomerContact.Text.ToString() + "')";
                    Customer.Ins(query);
                    MessageBox.Show("Customer Record is Saved");
                    reset();
                }
                // the following message box shows that if something went wrong then this messge will be displayed
                else {
                    MessageBox.Show("Record not Saved");
                }
            }
        }

        private void CustomerDelete_Click(object sender, EventArgs e)
        {
            if (CustomerID > 0)
            {
                //this validations is confirming no one can delete a customer in a single time because sometime 
                // accidently we click on delete button 
                var confirmResult = MessageBox.Show("Are you sure to Delete the Customer ??",
                                         "Confirm Delete!!",
                                         MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {

                    DataTable tbl = new DataTable();
                    String query = "select count(*) from Rental where CustomerID=" + Convert.ToInt32(RentalCustomerID.Text) + " and ReturnDate='Rent'";
                    tbl = rent.Srch(query);

                    if (tbl.Rows.Count == 0)
                    {
                        // If 'Yes', do something here.
                        query = "delete from Customer where ID=" + Convert.ToInt32(RentalCustomerID.Text) + "";
                        Customer.Ins(query);
                        MessageBox.Show("Customer Record is deleted");
                        reset();
                    }
                    else {
                        MessageBox.Show("You already have video on rent return first");
                        reset();
                    }

                }
                else
                {
                    MessageBox.Show("Customer not Deleted");
                }

            }
            else {
                MessageBox.Show("Select the Cusotmer Record First ");
            }



        }

        private void CustomerUpdate_Click(object sender, EventArgs e)
        {
            if (CustomerID > 0)
            {
                var confirmResult = MessageBox.Show("Are you sure to Delete the Customer ??",
                                         "Confirm Delete!!",
                                         MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    // If 'Yes', do something here.
                    String query = "update Customer set CustomerName='"+CustomerName.Text.ToString()+"',CustomerAddress='"+CustomerAddress.Text.ToString()+"',CustomerContact='"+CustomerContact.Text.ToString()+"' where ID=" + Convert.ToInt32(RentalCustomerID.Text) + "";
                    Customer.Ins(query);

                    MessageBox.Show("Customer Record is Updated");
                    reset();

                }
                else
                {
                    MessageBox.Show("Customer is not Updated");
                }
            }
            else {
                MessageBox.Show("Customer Record is not Updated");

            }


        }

        private void VideoReturn_Click(object sender, EventArgs e)
        {
            if (RentalID > 0)
            {
                var confirmResult = MessageBox.Show("Are you sure to Return  the Video ??",
                                         "Confirm Return!!",
                                         MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {

                    String query = "select * from Video where ID="+Convert.ToInt32(RentalVideoID.Text.ToString())+"";
                    DataTable tbl = new DataTable();
                    tbl = rent.Srch(query);
                    VideoPrice =Convert.ToInt32( tbl.Rows[0]["Cost"].ToString());


                    DateTime Current_date = DateTime.Now;

                    //convert the old date from string to Date fromat
                    DateTime Old_date = Convert.ToDateTime(RentalVideoDate.Text.ToString());


                    //get the difference in the days fromat
                    String diff = (Current_date - Old_date).TotalDays.ToString();


                    // calculate the round off value 
                    Double Days = Math.Round(Convert.ToDouble(diff));

                    

                    // return the total cost of the Video


                    VideoPrice=VideoPrice* Convert.ToInt32(Days);





                    // If 'Yes', do something here.
                    query = "update Rental set VideoID=" + Convert.ToInt32(RentalVideoID.Text.ToString())+ ", CustomerID=" + Convert.ToInt32(RentalCustomerID.Text.ToString()) + ",RentalDate='" + RentalVideoDate.Text.ToString() + "',ReturnDate='"+ ReturnVideoDate.Text.ToString()+"' where ID=" + RentalID + "";
                    Customer.Ins(query);

                    MessageBox.Show("Video is renturned and charges are $" + VideoPrice);
                    reset();
                }
                else
                {
                    MessageBox.Show("Video  is not Updated");
                }

            }
            else {

                MessageBox.Show("Select the Rental Video First then you can return  ");
            }


        }

        private void VideoInsert_Click(object sender, EventArgs e)
        {
            if (Title.Text.ToString().Equals("") && Year.Text.ToString().Equals("") && Copies.Text.ToString().Equals("") && Ratting.Text.ToString().Equals("") && Cost.Text.ToString().Equals("") && Plot.Text.ToString().Equals("") && Genre.Text.ToString().Equals(""))
            {
                MessageBox.Show("Fill the Record First");
            }
            else {
                var confirmResult = MessageBox.Show("Are you sure to Save this Video ??",
                                        "Confirm Save!!",
                                        MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    // If 'Yes', do something here.
                    String query = "insert into Video(Title,Ratting,Year,Copies,Cost,Plot,Genre) values('" +Title.Text.ToString() + "','" + Ratting.Text.ToString() + "','" + Year.Text.ToString() + "',"+Convert.ToInt32(Copies.Text.ToString())+","+Convert.ToInt32(Cost.Text.ToString())+",'"+Plot.Text.ToString()+"','"+Genre.Text.ToString()+"')";
                    video.Ins(query);
                    MessageBox.Show("Video Record is saved");
                    reset();
                }
                else
                {
                    MessageBox.Show("Record not Saved");
                }


            }



        }

        private void VideoDelete_Click(object sender, EventArgs e)
        {

            if (VideoID > 0)
            {
                var confirmResult = MessageBox.Show("Are you sure to Delete the Video ??",
                                        "Confirm Delete!!",
                                        MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    DataTable tbl = new DataTable();
                    String query = "select count(*) from Rental where VideoID=" + Convert.ToInt32(RentalVideoID.Text) + " and ReturnDate='Rent'";
                    tbl = rent.Srch(query);

                    if (tbl.Rows.Count == 0)
                    {
                        // If 'Yes', do something here.
                        query = "delete from Video where ID=" + VideoID + "";
                        video.Ins(query);
                        MessageBox.Show("Video record is deleted");
                        reset();
                    }
                    else {
                        MessageBox.Show("THis Video is on rent can't be deleted");
                        reset();
                    }
                }
                else
                {
                    MessageBox.Show("Video not Deleted");
                }

            }
            else {
                MessageBox.Show("Select the Video First to Delete");

            }
        }

        private void VideoUpdate_Click(object sender, EventArgs e)
        {
            if (VideoID > 0)
            {
                var confirmResult = MessageBox.Show("Are you sure to Delete the Customer ??",
                                        "Confirm Delete!!",
                                        MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    // If 'Yes', do something here.
                    String query = "update Video set Title='" + Title.Text.ToString() + "',Ratting='" + Ratting.Text.ToString() + "',Year='" + Year.Text.ToString() + "',Copies="+Convert.ToInt32(Copies.Text.ToString())+",Cost="+Convert.ToInt32(Cost.Text.ToString())+",Plot='"+Plot.Text.ToString()+"' where ID=" + VideoID + "";
                    video.Ins(query);
                    MessageBox.Show("Video Record is Updated");
                    reset();

                }
                else
                {
                    MessageBox.Show("Video Record is not Updated");
                }

            }
            else {
                MessageBox.Show("Select the Video First to Delete from the Record");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void CustomerData_Click(object sender, EventArgs e)
        {
            CustomerID ++;
            String query = "Select * from Customer";
            DataTable table = Customer.Srch(query);
            data.DataSource = table;
            VideoID = 0;
            RentID = 0;
        }

        private void VideoData_Click(object sender, EventArgs e)
        {
            VideoID++;
            String query = "Select * from Video";
            DataTable table = video.Srch(query);
            data.DataSource = table;
            CustomerID = 0;
            RentID = 0;
        }

        private void RentalData_Click(object sender, EventArgs e)
        {
            RentID++;

            String query = "Select * from Rental";
            DataTable table = rent.Srch(query);
            data.DataSource = table;
            VideoID = 0;
            CustomerID= 0;
        }

        private void btnTopCustomer_Click(object sender, EventArgs e)
        {
            String qry = "select CustomerID,ReturnDate, COUNT(*) FROM Rental GROUP BY CustomerID,ReturnDate HAVING COUNT(*)>=1";
            DataTable tb = new DataTable();
            tb = rent.Srch(qry);
            data.DataSource = tb;

            int greater = 0, topID = 0; ;
            for (int y=0;y<data.Rows.Count-1;y++) {
                if (greater< Convert.ToInt32(data.Rows[y].Cells[2].Value)) {
                    greater = Convert.ToInt32(data.Rows[y].Cells[2].Value);
                    topID= Convert.ToInt32(data.Rows[y].Cells[0].Value);
                }
                
            }
            data.DataSource = "";
            MessageBox.Show("Customer ID is==" +topID + " Having Most Video near to=="+greater);
        }

        private void btnMovieCount_Click(object sender, EventArgs e)
        {
            String qry = "select VideoID,ReturnDate, COUNT(*) FROM Rental GROUP BY VideoID,ReturnDate HAVING COUNT(*)>=1";
            DataTable tb = new DataTable();
            tb = rent.Srch(qry);
            data.DataSource = tb;

            int greater = 0, topID = 0; ;
            for (int y = 0; y < data.Rows.Count - 1; y++)
            {
                if (greater < Convert.ToInt32(data.Rows[y].Cells[2].Value))
                {
                    greater = Convert.ToInt32(data.Rows[y].Cells[2].Value);
                    topID = Convert.ToInt32(data.Rows[y].Cells[0].Value);
                }

            }
            data.DataSource = "";
            MessageBox.Show("Video ID is==" + topID + " Having Mostly gone on Rent  near to==" + greater);

        }

        private void Copies_TextChanged(object sender, EventArgs e)
        {
            //dislay the cost of the price of the video after adding the year of the video
            DateTime date = DateTime.Now;

            int year = date.Year;

            int diff = year - Convert.ToInt32(Year.Text.ToString());
            // MessageBox.Show(diff.ToString());
            if (diff >= 5)
            {
                Cost.Text = "2";
            }
            if (diff >= 0 && diff < 5)
            {
                Cost.Text = "5";
            }
        }

        private void data_DoubleClick(object sender, EventArgs e)
        {
            if (VideoID > 0)
            {
                RentalVideoID.Text = data.CurrentRow.Cells[0].Value.ToString();
                Title.Text = data.CurrentRow.Cells[1].Value.ToString();
                Ratting.Text = data.CurrentRow.Cells[2].Value.ToString();
                Year.Text = data.CurrentRow.Cells[3].Value.ToString();
                Copies.Text = data.CurrentRow.Cells[4].Value.ToString();

                VideoCopies = Convert.ToInt32(Copies.Text.ToString());

                Cost.Text = data.CurrentRow.Cells[5].Value.ToString();

                VideoPrice = Convert.ToInt32(Cost.Text.ToString());

                Plot.Text = data.CurrentRow.Cells[6].Value.ToString();
                Genre.Text = data.CurrentRow.Cells[7].Value.ToString();

            }
            else if (RentID > 0)
            {
                RentalID = Convert.ToInt32(data.CurrentRow.Cells[0].Value.ToString());
                RentalVideoID.Text = data.CurrentRow.Cells[1].Value.ToString();
                RentalCustomerID.Text = data.CurrentRow.Cells[2].Value.ToString();
                RentalVideoDate.Text = data.CurrentRow.Cells[3].Value.ToString();
            }
            else if (CustomerID > 0)
            {
                RentalCustomerID.Text = data.CurrentRow.Cells[0].Value.ToString();
                CustomerName.Text = data.CurrentRow.Cells[1].Value.ToString();
                CustomerAddress.Text = data.CurrentRow.Cells[2].Value.ToString();
                CustomerContact.Text = data.CurrentRow.Cells[3].Value.ToString();

            }
        }
        public void reset() {
            Title.Text = "";
            Ratting.Text = "";
            Copies.Text = "";
            Cost.Text = "";
            Year.Text = "";
            Plot.Text = "";
            Genre.Text = "";
            RentalID = 0;
            RentalVideoID.Text = "";
            RentalCustomerID.Text = "";
            CustomerName.Text = "";
            CustomerAddress.Text = "";
            CustomerContact.Text = "";
            data.DataSource = "";
        }

        private void data_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            

        }

        private void VideoRental_Click(object sender, EventArgs e)
        {
            if (RentalVideoID.Text.ToString().Equals("") && RentalCustomerID.Text.ToString().Equals("") && RentalVideoDate.Text.ToString().Equals("") && ReturnVideoDate.Text.ToString().Equals(""))
            {
                MessageBox.Show("Fill the Record Properly");
            }
            else {
                var confirmResult = MessageBox.Show("Are you sure to you want to get the  Video on Rent??",
                                        "Confirm Rental Video!!",
                                        MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {                    
                    String query = "select count(*) from Rental where CustomerID=" + Convert.ToInt32(RentalCustomerID.Text)+ " and ReturnDate='Rent'";

                    String query1 = "select count(*) from Rental where VideoID="+Convert.ToInt32(RentalVideoID.Text.ToString())+" and ReturnDate='Rent'";

                    int diff = video.CountSrch(query1);

                    if (Customer.CountSrch(query) < 2) {
                        if (diff < VideoCopies)
                        {

                            // If 'Yes', do something here.
                            query = "insert into Rental(VideoID,CustomerID,RentalDate,ReturnDate) values(" + Convert.ToInt32(RentalVideoID.Text.ToString()) + "," + Convert.ToInt32(RentalCustomerID.Text.ToString()) + ",'" + RentalVideoDate.Text.ToString() + "','Rent')";
                            rent.Ins(query);
                            MessageBox.Show("Video is on rent ");
                            reset();
                        }
                        else {
                            MessageBox.Show("no more videos are available  ");

                        }

                    }
                    else {
                        MessageBox.Show("You already have 2 Videos on Rent First Return them and then you can get more or Video Copies are booked on Rent ");

                    }

                

                }
                else
                {
                    MessageBox.Show("Video  is not Rented");
                }


            }
        }

        private void RentalVideoDelete_Click(object sender, EventArgs e)
        {
            if (RentalID > 0)
            {
                var confirmResult = MessageBox.Show("Are you sure to Delete the Rental Video ??",
                                     "Confirm Delete!!",
                                     MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    DataTable tbl = new DataTable();

                    String query = "select * from Rental where ID="+RentalID+" and ReturnDate='Rent'";
                    tbl = rent.Srch(query);

                    if (tbl.Rows.Count == 0)
                    {
                        // If 'Yes', do something here.
                        query = "delete from Rental where ID=" + RentalID + "";
                        Customer.Ins(query);
                        MessageBox.Show("Rental Video is Deleted");
                        reset();
                    }
                    else {
                        MessageBox.Show("Video is on Rent so can't be delete this Record");
                    }



                }
                else
                {
                    MessageBox.Show("Customer not Deleted");
                }


            }
            else {
                MessageBox.Show("Select the Rental Video First");
            }


        }
    }
}
