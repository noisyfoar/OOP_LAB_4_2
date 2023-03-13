using OOP_LAB_4_2.Properties;
using System.Reflection;

namespace OOP_LAB_4_2
{
    public partial class Form1 : Form
    {
        Model model;
        EventHandler handler;
        public Form1()
        {
            InitializeComponent();
            
            model = new Model();
            model.observers += new EventHandler(this.UpdateFromModel);
            model.observers.Invoke(this, EventArgs.Empty);
            
            handler += TextBoxA_LostFocus;
            handler += TextBoxB_LostFocus;
            handler += TextBoxC_LostFocus;
            
            textBoxA.LostFocus += TextBoxA_LostFocus;
            textBoxB.LostFocus +=TextBoxB_LostFocus;
            textBoxC.LostFocus+= TextBoxC_LostFocus;

            Application.ApplicationExit += Application_ApplicationExit;
        }

        private void Application_ApplicationExit(object sender, EventArgs e)
        {
            model.save();
        }

        private void TextBoxA_LostFocus(object sender, EventArgs e)
        {
            try
            {
                int a = Convert.ToInt32(textBoxA.Text);
                model.setA(a);
            }
            catch (Exception ex) { }
        }
        private void TextBoxB_LostFocus(object sender, EventArgs e)
        {
            try
            {
                int b = Convert.ToInt32(textBoxB.Text);
                model.setB(b);
            }
            catch (Exception ex) { }
        }
        private void TextBoxC_LostFocus(object sender, EventArgs e)
        {
            try
            {
                int c = Convert.ToInt32(textBoxC.Text);
                model.setC(c);
            }
            catch (Exception ex) { }
        }
        private void numericUpDownA_ValueChanged(object sender, EventArgs e)
        {
            model.setA(Decimal.ToInt32(numericUpDownA.Value));
        }
        private void numericUpDownB_ValueChanged(object sender, EventArgs e)
        {
            model.setB(Decimal.ToInt32(numericUpDownB.Value));
        }
        private void numericUpDownC_ValueChanged(object sender, EventArgs e)
        {
            model.setC(Decimal.ToInt32(numericUpDownC.Value));
        }

        private void trackBarA_Scroll(object sender, EventArgs e)
        {
            model.setA(trackBarA.Value);
        }
        private void trackBarB_Scroll(object sender, EventArgs e)
        {
            model.setB(trackBarB.Value);
        }
        private void trackBarC_Scroll(object sender, EventArgs e)
        {
            model.setC(trackBarC.Value);
        }


        private void UpdateFromModel(object sender, EventArgs e)
        {
            int a = model.getA();
            int b = model.getB();
            int c = model.getC();
            textBoxA.Text = a.ToString();
            textBoxB.Text = b.ToString();
            textBoxC.Text = c.ToString();
            numericUpDownA.Value = Convert.ToDecimal(a);
            numericUpDownB.Value = Convert.ToDecimal(b);
            numericUpDownC.Value = Convert.ToDecimal(c);
            trackBarA.Value = a;
            trackBarB.Value = b;
            trackBarC.Value = c;

        }
        private void textBoxA_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode== Keys.Enter)
            {
                handler.Invoke(sender, null);
            }
        }


    }
    class Model
    {
        private int A, B, C;
        public EventHandler observers;
        public Model()
        {
            A = Settings.Default.A;
            B = Settings.Default.B;
            C = Settings.Default.C;
        }
        public void setA(int value)
        {
            observers.Invoke(this, EventArgs.Empty);
            if (value < 0)
            {
                return;
            }
            if(value > 100)
            {
                return;
            }
            if(value > B)
            {
                setB(value);
            }
            if(value > C)
            {
                setC(value);
            }
            A = value;
            observers.Invoke(this, EventArgs.Empty);
        }
        public void setB(int value)
        {
            observers.Invoke(this, EventArgs.Empty);
            if (value < 0)
            {
                return;
            }
            if (value > 100)
            {
                return;
            }
            if(value < A)
            {
                return;
            }
            if(value > C)
            {
                return;
            }
            B = value;
            observers.Invoke(this, EventArgs.Empty);
        }
        public void setC(int value)
        {
            observers.Invoke(this, EventArgs.Empty);
            if (value < 0)
            {
                return;
            }
            if (100 < value)
            {
                return;
            }
            if (B > value)
            {
                setB(value);
            }
            if (A > value)
            {
                setA(value);
            }
            C = value;
            observers.Invoke(this, EventArgs.Empty);
        }

        public int getA()
        {
            return A;
        }
        public int getB()
        {
            return B;
        }
        public int getC()
        {
            return C;
        }
        public void save()
        {
            Settings.Default.A = A;
            Settings.Default.B = B;
            Settings.Default.C = C;
            Settings.Default.Save();
        }
    }
}