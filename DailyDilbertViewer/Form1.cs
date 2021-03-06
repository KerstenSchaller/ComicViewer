﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DailyDilbertViewer
{
    public partial class Form1 : Form
    {
        DilbertReceiver receiver;
        DateTime date;

        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        { 
            receiver = new DilbertReceiver(this.pictureBox_dilbertComic,this.listBox_Dates, this.listBox_tags);
            date = DateTime.Now;
            listBox_Dates.SelectedIndex = listBox_Dates.Items.Count - 1;
            setComic(date);
            
        }

        private void setComic(DateTime date)
        {
            if (receiver != null)
            {
                receiver.getDilbertComicImageByDate(date);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Left)  minusOneDay();
            if (keyData == Keys.Right) plusOneDay();
            return true;
        }

        private void button_date_forward_Click(object sender, EventArgs e)
        {
            plusOneDay();
        }

        private void button_date_back_Click(object sender, EventArgs e)
        {
            minusOneDay();
        }

        public void minusOneDay()
        {
            indexChangedEventActive = false;
            date -= new TimeSpan(1, 0, 0, 0);
            setComic(date);
            if (listBox_Dates.SelectedIndex > 0) listBox_Dates.SelectedIndex -= 1;
            indexChangedEventActive = true;
        }

        public void plusOneDay()
        {
            indexChangedEventActive = false;
            date += new TimeSpan(1, 0, 0, 0);
            setComic(date);
            if (listBox_Dates.SelectedIndex < listBox_Dates.Items.Count - 1) listBox_Dates.SelectedIndex += 1;
            indexChangedEventActive = true;
        }

 

        bool indexChangedEventActive = true;
        private void listBox_Dates_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (indexChangedEventActive)
            {
                DateTime selectedDate = (DateTime)listBox_Dates.SelectedItem;
                setComic(selectedDate);
            }
        }

        private void pictureBox_dilbertComic_SizeChanged(object sender, EventArgs e)
        {
            Size oldsize = this.listBox_Dates.Size;
            Size newsize = oldsize;
            newsize.Height = this.pictureBox_dilbertComic.Size.Height;
            this.listBox_Dates.Size = newsize;
            this.listBox_tags.Size = newsize;
            this.Size = newsize;
            
        }

        private bool menu_visible = false;
        private void pictureBox_dilbertComic_Click(object sender, EventArgs e)
        {
            bool state;
            if (menu_visible)
            {
                state = false;
                button_date_back.Visible = state;
                button_date_forward.Visible = state;
                listBox_Dates.Visible = state;
                listBox_tags.Visible = state;
                pictureBox_dilbertComic.Location = new Point(0,0);
                menu_visible = false;
            }
            else
            {
                state = true;
                button_date_back.Visible = state;
                button_date_forward.Visible = state;
                listBox_Dates.Visible = state;
                listBox_tags.Visible = state;
                pictureBox_dilbertComic.Location = new Point(285, 41);
                menu_visible = true;
            }
        }

        private void listBox_tags_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
