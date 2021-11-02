//Limits of Liability - Under no circumstances shall Performance Motion Devices, Inc. or its affiliates, partners, or suppliers be liable for any indirect
// incidental, consequential, special or exemplary damages arising out or in connection with the use this example,
// whether or not the damages were foreseeable and whether or not Performance Motion Devices, Inc. was advised of the possibility of such damages.
// Determining the suitability of this example is the responsibility of the user and subsequent usage is at their sole risk and discretion.
// There are no licensing restrictions associated with this example.


///  SCARA TLK 10/06/2021 
/// 
/// This example demonstrates the simulation and control path implentation of a SCARA robot.
///  .  
///   1.  Use SCARA Configuration button to define arm lengths.
///   2.  Click on end effector location to define starting point.
///   3.  Select Path # and press Teach
///   4.  Click screen multiple times (>3) to define XY path.
///   5.  Press SIMULATE MOVE.
///   6.  Can also click on position in path table.
///   7.  If a PMD Controller is present the DO MOVE button can be used after pressing CONNECT TO PMD DEVICE.
///       During the move the SCARA animation is based on the position information from the PMD Controller.
///   
///   
///  


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using PMDLibrary;
using SCARA;


namespace SCARA_Example
{
  
    public partial class Form1 : Form
    {
        public bool timerstop = false;
        delegate void SetCmdPosTextCallback(string text);
        delegate void SetActPosTextCallback(string text);
        delegate void SetEventLabelTextCallback(string text);
        delegate void SetListViewSelectCallback(int index);
        delegate void SetDextralityCallback();
        delegate void SetSelectedPointCallback(int index);
        delegate void SetStatusWindowCallback();

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
       
        float x0pixel, y0pixel;
        double pixelscale = 20.0;
     
        Scarabot Sparky;
        Point lastclick;
        int step;
       
        double home_x, home_y;
        UIStatus UIstatus;
        List<tablevector>[] movetables;
        int MoveState = 0;
        SCARAConfig ConfigDlgForm;
        double temp_elbow_length, temp_shoulder_length;

        public Form1()
        {
            InitializeComponent();
            UIstatus = UIStatus.Idle;
            try
            {
                x0pixel = pictureBox1.Width / 2;
                y0pixel = pictureBox1.Height/2;

                // When using a set of single axis products like the DK58113 the arcitecture is distributed 
                //Sparky = new Scarabot(6.0, 4.0,ArchitectureType.Distributed);


                // When using a mulit-axis product like the Prodigy Machine Controller the arcitecture is distributed 
                Sparky = new Scarabot(6.0, 4.0, ArchitectureType.Centralized);

                home_x = 8.0;
                home_y = 4.0;
                Sparky.InverseKinematics(home_x,home_y,RightyRadioButton.Checked);
                                
                Point homepixel = new Point(0,0);
                DrawSCARA(Sparky);
                SetStatusWindow();
                AllocateMoveTables(10);
                InitListBox();

                StateObjClass StateObj = new StateObjClass();
                StateObj.TimerCanceled = false;
                StateObj.SomeValue = 1;
                System.Threading.TimerCallback TimerDelegate = new System.Threading.TimerCallback(TimerTask);

                // Create a timer that calls a procedure every 500 milliseconds. 
                // Note: There is no Start method; the timer starts running as soon as  
                // the instance is created.
                System.Threading.Timer TimerItem = new System.Threading.Timer(TimerDelegate, StateObj, 500, 500);
                // Save a reference for Dispose.
                StateObj.TimerReference = TimerItem;
                // This is an optional general purpose back ground task 
                //this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
                // InitializeBackgoundWorker();

            }

            catch (Exception e)
            {
                MessageBox.Show(e.Message);

            }
        }

        

        public class ScaraAxis : PMD.PMDAxis
        {
            public UDPM path;
            uint maxpoints = 500;
            public short axisMask;
            public double EncoderScalar;    //  Encoder counts per revolution of shoulder or elbow.
            public bool shared;
                       
            public ScaraAxis(PMD.PMDDevice dev, PMD.PMDAxisNumber axisNumber, double scalar, ArchitectureType architecture) : base(dev, axisNumber)
            {
                EncoderScalar = scalar;
                if (architecture == ArchitectureType.Distributed) shared = false;
                else shared = true;
                try
                {
                    SetupAxis();
                    path = new UDPM(dev, this, shared, maxpoints);
                    switch ((int)axisNumber)
                    {
                        case 0:
                            axisMask = 0x0001;
                            break;
                        case 1:
                            axisMask = 0x0002;
                            break;
                        case 2:
                            axisMask = 0x0004;
                            break;
                        case 3:
                            axisMask = 0x0008;
                            break;
                    }
                }
                catch (Exception e)
                {
                    if (e is System.NullReferenceException)
                    {

                    }
                    else MessageBox.Show(e.Message);
                }
            }

            //Modify SetupAxis() with an application specific configuration
            //or have Pro-Motion do the configuration and do not call SetupAxis(). 
            public PMD.PMDResult SetupAxis()
            {
                try
                {
                    this.OperatingMode = 1;
                    this.MotorType = PMD.PMDMotorType.DCBrush;
                    this.OutputMode = PMD.PMDOutputMode.SPIDACOffsetBinary;
                    this.ActualPosition = 0;
                    this.ResetEventStatus(0);
                    this.Update();
                    this.OperatingMode = 0x33;
                    this.SetEventAction(PMD.PMDEventActionEvent.MotionError, PMD.PMDEventAction.None);
                    return PMD.PMDResult.ERR_OK;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    return PMD.PMDResult.ERR_CommandError;
                }
            }

            public PMD.PMDResult RestoreAxis()
            {
                try
                {
                    this.ClearPositionError();
                    this.Update();
                    this.ResetEventStatus(0);
                    this.Update();
                    this.OperatingMode = 0x33;
                    return PMD.PMDResult.ERR_OK;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    return PMD.PMDResult.ERR_CommandError;
                }
            }


        }

        public enum ArchitectureType
        {
            Centralized = 0,
            Distributed = 1,
        }

        public class Scarabot
        {
            public double l_shoulder;
            public double l_elbow;
            public double phi;
            public double theta;
            public double theta_unwrapped;
            public double maxr;
            public double minr;
            public double Xeffector;
            public double Yeffector;
            public PMD.PMDAxis testaxis;
            public ScaraAxis Shoulder;
            public ScaraAxis Elbow;
            static PMD.PMDDevice devMC,devMCShoulder,devMCElbow;
            static PMD.PMDPeripheral perTCP;
            static PMD.PMDPeripheral perSERShoulder,perSERElbow;
            public static volatile PMD.PMDAxis test;
            public double cycletime;
            public bool DeviceConnect = false;
            public ArchitectureType Architecture;

            String ipaddress = "192.168.2.2";

            public Scarabot(double shoulder, double elbow, ArchitectureType my_Architecture)
            {
                
                l_shoulder = shoulder;
                l_elbow = elbow;
                phi = 0.7;
                theta= 1.6;
                theta_unwrapped = theta;
                maxr = l_shoulder+l_elbow;
                minr = l_shoulder - l_elbow;
                Architecture = my_Architecture;
                              
            }

            // Connect to the PMD Controller
            // When connecting to PMD controllers such as the DK58113, DK58420, or ION use Device Type Motion Processor
            // When connecting to PMD controllers such as ION/CME, N-Series ION/CME, Prodigy Boards and Machine Contollers use Device Type Resource Protocol

            public PMD.PMDResult Connect()    
            {
                try
                {
                    // An assumption is made here that Disributed means Device Type Motion Processor.
                    if (Architecture==ArchitectureType.Distributed)
                    {
                        perSERShoulder = new PMD.PMDPeripheralCOM(11, 57600, PMD.PMDSerialParity.None, PMD.PMDSerialStopBits.SerialStopBits1);
                        devMCShoulder = new PMD.PMDDevice(perSERShoulder, PMD.PMDDeviceType.MotionProcessor);

                        perSERElbow = new PMD.PMDPeripheralCOM(2, 57600, PMD.PMDSerialParity.None, PMD.PMDSerialStopBits.SerialStopBits1);
                        devMCElbow = new PMD.PMDDevice(perSERElbow, PMD.PMDDeviceType.MotionProcessor);

                        Shoulder = new ScaraAxis(devMCShoulder, PMD.PMDAxisNumber.Axis1, -1, ArchitectureType.Distributed);
                        Elbow = new ScaraAxis(devMCElbow, PMD.PMDAxisNumber.Axis1, -1, ArchitectureType.Distributed);
                        cycletime = Shoulder.SampleTime * 0.000001;
                        DeviceConnect = true;

                        Shoulder.SynchronizationMode = PMD.PMDSynchronizationMode.Disabled;
                        Elbow.SynchronizationMode = PMD.PMDSynchronizationMode.Slave;
                        Thread.Sleep(1);
                        Shoulder.SynchronizationMode = PMD.PMDSynchronizationMode.Master;
                        // The two axes are now in sync
                        return PMD.PMDResult.ERR_OK;
                    }
                    
                    
                    // An assumption is make here that Centralized means Device Type PRP. 
                    else  // Connect to a 2Axis EtherNet conntroller using PRP protocol.  For example a PMD Machine Controller
                    {

                        perTCP = new PMD.PMDPeripheralTCP(System.Net.IPAddress.Parse(ipaddress), 40100, 1000);
                        
                        devMC = new PMD.PMDDevice(perTCP, PMD.PMDDeviceType.ResourceProtocol);
                        testaxis = new PMD.PMDAxis(devMC, PMD.PMDAxisNumber.Axis1);

                        Shoulder = new ScaraAxis(devMC, PMD.PMDAxisNumber.Axis1, -1, ArchitectureType.Centralized);
                        Elbow = new ScaraAxis(devMC, PMD.PMDAxisNumber.Axis2, -1,ArchitectureType.Centralized);
                        cycletime = Shoulder.SampleTime * 0.000001;
                        DeviceConnect = true;
                        return PMD.PMDResult.ERR_OK;
                    }
                    

                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    return PMD.PMDResult.ERR_OpeningPort;

                }

              

            }

            public PMD.PMDResult Disconnect()
            {
                try
                {
                    if ((this.Shoulder == null) == false) this.Shoulder.Close();
                    if ((this.Elbow == null) == false) this.Elbow.Close();
                    
                    if ((devMC == null) == false) devMC.Close();
                    if ((devMCShoulder == null) == false) devMCShoulder.Close();
                    if ((devMCElbow == null) == false) devMCElbow.Close();
                    
                    if ((perTCP == null) == false) perTCP.Close();
                    if ((perSERShoulder == null) == false) perSERShoulder.Close();
                    if ((perSERElbow == null) == false) perSERElbow.Close();
                    
                    DeviceConnect = false;
                    return PMD.PMDResult.ERR_OK;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    return PMD.PMDResult.ERR_OpeningPort;

                }

            }

            public bool IsSynced()
            {
                uint stime,etime1;
                if (Elbow.SynchronizationMode != PMD.PMDSynchronizationMode.Slave) return false;
                stime = Shoulder.Time;
                etime1 = Elbow.Time;
                if (etime1 == 0) return false;
                if (stime > etime1) return false;
                return true;

            }

            public void SetAngles(double mphi, double mtheta)
            {
                phi = mphi;
                theta = mtheta;
                theta_unwrapped = UnWrap(theta);
                ForwardKinematics();
            }

            public void SetLengths(double upper, double lower)
            {
                l_shoulder = upper;
                l_elbow = lower;
                maxr = l_shoulder + l_elbow;
                minr = l_shoulder - l_elbow;
            }

            public void SetConfig(double upper, double lower, double shoulderres, double elbowres)
            {
                SetLengths(upper, lower);
                Shoulder.EncoderScalar = shoulderres;
                Elbow.EncoderScalar = elbowres;
            }

            public void Stop()    // Stop immediately and hold position
            {
                if (this.DeviceConnect == false)
                {
                    MessageBox.Show("Not Connected to Device!!");
                    return;
                }

                this.Shoulder.StopMode = PMD.PMDStopMode.Abrupt;
                this.Elbow.StopMode = PMD.PMDStopMode.Abrupt;
                
                this.Shoulder.SetProfileParameter(PMD.PMDProfileParameter.RateScalar, 0);
                this.Elbow.SetProfileParameter(PMD.PMDProfileParameter.RateScalar, 0);


                if (this.Architecture == ArchitectureType.Centralized) this.Shoulder.MultiUpdate((ushort)(this.Shoulder.axisMask | this.Elbow.axisMask));
                else
                {
                    this.Shoulder.Update();
                    this.Elbow.Update();
                }

              
            }

            //multi axis controllers can use MultiUpdate.
            //single axis controllers which are synchronized can use a time breakpoint
            //single axis controllers which are not synchronized must be updated one at a time which means there will be a delay.
            //This is not shown but AxisIn can be used to trigger the breakpoint instead of time.  This could be an option for 
            //non-synchorized single axis controllers
            public void Update()
            {
                if (this.Architecture == ArchitectureType.Centralized) this.Shoulder.MultiUpdate((ushort)(this.Shoulder.axisMask | this.Elbow.axisMask));
                else
                {

                    if (IsSynced())
                    {
                        UInt32 timenow;
                        double startdelay;

                        startdelay = 0.500; // seconds
                        // This value needs to be at least as large as the time required to send the two SetBreakpointValue and two SetBreakpoint commamnds below
                        this.Shoulder.ResetEventStatus(0xFFFB);
                        this.Elbow.ResetEventStatus(0xFFFB);

                        timenow = this.Shoulder.Time;
                        this.Shoulder.SetBreakpointValue((short)PMD.PMDBreakpoint.Breakpoint1, (int)(timenow + startdelay / this.cycletime));
                        this.Shoulder.SetBreakpoint((short)PMD.PMDBreakpoint.Breakpoint1, PMD.PMDAxisNumber.Axis1, PMD.PMDBreakpointAction.Update, PMD.PMDBreakpointTrigger.Time);
                        this.Elbow.SetBreakpointValue((short)PMD.PMDBreakpoint.Breakpoint1, (int)(timenow + startdelay / this.cycletime));
                        this.Elbow.SetBreakpoint((short)PMD.PMDBreakpoint.Breakpoint1, PMD.PMDAxisNumber.Axis1, PMD.PMDBreakpointAction.Update, PMD.PMDBreakpointTrigger.Time);
                        
                        Thread.Sleep((int)(startdelay*1000.0));   // pause execution until motion occurrs, this will also delay the UIstatus change to "DoMove".
                    
                    }
                    else
                    {
                        this.Shoulder.Update();
                        this.Elbow.Update();
                    }
                }
            }

            public void Deservo()    // no power to motors
            {
                if (this.DeviceConnect == false)
                {
                    MessageBox.Show("Not Connected to Device!!");
                    return;
                }
                this.Shoulder.OperatingMode = (ushort)PMD.PMDOperatingMode.AxisEnabled;
                this.Elbow.OperatingMode = (ushort)PMD.PMDOperatingMode.AxisEnabled;
            }

            public void ForwardKinematics()
            {
                double elbowposx, elbowposy;
                elbowposx = this.l_shoulder*Math.Cos(this.phi);
                elbowposy = this.l_shoulder*Math.Sin(this.phi);
                this.Xeffector = elbowposx + this.l_elbow*Math.Cos(this.theta+this.phi);
                this.Yeffector = elbowposy + this.l_elbow*Math.Sin(this.theta+this.phi);
                
            }

            public bool InverseKinematics(double x, double y,bool righty)
            {
                double C2, S2;
                double num, den;
                double d,diff;
                double last_phi, last_theta;
                double temp;
                Xeffector=x;
                Yeffector=y;
                last_phi = phi;
                last_theta = theta;

                d = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
                if (d > maxr)
                {
                    theta = 0;
                    phi = Math.Atan2(y, x);
                }
                else if (d < minr)
                {
                    theta = Math.PI;
                    phi = Math.Atan2(y, x);
                }
                else
                {
                    C2 = (Math.Pow(x, 2) + Math.Pow(y, 2) - Math.Pow(l_shoulder, 2) - Math.Pow(l_elbow, 2)) / (2 * l_shoulder * l_elbow);
                    if (C2 > 1.0) C2 = 1.0;
                    S2 = Math.Sqrt(1 - Math.Pow(C2, 2));
                    theta = -Math.Acos(C2);
                    num = l_elbow * S2 * x + (l_shoulder + l_elbow * C2) * y;
                    den = -l_elbow * S2 * y + (l_shoulder + l_elbow * C2) * x;
                    phi = Math.Atan2(num, den);
                    if (righty)
                    {
                        phi = -phi + 2 * Math.Atan2(y, x);
                        if (phi > 2 * Math.PI) phi -= 2 * Math.PI;
                        if (phi < -Math.PI) phi += 2 * Math.PI;
                        theta = Math.Acos(C2);
                    }

                }

                // wrap up the anlge
                phi = Wrap(phi, last_phi);
                theta = Wrap(theta, last_theta);
                                 
                return true;
            }

            
            public double UnWrap(double theta)
             {
                    //unwrap
                    return theta % (2 * Math.PI);
             }

            public double Wrap(double angle, double last_angle)
            {
                
                double offset_angle, wrapped_angle = 0; ;
                offset_angle = last_angle - last_angle % (2 * Math.PI);
                if (last_angle >= 0)
                {
                    if (FindQuadrant(last_angle) < 3)
                    {
                        if (angle > 0) wrapped_angle = angle + offset_angle;
                        else
                        {
                            if ((UnWrap(last_angle) - angle) < Math.PI)   // reverse
                                wrapped_angle = angle+offset_angle;
                            else //forward
                            {
                                angle = 2 * Math.PI + angle;
                                wrapped_angle = angle + offset_angle;
                            }
                        }
                    }
                    else // last_angle in quad 3/4
                    {
                        if (angle < 0)
                        {
                            angle = 2 * Math.PI + angle;
                            wrapped_angle = angle+ offset_angle;
                        }
                        else
                        {
                            if ((UnWrap(last_angle) - angle) < Math.PI)   // reverse
                                wrapped_angle = angle + offset_angle;
                            else wrapped_angle = angle + offset_angle + 2 * Math.PI;
                        }
                    }
                }
                else
                {
                    if (FindQuadrant(last_angle) < 3)
                    {
                        if (angle > 0) wrapped_angle = angle - 2*Math.PI + offset_angle ;
                        else
                        {
                            if (angle- (UnWrap(last_angle)) < Math.PI)   // pos
                                wrapped_angle = angle + offset_angle;
                            else //forward
                            {
                                angle = -2 * Math.PI + angle;
                                wrapped_angle = angle + offset_angle;
                            }
                        }
                    }

                    else
                    {
                        {
                            if (angle < 0)
                            {
                                 wrapped_angle = angle + offset_angle;
                            }
                            else
                            {
                                if (angle-(UnWrap(last_angle)) < Math.PI)   // pos/ccw
                                    wrapped_angle = angle + offset_angle;
                                else wrapped_angle = angle + offset_angle - 2 * Math.PI;
                            }
                        }
                    }

                }
                return wrapped_angle;
            }
        }
// End of SCARABOT object

        static public int FindQuadrant(double otheta)
        {
            //unwrap
            double theta;
            theta = otheta % (2 * Math.PI);
            if (theta < 0) theta += 2 * Math.PI;
            if ((theta == 0) && (otheta < 0)) return 4;
            if (theta < Math.PI / 2) return 1;
            if ((theta >= Math.PI / 2) & (theta < Math.PI)) return 2;
            if ((theta >= Math.PI) & (theta < 1.5 * Math.PI)) return 3;
            else return 4;

        }

/// Graphics /////////////////////////////////////////////////////////////////////////////////////////////////

        public void DrawSCARA(Scarabot bot, Point? myclick=null)
        {
            double x, y;
            double xe, ye;
            float xpixel, ypixel; //, x0pixel, y0pixel;
            float xepixel, yepixel;

            xe = bot.l_shoulder * Math.Cos(bot.phi);
            ye = bot.l_shoulder * Math.Sin(bot.phi);

            x = bot.l_elbow * Math.Cos(bot.phi + bot.theta) + xe;
            y = bot.l_elbow * Math.Sin(bot.phi + bot.theta) + ye;

            xepixel = (float)(xe * pixelscale) + x0pixel;
            yepixel = -(float)(ye * pixelscale) + y0pixel;

            xpixel = (float)(x * pixelscale) + x0pixel;
            ypixel = -(float)(y * pixelscale) + y0pixel;

            Point clickremap = new Point();
            Point temp = new Point();
            if (myclick != null)
            {
                temp = (Point) myclick;
                clickremap.X = temp.X - pictureBox1.Location.X - this.Location.X - 10;
                clickremap.Y = temp.Y - pictureBox1.Location.Y - this.Location.Y - 35;
            }
            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                // Draw base 
                DrawCircle(g, new Pen(Color.Red, width: 18), x0pixel, y0pixel, 20);

                //Draw 1st link
                g.DrawLine(new Pen(Color.Red, width: 35), x0pixel, y0pixel, xepixel, yepixel);

                //Draw Elbow
                DrawCircle(g, new Pen(Color.Green, width: 18), xepixel, yepixel, 8);

                //Draw 2nd link
                g.DrawLine(new Pen(Color.Green, width:25), xepixel, yepixel, xpixel, ypixel);
                DrawCircle(g, new Pen(Color.Green, width: 8), xpixel, ypixel, 10);

                if(myclick!=null) DrawCrossHairs(g,new Pen(Color.DarkOrange, width: 3), clickremap.X,clickremap.Y, 10);

            }
            pictureBox1.Image = bmp;
          
        }

        public void DrawCircle(Graphics g, Pen pen,
                                 float centerX, float centerY, float radius)
        {
            g.DrawEllipse(pen, centerX - radius, centerY - radius,
                          radius + radius, radius + radius);
        }

        public void FillCircle(Graphics g, Brush brush,
                                      float centerX, float centerY, float radius)
        {
            g.FillEllipse(brush, centerX - radius, centerY - radius,
                          radius + radius, radius + radius);
        }

        public void DrawCrossHairs(Graphics g, Pen pen,
                                 float centerX, float centerY, float radius)
        {
            g.DrawLine(new Pen(Color.DarkOrange, width: 3), centerX, centerY - radius, centerX, centerY + radius);
            g.DrawLine(new Pen(Color.DarkOrange, width: 3), centerX - radius, centerY, centerX + radius, centerY);
        }



        public void DrawMouseClick(Point myclick)
        {

            Point remap = new Point();
        
            remap.X = myclick.X - pictureBox1.Location.X -this.Location.X-10;
            remap.Y = myclick.Y - pictureBox1.Location.Y - this.Location.Y-35;
            

            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.DrawLine(new Pen(Color.Blue, width: 3), remap.X - 10, remap.Y, remap.X + 10, remap.Y);
                g.DrawLine(new Pen(Color.Blue, width: 3), remap.X, remap.Y - 10, remap.X, remap.Y + 10);
            }
            pictureBox1.Image = bmp;
        }

        //Tablles and Lists  //////////////////////////////////////////////////////////////////////////////
        public class tablevector
        {
            public double X;
            public double Y;
            public double shoulder;
            public double elbow;
            public double vtime;

            public tablevector(double m_X, double m_Y, double m_shoulder, double m_elbow, double m_time)
            {
                X = m_X;
                Y = m_Y;
                shoulder = m_shoulder;
                elbow = m_elbow;
                vtime = m_time;
            }

            public Int32[] ConvertTableVector(Scarabot bot)
            {
                Int32[] nativevector = new Int32[3];
                nativevector[0] = (int)(this.vtime / bot.cycletime);
                nativevector[1] = (int)(this.shoulder * bot.Shoulder.EncoderScalar);
                nativevector[2] = (int)(this.elbow * bot.Elbow.EncoderScalar);
                return nativevector;
            }

        }

        void AllocateMoveTables(int num_tables)
        {
            int i;
            movetables = new List<tablevector>[num_tables + 1];
            for (i = 0; i < num_tables + 1; i++) movetables[i] = new List<tablevector>();
        }

        void ClearAllPaths()
        {
            foreach (var v in movetables) v.Clear();
            LoadListBox();
        }

        public void InitListBox()
        {
            listView1.Clear();
            listView1.Columns.Add("X", 60);
            listView1.Columns.Add("Y", 60);
            listView1.Columns.Add("Shoulder", 80);
            listView1.Columns.Add("Elbow", 80);
            listView1.Columns.Add("Delta Time", 80);
        }

        void LoadListBox()
        {
            InitListBox();
            foreach (var v in movetables[Convert.ToInt16(MoveNumber.Value)])
            {
                ListViewItem strvector = new ListViewItem(new[] { v.X.ToString("#0.000"), v.Y.ToString("#0.000"), v.shoulder.ToString("#0.000"), v.elbow.ToString("#0.000"), v.vtime.ToString("#0.000") });
                listView1.Items.Add(strvector);
            }
        }




///  Delegates  ////////////////////////////////////////////////////////////////////////////

        private void SetListViewSelect(int index)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.listView1.InvokeRequired)
            {
                SetListViewSelectCallback d = new SetListViewSelectCallback(SetListViewSelect);
                this.Invoke(d, new object[] { index });
                
            }
            else
            {
               // this.SetListViewSelect.Text = text;
                listView1.Items[index].Selected = true;
                //if(UIstatus=UIStatus.DoMove) listV
            }

        }

        private void SetDextrality()
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.RightyRadioButton.InvokeRequired)
            {
                SetDextralityCallback d = new SetDextralityCallback(SetDextrality);
                this.Invoke(d, new object[] {});
            }
            else
            {
                if (FindQuadrant(Sparky.theta) > 2) radioButton2.Checked = true;
                else RightyRadioButton.Checked = true;
            }
        }

        void SetStatusWindow()
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.X_textBox.InvokeRequired)
            {
                SetStatusWindowCallback d = new SetStatusWindowCallback(SetStatusWindow);
                this.Invoke(d, new object[] { });
            }
            else
            {
                X_textBox.Text = Sparky.Xeffector.ToString("#0.000");
                Y_textBox.Text = Sparky.Yeffector.ToString("#0.000");
                Phi_textBox.Text = Sparky.phi.ToString("#0.000"); 
                Theta_textBox.Text = Sparky.theta.ToString("#0.000"); 
            }

            if ((UIstatus!=UIStatus.Idle) && (UIstatus!=UIStatus.Teach)) SetDextrality();
        }

        


///  Timer Thread //////////////////////////////////////////////////////////////////////////////////////////////////

        private class StateObjClass
        {
            // Used to hold parameters for calls to TimerTask. 
            public int SomeValue;
            public System.Threading.Timer TimerReference;
            public bool TimerCanceled;
        }

        public void RunTimer()
        {

            StateObjClass StateObj = new StateObjClass();
            StateObj.TimerCanceled = false;
            StateObj.SomeValue = 1;
            System.Threading.TimerCallback TimerDelegate = new System.Threading.TimerCallback(TimerTask);

            // Create a timer that calls a procedure every 2 seconds. 
            // Note: There is no Start method; the timer starts running as soon as  
            // the instance is created.
            System.Threading.Timer TimerItem = new System.Threading.Timer(TimerDelegate, StateObj, 500, 500);

            // Save a reference for Dispose.
            StateObj.TimerReference = TimerItem;

        }

        enum UIStatus
        {
            Idle = 0,
            Teach = 1,
            Sim = 2,
            DoMove = 3,
            UserSelect = 4     /// user select vector in table
        }


        private void TimerTask(object StateObj)
        {
            StateObjClass State = (StateObjClass)StateObj;
            Int32 CmdPosS = 0, CmdPosE = 0; //ActPos = 0; //, tester = 0;
            Int32 index=0;
                     
            System.Diagnostics.Debug.WriteLine("Launched new thread  " + DateTime.Now.ToString());
            
            // Dispose Requested.
            if (timerstop)
            {
                State.TimerReference.Dispose();
                System.Diagnostics.Debug.WriteLine("Done  " + DateTime.Now.ToString());
                return;
            }

            switch (UIstatus)
            {
                case UIStatus.Idle:
                    step = 1;
 //                   if(movetables[Convert.ToInt16(MoveNumber.Value)].Count>0)SetListViewSelect(step);
                    break;

                case UIStatus.Sim:
                {
                    SetListViewSelect(step);
                    step++;
                    if (step > (movetables[Convert.ToInt16(MoveNumber.Value)].Count - 1)) UIstatus = UIStatus.Idle;
                    break;
                }

                case UIStatus.Teach:
              //      if (movetables[Convert.ToInt16(MoveNumber.Value)].Count > 0) SetListViewSelect(0); 
                    break;

                case UIStatus.DoMove:
                {
                    // Update Commanded Position Text Box
                    CmdPosS = Sparky.Shoulder.CommandedPosition;
                    CmdPosE = Sparky.Elbow.CommandedPosition;
                    Sparky.SetAngles((double)CmdPosS / Sparky.Shoulder.EncoderScalar, (double)CmdPosE / Sparky.Elbow.EncoderScalar);
                    DrawSCARA(Sparky);
                        
                    Sparky.Shoulder.GetTraceValue((PMD.PMDTraceVariable)91, ref index);
                    if (MoveState == 0)
                    {
                        if (index > 1) MoveState = 1;
                    }
                    else if (index == 0)
                    {
                        Sparky.Stop();
                        UIstatus = UIStatus.Idle;
                        MoveState = 0;
                        // set index to last table vector
                        index = movetables[Convert.ToInt16(MoveNumber.Value)].Count - 1;
                    }

                    SetListViewSelect(index);

                        //check for events that would indicate a problem
                    UInt16 Aevent, Aerror;
                    String EventString;
                    
                    Aevent = Sparky.Shoulder.EventStatus;
                    Aerror = (UInt16) Sparky.Shoulder.RuntimeError;
                    EventString= ProcessEvent(Aevent,Aerror);
                    if (EventString != "")
                    {
                         UIstatus = UIStatus.Idle;
                         MoveState = 0;
                         MessageBox.Show("Shoulder Error: " + EventString);
                    }

                        
                    Aevent = Sparky.Elbow.EventStatus;
                    Aerror = (UInt16)Sparky.Elbow.RuntimeError;
                    EventString = ProcessEvent(Aevent, Aerror);
                    if (EventString != "")
                    {
                            UIstatus = UIStatus.Idle;
                            MoveState = 0;
                            MessageBox.Show("Elbow Error: " + EventString);
                    }
                    break;
                }
            }

            SetStatusWindow();
        }

/// <summary>
/// ///////////////////////////////////////////////////////////
/// </summary>
//  RunTime error codes:
// Error 1 is signaled if the difference between two consecutive entries in the I table is not positive or is > 32767.
//This check is done as the entries are required – the entire buffer is not checked at once.
//Error 1 is also signaled if the length of the I table and P table are not the same.
//Error 2 is signaled if the absolute value of the master count is > 32767, or if the increment is large enough in absolute value that that more that an entry in the I table would have to be skipped.
//Error 3 is signaled if the absolute value of the difference in consecutive entries of the P table is > 32767, or if the length of the P table is < 2 or > 65535.
//Error 4 is signaled if the specified starting index is larger than the table length – 1.

        private string ProcessEvent(UInt16 Aevent, UInt16 Aerror)
        {
            string EventString = "";
            if (Aerror==1) EventString = "Error in Input Table";
            if (Aerror==2) EventString = "Table Overrun";
            if (Aerror==3) EventString = "Error in P Table";
            if (Aerror==4) EventString = "Start index Error";
            if (EventString == "") return EventString;

            // check for MotionError
            if ((Aevent & 0x0010) != 0)
                EventString = "Motion Error";
            else if ((Aevent & 0x0020) != 0)
                EventString = "Postive Limit Switch Reaced";
            else if ((Aevent & 0x0030) != 0)
                EventString = "Negative Limit Switch Reached";
            
            // The Motion Complete Bit is not valid when using UDPM.
            //   else if ((Aevent & 0x001) != 0)
            //      EventString = "Motion Compelete";

            return EventString;
        }

               
  // User Controls  /////////////////////////////////////////////////////////
        private void MoveButton_Click(object sender, EventArgs e)
        {
            Int32[] inputtable = new Int32[100];   
            Int32[] s_table = new Int32[100];      
            Int32[] e_table = new Int32[100];
            Int32[] native = new int[3];
         
            Int32[] dinputtable = new Int32[100];
            Int32[] ds_table = new Int32[100];
            Int32[] de_table = new Int32[100];


            // These are test arrays
            Int32[] testinputtable = new Int32[11] {0  , 100 ,  200,  300,  400,  500,  600,  700,  800,  900, 1000 };
            Int32[] testxtable = new Int32[11]     {0  , 1100, 2100, 3300, 4400, 2500, 5600, 8700, 1800,   90,   40 };
            Int32[] testytable = new Int32[11]     {388,  424, 3244, 4324, 5999, 2000, 4243, 2000, 1000, -200, -400 };

            uint i=0;
            int PathNumber = Convert.ToInt16(MoveNumber.Value);
            uint NumberOfVectors = (uint)movetables[PathNumber].Count;
            UInt16 mode;
            MoveState = 0;

            if (Sparky.DeviceConnect == false)
            {
                MessageBox.Show("Not Connected to Device!!");
                return;
            }

            
            if ((Sparky.Shoulder.EncoderScalar < 0)||(Sparky.Elbow.EncoderScalar <0))
            {
                MessageBox.Show("Encoder Resolution must be set in SCARA Configuration dialog!!");
                return;
            }
            
            mode = Sparky.Shoulder.ActiveOperatingMode;
            if ((mode & 0x0020) != 0)
            {

#if true
                if (NumberOfVectors < 4)
                {
                    MessageBox.Show("Minimum number of points is 4!!");
                    return;
                }

                inputtable[0] = 0;
                foreach (var vector in movetables[PathNumber])
                {
                    native = vector.ConvertTableVector(Sparky);
                    if (i > 0) inputtable[i] = inputtable[i - 1] + native[0];
                    s_table[i] = native[1];
                    e_table[i] = native[2];
                    i++;
                }
#else

                NumberOfVectors = 10;
                i = NumberOfVectors;
                inputtable = testinputtable;
                xtable = testxtable;
                ytable = testytable;
            //        (movetables[Convert.ToInt16(MoveNumber.Value)].Count - 1)) 
#endif
                // need extra vector at the end.
                inputtable[NumberOfVectors] = inputtable[NumberOfVectors - 1] + 1;
                //   inputtable[i] = inputtable[NumberOfVectors - 1] + 1;
                s_table[NumberOfVectors] = s_table[NumberOfVectors - 1];
                e_table[NumberOfVectors] = e_table[NumberOfVectors - 1];

                Sparky.Shoulder.path.Write(inputtable, s_table, NumberOfVectors + 1);
                if (Sparky.Architecture == ArchitectureType.Centralized) Sparky.Elbow.path.Write(e_table, NumberOfVectors + 1);
                // if these axes are distributed then Elbow has its own input table.
                else Sparky.Elbow.path.Write(inputtable, e_table, NumberOfVectors + 1);

                Sparky.Shoulder.path.Read(dinputtable, ds_table);
                Sparky.Shoulder.path.Read(dinputtable, de_table);

                Sparky.Shoulder.ActualPosition = (int)s_table[0];
                Sparky.Shoulder.ClearPositionError();
                Sparky.Elbow.ActualPosition = (int)e_table[0];
                Sparky.Elbow.ClearPositionError();

                if (Sparky.Architecture == ArchitectureType.Centralized) Sparky.Shoulder.MultiUpdate((ushort)(Sparky.Shoulder.axisMask | Sparky.Elbow.axisMask));
                else
                {
                    Sparky.Shoulder.Update();
                    Sparky.Elbow.Update();
                }

                Sparky.Shoulder.path.Init();
                Sparky.Elbow.path.Init();
                
                double test = Convert.ToDouble(SourceScalarBox.Text);
                double temp = (test * 65536.0);
                int tScalar = (int)temp;

                Sparky.Shoulder.SetProfileParameter(PMD.PMDProfileParameter.RateScalar, tScalar);
                Sparky.Elbow.SetProfileParameter(PMD.PMDProfileParameter.RateScalar, tScalar);
                Sparky.Shoulder.ProfileMode = (PMD.PMDProfileMode)10;
                Sparky.Elbow.ProfileMode = (PMD.PMDProfileMode)10;

                // This will start the motion
                Sparky.Update();
                UIstatus = UIStatus.DoMove;
            }
            else
            {
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show("Operating Mode is not Enabled. Would you like to enable the device?", "Error", buttons);
                if (result == DialogResult.Yes)
                {
                    Sparky.Shoulder.RestoreAxis();
                    Sparky.Elbow.RestoreAxis();
                }
                else
                {
                    // Do something  
                }
            }
        }
               

        private void Stopbutton_Click_1(object sender, EventArgs e)
        {
            Sparky.Stop();
            UIstatus = UIStatus.Idle;
            MoveState = 0;
        }

       

        private void Disablebutton_Click(object sender, EventArgs e)
        {
            Sparky.Deservo();
            UIstatus = UIStatus.Idle;
            MoveState = 0;
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {

            timerstop = true;
            Sparky.Disconnect();
            this.Close();
        }


        // Dextrality radio buttons
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if((UIstatus==UIStatus.Idle)|(UIstatus==UIStatus.Teach))
            {
                Sparky.InverseKinematics(Sparky.Xeffector, Sparky.Yeffector, RightyRadioButton.Checked);
                DrawSCARA(Sparky, lastclick);
                Phi_textBox.Text = Convert.ToString(Sparky.phi);
                Theta_textBox.Text = Convert.ToString(Sparky.theta);

            }
         }

        private void SimMove_Click(object sender, EventArgs e)
        {
            step = 0;
            if (movetables[Convert.ToInt16(MoveNumber.Value)].Count != 0) UIstatus = UIStatus.Sim;
            else MessageBox.Show("No points in path defined!!");
            
        }
                

        private void Teachbutton_Click(object sender, EventArgs e)
        {
            UIstatus = UIStatus.Teach;
            step = 0;
        }

        private void Connectbutton_Click(object sender, EventArgs e)
        {
            Sparky.Connect();
        }

        //  The user has clicked on the list of points in the path
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int val;
            tablevector temp;

            if (UIstatus == UIStatus.DoMove) return;

            if (this.listView1.SelectedItems.Count == 0)
                return;

            val =this.listView1.SelectedItems[0].Index;
            temp=movetables[Convert.ToInt16(MoveNumber.Value)][val];
            Sparky.SetAngles(temp.shoulder,temp.elbow);
            DrawSCARA(Sparky, lastclick);
       //     if (UIstatus != UIStatus.Sim) UIstatus = UIStatus.UserSelect;
          
        }

        private void ClearPath_Click(object sender, EventArgs e)
        {
            movetables[Convert.ToInt16(MoveNumber.Value)].Clear();
            LoadListBox();
        }


        private void SCARAConfigDlg_button_Click(object sender, EventArgs e)
        {
            temp_shoulder_length = Sparky.l_shoulder;
            temp_elbow_length = Sparky.l_elbow;
            if (ConfigDlgForm != null) return;
            ConfigDlgForm = new SCARAConfig(Sparky, movetables[1].Count);
            ConfigDlgForm.Owner = this;
            ConfigDlgForm.Show();
            ConfigDlgForm.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ConfigDlgForm_FormClosed);

        }

        private void ConfigDlgForm_FormClosed(object sender, FormClosedEventArgs e)
        {

            ConfigDlgForm = null;
            //  If no change than do nothing
            if ((temp_shoulder_length == Sparky.l_shoulder) & (temp_elbow_length == Sparky.l_elbow)) return;

            // clear paths if any are defined
            ClearAllPaths();
                                       
            DrawSCARA(Sparky);
         }

    
        private void MoveNumber_ValueChanged(object sender, EventArgs e)
        {
            LoadListBox();
        }


        // The user has clicked the mouse to teach an XY point
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            double newx, newy;
            lastclick = MousePosition;
            Point remap = new Point
            {
                X = MousePosition.X - pictureBox1.Location.X - this.Location.X - 10 - (int)x0pixel,
                Y = -(MousePosition.Y - pictureBox1.Location.Y - this.Location.Y - 35) + (int)y0pixel
            };
            newx = remap.X / pixelscale;
            newy = remap.Y / pixelscale;

            Sparky.InverseKinematics(newx, newy, RightyRadioButton.Checked);
            Sparky.ForwardKinematics();

            DrawSCARA(Sparky, MousePosition);

            if (UIstatus != UIStatus.Teach)
            {
                UIstatus = UIStatus.Idle;
                return;
            }

            tablevector vector = new tablevector(Sparky.Xeffector, Sparky.Yeffector, Sparky.phi, Sparky.theta, Convert.ToDouble(DeltaTime.Text));
            movetables[Convert.ToInt16(MoveNumber.Value)].Add(vector);
            ListViewItem strvector = new ListViewItem(new[] { Sparky.Xeffector.ToString("#0.000"), Sparky.Yeffector.ToString("#0.000"), Sparky.phi.ToString("#0.000"), Sparky.theta.ToString("#0.000"), Convert.ToDouble(DeltaTime.Text).ToString("#0.000")});
            listView1.Items.Add(strvector);

        }

        private void InitializeBackgoundWorker()
        {
            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);

            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
            backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void MoveDebug_Click(object sender, EventArgs e)
        {
            Int32[] inputtable = new Int32[100];
            Int32[] s_table = new Int32[100];
            Int32[] e_table = new Int32[100];
            Int32[] native = new int[3];

            Int32[] dinputtable = new Int32[100];
            Int32[] ds_table = new Int32[100];
            Int32[] de_table = new Int32[100];


            // These are test arrays
            Int32[] testinputtable = new Int32[11] { 0, 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };
            Int32[] testxtable = new Int32[11] { 0, 1100, 2100, 3300, 4400, 2500, 5600, 8700, 1800, 90, 40 };
            Int32[] testytable = new Int32[11] { 388, 424, 3244, 4324, 5999, 2000, 4243, 2000, 1000, -200, -400 };

            uint i = 0;
            int PathNumber = Convert.ToInt16(MoveNumber.Value);
            uint NumberOfVectors = (uint)movetables[PathNumber].Count;
            UInt16 mode;
            MoveState = 0;

            if (Sparky.DeviceConnect == false)
            {
                MessageBox.Show("Not Connected to Device!!");
                return;
            }


            if ((Sparky.Shoulder.EncoderScalar < 0) || (Sparky.Elbow.EncoderScalar < 0))
            {
                MessageBox.Show("Encoder Resolution must be set in SCARA Configuration dialog!!");
                return;
            }

            mode = Sparky.Shoulder.ActiveOperatingMode;
            if ((mode & 0x0020) != 0)
            {

#if true
                if (NumberOfVectors < 4)
                {
                    MessageBox.Show("Minimum number of points is 4!!");
                    return;
                }

                inputtable[0] = 0;
                foreach (var vector in movetables[PathNumber])
                {
                    native = vector.ConvertTableVector(Sparky);
                    if (i > 0) inputtable[i] = inputtable[i - 1] + native[0];
                    s_table[i] = native[1];
                    e_table[i] = native[2];
                    i++;
                }
#else

                NumberOfVectors = 10;
                i = NumberOfVectors;
                inputtable = testinputtable;
                xtable = testxtable;
                ytable = testytable;
            //        (movetables[Convert.ToInt16(MoveNumber.Value)].Count - 1)) 
#endif
                // need extra vector at the end.
                inputtable[NumberOfVectors] = inputtable[NumberOfVectors - 1] + 1;
                //   inputtable[i] = inputtable[NumberOfVectors - 1] + 1;
                s_table[NumberOfVectors] = s_table[NumberOfVectors - 1];
                e_table[NumberOfVectors] = e_table[NumberOfVectors - 1];

                Sparky.Shoulder.path.Write(inputtable, s_table, NumberOfVectors + 1);
                if (Sparky.Architecture == ArchitectureType.Centralized) Sparky.Elbow.path.Write(e_table, NumberOfVectors + 1);
                // if these axes are distributed then Elbow has its own input table.
                else Sparky.Elbow.path.Write(inputtable, e_table, NumberOfVectors + 1);

 
                Sparky.Shoulder.path.Read(dinputtable, ds_table);
                Sparky.Shoulder.path.Read(dinputtable, de_table);

                Sparky.Shoulder.ActualPosition = (int)s_table[0];
                Sparky.Shoulder.ClearPositionError();
                Sparky.Elbow.ActualPosition = (int)e_table[0];
                Sparky.Elbow.ClearPositionError();

                if (Sparky.Architecture == ArchitectureType.Centralized) Sparky.Shoulder.MultiUpdate((ushort)(Sparky.Shoulder.axisMask | Sparky.Elbow.axisMask));
                else
                {
                    Sparky.Shoulder.Update();
                    Sparky.Elbow.Update();
                }

       //         Sparky.Shoulder.path.Init();
        //        Sparky.Elbow.path.Init();
                double test = Convert.ToDouble(SourceScalarBox.Text);
                double temp = (test * 65536.0);
                int tScalar = (int)temp;

                Sparky.Shoulder.SetProfileParameter(PMD.PMDProfileParameter.RateScalar, tScalar);
                Sparky.Elbow.SetProfileParameter(PMD.PMDProfileParameter.RateScalar, tScalar);
                Sparky.Shoulder.ProfileMode = (PMD.PMDProfileMode)10;
                Sparky.Elbow.ProfileMode = (PMD.PMDProfileMode)10;

                // This will start the motion
    //            Sparky.Update();
                UIstatus = UIStatus.DoMove;
            }
            else
            {
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show("Operating Mode is not Enabled. Would you like to enable the device?", "Error", buttons);
                if (result == DialogResult.Yes)
                {
                    Sparky.Shoulder.SetupAxis();
                    Sparky.Elbow.SetupAxis();
                }
                else
                {
                    // Do something  
                }
            }



            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


#if false
            Int32[] inputtable = new Int32[100];
            Int32[] s_table = new Int32[100];
            Int32[] e_table = new Int32[100];
            Int32[] native = new int[3];

            Int32[] dinputtable = new Int32[100];
            Int32[] ds_table = new Int32[100];
            Int32[] de_table = new Int32[100];


            // These are test arrays
            Int32[] testinputtable = new Int32[11] { 0, 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };
            Int32[] testxtable = new Int32[11] { 0, 1100, 2100, 3300, 4400, 2500, 5600, 8700, 1800, 90, 40 };
            Int32[] testytable = new Int32[11] { 388, 424, 3244, 4324, 5999, 2000, 4243, 2000, 1000, -200, -400 };

            uint i = 0;
            int PathNumber = Convert.ToInt16(MoveNumber.Value);
            uint NumberOfVectors = (uint)movetables[PathNumber].Count;
            UInt16 mode;
#endif
  //          MoveState = 0;

            if (Sparky.DeviceConnect == false)
            {
                MessageBox.Show("Not Connected to Device!!");
                return;
            }


            if ((Sparky.Shoulder.EncoderScalar < 0) || (Sparky.Elbow.EncoderScalar < 0))
            {
                MessageBox.Show("Encoder Resolution must be set in SCARA Configuration dialog!!");
                return;
            }

            mode = Sparky.Shoulder.ActiveOperatingMode;
            if ((mode & 0x0020) != 0)
            {

#if true
                if (NumberOfVectors < 4)
                {
                    MessageBox.Show("Minimum number of points is 4!!");
                    return;
                }

 //               inputtable[0] = 0;
 //               foreach (var vector in movetables[PathNumber])
 //               {
 //                   native = vector.ConvertTableVector(Sparky);
 //                   if (i > 0) inputtable[i] = inputtable[i - 1] + native[0];
 //                   s_table[i] = native[1];
 //                   e_table[i] = native[2];
 //                   i++;
 //               }
#else

                NumberOfVectors = 10;
                i = NumberOfVectors;
                inputtable = testinputtable;
                xtable = testxtable;
                ytable = testytable;
            //        (movetables[Convert.ToInt16(MoveNumber.Value)].Count - 1)) 
#endif
                // need extra vector at the end.
                inputtable[NumberOfVectors] = inputtable[NumberOfVectors - 1] + 1;
                //   inputtable[i] = inputtable[NumberOfVectors - 1] + 1;
                s_table[NumberOfVectors] = s_table[NumberOfVectors - 1];
                e_table[NumberOfVectors] = e_table[NumberOfVectors - 1];

#if true
                Sparky.Shoulder.path.Write(inputtable, s_table, NumberOfVectors + 1);
                if (Sparky.Architecture == ArchitectureType.Centralized) Sparky.Elbow.path.Write(e_table, NumberOfVectors + 1);
                // if these axes are distributed then Elbow has its own input table.
                else Sparky.Elbow.path.Write(inputtable, e_table, NumberOfVectors + 1);

 //               Sparky.Shoulder.path.Read(dinputtable, ds_table);
 //               Sparky.Shoulder.path.Read(dinputtable, de_table);
#endif

#if false
                Sparky.Shoulder.ActualPosition = (int)s_table[0];
                Sparky.Shoulder.ClearPositionError();
                Sparky.Elbow.ActualPosition = (int)e_table[0];
                Sparky.Elbow.ClearPositionError();

#endif
                if (Sparky.Architecture == ArchitectureType.Centralized) Sparky.Shoulder.MultiUpdate((ushort)(Sparky.Shoulder.axisMask | Sparky.Elbow.axisMask));
                else
                {
                    Sparky.Shoulder.Update();
                    Sparky.Elbow.Update();
                }
                //#endif
                return;

                Sparky.Shoulder.path.Init();
                Sparky.Elbow.path.Init();

                double test = Convert.ToDouble(SourceScalarBox.Text);
                double temp = (test * 65536.0);
                int tScalar = (int)temp;
#if true
                Sparky.Shoulder.SetProfileParameter(PMD.PMDProfileParameter.RateScalar, tScalar);
                Sparky.Elbow.SetProfileParameter(PMD.PMDProfileParameter.RateScalar, tScalar);
                Sparky.Shoulder.ProfileMode = (PMD.PMDProfileMode)10;
                Sparky.Elbow.ProfileMode = (PMD.PMDProfileMode)10;
#endif

                // This will start the motion
                Sparky.Update();
                UIstatus = UIStatus.DoMove;
            }
            else
            {
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show("Operating Mode is not Enabled. Would you like to enable the device?", "Error", buttons);
                if (result == DialogResult.Yes)
                {
                    Sparky.Shoulder.SetupAxis();
                    Sparky.Elbow.SetupAxis();
                }
                else
                {
                    // Do something  
                }
            }
        }

        private void Reset_Click(object sender, EventArgs e)
        {
            Sparky.Shoulder.Reset();
            if (Sparky.Architecture == ArchitectureType.Distributed) Sparky.Elbow.Reset();
            Thread.Sleep(200);
            timerstop = true;
            Sparky.Disconnect();


        }

        // This event handler is where the actual,
        // potentially time-consuming work is done.
        private void backgroundWorker1_DoWork(object sender,
            DoWorkEventArgs e)
        {
            // Get the BackgroundWorker that raised this event.
            BackgroundWorker worker = sender as BackgroundWorker;

            // Assign the result of the computation
            // to the Result property of the DoWorkEventArgs
            // object. This is will be available to the 
            // RunWorkerCompleted eventhandler.
            //  e.Result = ComputeFibonacci((int)e.Argument, worker, e);
        }



        // This event handler deals with the results of the
        // background operation.
        private void backgroundWorker1_RunWorkerCompleted(
            object sender, RunWorkerCompletedEventArgs e)
        {
            // First, handle the case where an exception was thrown.
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled 
                // the operation.
                // Note that due to a race condition in 
                // the DoWork event handler, the Cancelled
                // flag may not have been set, even though
                // CancelAsync was called.
                //          resultLabel.Text = "Canceled";
            }
            else
            {
                // Finally, handle the case where the operation 
                // succeeded.
                //       resultLabel.Text = e.Result.ToString();
                //           this.SetText(e.Result.ToString());
            }

            // Enable the UpDown control.
            //   this.numericUpDown1.Enabled = true;

            // Enable the Start button.
            //   startAsyncButton.Enabled = true;

            // Disable the Cancel button.
            //   cancelAsyncButton.Enabled = false;
        }

        // This event handler updates the progress bar.
        private void backgroundWorker1_ProgressChanged(object sender,
            ProgressChangedEventArgs e)
        {
            //     this.progressBar1.Value = e.ProgressPercentage;
        }



    }
}




