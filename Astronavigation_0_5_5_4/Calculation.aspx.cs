using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Calculation : System.Web.UI.Page
{
    //protected bool cb1_ch, cb2_ch;
    protected double a = 1.0000002, alpha, delta, e1, e0, eps0, eps/*, hamma0, hamma*/, lambda_0, lambda, l0, l, m0, m;
    protected double rad_alpha, rad_delta, rad_eps, /*rad_hamma,*/ rad_lambda, rad_l, rad_m;
    protected DateTime reftime_gen = new DateTime(1975, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    protected void Page_Load(object sender, EventArgs e)
    {
        cur_date.Value = DateTime.UtcNow.ToString();
        lat.InnerText = "";
        lon.InnerText = "";
        //cb1_ch = false;
        //cb2_ch = false;
        eps0 = 23.442533;
        e0 = 0.01671974;
        l0 = 279.0415;
        lambda_0 = 248.84411;
        m0 = 356.5311;

        //MakeList();
        FillList();
        
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
    /*public void MakeList()
    {
        List<Celbody> cels = new List<Celbody>();
        try
        {
            cels.Add(new Celbody("Солнце", BodyType.MainLight, 0.000, 0.000, new DateTime(2017, 3, 20, 10, 29, 0, DateTimeKind.Utc)));
            cels[cels.Count - 1].GetNatMov(0.985646, 0.12739477815);
            //cels.Add(new Celbody("Луна", BodyType.MainLight, 0.000, 0.000, new DateTime(2017, 3, 21)));
            //cels[cels.Count - 1].GetNatMov(0.000, 0.000);

            cels.Add(new Celbody("Альферас (альфа Андромеды)", BodyType.Star, 28.965, 1.8083, new DateTime(1977, 7, 1, 0, 0, 0, DateTimeKind.Utc)));
            cels[cels.Count - 1].GetNatMov(0.000037689, -0.0000452639);
            cels.Add(new Celbody("Канопус (альфа Киля)", BodyType.Star, -52.685, 95.855, new DateTime(1977, 7, 1, 0, 0, 0, DateTimeKind.Utc)));
            cels[cels.Count - 1].GetNatMov(0.00000555277, 0.000006575);
            cels.Add(new Celbody("Кастор (альфа Близнецов)", BodyType.Star, 31.937, 113.287, new DateTime(1977, 7, 1, 0, 0, 0, DateTimeKind.Utc)));
            cels[cels.Count - 1].GetNatMov(-0.00000573139, -0.00004116111);
            cels.Add(new Celbody("Поллукс (бета Близнецов)", BodyType.Star, 28.08, 115.98, new DateTime(1977, 7, 1, 0, 0, 0, DateTimeKind.Utc)));
            cels[cels.Count - 1].GetNatMov(-0.00017377777, -0.00001276389);
            cels.Add(new Celbody("Дуббе (альфа Большой Медведицы)", BodyType.Star, 61.877, 165.58, new DateTime(1977, 7, 1, 0, 0, 0, DateTimeKind.Utc)));
            cels[cels.Count - 1].GetNatMov(-0.00003790555, -0.0000097917);
            cels.Add(new Celbody("Алиот (эпсилон Большой Медведицы)", BodyType.Star, 56.087, 193.26, new DateTime(1977, 7, 1, 0, 0, 0, DateTimeKind.Utc)));
            cels[cels.Count - 1].GetNatMov(0.0000310389, -0.00000249722);
            cels.Add(new Celbody("Бенетнаш (эта Большой Медведицы)", BodyType.Star, 49.43, 206.665, new DateTime(1977, 7, 1, 0, 0, 0, DateTimeKind.Utc)));
            cels[cels.Count - 1].GetNatMov(-0.000033675, 0.00000432222);
            cels.Add(new Celbody("Сириус (альфа Большого Пса)", BodyType.Star, -16.687, 101.03, new DateTime(1977, 7, 1, 0, 0, 0, DateTimeKind.Utc)));
            cels[cels.Count - 1].GetNatMov(-0.00015166944, -0.0003397417);
            cels.Add(new Celbody("Капелла (альфа Возничего)", BodyType.Star, 45.973, 78.7517, new DateTime(1977, 7, 1, 0, 0, 0, DateTimeKind.Utc)));
            cels[cels.Count - 1].GetNatMov(0.00002097777, -0.00011864722);
            cels.Add(new Celbody("Арктур (альфа Волопаса)", BodyType.Star, 19.3017, 213.6617, new DateTime(1977, 7, 1, 0, 0, 0, DateTimeKind.Utc)));
            cels[cels.Count - 1].GetNatMov(-0.00030373055, -0.00055539722);
            cels.Add(new Celbody("Альфард (альфа Гидры)", BodyType.Star, -8.563, 141.6183, new DateTime(1977, 7, 1, 0, 0, 0, DateTimeKind.Utc)));
            cels[cels.Count - 1].GetNatMov(-0.000004025, 0.00000923611);
            cels.Add(new Celbody("Спика (альфа Девы)", BodyType.Star, -11.045, 201.005, new DateTime(1977, 7, 1, 0, 0, 0, DateTimeKind.Utc)));
            cels[cels.Count - 1].GetNatMov(-0.00001180555, -0.00000881388);
            cels.Add(new Celbody("Рас-Алхаг (альфа Змееносца)", BodyType.Star, 12.5783, 263.4783, new DateTime(1977, 7, 1, 0, 0, 0, DateTimeKind.Utc)));
            cels[cels.Count - 1].GetNatMov(0.00003001944, -0.00006154722);
            cels.Add(new Celbody("Шедар (альфа Кассиопеи)", BodyType.Star, 56.41, 9.81, new DateTime(1977, 7, 1, 0, 0, 0, DateTimeKind.Utc)));
            cels[cels.Count - 1].GetNatMov(0.00001398888, -0.00000893611);
            cels.Add(new Celbody("Дифда (бета Кита)", BodyType.Star, -18.1083, 10.615, new DateTime(1977, 7, 1, 0, 0, 0, DateTimeKind.Utc)));
            cels[cels.Count - 1].GetNatMov(0.00006459722, 0.00000888611);
            cels.Add(new Celbody("Денеб (альфа Лебедя)", BodyType.Star, 45.2, 310.175, new DateTime(1977, 7, 1, 0, 0, 0, DateTimeKind.Utc)));
            cels[cels.Count - 1].GetNatMov(0.00000055277, 0.0000005417);
            cels.Add(new Celbody("Регул (альфа Льва)", BodyType.Star, 12.077, 151.7917, new DateTime(1977, 7, 1, 0, 0, 0, DateTimeKind.Utc)));
            cels[cels.Count - 1].GetNatMov(0.00006917, 0.00000055555);
            cels.Add(new Celbody("Вега (альфа Лиры)", BodyType.Star, 38.765, 279.0517, new DateTime(1977, 7, 1, 0, 0, 0, DateTimeKind.Utc)));
            cels[cels.Count - 1].GetNatMov(0.0000558417, 0.00007985277);
            cels.Add(new Celbody("Процион (альфа Малого Пса)", BodyType.Star, 5.2817, 114.527, new DateTime(1977, 7, 1, 0, 0, 0, DateTimeKind.Utc)));
            cels[cels.Count - 1].GetNatMov(-0.00019849722, -0.000288);
            cels.Add(new Celbody("Альтаир (альфа Орла)", BodyType.Star, 8.81, 62.5717, new DateTime(1977, 7, 1, 0, 0, 0, DateTimeKind.Utc)));
            cels[cels.Count - 1].GetNatMov(0.000149117, 0.000107094);
            cels.Add(new Celbody("Бетельгейзе (альфа Ориона)", BodyType.Star, 7.4017, 88.485, new DateTime(1977, 7, 1, 0, 0, 0, DateTimeKind.Utc)));
            cels[cels.Count - 1].GetNatMov(0.0000075917, 0.000003017);
            cels.Add(new Celbody("Ригель (бета Ориона)", BodyType.Star, -8.2283, 78.36, new DateTime(1977, 7, 1, 0, 0, 0, DateTimeKind.Utc)));
            cels[cels.Count - 1].GetNatMov(0.00000036388, 0.00000013888);
            cels.Add(new Celbody("Мирфак (альфа Персея)", BodyType.Star, 49.7783, 50.675, new DateTime(1977, 7, 1, 0, 0, 0, DateTimeKind.Utc)));
            cels[cels.Count - 1].GetNatMov(0.00000669722, -0.000007225);
            cels.Add(new Celbody("Альфакка (альфа Северной Короны)", BodyType.Star, 26.7933, 233.437, new DateTime(1977, 7, 1, 0, 0, 0, DateTimeKind.Utc)));
            cels[cels.Count - 1].GetNatMov(0.0000334389, -0.00002484444);
            cels.Add(new Celbody("Антарес (альфа Скорпиона)", BodyType.Star, -26.3817, 247.0133, new DateTime(1977, 7, 1, 0, 0, 0, DateTimeKind.Utc)));
            cels[cels.Count - 1].GetNatMov(-0.00000282222, -0.00000644722);
            cels.Add(new Celbody("Нунки (сигма Стрельца)", BodyType.Star, -26.3233, 283.475, new DateTime(1977, 7, 1, 0, 0, 0, DateTimeKind.Utc)));
            cels[cels.Count - 1].GetNatMov(0.00000421, -0.0000148417);
            cels.Add(new Celbody("Альдебаран (альфа Тельца)", BodyType.Star, 16.4633, 68.6533, new DateTime(1977, 7, 1, 0, 0, 0, DateTimeKind.Utc)));
            cels[cels.Count - 1].GetNatMov(0.0000174389, -0.0000526);
            cels.Add(new Celbody("Ригиль Центавр (альфа Центавры)", BodyType.Star, -60.7433, 219.53, new DateTime(1977, 7, 1, 0, 0, 0, DateTimeKind.Utc)));
            cels[cels.Count - 1].GetNatMov(-0.0010217194, 0.00013384444);
            cels.Add(new Celbody("Ахернар (альфа Эридана)", BodyType.Star, -57.347, 24.215, new DateTime(1977, 7, 1, 0, 0, 0, DateTimeKind.Utc)));
            cels[cels.Count - 1].GetNatMov(0.00002417, -0.00001062);
            cels.Add(new Celbody("Акрукс (альфа Южного Креста)", BodyType.Star, -62.98, 186.34, new DateTime(1977, 7, 1, 0, 0, 0, DateTimeKind.Utc)));
            cels[cels.Count - 1].GetNatMov(-0.000009825, -0.00000409166);
            cels.Add(new Celbody("Фомальгаут (альфа Южной Рыбы)", BodyType.Star, -29.7383, 344.105, new DateTime(1977, 7, 1, 0, 0, 0, DateTimeKind.Utc)));
            cels[cels.Count - 1].GetNatMov(0.00009145, -0.00004562);
            cels.Add(new Celbody("Атриа (альфа Южного Треугольника)", BodyType.Star, -68.98833, 251.5917, new DateTime(1977, 7, 1, 0, 0, 0, DateTimeKind.Utc)));
            cels[cels.Count - 1].GetNatMov(0.00000499722, -0.0000087722);

            FileStream A = new FileStream("C:/Users/Lenovo/Documents/Visual Studio 2013/WebSites/Astronavigation/Eddata/Celbodies", FileMode.OpenOrCreate);
            BinaryFormatter B = new BinaryFormatter();
            B.Serialize(A, cels);
            A.Close();
            
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }*/

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
        //val = (v_p / (273 + v_t)) * (Math.Cos(h * (Math.PI / 180)) / Math.Sin(h * (Math.PI / 180))) * (0.359995 - 0.000399 * (Math.Cos((h*(Math.PI / 180)) * h) / Math.Sin(h * h)));
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
        for(i = 0; i < cels.Count; i++)
        {
            if(b1 == cels[i].GetName())
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
        for(i = 0; i < n; i++)
        {
            //al_proc = al_proc + (((46.106 / 3600) * Math.Cos(del_i) + (20.540 / 3600) * Math.Sin(al_i) * Math.Sin(del_i)) / Math.Cos(del_i) + mu_al) * ((t - reftime_gen).TotalDays / n);
            //del_proc = del_proc + ((20.540 / 3600) * Math.Cos(al_i) + mu_del) * ((t - reftime_gen).TotalDays / n);

            //al_proc = al_proc + (((46.106 / 3600) * Math.Cos(del_i) + (20.540 / 3600) * Math.Sin(al_i) * Math.Sin(del_i)) / Math.Cos(del_i) + mu_al) * (((t - reftime_gen).TotalDays / n) / 365.2426);
            //del_proc = del_proc + ((20.540 / 3600) * Math.Cos(al_i) + mu_del) * (((t - reftime_gen).TotalDays / n) / 365.2426);

            al_i = (((46.106 / 3600) * Math.Cos(del_i) + (20.540 / 3600) * Math.Sin(al_i) * Math.Sin(del_i)) / Math.Cos(del_i) + mu_al) * (((t - reftime_gen).TotalDays / n) / 365.2426); 
            del_i = ((20.540 / 3600) * Math.Cos(al_i) + mu_del) * (((t - reftime_gen).TotalDays / n) / 365.2426);

            al_proc = al_proc + al_i;
            del_proc = del_proc + del_i;
        }
    }
    protected void fir_body1_SelectedIndexChanged(object sender, EventArgs e)
    {
        /*int i;
        for (i = 0; i < sec_body1.Items.Count; i++)
        {
            if(sec_body1.Items[i].Value == fir_body1.SelectedValue)
            {
                sec_body1.Items[i].Enabled = false;
            }
        }*/
        /*if (fir_body1.SelectedIndex != -1)
        {
            cb1_ch = true;
        }
        if (cb1_ch && cb2_ch)
        {
            button1.Disabled = false;
        }*/
    }
    public double GetHour(DateTime t1)
    {
        double val = 0.000;
        //Formulae2
        DateTime dt = new DateTime(2017, 11, 3, 11, 43, 36, DateTimeKind.Utc);
        //val = 12 + (t1 - dt).TotalHours + 0.0027379 * (t1 - dt).TotalHours + ((-17.24 / 3600) * Math.Sin(hamma)) * Math.Cos(eps);
        //val = (t1 - dt).TotalHours + 0.0027379 * (t1 - dt).TotalHours;
        val = (t1 - dt).TotalDays + 0.0027379 * (t1 - dt).TotalDays;
        val = val * 24;
        val = val % 24;
        val = (val / 24) * 360 + ((-17.24 / 3600) * Math.Sin(rad_lambda)) * Math.Cos(rad_eps);
        return val;
    }
    public void DayMov(out double al, out double del, DateTime t)
    {
        //al = 0.000;
        //del = 0.000;
        double e_kep, e_kep1 = 0.000, hamma, ham1, ham2, m_ven, m_jup, r, v = 0.000, cos_v, sin_v;
        //lambda = lambda_0 - (3.000 / 60 + 10.6 / 3600) * (t - reftime_gen).TotalDays;
        eps = eps0 - (0.468 / 3600) * ((t - reftime_gen).TotalDays / 365.2426);
        e1 = e0 - (0.00000042) * ((t - reftime_gen).TotalDays / 365.2426);
        l = l0 + (59.000 / 60 + 8.33 / 3600) * (t - reftime_gen).TotalDays;
        m = m0 + (59.000 / 60 + 8.161 / 3600) * (t - reftime_gen).TotalDays;
        e_kep = m + e1 * Math.Sin(m);
        while(Math.Abs(e_kep1 - e_kep) < 0.0001)
        {
            e_kep1 = m + e1 * Math.Sin(e_kep);
        }
        r = a * (1 - e1 * Math.Cos(e_kep1 * (Math.PI / 180)));
        cos_v = (a - (Math.Cos(e_kep1 * (Math.PI / 180)) - e1)) / r;
        sin_v = (a * Math.Sqrt(1 - Math.Pow(e1, 2)) * Math.Sin(e_kep1 * (Math.PI / 180))) / r;
        if(cos_v > 0)
        {
            if(sin_v < 0)
            {
                v = Math.Asin(sin_v) * (180 / Math.PI) + 360;
            }
            else
            {
                v = Math.Acos(cos_v) * (180 / Math.PI) + 180;
            }
        }
        else if(cos_v < 0)
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
    protected void sec_body1_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
    protected void button1_Click(object sender, EventArgs e)
    {
        //Work with data
        double A, B, C, a_1, b_1, a_2, b_2, c, alpha1, alpha2, delta1, delta2, al_nut, del_nut, al_proc, del_proc, al_ab, del_ab;
        double mu_al, mu_del, hs1, hs2, hh1, hh2, fi, longde, tG1, tG2;
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
            hs1 = double.Parse(fir_ht.Value.Replace('.',','));

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

            hs1 = hs1 * (Math.PI / 180);

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


            hs2 = hs2 * (Math.PI / 180);

            delta1 = delta1 * (Math.PI / 180);
            delta2 = delta2 * (Math.PI / 180);
            alpha1 = alpha1 * (Math.PI / 180);
            alpha2 = alpha2 * (Math.PI / 180);

            a_1 = Math.Tan(delta1);
            a_2 = Math.Tan(delta2);

            b_1 = -Math.Sin(hs1) / Math.Cos(delta1);
            b_2 = -Math.Sin(hs2) / Math.Cos(delta2);

            tG1 = GetHour(t1) * (Math.PI / 180) - alpha1;
            tG2 = GetHour(t2) * (Math.PI / 180) - alpha2;
            c = Math.Cos(tG1 - tG2);
            //Сама формула нахождения координат по высотам, склонению и восхождению
            //A = 1 + Math.Pow(Math.Tan(delta1), 2) + Math.Pow(Math.Tan(delta2), 2);
            A = 1 + Math.Pow(a_1, 2) + Math.Pow(a_2, 2) - 2 * a_1 * a_2 * c - Math.Pow(c, 2);
            //A = A - 2 * Math.Tan(delta1) * Math.Tan(alpha2) * Math.Cos(alpha1 - alpha2) - Math.Pow(Math.Cos(alpha1 - alpha2), 2);
            //A = A - 2 * Math.Tan(delta1) * Math.Tan(alpha2) * Math.Cos((GetHour(t1) - GetHour(t2)) * (Math.PI / 180) + alpha2 - alpha1) - Math.Pow(Math.Cos((GetHour(t1) - GetHour(t2)) * (Math.PI / 180) + alpha2 - alpha1), 2);
            //B = -Math.Tan(delta1) * Math.Sin(hs1) / Math.Cos(delta1) - Math.Tan(delta2) * Math.Sin(hs2) / Math.Cos(delta2);
            B = a_1 * b_1 + a_2 * b_2 - (a_1 * b_2 + a_2 * b_2 + a_2 * b_1) * c;
            //B = B - (-Math.Tan(delta1) * Math.Sin(hs2) / Math.Cos(delta2) - Math.Tan(delta2) * Math.Sin(hs2) / Math.Cos(delta2) - Math.Tan(delta2) * Math.Sin(hs1) / Math.Cos(delta1)) * Math.Cos(alpha1 - alpha2);
            //B = B - (-Math.Tan(delta1) * Math.Sin(hs2) / Math.Cos(delta2) - Math.Tan(delta2) * Math.Sin(hs2) / Math.Cos(delta2) - Math.Tan(delta2) * Math.Sin(hs1) / Math.Cos(delta1)) * Math.Cos((GetHour(t1) - GetHour(t2)) * (Math.PI / 180) + alpha2 - alpha1);
            C = Math.Pow(b_1, 2) + Math.Pow(b_2, 2) + Math.Pow(c, 2) - 2 * b_1 * b_2 * c - 1;
            //C = Math.Pow(Math.Sin(hs1) / Math.Cos(delta1), 2) + Math.Pow(Math.Sin(hs2) / Math.Cos(delta2), 2) + Math.Pow(Math.Cos((GetHour(t1) - GetHour(t2)) * (Math.PI / 180) + alpha2 - alpha1), 2);
            //C = C - 2 * (Math.Sin(hs1) / Math.Cos(delta1)) * (Math.Sin(hs2) / Math.Cos(delta2)) * Math.Cos((GetHour(t1) - GetHour(t2)) * (Math.PI / 180) + alpha2 - alpha1) - 1;
            //Для квадратного трёхчлена
            x[0] = (-B + Math.Sqrt(Math.Pow(B, 2) - A * C)) / A;
            x[1] = (-B - Math.Sqrt(Math.Pow(B, 2) - A * C)) / A;
            //Арксинус широты
            for (int i = 0; i < 2; i++)
            {
                if ((x[i] >= -1) && (x[i] <= 1))
                {
                    fi = Math.Asin(x[i]);

                    fi = fi * (180 / Math.PI);

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
                    //y[i] = -(Math.Tan(delta1) * x[i] - (Math.Sin(hs1) / Math.Cos(delta1))) / Math.Sqrt(1 - Math.Pow(x[i], 2));
                    y[i] = -(a_1 * x[i] + b_1) / Math.Sqrt(1 - Math.Pow(x[i], 2));
                    if ((y[i] >= -1) && (y[i] <= 1))
                    {
                        longde = GetHour(t1) * (Math.PI / 180) - alpha1 - Math.Acos(y[i]);
                        longde = longde * (180 / Math.PI);
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
                    }
                    else
                    {
                        lon.InnerText += " - ";
                    }
                    
                    if (i == 0)
                    {
                        lat.InnerText += " или ";
                        lon.InnerText += " или ";
                    }
                }
            }
            lat.InnerText += ".";
            lon.InnerText += ".";
        /*}
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }*/
    }
}