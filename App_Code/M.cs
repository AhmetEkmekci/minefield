using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;

/// <summary>
/// Summary description for M
/// </summary>
public class M : Page
{
    public int[,] myn;
    public Button[,] cells;
    int mayinsayisi;
	public M(bool r, int k, int msayisi)
	{
		//
		// TODO: Add constructor logic here
		//

        mayinsayisi = msayisi;
        myn = new int[msayisi, 2];
        cells = new Button[k, k];

        if (r)
        {
            Session.Clear();
            Session["msayisi"] = msayisi;
            Session["k"] = k;
            yenibuttonolustur();
            mayinolustur();
        }
        else mayin_init();
        
	}


    public void yenibuttonolustur()
    {

        /*
          * cell0,0-txt
          * cell0,0-enb
          * cell0,0-col
          * 
         */

        for (int i = 0; i < cells.GetLength(0); i++)
        {
            for (int ii = 0; ii < cells.GetLength(1); ii++)
            {
                cells[i, ii] = new Button();
                cells[i, ii].ID = "b" + i + "_" + ii;

                cells[i, ii].Style.Add("font-size:", "20px");
                cells[i, ii].Style.Add("margin-left", "1px");
                cells[i, ii].Style.Add("margin-top", "1px");
                cells[i, ii].Style.Add("width", "24px");
                cells[i, ii].Style.Add("height", "22px");
                cells[i, ii].Style.Add("background-color", "#ebebeb");
                cells[i, ii].Style.Add("border", "#ccc solid 1px");

                Session["cell" + i + "," + ii + "-txt"] = "";
                Session["cell" + i + "," + ii + "-enb"] = "true";
                Session["cell" + i + "," + ii + "-col"] = "#000";


            }
        }

    }



    public void buttonolustur()
    {
        /*
         * cell0,0-txt
         * cell0,0-enb
         * cell0,0-col
         * 
        */

        for (int i = 0; i < cells.GetLength(0); i++)
        {
            for (int ii = 0; ii < cells.GetLength(1); ii++)
            {
                cells[i, ii] = new Button();
                cells[i, ii].ID = "b" + i +"_" + ii;

                cells[i, ii].Style.Add("font-size:", "20px");
                cells[i, ii].Style.Add("margin-left", "1px");
                cells[i, ii].Style.Add("margin-top", "1px");
                cells[i, ii].Style.Add("width", "24px");
                cells[i, ii].Style.Add("height", "22px");
                cells[i, ii].Style.Add("border", "#ccc solid 1px");

                cells[i, ii].Text=  Session["cell" + i + "," + ii + "-txt"].ToString() ;
                cells[i, ii].Enabled = Convert.ToBoolean(Session["cell" + i + "," + ii + "-enb"] );
                cells[i, ii].Style.Add("color", Session["cell" + i + "," + ii + "-col"].ToString());

                if(! Convert.ToBoolean(Session["cell" + i + "," + ii + "-enb"] ))
                    cells[i, ii].Style.Add("background-color", "#fafafa");
                else
                    cells[i, ii].Style.Add("background-color", "#ebebeb");
            }
        }

    }


    void mayinolustur()
    {
        Random rnd = new Random();
        for (int i = 0; i < mayinsayisi; i++)
        {
            myn[i, 0] = rnd.Next(cells.GetLength(0));
            myn[i, 1] = rnd.Next(cells.GetLength(0));
            for (int ii = 0; ii < i; ii++)
            {
                if (myn[i, 0] == myn[ii, 0] && myn[i, 1] == myn[ii, 1])
                {
                    i = i - 1;
                    break;
                }
            }

            //cells[myn[i, 0], myn[i, 1]].Text = "*";

        }
        mayinkaydet();
    }

    void mayinkaydet()
    {
        for (int i = 0; i < mayinsayisi; i++)
        {
            Session["m" + i + "r"] = myn[i, 0];
            Session["m" + i + "c"] = myn[i, 1];
        }


    }

    void mayin_init()
    {
        for (int i = 0; i < mayinsayisi; i++)
        {
            myn[i, 0] = Convert.ToInt16(Session["m" + i + "r"]);
            myn[i, 1] = Convert.ToInt16(Session["m" + i + "c"]);
        }

    }

    public void mayinkoy()
    {
        for (int i = 0; i < myn.GetLength(0); i++)
        {
            Session["cell" + myn[i, 0] + "," + myn[i, 1] + "-txt"] = ".";
            //Session["cell" + myn[i, 0] + "," + myn[i, 1] + "-enb"] = "false";
            Session["cell" + myn[i, 0] + "," + myn[i, 1] + "-col"] = "#f00";
            //cells[myn[i, 0], myn[i, 1]].Text = ".";
        }


    }

    public void BTNClickEvent(int column, int row)
    {
        if (Convert.ToBoolean(Session["cell" + column + "," + row + "-enb"]))
        {
            string hsp = hesapla(column, row);
            if (hsp == "")
            {
                BTNClickEvent(Math.Abs(column - 1), Math.Abs(row - 1));
                BTNClickEvent(Math.Abs(column - 1), Math.Abs(row + 1));
                BTNClickEvent(Math.Abs(column - 1), Math.Abs(row));
                BTNClickEvent(Math.Abs(column + 1), Math.Abs(row - 1));
                BTNClickEvent(Math.Abs(column + 1), Math.Abs(row + 1));
                BTNClickEvent(Math.Abs(column + 1), Math.Abs(row));
                BTNClickEvent(Math.Abs(column), Math.Abs(row - 1));
                BTNClickEvent(Math.Abs(column), Math.Abs(row + 1));
            }

            Session["cell" + column + "," + row + "-txt"] = hsp;
            Session["cell" + column + "," + row + "-enb"] = false;
        }
    }


    string []colorset = { "#000", "#00f", "#0f0", "#f55", "#005", "#050", "#f50", "#407", "#703" };
    public string hesapla(int column, int row)
    {
        int sayac = 0;
        for (int i = 0; i < mayinsayisi; i++)
            if (column == myn[i, 0] && row == myn[i, 1])
            {

                OyunBitir();
                return "X";
            }
            else if (column == myn[i, 0] - 1 && row >= myn[i, 1] - 1 && row <= myn[i, 1] + 1)
                sayac += 1;
            else if (column == myn[i, 0] && (row == myn[i, 1] - 1 || row == myn[i, 1] + 1))
                sayac += 1;
            else if (column == myn[i, 0] + 1 && row >= myn[i, 1] - 1 && row <= myn[i, 1] + 1)
                sayac += 1;

        Session["cell" + column + "," + row + "-col"] = colorset[sayac];
        Session["cell" + column + "," + row + "-enb"] = false;

        if (sayac > 0) return sayac.ToString(); else return "";

    }

    private void OyunBitir()
    {
        for (int i = 0; i < mayinsayisi; i++)
        {
            Session["cell" + myn[i, 0] + "," + myn[i, 1] + "-txt"] = "*";
            Session["cell" + myn[i, 0] + "," + myn[i, 1] + "-enb"] = "false";
            Session["cell" + myn[i, 0] + "," + myn[i, 1] + "-col"] = "#f00";
        }
        //for (int i = 0; i < cells.GetLength(0); i++)
        //{
        //    for (int ii = 0; ii < cells.GetLength(1); ii++)
        //    {
        //        Session["cell" + i + "," + ii + "-enb"] = "false";
        //    }
        //}
    }
}