using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    M mayinclass;
    protected void Page_Init(object sender, EventArgs e)
    {
        //Session.Clear();
        if (Convert.ToInt32(Session["k"]) > 0)
        {
            if (!IsPostBack)
            {
                ktxt.Text = Session["k"].ToString();
                msayisitxt.Text = Session["msayisi"].ToString();
            }
            mayinclass = new M(false, Convert.ToInt32(Session["k"]), Convert.ToInt32(Session["msayisi"]));
            try
            {
                mayinclass.buttonolustur();
            }
            catch (Exception)
            {
            } 
        }
        else
            mayinclass = new M(true, Convert.ToInt32(ktxt.Text), Convert.ToInt32(msayisitxt.Text));


        Literal li;
        for (int i = 0; i < mayinclass.cells.GetLength(0); i++)
        {
            for (int ii = 0; ii < mayinclass.cells.GetLength(1); ii++)
            {
                try
                {
                    mayinclass.cells[i, ii].Click += new EventHandler(cell_Click);
                    pnl.Controls.Add(mayinclass.cells[i, ii]);
                }
                catch (Exception)
                {
                }
            }
            li = new Literal(); li.Text = "<br />";
            pnl.Controls.Add(li);
        }

        if (!IsPostBack)
        {
            if (Convert.ToInt16(Session["gstr"]) > 0)
                gstr.Checked = true;
            else
                gstr.Checked = false;
        }

    }

   
    protected void olustur_Click(object sender, EventArgs e)
    {
        mayinclass = new M(true, Convert.ToInt32(ktxt.Text), Convert.ToInt32(msayisitxt.Text));
        Response.Redirect("Default.aspx");

    }

    protected void cell_Click(object sender, EventArgs e)
    {
        Button s = (Button)sender;
        int column, row;
        column = Convert.ToInt16(s.ID.Substring(1, s.ID.IndexOf("_") - 1));
        row = Convert.ToInt16(s.ID.Substring(s.ID.IndexOf("_")+1));

        mayinclass.BTNClickEvent(column, row);
        //Session["cell" + column + "," + row + "-enb"] = false;
        //Session["cell" + column + "," + row + "-txt"] = mayinclass.hesapla(column,row);

        //for (int i = 0; i < mayinclass.myn.GetLength(0); i++)
        //    {
        //        Response.Write("<br>m:" + mayinclass.myn[i, 0] + "-" + mayinclass.myn[i, 1]);
        //        if (mayinclass.myn[i, 0] == column && mayinclass.myn[i, 1] == row)
        //        {
        //            Response.Write("<br>Mayın: " + i);
        //            break;
        //        }
			 

        //    }

        //mayinclass.BTNClickEvent();
        Response.Redirect("Default.aspx");
    }

    protected void gstr_CheckedChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt16(Session["gstr"])== 0)
        {
            mayinclass.mayinkoy();
            Session["gstr"] = 1;
        }
        else
        {
            for (int i = 0; i < mayinclass.myn.GetLength(0); i++)
            {
                Session["cell" + mayinclass.myn[i, 0] + "," + mayinclass.myn[i, 1] + "-txt"] = "";
            }
            Session["gstr"] = 0;
        }
        Response.Redirect("Default.aspx");
    }
}