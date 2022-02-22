using System;
using System.Collections.Generic;
using System.Text;

namespace Jogo21
{
   public class Jogador
    {

        private string Nome;
        private int Pontos = 0;
        private int IdJogador;
        private bool Parou;
        private bool Estourou;


        public Jogador(Jogador jogadorVencedor)
        {
            
        }

        public Jogador(string nome, int idJogador )
        {
            this.Nome = nome;
            this.IdJogador = idJogador;            
        }
        
        public string GetNomeJogador()
        {
            return this.Nome;

        }
        public bool GetParou()
        {
            return Parou;
        } 
        public bool GetEstorou()
        {
            return Estourou;
        }



        public void PegarCarta(Baralho baralho)
        {
            Random random = new Random();            
            
            int numeroDaCarta = random.Next(0, 39);// gera um numero randomico semelhante a tirar uma carta de um baralho 
            Carta carta = baralho.RetornaPorId(numeroDaCarta); //pega essa carta do baralho
            
            while (carta.ConsultaNoBaralho())
            {
                //Console.WriteLine(numeroDaCarta);
                numeroDaCarta = random.Next(0, 40);
                carta = baralho.RetornaPorId(numeroDaCarta);
            } // Confirma se essa carta ja está no jogo para não haver cartas repetidas


            this.SetPontos(carta); // somente quando a carta é diferente sai do loop e atriui os pontos ao jogador        
            carta.TirarDoBaralho(); //Tira a carta do baralho para não tirar a mesma carta em um próximo loop
            Console.WriteLine("O Jogador: " + this.Nome + " Retirou do Baralho " + carta.ToString());
        }

        public void SetPontos(Carta carta)
        {
            this.Pontos += carta.GetPontos();
            if (this.Pontos > 21)
            {
                this.Estourou = true;
            }
        }
        public int GetPontos()
        {
            return this.Pontos;
        }
        public void SetParou()
        {
            this.Parou = true;
        }

       

        public override string ToString()
        {
            return $"Jogador nº{IdJogador + 1}: Nome:{ Nome }| Pontos: {Pontos}";
        }
    }
}
