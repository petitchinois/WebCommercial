using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebCommercial.Models.MesExceptions
{
    public class MonException : Exception
    {
        private string _utilisateur, _application, _systeme;
        private string _support = "Si l'erreur persiste, relevez les messages ci-dessus et prenez contact avec le support technique.";

        public MonException()
        {
            _utilisateur = _application = _systeme = "";

        }
        public string MsgUtilisateur
        {
            get { return _utilisateur; }
            set { _utilisateur = value + "\r\n"; }
        }
        public string MsgApplication
        {
            get { return _application; }
            set { _application = "Origine de l'erreur : " + value + "\r\n"; }
        }

        public string MsgSysteme
        {
            get { return _systeme; }
            set { _systeme = "Erreur système : " + value + "\r\n"; }
        }

        public MonException(string u, string a, string s)
        {
            _utilisateur = _application = _systeme = "";
            if (u != "")
                _utilisateur = u + "\r\n";

            if (a != "")
                _application = "Origine de l'erreur : " + a + "\r\n";
            if (s != "")
                _systeme = "Erreur système : " + s + "\r\n";
        }

        public string MessageUtilisateur()
        {
            return (_utilisateur);
        }

        public string MessageApplication()
        {
            return (_application);
        }

        public string MessageSysteme()
        {
            return (_systeme);
        }

        public string MessageSupport()
        {
            return (_support);
        }

        public string Messages()
        {
            if (_systeme == "")
                _support = "";
            return (_utilisateur + _systeme + _support);
        }
    }
}