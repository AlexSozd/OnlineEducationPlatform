using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Celbody
/// </summary>
[Serializable] public class Celbody
{
    private double decl, r_asc, sha, mu_al, mu_del;
    private string name;
    private DateTime epoch;
    private BodyType bt;
    //Поправки на нутацию, процессию и аберрацию - отдельно, в основной программе, собственное движение - здесь
    /*public Celbody()
	{
		//
		// TODO: Add constructor logic here
		//
	}*/
    public Celbody(string name, BodyType bt, double decl, double r_asc, DateTime dt)
    {
        this.name = name;
        this.bt = bt;
        this.decl = decl;
        this.r_asc = r_asc;
        sha = 360 - r_asc;
        epoch = dt;
    }
    public void GetNatMov(double al, double del)
    {
        mu_al = al;
        mu_del = del;
    }
    public string GetName()
    {
        return name;
    }
    public double Dec { get { return decl; } }
    public double RA { get {return r_asc; } }
    public double SHA { get { return sha; } }
    public double gradDec { get { return mu_del; } }
    public double gradRA { get { return mu_al; } }
    public DateTime RefTime { get { return epoch; } }
    public BodyType BType { get { return bt; } }
};
public enum BodyType
{
    MainLight = 1,
    Planet = 2,
    Star = 3
}