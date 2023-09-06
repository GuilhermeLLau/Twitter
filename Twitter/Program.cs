using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitter
{
    internal class Program
    {
        public interface Observer
        {
          void update(string _mensagem);
        }
        public interface Subject
        {
            void registraSeguidor(Observer obs);
            void removeSeguidor(Observer obs);
            void notificaSeguidores();
            void Tweetar(string str);
        }


        public class ContaTwitter : Observer , Subject
        {
            public string NomeDaConta;
            public List<Observer> Seguidores = new List<Observer>();
            public List<string> mensagensRecebidas = new List<string>();
            public string tweet;

            public ContaTwitter(string str)
            {
                NomeDaConta = str;
            }

            public void registraSeguidor(Observer obs)
            {
                Seguidores.Add(obs);
            }
            public void removeSeguidor(Observer obs)
            {
                Seguidores.Remove(obs);
            }
            public void notificaSeguidores()
            {
                for(int i = 0; i < Seguidores.Count; i++)
                {
                    Seguidores[i].update(this.tweet);
                }
            }

            public void update(string mensagem)
            {
                tweet = mensagem;
                mensagensRecebidas.Add(tweet);
            }

            public void Tweetar(string tweet)
            {
                this.tweet = tweet;
                notificaSeguidores();
            }

            public void MostraFeed()
            {
                foreach (var mensagem in mensagensRecebidas)
                {
                    Console.WriteLine($"Mensagem de {mensagem}");
                }
            }
        }

        public class ContaYahoo : Observer
        {
            public string NomeDaConta;
            public string tweet;
            public List<string> mensagensRecebidas = new List<string>();

            public ContaYahoo(string str)
            {
                NomeDaConta=str;
            }
            public void update(string mensagem)
            {
                tweet = mensagem;
                mensagensRecebidas.Add(tweet);
            }
            public void MostraFeed()
            {
                foreach (var mensagem in mensagensRecebidas)
                {
                    Console.WriteLine($"Mensagem de {mensagem}");
                }
            }
        }

        static void Main(string[] args)
        {
            ContaTwitter guilherme = new ContaTwitter("Guilherme");
            ContaTwitter gabriel = new ContaTwitter("Gabriel");
            ContaTwitter diogo = new ContaTwitter("Diogo");
            ContaYahoo yahoo = new ContaYahoo("Yahoo");
            guilherme.registraSeguidor(gabriel);
            guilherme.registraSeguidor(diogo);
            guilherme.registraSeguidor(yahoo);
            gabriel.registraSeguidor(guilherme);
            gabriel.registraSeguidor(diogo);
            gabriel.Tweetar(gabriel.NomeDaConta + ": " + "tweetTeste");
            guilherme.Tweetar(guilherme.NomeDaConta + ": " + "tweet");
            guilherme.MostraFeed();
            gabriel.MostraFeed();
            diogo.MostraFeed();
            yahoo.MostraFeed();
            Console.ReadKey();
        }
    }
}
