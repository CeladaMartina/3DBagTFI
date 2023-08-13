using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Propiedades_BE;

namespace _3DBag
{
    public partial class SiteMaster : MasterPage, ISubject
    {
        Negocio_BLL.Idioma GestorIdioma = new Negocio_BLL.Idioma();
        Negocio_BLL.Traduccion GestorTraduccion = new Negocio_BLL.Traduccion();
        Negocio_BLL.Seguridad Seguridad = new Negocio_BLL.Seguridad();

        List<IObserver> ListaObservadores = new List<IObserver>();

        public static Dictionary<string, string> DiccionarioTraduccionGlobal;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if(Session["UserSession"] != null)
                //{
                //    Usuario objUsuario = (Usuario)(Session["UserSession"]);
                //}
                //if ((Usuario)(Session["UserSession"]) == null)
                //{
                //    //Response.Redirect("../Home/Login.aspx");
                //}
                //else
                //{
                //    Control fc = FindControl("lblNombre");
                //    fc.
                //}
            }
            //DropDownListIdioma.DataSource = GestorIdioma.NombreIdioma();
            //DropDownListIdioma.DataBind();
            //DropDownListIdioma.Items.Insert(0, new ListItem("Idioma", "0"));
        }

        
        #region Metodos traduccion

        public void Attach(IObserver observer)
        {
            ListaObservadores.Add(observer);
        }

        public void NotificarCambio()
        {
            var numerator = Application.GetEnumerator();
            while (numerator.MoveNext())
            {
                if (numerator.Current is IObserver)
                {
                    this.Attach((IObserver)numerator.Current);
                }
            }
            ListaObservadores.ForEach(item => item.Update(this));
        }

        public string TraducirObserver(string variable)
        {
            string trad = "";
            try
            {
                trad = DiccionarioTraduccionGlobal[variable];
            }
            catch (Exception)
            {
                trad = null;
            }
            return trad;
        }

        public static string TraducirGlobal(string variable)
        {
            string trad = "";
            try
            {
                trad = DiccionarioTraduccionGlobal[variable];
            }
            catch (Exception)
            {
                trad = null;
            }
            return trad;
        }
        #endregion

        protected void ddlIdioma_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    DiccionarioTraduccionGlobal = new Dictionary<string, string>();
            //    foreach (var item in GestorTraduccion.ListarTraduccionDicionario(DropDownListIdioma.SelectedItem.ToString()))
            //    {
            //        DiccionarioTraduccionGlobal.Add(item.Original, item.Traducido);
            //    }
            //    this.NotificarCambio();
            //    //Traducir();
            //}
            //catch (Exception)
            //{
            //    //MessageBox.Show(TraducirGlobal("Error") ?? "Error");
            //}
        }
    }
}