using System;
using System.Collections.Generic;
using System.Text;

namespace Jogo21
{
    public class Baralho
    {

        List<Carta> baralho = new List<Carta>();

        public void AddCartas(Carta carta)
        {
            baralho.Add(carta);
        }

        public Carta RetornaPorId(int numeroDaCarta)
        {
            return baralho[numeroDaCarta];
        }

       public void CartasDisponiveis()
        {
            Console.WriteLine("Cartas Disponíveis:");
            foreach (Carta c in baralho)
            {
                if (!c.ConsultaNoBaralho())
                {
                    Console.WriteLine(c.ToString());
                }
            }
        }
        public override string ToString()
        {
            foreach (Carta c in baralho)
            {
                Console.WriteLine(c.ToString());
            }
            return "";
        }

    }
}
