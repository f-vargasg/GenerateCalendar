using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GenerateCalendar.WinUI
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
            InitMyComponents();
        }



        private void InitMyComponents()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = ConfigurationManager.AppSettings["formCaption"];
            DateTime scrap = DateTime.Now;
            txtMes.Text = scrap.Month.ToString();
            txtYear.Text = scrap.Year.ToString();
        }


        // Function that returns the index of the
        // day for date DD/MM/YYYY 
        public static int DayOfWeek(int day, int month, int year)
        {

            int res = 0;
            /*
            int[] t = { 0, 3, 2, 5, 0, 3, 5, 
                      1, 4, 6, 2, 4 };
            
            year -= ((month < 3) ? 1 : 0);

            res = (year + year / 4 - year / 100 + year / 400
                             + t[month - 1] + day) % 7;
            */
            DateTime scrap = new DateTime(year, month + 1, day + 1);
            res = (int)(scrap.DayOfWeek);
            return res;
        }

        // Function that returns the name of the 
        // month for the given month Number 
        // January - 0, February - 1 and so on 
        public static string GetMonthName(int monthNumber)
        {
            string month = string.Empty;

            switch (monthNumber)
            {
                case 0:
                    month = "January";
                    break;
                case 1:
                    month = "February";
                    break;
                case 2:
                    month = "March";
                    break;
                case 3:
                    month = "April";
                    break;
                case 4:
                    month = "May";
                    break;
                case 5:
                    month = "June";
                    break;
                case 6:
                    month = "July";
                    break;
                case 7:
                    month = "August";
                    break;
                case 8:
                    month = "September";
                    break;
                case 9:
                    month = "October";
                    break;
                case 10:
                    month = "November";
                    break;
                case 11:
                    month = "December";
                    break;
            }
            return month;
        }

        // Function to return the number of days 
        // in a month 
        public static int NumberOfDays(int monthNumber, int year)
        {
            // January 
            if (monthNumber == 0)
                return (31);

            // February 
            if (monthNumber == 1)
            {
                // If the year is leap then Feb 
                // has 29 days 
                if (year % 400 == 0
                    || (year % 4 == 0
                        && year % 100 != 0))
                    return (29);
                else
                    return (28);
            }

            // March 
            if (monthNumber == 2)
                return (31);

            // April 
            if (monthNumber == 3)
                return (30);

            // May 
            if (monthNumber == 4)
                return (31);

            // June 
            if (monthNumber == 5)
                return (30);

            // July 
            if (monthNumber == 6)
                return (31);

            // August 
            if (monthNumber == 7)
                return (31);

            // September 
            if (monthNumber == 8)
                return (30);

            // October 
            if (monthNumber == 9)
                return (31);

            // November 
            if (monthNumber == 10)
                return (30);

            // December 
            if (monthNumber == 11)
                return (31);

            return -1;
        }

        DataTable BuildCalendar(int month, int year)
        {
            DataTable dtRes = null;
            int days;

            try
            {
                dtRes = new DataTable();
                
                dtRes.Columns.Add("Sun");
                dtRes.Columns.Add("Mon");
                dtRes.Columns.Add("Tue");
                dtRes.Columns.Add("Wed");
                dtRes.Columns.Add("Thu");
                dtRes.Columns.Add("Fri");
                dtRes.Columns.Add("Sat");
                // Index of the day from 0 to 6 
                int current = DayOfWeek(0, month, year);

                // i for Iterate through months 
                // j for Iterate through days 
                // of the month - i 

                days = NumberOfDays(month, year);
                string nomCol = string.Empty;

                int k;
                string[] nomColums = {"Sun", "Mon", "Tue", "Wed", "Thu",
                                      "Fri", "Sat"};
                DataRow dr = null;
                k = 0;
                dr = dtRes.NewRow();
                if (current > 0)
                {
                    for (; k < current; k++)
                    {
                        nomCol = nomColums[k];
                        dr[nomCol] = null;      // res += "     ";
                    }
                }
                    

                for (int j = 1; j <= days; j++)
                {
                    nomCol = nomColums[k];
                    dr[nomCol] = j;

                    if ((++k) > 6)
                    {
                        k = 0;
                        dtRes.Rows.Add(dr);
                        dr = dtRes.NewRow();
                    }
                 }

                if (k != 0)   // hay un sobrante
                {
                    dtRes.Rows.Add(dr);
                }
                return dtRes;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Function to print the calendar of 
        // the given year 
        string PrintCalendar(int month, int year)
        {
            string res = string.Empty;
            res = string.Format("     Calendar - {0}"+ Environment.NewLine + Environment.NewLine , year);
            int days;

            // Index of the day from 0 to 6 
            int current = DayOfWeek(0, month, year);

            // i for Iterate through months 
            // j for Iterate through days 
            // of the month - i 

            days = NumberOfDays(month, year);

            // Print the current month name 
            res += string.Format("{0} ------------{1}-------------{2}",
                                 Environment.NewLine,  GetMonthName(month), 
                                 Environment.NewLine);

            // Print the columns 
            res += string.Format(" Sun   Mon  Tue  Wed  Thu  Fri  Sat{0}", 
                                 Environment.NewLine);

            // Print appropriate spaces 
            int k;
            for (k = 0; k < current; k++)
                res += "     ";

            for (int j = 1; j <= days; j++)
            {
                res += string.Format("{0,5:##}", j);

                if ((++k) > 6)
                {
                    k = 0;
                    res += Environment.NewLine;
                }
            }

            if (k == 0)
                res += Environment.NewLine;

            current = k;


            return res;
        }

        private void tlsStrSalir_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void tlsStrDo_Click(object sender, EventArgs e)
        {
            int mes = Convert.ToInt32(txtMes.Text) - 1;
            int year = Convert.ToInt32(txtYear.Text);
            txtOutput.Text = PrintCalendar(mes, year);
            DataTable dt = BuildCalendar(mes, year);
            dgrCalendar.DataSource = dt;
        }
    }




}
