using System;
using System.Collections.Generic;
using System.Text;

namespace Jogo21
{
    public class Carta
    {
        private string Nipe;
        private int Numero;
        private bool CartaRetirada;

        public Carta(string nipe, int numero)
        {
            Nipe = nipe;
            Numero = numero;
            CartaRetirada = false;
        }
        public int GetPontos()
        {
            return this.Numero;
        }
        public void TirarDoBaralho()
        {
            this.CartaRetirada = true;            
        }
        public bool ConsultaNoBaralho()
        {
            return CartaRetirada;
        }


        public override string ToString()
        {
            return $"{Numero} de {Nipe} ";
        }
    }
}
