using System;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace FontTest
{
    public partial class Form1 : Form
    {
        #region Global Variables

        private const string TempPath = "TEMP";
        private const string EmbeddedFontName = "AlexBrush-Regular.ttf";
        public static Font MyFont = new Font("Arial", 11);

        #endregion

        #region Form Components
        public Form1()
        {
            InitializeComponent();

            if ( ! UseBuiltFont(EmbeddedFontName, 18) )
            { MessageBox.Show("Error to install font!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); };
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            label2.Font = MyFont;
            label2.Text = textBox1.Text;
        }
        #endregion

        #region Functions

        public static bool UseBuiltFont(string pEmbeddedFontName, int pSize = 18)
        {
            var FontPath = GetBuiltFont(pEmbeddedFontName);
            try
            {
                // Use a font as "Private Colletion" without install it on Windows Fonts
                PrivateFontCollection privateFonts = new PrivateFontCollection();
                privateFonts.AddFontFile(FontPath);
                Font font = new Font(privateFonts.Families[0], pSize);
                MyFont = font;

                return true;
            }
            catch { return false; }
        }
        public static string GetBuiltFont(string pEmbeddedFontName)
        {
            string result = null;

            // Make a new temp file name
            string TempFile = Path.GetTempFileName();
            string MyFont = "MyFont." + Path.ChangeExtension(Path.GetFileName(TempFile), "ttf");
            MyFont = Path.Combine(Environment.GetEnvironmentVariable(TempPath), MyFont);

            // Delete old files
            try
            {
                DirectoryInfo FilesToDelete = new DirectoryInfo(Environment.GetEnvironmentVariable(TempPath));
                foreach (FileInfo File in FilesToDelete.GetFiles())
                {
                    if (File.Name.Contains("MyFont.tmp"))
                    { File.Delete(); }
                }
            }
            catch { }

            // Extract Font
            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                Stream LocalPsExec = assembly.GetManifestResourceStream(assembly.GetName().Name + "." + pEmbeddedFontName);
                FileStream writer = new FileStream(MyFont, FileMode.Create, FileAccess.Write);
                LocalPsExec.CopyTo(writer);
                writer.Close();
            }
            catch { }
            result = MyFont;

            return result;
        }

        #endregion

    }
}
