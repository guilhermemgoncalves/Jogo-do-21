using System;

/// <summary>
/// Fazer um jogo de 21 orientado a objetos com Quantidade de jogadores selecionavel e limitadas ao numero de cartas do baralho;
/// 
/// Classe jogador, Lista de Jogadores/Pontos
/// 
/// Classe Baralho, Com lista de Cartas e Valores. Do as ao 10 / 4 Nipes
/// 
/// 40 cartas = 10 jogadores
/// 
/// </summary>

namespace Jogo21
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string acoesMenu = "0";
            while (acoesMenu != "9")
            {
                MenuPrincipal(); // Chama as opções do menu principal!
                acoesMenu = Console.ReadLine();
                switch (acoesMenu)
                {
                    case "1":
                        NovoJogo(); // Começar um novo jogo
                        break;
                    case "2":
                        RegrasGerais(); // Ajuda para ver o funcionamento do jogo
                        break;
                    case "9":
                        Console.WriteLine("Obirgado por jogae 21"); // Finaliza a aplicação
                        break;
                    default:
                        Console.WriteLine("Opção invalida");
                        break;
                }
            }
            void MenuPrincipal()
            {
                Console.WriteLine("\n *****Jogo do 21****\n");
                Console.WriteLine("1 - Novo jogo");
                Console.WriteLine("2 - Regras");
                Console.WriteLine("9 - Sair\n");
            }
            void RegrasGerais()
            {
                Console.Clear();
                Console.WriteLine("Regras:");
                Console.WriteLine(" -------------------------------------------------------");
                Console.WriteLine("1 - Cartas de baralho validas do 1 ao 10");
                Console.WriteLine("2 - O Objetivo do jogo é Obter a soma 21 com as cartas da mesa na sua vez de jogar.");
                Console.WriteLine("3 - Caso o jogador passe de 21 Pontos ele perde o jogo");
                Console.WriteLine("4 - Caso o jogador se aproxime do numero 21 ele pode pedir para não receber mais cartas");
                Console.WriteLine("5 - O jogador que chegar mais perto do numero 21 vence");
                Console.WriteLine("6 - O jogador inicia com duas cartas sorteadas do monte");
                Console.WriteLine("7 - Os jogadores tem direito de sortear uma carta do monte a cada rodada");
                Console.WriteLine(".");
                Console.WriteLine(".");
                Console.WriteLine();
                Console.ReadLine();
                Console.Clear();

            }


            void NovoJogo()
            {
                Jogador[] jogadores = new Jogador[10];// instancia um numero máximo de jogadores
                Baralho baralho = new Baralho(); // carrega um novo baralho
                CarregarBaralho(baralho); //Carrega todas as cartas do baralho
                GerarJogadores(QuantifiicaJogadores(), jogadores, baralho); // Criar novos jogadores para a partida atual
                MostraJogadores(jogadores); 
                Rodadas(jogadores, baralho); //Faz o loop de jogadas e finaliza quando não há mais nenhum jogador apto a jogar.
            }
            void CarregarBaralho(Baralho baralho)
            {
                string nipeDaCarta = "";
                for (int j = 0; j < 4; j++)
                {
                    if (j == 0)
                    {
                        nipeDaCarta = "Copas";
                    }
                    else if (j == 1)
                    {
                        nipeDaCarta = "Espadas";
                    }
                    else if (j == 2)
                    {
                        nipeDaCarta = "Ouro";
                    }
                    else
                    {
                        nipeDaCarta = "Paus";
                    }

                    for (int i = 1; i <= 10; i++)
                    {
                        Carta carta = new Carta(nipe: nipeDaCarta, numero: i);
                        baralho.AddCartas(carta);
                    }
                }
            }
            int QuantifiicaJogadores() // função utilizada para gerar um numero de jogadores depois que a aplicação foi criada
            {
                Console.Clear();
                Console.WriteLine("Digite o numero de jogadores desejados (2~10): ");

                int numeroDeJogadores = int.Parse(Console.ReadLine());
                while (numeroDeJogadores < 2 || numeroDeJogadores > 10)
                {
                    Console.Clear();
                    Console.WriteLine("Quantidade Invalida:");
                    Console.WriteLine("Digite o numero de jogadores desejados (2~10): ");
                    numeroDeJogadores = int.Parse(Console.ReadLine());
                }
                Console.Clear();

                return numeroDeJogadores;
            }
            void GerarJogadores(int numeroDeJogadores, Jogador[] jogadores, Baralho baralho)
            {
                for (int i = 0; i < numeroDeJogadores; i++)
                {
                    Console.WriteLine($"Digite o nome do jogador do {i + 1}:");
                    string nomeJogador = Console.ReadLine();
                    Jogador jogador = new Jogador(nomeJogador, i);
                    jogadores[i] = jogador;
                    jogadores[i].PegarCarta(baralho);
                    jogadores[i].PegarCarta(baralho);
                    Limpa();
                }
            }
            void MostraJogadores(Jogador[] jogadores)
            {
                foreach (var item in jogadores)
                {
                    if (item != null)
                    {
                        Console.WriteLine(item.ToString());
                    }
                }
                Limpa();
            } //função que mostra todos os jogadores (usado ao longo do projeto)

            void Limpa()
            {
                Console.WriteLine("Digite enter para continuar...");
                Console.ReadLine();
                Console.Clear();
            } // Função usada para organizar o console e não deixar poluido

            void Rodadas(Jogador[] jogadores, Baralho baralho)
            {
                bool finaliza = false;
                while (true)
                {
                    Console.WriteLine("1 - Nova rodada de aposta");
                    Console.WriteLine("2 - Parar de apostar");
                    Console.WriteLine("3 - Consultar cartas do baralho disponíveis");
                    string EscolhaRodada = Console.ReadLine();


                    switch (EscolhaRodada)
                    {

                        case "1":
                            RodadaAposta(jogadores, baralho);
                            Limpa();
                            break;
                        case "2":
                            PararDeApostar(jogadores);
                            Limpa();
                            break;
                        case "3":
                            Limpa();
                            baralho.CartasDisponiveis();
                            Limpa();
                            break;
                        default:
                            Console.WriteLine("Opção invalida");
                            Limpa();
                            break;



                    }
                    finaliza = ConfereJogadores(jogadores);
                    if (finaliza == false)
                    {
                        Limpa();
                        break;
                    }


                }
                ContaPontos(jogadores);

            } //loop que organiza o que fazer no jogo e mostra as opções



            bool ConfereJogadores(Jogador[] jogadores)
            {
                Console.WriteLine("Placar:\n--------------------------");
                bool retornaNovoLoop = false;
                foreach (var item in jogadores)
                {
                    if (item != null)
                    {
                        Console.Write($"O Jogador {item.GetNomeJogador()} ");
                        if (item.GetEstorou())
                        {
                            Console.Write("Estourou a contagem e perdeu o jogo");
                        }
                        else if (item.GetParou())
                        {
                            Console.Write("Está espererando a contagem final");
                        }
                        else
                        {
                            Console.Write("Ainda está coletando cartas");
                            retornaNovoLoop = true;
                        }
                        Console.WriteLine($". Total {item.GetPontos()} pontos.");
                    }
                }
                return retornaNovoLoop;
            }// função para sempre mostrar o estado dos jogadores
            void RodadaAposta(Jogador[] jogadores, Baralho baralho)
            {
                foreach (var item in jogadores)
                {

                    if (item != null && !item.GetEstorou() && !item.GetParou())
                    {
                        while (true)
                        {
                            Console.WriteLine($"Jogador {item.GetNomeJogador()}, Deseja pegar mais uma carta?");
                            char[] escolher = Console.ReadLine().ToLower().ToCharArray();
                            if (escolher[0] == 's')
                            {
                                item.PegarCarta(baralho);
                                break;
                            }
                            else if (escolher[0] == 'n')
                            {
                                Console.WriteLine($"O Jogador {item.GetNomeJogador()} não quis pegar carta nessa rodada!");
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Opção Invalida!");
                            }

                        }
                        Limpa();
                    }
                }
            } // função do jogo


            void PararDeApostar(Jogador[] jogadores)
            {
                Console.WriteLine("Qual jogador deseja parar de apostar?\n" +
                    "Digite um numero da lista correspondente ao jogador ");
                jogadores.ToString();
                int JogadorDesistente = int.Parse(Console.ReadLine());
                jogadores[JogadorDesistente-1].SetParou();
                Limpa();
            } // função do jogo
            void ContaPontos(Jogador[] jogadores)
            {
                Jogador jogadorVencedor = new Jogador("", -1);
                foreach (var item in jogadores)
                {
                    if (item != null && item.GetPontos() < 21)
                    {
                        if (item.GetPontos() > jogadorVencedor.GetPontos())
                        {
                            jogadorVencedor = item;
                        }
                    }
                }
                Console.WriteLine($"O jogador vencedor é {jogadorVencedor}!");
                Limpa();
            } // qunado ninguem mais esta apto para jogar, finaliza o jogo e diz quem foi o campeão
        }      

    }
}
