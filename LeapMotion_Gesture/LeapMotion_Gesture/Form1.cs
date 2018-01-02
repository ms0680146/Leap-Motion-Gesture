using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Leap;

namespace LeapMotion_Gesture
{
    public partial class Form1 : Form, ILeapEventDelegate
    {
        private Controller controller;
        private LeapEventListener listener;

        public Form1()
        {
            InitializeComponent();
            this.controller = new Controller();
            this.listener = new LeapEventListener(this);
            controller.AddListener(listener);
        }

        delegate void LeapEventDelegate(string EventName);
        public void LeapEventNotification(string EventName)
        {
            if (!this.InvokeRequired)
            {
                switch (EventName)
                {
                    case "onInit":

                        break;
                    case "onConnect":
                        connectHandler();
                        break;
                    case "onFrame":
                        detectGesture(this.controller.Frame());
                        break;
                }
            }
            else
            {
                BeginInvoke(new LeapEventDelegate(LeapEventNotification), new object[] { EventName });
            }
        }
        public void connectHandler()
        {
            this.controller.EnableGesture(Gesture.GestureType.TYPE_CIRCLE);
            this.controller.EnableGesture(Gesture.GestureType.TYPE_KEY_TAP);
            this.controller.EnableGesture(Gesture.GestureType.TYPE_SWIPE);
            this.controller.EnableGesture(Gesture.GestureType.TYPE_SCREEN_TAP);

        }
        public void detectGesture(Leap.Frame frame)
        {
            GestureList gestures = frame.Gestures(); //Return a list of gestures
            for (int i = 0; i<gestures.Count(); i++) // enumerate all the gestures detected in a frame
            {
                Gesture gesture = gestures[i];
                switch (gesture.Type)
                {
                    case Gesture.GestureType.TYPE_CIRCLE:
                        richTextBox1.AppendText("Circle detected!" + Environment.NewLine);
                        break;
                    case Gesture.GestureType.TYPE_KEY_TAP:
                        richTextBox1.AppendText("Key Tap detected!" + Environment.NewLine);
                        break;
                    case Gesture.GestureType.TYPE_SWIPE:
                        richTextBox1.AppendText("Swipe detected!" + Environment.NewLine);
                        break;
                    case Gesture.GestureType.TYPE_SCREEN_TAP:
                        richTextBox1.AppendText("Screen Tap detected!" + Environment.NewLine);
                        break;
                }
            }
        }

    }

    public interface ILeapEventDelegate
    {
        void LeapEventNotification(string EventName);
    }

    public class LeapEventListener : Listener
    {
        ILeapEventDelegate eventDelegate;

        public LeapEventListener(ILeapEventDelegate delegateObject)
        {
            this.eventDelegate = delegateObject;
        }
        public override void OnInit(Controller controller)
        {
            this.eventDelegate.LeapEventNotification("onInit");
        }
        public override void OnConnect(Controller controller)
        {
            this.eventDelegate.LeapEventNotification("onConnect");
        }
        public override void OnFrame(Controller controller)
        {
            this.eventDelegate.LeapEventNotification("onFrame");
        }
        public override void OnExit(Controller controller)
        {
            this.eventDelegate.LeapEventNotification("onExit");
        }
        public override void OnDisconnect(Controller controller)
        {
            this.eventDelegate.LeapEventNotification("onDisconnect");
        }
    }
}
