//Limits of Liability - Under no circumstances shall Performance Motion Devices, Inc. or its affiliates, partners, or suppliers be liable for any indirect
// incidental, consequential, special or exemplary damages arising out or in connection with the use this example,
// whether or not the damages were foreseeable and whether or not Performance Motion Devices, Inc. was advised of the possibility of such damages.
// Determining the suitability of this example is the responsibility of the user and subsequent usage is at their sole risk and discretion.
// There are no licensing restrictions associated with this example.

//SCARA Config  TLK 9/28/2021
//This form opens when the SCARA config button (Form1) is clicked.
//The four parameters, shoulder length, shoulder resolution, elbow length, and elbow resolution are saved as part of the SCARA object


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static SCARA_Example.Form1;

namespace SCARA
{
    public partial class SCARAConfig : Form
    {

        Scarabot mybot;
        int mypoints;
        public SCARAConfig(Scarabot bot, int points)
        {
            InitializeComponent();
            mybot = bot;
            mypoints = points;
            UpperLength.Text = mybot.l_shoulder.ToString();
            LowerLength.Text = mybot.l_elbow.ToString();
            if (mybot.DeviceConnect)
            {
                ShoulderResolution.Enabled = true;
                ElbowResolution.Enabled = true;
                label7.Visible = false;
                if (mybot.Shoulder.EncoderScalar > 0) ShoulderResolution.Text = mybot.Shoulder.EncoderScalar.ToString();   // EnocderScalar=-1 means uninitialized.
                if (mybot.Elbow.EncoderScalar > 0) ElbowResolution.Text = mybot.Elbow.EncoderScalar.ToString();
            }
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double upper, lower;
            upper = Convert.ToDouble(UpperLength.Text);
            lower = Convert.ToDouble(LowerLength.Text);

            if ((upper != mybot.l_shoulder) || (lower != mybot.l_elbow))
                if (mypoints > 0)
                {
                    DialogResult dialogResult = MessageBox.Show("Changing arm lengths will delete all paths.  Continue?", "WARNING", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.No)
                    {
                        this.Close();
                        return;
                    }
                }

            if (mybot.DeviceConnect)
            {
                if (ShoulderResolution.Text == "") ShoulderResolution.Text = "-1";
                if (ElbowResolution.Text == "") ElbowResolution.Text = "-1";
                mybot.SetConfig(upper, lower, Convert.ToDouble(ShoulderResolution.Text), Convert.ToDouble(ElbowResolution.Text));
            }
            else mybot.SetLengths(upper, lower);

            this.Close();
        }
    }
}
