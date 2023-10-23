using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace Moscu_Diana_Stephani_Lab2.Controllers
{
    public class HomeController1 : Controller
    {
        public string Index()
        {
            return "Bine ati venit!";
        }

        public string Salut()
        {
            return "Salut!";
        }

        public string MesajCuParametri(string text, int numar)
        {
            return $"Textul primit: {text}, Numarul primit: {numar}"; ;
        }
    }
}
