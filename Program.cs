using System.Text;
using DesafioProjetoHospedagem.Models;

Console.OutputEncoding = Encoding.UTF8;

// Cria os modelos de hóspedes e cadastra na lista de hóspedes
List<Pessoa> hospedes = new List<Pessoa>();

Pessoa p1 = new Pessoa(nome: "Hóspede 1");
Pessoa p2 = new Pessoa(nome: "Hóspede 2");
Pessoa p3 = new Pessoa(nome: "Hóspede 3", sobrenome: "Sobrenome 3"); // Exemplo com sobrenome

hospedes.Add(p1);
hospedes.Add(p2);
hospedes.Add(p3); // Adicionando mais um hóspede para testar a validação de capacidade

// Cria a suíte
Suite suite = new Suite(tipoSuite: "Premium", capacidade: 2, valorDiaria: 30); // Capacidade 2 para teste

// Cria uma nova reserva, passando a suíte e os hóspedes
Reserva reserva = new Reserva(diasReservados: 12); // Testando com 12 dias para verificar o desconto

try
{
    reserva.CadastrarSuite(suite); // Cadastra a suíte primeiro
    reserva.CadastrarHospedes(hospedes); // Depois cadastra os hóspedes

    // Exibe a quantidade de hóspedes e o valor da diária
    Console.WriteLine($"Suíte: {reserva.Suite.TipoSuite} - Capacidade: {reserva.Suite.Capacidade}");
    Console.WriteLine($"Dias Reservados: {reserva.DiasReservados}");
    Console.WriteLine($"Hóspedes: {reserva.ObterQuantidadeHospedes()}");
    Console.WriteLine($"Valor diária SEM desconto: {reserva.DiasReservados * reserva.Suite.ValorDiaria:C}"); // Para comparação
    Console.WriteLine($"Valor diária COM desconto: {reserva.CalcularValorDiaria():C}"); // Usando :C para formatar como moeda

    Console.WriteLine("\nLista de Hóspedes:");
    foreach (Pessoa p in reserva.Hospedes)
    {
        Console.WriteLine($"- {p.NomeCompleto}");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Erro ao realizar a reserva: {ex.Message}");
}

// --- Teste com reserva válida (menos hóspedes que a capacidade) ---
Console.WriteLine("\n--- Teste de Reserva Válida ---");
List<Pessoa> hospedesValidos = new List<Pessoa>();
hospedesValidos.Add(new Pessoa(nome: "Hóspede Válido 1"));
hospedesValidos.Add(new Pessoa(nome: "Hóspede Válido 2"));

Suite suitePequena = new Suite(tipoSuite: "Standard", capacidade: 2, valorDiaria: 50);
Reserva reservaValida = new Reserva(diasReservados: 5); // Menos de 10 dias, sem desconto

try
{
    reservaValida.CadastrarSuite(suitePequena);
    reservaValida.CadastrarHospedes(hospedesValidos);
    Console.WriteLine($"Suíte: {reservaValida.Suite.TipoSuite} - Capacidade: {reservaValida.Suite.Capacidade}");
    Console.WriteLine($"Dias Reservados: {reservaValida.DiasReservados}");
    Console.WriteLine($"Hóspedes: {reservaValida.ObterQuantidadeHospedes()}");
    Console.WriteLine($"Valor diária: {reservaValida.CalcularValorDiaria():C}");
}
catch (Exception ex)
{
    Console.WriteLine($"Erro inesperado na reserva válida: {ex.Message}");
}

// --- Teste com desconto (mais de 10 dias) ---
Console.WriteLine("\n--- Teste de Desconto ---");
List<Pessoa> hospedesDesconto = new List<Pessoa>();
hospedesDesconto.Add(new Pessoa(nome: "Hóspede Desconto 1"));

Suite suiteDesconto = new Suite(tipoSuite: "Econômica", capacidade: 1, valorDiaria: 100);
Reserva reservaComDesconto = new Reserva(diasReservados: 10); // Exatamente 10 dias, com desconto

try
{
    reservaComDesconto.CadastrarSuite(suiteDesconto);
    reservaComDesconto.CadastrarHospedes(hospedesDesconto);
    Console.WriteLine($"Suíte: {reservaComDesconto.Suite.TipoSuite} - Capacidade: {reservaComDesconto.Suite.Capacidade}");
    Console.WriteLine($"Dias Reservados: {reservaComDesconto.DiasReservados}");
    Console.WriteLine($"Hóspedes: {reservaComDesconto.ObterQuantidadeHospedes()}");
    Console.WriteLine($"Valor diária SEM desconto: {reservaComDesconto.DiasReservados * reservaComDesconto.Suite.ValorDiaria:C}");
    Console.WriteLine($"Valor diária COM desconto: {reservaComDesconto.CalcularValorDiaria():C}");
}
catch (Exception ex)
{
    Console.WriteLine($"Erro inesperado na reserva com desconto: {ex.Message}");
}