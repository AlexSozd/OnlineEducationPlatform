using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Calculation1 : System.Web.UI.Page
{
    protected double a = 1.0000002, alpha, delta, e1, e0, eps0, eps, fi, /*hamma0, hamma,*/ hs1, hs2, lambda_0, lambda, l0, l, m0, m;
    protected double rad_alpha, rad_delta, rad_eps, /*rad_hamma,*/ rad_lambda, rad_l, rad_m;
    protected DateTime reftime_gen = new DateTime(1975, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    protected void Page_Load(object sender, EventArgs e)
    {
        lat.InnerText = "";
        lon.InnerText = "";
        if(!IsPostBack)
        {
            cur_date.Value = DateTime.UtcNow.ToString();
            //centr1.InnerText = "";
            //rad1.InnerText = "";
            //centr2.InnerText = "";
            //rad2.InnerText = "";
            eps0 = 23.442533;
            e0 = 0.01671974;
            l0 = 279.0415;
            lambda_0 = 248.84411;
            m0 = 356.5311;

            //MakeList();
            FillList();
        }
    }
    public void FillList()
    {
        int i;
        List<Celbody> cels;
        FileStream A = new FileStream("C:/Users/Lenovo/Documents/Visual Studio 2013/WebSites/Astronavigation/Eddata/Celbodies", FileMode.Open);
        BinaryFormatter C = new BinaryFormatter();
        cels = (List<Celbody>)C.Deserialize(A);
        for (i = 0; i < cels.Count; i++)
        {
            fir_body1.Items.Add(cels[i].GetName());
            sec_body1.Items.Add(cels[i].GetName());
        }
        A.Close();
    }
    protected void fir_body1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void sec_body1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void button1_Click(object sender, EventArgs e)
    {
        centr1.InnerText = "";
        rad1.InnerText = "";
        centr2.InnerText = "";
        rad2.InnerText = "";
        //Work with data
        double alpha1, alpha2, delta1, delta2, al_nut, del_nut, al_proc, del_proc, al_ab, del_ab;
        double mu_al, mu_del, /*hs1, hs2,*/ hh1, hh2, longde;
        double[] x = new double[2], y = new double[2];
        string b1, b2;
        DateTime t1, t2;
        Celbody cb1, cb2;
        /*try
        {*/
        b1 = fir_body1.SelectedValue;
        b2 = sec_body1.SelectedValue;
        //Скопировать объекты из списка в файле
        GetObj(b1, b2, out cb1, out cb2);
        hs1 = double.Parse(fir_ht.Value.Replace('.', ','));

        hh1 = GetAbsHeight(fir_ht1.Value.Replace('.', ','));
        hs1 = hs1 - hh1;
        //Поправки на температуру и давление; 
        //видимый радиус - добавить (через таблицы)
        //hs1 = hs1 - (0.97 / 3600) * (1.000 / Math.Tan(hs1));

        hs1 = hs1 - GetDelWeather(hs1, fir_t.Value, fir_p.Value);
        t1 = Convert.ToDateTime(fir_time.Value.Replace('.', ','), new System.Globalization.CultureInfo("ru-RU"));
        t1 = DateTime.SpecifyKind(t1, DateTimeKind.Utc);
        lambda = lambda_0 - (3.000 / 60 + 10.6 / 3600) * (t1 - reftime_gen).TotalDays;
        eps = eps0 - (0.468 / 3600) * ((t1 - reftime_gen).TotalDays / 365.2426);
        e1 = e0 - (0.00000042) * ((t1 - reftime_gen).TotalDays / 365.2426);
        l = l0 + (59.000 / 60 + 8.33 / 3600) * (t1 - reftime_gen).TotalDays;
        m = m0 + (59.000 / 60 + 8.161 / 3600) * (t1 - reftime_gen).TotalDays;
        alpha = cb1.RA;
        delta = cb1.Dec;
        alpha1 = cb1.RA;
        delta1 = cb1.Dec;
        mu_al = cb1.gradRA;
        mu_del = cb1.gradDec;

        rad_alpha = alpha * (Math.PI / 180);
        rad_delta = delta * (Math.PI / 180);
        rad_eps = eps * (Math.PI / 180);
        //rad_hamma = hamma * (Math.PI / 180);
        rad_l = l * (Math.PI / 180);
        rad_lambda = lambda * (Math.PI / 180);
        rad_m = m * (Math.PI / 180);

        Nutation(out al_nut, out del_nut);
        Aberration(out al_ab, out del_ab);
        if (cb1.BType == BodyType.Star)
        {
            ProcesAndNatMov(mu_al, mu_del, t1, out al_proc, out del_proc);
            alpha1 = alpha + (al_proc + mu_al) * ((t1 - cb1.RefTime).TotalDays / 365.2426) + al_nut + al_ab;
            delta1 = delta + (del_proc + mu_del) * ((t1 - cb1.RefTime).TotalDays / 365.2426) + del_nut + del_ab;
        }
        if (cb1.BType == BodyType.MainLight)
        {
            DayMov(out alpha, out delta, t1);
            Nutation(out al_nut, out del_nut);
            Aberration(out al_ab, out del_ab);
            alpha1 = alpha + al_nut + al_ab;
            delta1 = delta + del_nut + del_ab;
            if (cb1.GetName() == "Солнце")
            {
                hs1 = hs1 + (8.79 / 3600) * Math.Cos(hs1 * (Math.PI / 180)) + (16.05 / 60);
            }
        }

        rad1.InnerText = (90 - hs1) + " градусов";
        //hs1 = hs1 * (Math.PI / 180);

        hs2 = double.Parse(sec_ht.Value.Replace('.', ','));

        hh2 = GetAbsHeight(sec_ht1.Value.Replace('.', ','));
        hs2 = hs2 - hh2;
        //hs2 = hs2 - (0.97 / 3600) * (1.000 / Math.Tan(hs2));
        hs2 = hs2 - GetDelWeather(hs2, sec_t.Value, sec_p.Value);
        t2 = Convert.ToDateTime(sec_time.Value.Replace(',', '.'), new System.Globalization.CultureInfo("ru-RU"));
        t2 = DateTime.SpecifyKind(t2, DateTimeKind.Utc);
        lambda = lambda_0 - (3.000 / 60 + 10.6 / 3600) * (t2 - reftime_gen).TotalDays;
        eps = eps0 - (0.468 / 3600) * ((t2 - reftime_gen).TotalDays / 365.2426);
        e1 = e0 - (0.00000042) * ((t2 - reftime_gen).TotalDays / 365.2426);
        l = l0 + (59.000 / 60 + 8.33 / 3600) * (t2 - reftime_gen).TotalDays;
        m = m0 + (59.000 / 60 + 8.161 / 3600) * (t2 - reftime_gen).TotalDays;
        alpha = cb2.RA;
        delta = cb2.Dec;
        alpha2 = cb2.RA;
        delta2 = cb2.Dec;
        mu_al = cb2.gradRA;
        mu_del = cb2.gradDec;

        rad_alpha = alpha * (Math.PI / 180);
        rad_delta = delta * (Math.PI / 180);
        rad_eps = eps * (Math.PI / 180);
        //rad_hamma = hamma * (Math.PI / 180);
        rad_l = l * (Math.PI / 180);
        rad_lambda = lambda * (Math.PI / 180);
        rad_m = m * (Math.PI / 180);

        Nutation(out al_nut, out del_nut);
        Aberration(out al_ab, out del_ab);
        if (cb2.BType == BodyType.Star)
        {
            ProcesAndNatMov(mu_al, mu_del, t2, out al_proc, out del_proc);
            alpha2 = alpha + (al_proc + mu_al) * ((t2 - cb2.RefTime).TotalDays / 365.2426) + al_nut + al_ab;
            delta2 = delta + (del_proc + mu_del) * ((t2 - cb2.RefTime).TotalDays / 365.2426) + del_nut + del_ab;
        }
        if (cb2.BType == BodyType.MainLight)
        {
            DayMov(out alpha, out delta, t2);
            Nutation(out al_nut, out del_nut);
            Aberration(out al_ab, out del_ab);
            alpha2 = alpha + al_nut + al_ab;
            delta2 = delta + del_nut + del_ab;
            if (cb2.GetName() == "Солнце")
            {
                hs2 = hs2 + (8.79 / 3600) * Math.Cos(hs2) + (16.05 / 60);
            }
        }

        rad2.InnerText = (90 - hs2) + " градусов";
        //hs2 = hs2 * (Math.PI / 180);

        centr1.InnerText += Math.Floor(Math.Abs(delta1)) + " градусов " + Math.Floor((Math.Abs(delta1) - Math.Floor(Math.Abs(delta1))) * 60) + " минут ";
        centr1.InnerText += Math.Round(((((Math.Abs(delta1) - Math.Floor(Math.Abs(delta1))) * 60) - Math.Floor((Math.Abs(delta1) - Math.Floor(Math.Abs(delta1))) * 60)) * 60), 2, MidpointRounding.AwayFromZero) + " секунд";
        if (delta1 < 0)
        {
            centr1.InnerText += " южной широты ";
        }
        else
        {
            centr1.InnerText += " северной широты ";
        }
        longde = GetHour(t1) * (Math.PI / 180) - alpha1;
        longde = longde * (180 / Math.PI);
        longde = longde % 360;
        if (longde > 180)
        {
            longde = longde - 360;
        }
        if (longde < -180)
        {
            longde = longde + 360;
        }
        centr1.InnerText += Math.Floor(Math.Abs(longde)) + " градусов " + Math.Floor((Math.Abs(longde) - Math.Floor(Math.Abs(longde))) * 60) + " минут ";
        centr1.InnerText += Math.Round(((((Math.Abs(longde) - Math.Floor(Math.Abs(longde))) * 60) - Math.Floor((Math.Abs(longde) - Math.Floor(Math.Abs(longde))) * 60)) * 60), 2, MidpointRounding.AwayFromZero) + " секунд";
        if (longde < 0)
        {
            centr1.InnerText += " западной долготы";
        }
        else
        {
            centr1.InnerText += " восточной долготы";
        }

        centr2.InnerText += Math.Floor(Math.Abs(delta2)) + " градусов " + Math.Floor((Math.Abs(delta2) - Math.Floor(Math.Abs(delta2))) * 60) + " минут ";
        centr2.InnerText += Math.Round(((((Math.Abs(delta2) - Math.Floor(Math.Abs(delta2))) * 60) - Math.Floor((Math.Abs(delta2) - Math.Floor(Math.Abs(delta2))) * 60)) * 60), 2, MidpointRounding.AwayFromZero) + " секунд";
        if (delta2 < 0)
        {
            centr2.InnerText += " южной широты ";
        }
        else
        {
            centr2.InnerText += " северной широты ";
        }
        /*if (alpha2 > 180)
        {
            longde = alpha2 - 360;
        }
        else
        {
            longde = alpha2;
        }
        longde = GetHour(t1) * (Math.PI / 180) - alpha2;
        longde = longde * (180 / Math.PI);*/

        longde = GetHour(t1) * (Math.PI / 180) - alpha2;
        longde = longde * (180 / Math.PI);
        longde = longde % 360;
        if (longde > 180)
        {
            longde = longde - 360;
        }
        if (longde < -180)
        {
            longde = longde + 360;
        }

        centr2.InnerText += Math.Floor(Math.Abs(longde)) + " градусов " + Math.Floor((Math.Abs(longde) - Math.Floor(Math.Abs(longde))) * 60) + " минут ";
        centr2.InnerText += Math.Round(((((Math.Abs(longde) - Math.Floor(Math.Abs(longde))) * 60) - Math.Floor((Math.Abs(longde) - Math.Floor(Math.Abs(longde))) * 60)) * 60), 2, MidpointRounding.AwayFromZero) + " секунд";
        if (longde < 0)
        {
            centr2.InnerText += " западной долготы";
        }
        else
        {
            centr2.InnerText += " восточной долготы";
        }

        /*delta1 = delta1 * (Math.PI / 180);
        delta2 = delta2 * (Math.PI / 180);
        alpha1 = alpha1 * (Math.PI / 180);
        alpha2 = alpha2 * (Math.PI / 180);*/
        //Сама формула нахождения координат по высотам, склонению и восхождению - здесь её нет
    }
    public double GetAbsHeight(string str)
    {
        double val = 0.000, h_abs;
        h_abs = double.Parse(str);
        //Formulae
        val = 0.92 * Math.Sqrt(2 * h_abs / (6371116 + h_abs));
        return val;
    }
    public double GetDelWeather(double h, string t, string p)
    {
        double val = 0.000, v_h, v_t, v_p;
        v_h = h * (Math.PI / 180);
        v_t = double.Parse(t.Replace('.', ','));
        v_p = double.Parse(p.Replace('.', ','));
        val = (v_p / (273 + v_t)) * (Math.Cos(v_h) / Math.Sin(v_h)) * (0.359995 - 0.000399 * (Math.Cos(v_h * v_h) / Math.Sin(v_h * v_h)));
        return val;
    }
    public void GetObj(string b1, string b2, out Celbody cb1, out Celbody cb2)
    {
        int i;
        List<Celbody> cels;
        cb1 = null;
        cb2 = null;
        FileStream A = new FileStream("C:/Users/Lenovo/Documents/Visual Studio 2013/WebSites/Astronavigation/Eddata/Celbodies", FileMode.Open);
        BinaryFormatter C = new BinaryFormatter();
        cels = (List<Celbody>)C.Deserialize(A);
        for (i = 0; i < cels.Count; i++)
        {
            if (b1 == cels[i].GetName())
            {
                cb1 = cels[i];
            }
            if (b2 == cels[i].GetName())
            {
                cb2 = cels[i];
            }
        }
        A.Close();
    }
    public void Nutation(out double al_nut, out double del_nut)
    {
        //al_nut = (((-17.24 / 3600) * Math.Sin(hamma)) / Math.Cos(delta)) * (Math.Cos(eps) * Math.Cos(delta) + Math.Sin(eps) * Math.Sin(alpha) * Math.Sin(delta));
        //al_nut = al_nut - (((9.21 / 3600) * Math.Cos(hamma)) / Math.Cos(delta)) * (Math.Cos(alpha) * Math.Sin(delta));
        //del_nut = ((-17.24 / 3600) * Math.Sin(hamma)) * Math.Sin(eps) * Math.Cos(alpha) + ((9.21 / 3600) * Math.Cos(hamma)) * Math.Sin(alpha);
        
        //al_nut = (((-17.24 / 3600) * Math.Sin(rad_hamma)) / Math.Cos(rad_delta)) * (Math.Cos(rad_eps) * Math.Cos(rad_delta) + Math.Sin(rad_eps) * Math.Sin(rad_alpha) * Math.Sin(rad_delta));
        //al_nut = al_nut - (((9.21 / 3600) * Math.Cos(rad_hamma)) / Math.Cos(rad_delta)) * (Math.Cos(rad_alpha) * Math.Sin(rad_delta));
        //del_nut = ((-17.24 / 3600) * Math.Sin(rad_hamma)) * Math.Sin(rad_eps) * Math.Cos(rad_alpha) + ((9.21 / 3600) * Math.Cos(rad_hamma)) * Math.Sin(rad_alpha);

        al_nut = (((-17.24 / 3600) * Math.Sin(rad_lambda)) / Math.Cos(rad_delta)) * (Math.Cos(rad_eps) * Math.Cos(rad_delta) + Math.Sin(rad_eps) * Math.Sin(rad_alpha) * Math.Sin(rad_delta));
        al_nut = al_nut - (((9.21 / 3600) * Math.Cos(rad_lambda)) / Math.Cos(rad_delta)) * (Math.Cos(rad_alpha) * Math.Sin(rad_delta));
        del_nut = ((-17.24 / 3600) * Math.Sin(rad_lambda)) * Math.Sin(rad_eps) * Math.Cos(rad_alpha) + ((9.21 / 3600) * Math.Cos(rad_lambda)) * Math.Sin(rad_alpha);
    }
    public void Aberration(out double al_ab, out double del_ab)
    {
        //al_ab = -((20.496 / 3600) / Math.Cos(delta)) * (Math.Sin(alpha) * Math.Sin(l) + Math.Cos(alpha) * Math.Cos(l) * Math.Cos(eps));
        //del_ab = -(20.496 / 3600) * (Math.Cos(l) * (Math.Sin(eps) * Math.Cos(delta) - Math.Cos(eps) * Math.Sin(delta) * Math.Sin(alpha)) + Math.Sin(l) - Math.Cos(alpha) * Math.Sin(delta));
        al_ab = -((20.496 / 3600) / Math.Cos(rad_delta)) * (Math.Sin(rad_alpha) * Math.Sin(rad_l) + Math.Cos(rad_alpha) * Math.Cos(rad_l) * Math.Cos(rad_eps));
        del_ab = -(20.496 / 3600) * (Math.Cos(rad_l) * (Math.Sin(rad_eps) * Math.Cos(rad_delta) - Math.Cos(rad_eps) * Math.Sin(rad_delta) * Math.Sin(rad_alpha)) + Math.Sin(rad_l) - Math.Cos(rad_alpha) * Math.Sin(rad_delta));
    }
    public void ProcesAndNatMov(double mu_al, double mu_del, DateTime t, out double al_proc, out double del_proc)
    {
        al_proc = 0.000;
        del_proc = 0.000;
        int i, n = (int)Math.Round((t - reftime_gen).TotalDays / 365.2426);
        //double al_i = alpha, del_i = delta;
        double al_i = rad_alpha, del_i = rad_delta;
        for (i = 0; i < n; i++)
        {
            //al_proc = al_proc + (((46.106 / 3600) * Math.Cos(del_i) + (20.540 / 3600) * Math.Sin(al_i) * Math.Sin(del_i)) / Math.Cos(del_i) + mu_al) * ((t - reftime_gen).TotalDays / n);
            //del_proc = del_proc + ((20.540 / 3600) * Math.Cos(al_i) + mu_del) * ((t - reftime_gen).TotalDays / n);

            al_i = (((46.106 / 3600) * Math.Cos(del_i) + (20.540 / 3600) * Math.Sin(al_i) * Math.Sin(del_i)) / Math.Cos(del_i) + mu_al) * (((t - reftime_gen).TotalDays / n) / 365.2426);
            del_i = ((20.540 / 3600) * Math.Cos(al_i) + mu_del) * (((t - reftime_gen).TotalDays / n) / 365.2426);

            al_proc = al_proc + al_i;
            del_proc = del_proc + del_i;
        }
    }
    public double GetHour(DateTime t1)
    {
        double val = 0.000;
        DateTime dt = new DateTime(2017, 11, 3, 11, 43, 36, DateTimeKind.Utc);
        //val = 12 + (t1 - dt).TotalHours + 0.0027379 * (t1 - dt).TotalHours + ((-17.24 / 3600) * Math.Sin(hamma)) * Math.Cos(eps);
        //val = (t1 - dt).TotalHours + 0.0027379 * (t1 - dt).TotalHours;
        val = (t1 - dt).TotalDays + 0.0027379 * (t1 - dt).TotalDays;
        val = val * 24;
        val = val % 24;
        //val = (val - Math.Floor(val)) * 24;
        //val = (val / 24) * 360 + ((-17.24 / 3600) * Math.Sin(rad_hamma)) * Math.Cos(rad_eps);
        val = (val / 24) * 360 + ((-17.24 / 3600) * Math.Sin(rad_lambda)) * Math.Cos(rad_eps);
        return val;
    }
    public void DayMov(out double al, out double del, DateTime t)
    {
        //al = 0.000;
        //del = 0.000;
        double e_kep, e_kep1 = 0.000, hamma, ham1, ham2, m_ven, m_jup, r, v = 0.000, cos_v, sin_v;
        eps = eps0 - (0.468 / 3600) * ((t - reftime_gen).TotalDays / 365.2426);
        e1 = e0 - (0.00000042) * ((t - reftime_gen).TotalDays / 365.2426);
        l = l0 + (59.000 / 60 + 8.33 / 3600) * (t - reftime_gen).TotalDays;
        m = m0 + (59.000 / 60 + 8.161 / 3600) * (t - reftime_gen).TotalDays;
        e_kep = m + e1 * Math.Sin(m);
        while (Math.Abs(e_kep1 - e_kep) < 0.0001)
        {
            e_kep1 = m + e1 * Math.Sin(e_kep);
        }
        r = a * (1 - e1 * Math.Cos(e_kep1 * (Math.PI / 180)));
        cos_v = (a - (Math.Cos(e_kep1 * (Math.PI / 180)) - e1)) / r;
        sin_v = (a * Math.Sqrt(1 - Math.Pow(e1, 2)) * Math.Sin(e_kep1 * (Math.PI / 180))) / r;
        if (cos_v > 0)
        {
            if (sin_v < 0)
            {
                v = Math.Asin(sin_v) * (180 / Math.PI) + 360;
            }
            else
            {
                v = Math.Acos(cos_v) * (180 / Math.PI) + 180;
            }
        }
        else if (cos_v < 0)
        {
            v = Math.Asin(sin_v) * (180 / Math.PI) + 180;
        }
        else
        {
            if (sin_v < 0)
            {
                v = 270;
            }
            else
            {
                v = 90;
            }
        }
        hamma = v + l - m;
        m_ven = 180.9567 + 58.1781 * ((t - reftime_gen).TotalDays / 365.2426);
        m_jup = 342.1735 + 30.705 * ((t - reftime_gen).TotalDays / 365.2426);
        //ham1 = (4.8 / 3600) * Math.Cos(299.1 + m_ven - m) + (5.5 / 3600) * Math.Cos(148.3167 + 2 * m_ven - 2 * m);
        //ham2 = (7.2 / 3600) * Math.Cos(179.533 - m_jup + m);
        ham1 = (4.8 / 3600) * Math.Cos((299.1 + m_ven - m) * (Math.PI / 180)) + (5.5 / 3600) * Math.Cos((148.3167 + 2 * m_ven - 2 * m) * (Math.PI / 180));
        ham2 = (7.2 / 3600) * Math.Cos((179.533 - m_jup + m) * (Math.PI / 180));

        hamma = hamma + ham1 + ham2;

        hamma = hamma * (Math.PI / 180);

        al = Math.Atan(Math.Tan(hamma) * Math.Cos(eps * (Math.PI / 180))) * (180 / Math.PI);
        del = Math.Asin(Math.Sin(hamma) * Math.Sin(eps * (Math.PI / 180))) * (180 / Math.PI);
    }
    protected void button2_Click(object sender, EventArgs e)
    {
        //h_s.Style["outline"] = form1.Style[HtmlTextWriterStyle.BackgroundColor];
        //lat_s.Style["outline"] = form1.Style[HtmlTextWriterStyle.BackgroundColor];
        //lon_s.Style["outline"] = form1.Style[HtmlTextWriterStyle.BackgroundColor];
        
        if(h_s.Value.Length > 0)
        {
            if (lat_s.Value.Length > 0)
            {
                if(lon_s.Value.Length > 0)
                {
                    Iterations();
                }
                else
                {
                    //lon_s.Value = Int32.MinValue.ToString();
                    //lon_s.Style["outline"] = "1px solid red";
                }
            }
            else
            {
                //lat_s.Value = Int32.MinValue.ToString();
                //lat_s.Style["outline"] = "1px solid red";
            }
        }
        else
        {
            //h_s.Value = ((hs1 + hs2) / 2).ToString();
            //h_s.Style.Add(HtmlTextWriterStyle.Color, "red");
            //h_s.Style["outline"] = "1px solid red";
            //h_s.Style.Add(HtmlTextWriterStyle.BorderColor, "red");
        }
    }
    protected void Iterations()
    {
        double a_s1, a_s2, h0, det_fi, det_long, fi_s, fi, longde_s, longde, rad_fi, n1, n2;
        
        h0 = double.Parse(h_s.Value.Replace('.', ','));
        fi_s = double.Parse(lat_s.Value.Replace('.', ','));
        longde_s = double.Parse(lon_s.Value.Replace('.', ','));
        a_s1 = double.Parse(as_1.Value.Replace('.', ','));
        a_s2 = double.Parse(as_2.Value.Replace('.', ','));

        n1 = h0 - hs1;
        n2 = h0 - hs2;
        //Add asimuths
        a_s1 = a_s1 * (Math.PI / 180);
        a_s2 = a_s2 * (Math.PI / 180);
        //dφ = (dh1 × sin A_s2 - dh2 × sin A_s1) / sin (A_s2 - A_s1)
        //det_fi = ((hs1 - h0) * Math.Sin(a_s2) - (hs2 - h0) * Math.Sin(a_s1)) / Math.Sin(a_s2 - a_s1);
        det_fi = (n1 * Math.Sin(a_s2) - n2 * Math.Sin(a_s1)) / Math.Sin(a_s2 - a_s1);
        //φ = φ_s + dφ
        //fi = fi_s + ((hs1 - h0) * Math.Sin(a_s2) - (hs2 - h0) * Math.Sin(a_s1)) / Math.Sin(a_s2 - a_s1);
        fi = fi_s + det_fi;
        //dλ = (dh2 × cos A_s1 - dh1 × cos A_s2) / (cos φ × sin (A_s2 - A_s1))
        rad_fi = fi * (Math.PI / 180);
        //det_long = ((hs2 - h0) * Math.Cos(a_s1) - (hs1 - h0) * Math.Cos(a_s2)) / (Math.Cos(fi * (Math.PI / 180)) * Math.Sin(a_s2 - a_s1));
        //det_long = ((hs2 - h0) * Math.Cos(a_s1) - (hs1 - h0) * Math.Cos(a_s2)) / (Math.Cos(rad_fi) * Math.Sin(a_s2 - a_s1));
        det_long = (n2 * Math.Cos(a_s1) - n1 * Math.Cos(a_s2)) / (Math.Cos(rad_fi) * Math.Sin(a_s2 - a_s1));
        //λ = λ_s + dλ
        //longde = longde_s + ((hs2 - h0) * Math.Cos(a_s1) - (hs1 - h0) * Math.Cos(a_s2)) / (Math.Cos(fi * (Math.PI / 180)) * Math.Sin(a_s2 - a_s1));
        //longde = longde_s + ((hs2 - h0) * Math.Cos(a_s1) - (hs1 - h0) * Math.Cos(a_s2)) / (Math.Cos(rad_fi) * Math.Sin(a_s2 - a_s1));
        longde = longde_s + det_long;
        
        lat.InnerText += Math.Floor(Math.Abs(fi)) + " градусов " + Math.Floor((Math.Abs(fi) - Math.Floor(Math.Abs(fi))) * 60) + " минут ";
        lat.InnerText += Math.Round(((((Math.Abs(fi) - Math.Floor(Math.Abs(fi))) * 60) - Math.Floor((Math.Abs(fi) - Math.Floor(Math.Abs(fi))) * 60)) * 60), 2, MidpointRounding.AwayFromZero) + " секунд";
        if (fi < 0)
        {
            lat.InnerText += " южной широты";
        }
        else
        {
            lat.InnerText += " северной широты";
        }

        if ((longde > 360) || (longde < -360))
        {
            longde = longde % 360;
            if (longde > 180)
            {
                longde = longde - 360;
            }
            if (longde < -180)
            {
                longde = longde + 360;
            }
        }
        else
        {
            if (longde > 180)
            {
                longde = longde - 360;
            }
            if (longde < -180)
            {
                longde = longde + 360;
            }
        }
        lon.InnerText += Math.Floor(Math.Abs(longde)) + " градусов " + Math.Floor((Math.Abs(longde) - Math.Floor(Math.Abs(longde))) * 60) + " минут ";
        lon.InnerText += Math.Round(((((Math.Abs(longde) - Math.Floor(Math.Abs(longde))) * 60) - Math.Floor((Math.Abs(longde) - Math.Floor(Math.Abs(longde))) * 60)) * 60), 2, MidpointRounding.AwayFromZero) + " секунд";
        if (longde < 0)
        {
            lon.InnerText += " западной долготы";
        }
        else
        {
            lon.InnerText += " восточной долготы";
        }
        lat.InnerText += ".";
        lon.InnerText += ".";
    }
}