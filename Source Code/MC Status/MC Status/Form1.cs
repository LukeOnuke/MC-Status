using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MC_Status
{
    public partial class MCStatus : Form
    {
        string port = "25565";
        ushort portA = 25565;
        string adressA = "localhost";

        //self explanatory refreshing script
        public void Refresh(string adress, ushort port)
        {


            //probing for data
            MineStat ms = new MineStat(adress, port); //needs to be here

            //Online checker
            if (ms.IsServerUp())
            {
                textBox2.Text = "Online";
            }
            else
            {
                textBox2.Text = "Offline";
            }
            //write ip
            string stringAdress = adress + port.ToString();
            textBox1.Text = adress;
            textBox9.Text = port.ToString();


            //getping
            long ping = ms.GetLatency();
            string stringPing = ping.ToString();
            textBox3.Text = stringPing;

            //current players
            string currentPlayers = ms.GetCurrentPlayers();
            textBox5.Text = currentPlayers;

            //max players
            string maxPlayers = ms.GetMaximumPlayers();
            textBox6.Text = maxPlayers;

            //motd

            string motd = ms.GetMotd();
            textBox4.Text = motd;
        }
        public MCStatus()
        {
            InitializeComponent();
            //reding the file
            if (File.Exists(@"Config.txt"))
            {
                String line;
                try
                {
                    //Pass the file path and file name to the StreamReader constructor
                    StreamReader sr = new StreamReader(@"Config.txt");

                    //Read the first line of text
                    line = "aaaaa";
                    sr.ReadLine();

                    //Continue to read until you reach end of file
                    while (line != null)
                    {
                        //Read the next line
                        adressA = sr.ReadLine();
                        textBox7.Text = adressA;
                        //Read the next line
                        port = sr.ReadLine();
                        textBox8.Text = port;
                        line = sr.ReadLine();

                    }
                }
                catch (Exception e)
                {
                    richTextBox1.Text = "EXEPTION : " + e.ToString();
                }
                portA = Convert.ToUInt16(portA);

                //writing the file contents to the textbox
                richTextBox1.Text = adressA + ":" + port;
                Refresh(adressA, portA);
            }
            else {
                portA = Convert.ToUInt16(port);
                Refresh(adressA, portA);
            }

            textBox10.Text = adressA;
            textBox11.Text = port;
            textBox1.Text = adressA;
            textBox9.Text = port;
        }




        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ButtonToggle(false); //disable the buttons
            //refresh
            adressA = textBox1.Text;
            portA = Convert.ToUInt16(textBox9.Text);
            Refresh(adressA, portA);
            ButtonToggle(true); //enable the buttons
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ButtonToggle(false);
            using (System.IO.StreamWriter file =
           new System.IO.StreamWriter(@"Config.txt"))
            {
                file.WriteLine("");
                file.WriteLine(textBox10.Text);
                file.WriteLine(textBox11.Text);
            }

            //refreshing satGUI + MineStat
            adressA = textBox10.Text;
            portA = Convert.ToUInt16(textBox11.Text);

            Refresh(adressA, portA);

            //writing all the stuf again (refreshing)
            textBox7.Text = adressA;
            textBox8.Text = portA.ToString();

            richTextBox1.Text = adressA + ":" + portA.ToString();
            //disable the buttons
            ButtonToggle(true);
        }

        //Enable or disable the b u t t o n s
        public void ButtonToggle (bool contition) {
            button1.Enabled = contition;
            button2.Enabled = contition;

        }
    }
}