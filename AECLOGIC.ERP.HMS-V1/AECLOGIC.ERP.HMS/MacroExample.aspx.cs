using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Office.Interop.Word;
using System.Drawing;
public partial class MacroExample : WebFormMaster
{
    object fileName, missing;

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            ApplicationClass wordApp = new ApplicationClass();
            for (int i = 1; i <= 5; i++)
            {

                fileName = "D:\\" + "Macro\\" + Convert.ToString(i) + ".docx";
                missing = System.Reflection.Missing.Value;
                Document wordDoc = wordApp.Documents.Add(ref missing, ref missing, ref missing, ref missing);

                wordDoc.Activate();
                wordDoc.Content.InsertBefore("This is Word document created in ASP.NET");
                wordDoc.Content.InsertParagraphAfter();
                wordDoc.SaveAs(ref fileName);
                //Label1.Text = "Word document is created";
            }
            wordApp.Application.Quit(missing, missing, missing);
        }
        catch
        {
            Label1.Text = "Cannot create word document";
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            ApplicationClass wordApp = new ApplicationClass();
            for (int i = 1; i <= 5; i++)
            {
                  fileName = "D:\\" + "Macro\\" + Convert.ToString(i) + ".docx";
                  string x = fileName.ToString();
                  missing = System.Reflection.Missing.Value;
                object visible = true;
                object readOnly = false;

                if (x != "D:\\" + "Macro\\" + "5" + ".docx")
                {
                    Document wordDoc = wordApp.Documents.Open(ref fileName, ref missing, ref readOnly, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, visible, ref missing, ref missing, ref missing, ref missing);
                    wordDoc.Content.InsertAfter("Insert Text in already created word document");
                    wordDoc.Save();
                }
                else
                {
                    Document wordDoc = wordApp.Documents.Open(ref fileName, ref missing, ref readOnly, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, visible, ref missing, ref missing, ref missing, ref missing);
                    wordDoc.Content.InsertAfter("Hi Dileeph how are you.!");
                    wordDoc.Content.Font.Bold = 1;
                    wordDoc.Content.Font.Color = WdColor.wdColorLightBlue;
                    wordDoc.Content.Font.Italic = 1;
                    //wordDoc.Content.CheckGrammar();
                    wordDoc.Save();
                }
            }
            wordApp.Application.Quit(ref missing, ref missing, ref missing);

            Label1.Text = "Text inserted in word document";
        }
        catch
        {
            Label1.Text = "Cannot insert text in word document";
        }
    }
}