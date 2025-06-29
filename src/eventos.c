#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <time.h>
#include <stdbool.h>

#include "../include/eventos.h"
#include "../utils/utils.h"

// Capacidade maxima para o vetor de eventos.
#define MAX_EVENTOS 100

/**
 * @brief Lê os eventos do arquivo e armazena em um vetor
 *
 * @param eventos Vetor de eventos para armazenar os dados
 * @param max_eventos Numero maximo de eventos que o vetor pode armazenar
 * @return int Retorna o numero de eventos lidos ou -1 em caso de erro ao abrir o arquivo
 */
int carregar_eventos(Evento *eventos, int max_eventos)
{
    FILE *fp = fopen("../../data/eventos.txt", "r");
    if (fp == NULL)
    {
        // Se o arquivo nao existir na primeira execucao, nao e um erro, apenas nao ha eventos
        return 0; // Retorna 0 eventos se o arquivo nao existir ou houver erro
    }

    int count = 0;
    while (fscanf(fp, "%49s %10s %5s %f %9s |", eventos[count].nome, eventos[count].data, eventos[count].horario,
        &eventos[count].valorEntrada, eventos[count].status) == 5)
    {
        count++;
        if (count >= max_eventos)
        {
            printf("Aviso: Numero maximo de eventos (%d) atingido. Alguns eventos podem nao ser carregados.\n\n", max_eventos);
            break;
        }
    }

    fclose(fp);
    return count;
}

/**
 * @brief Salva um vetor de eventos no arquivo, sobrescrevendo o conteudo existente.
 *
 * Recebe um vetor de eventos e seu tamanho, e salva todos no arquivo.
 * Este e um helper para escrever a lista completa de volta.
 *
 * @param eventos Vetor de eventos a serem salvos
 * @param count O numero de eventos no vetor
 * @return int Retorna 0 em caso de sucesso, 1 em caso de erro ao abrir o arquivo.
 */
int salvar_eventos(Evento *eventos, int count)
{
    FILE *fp;

    // Abre o arquivo no modo de escrita "w" para sobrescrever o conteudo existente
    fp = fopen("../../data/eventos.txt", "w");
    if (fp == NULL)
    {
        printf("Erro ao abrir o arquivo de eventos para escrita.\n\n");
        return 1;
    }

    for (int i = 0; i < count; i++)
    {
        fprintf(fp, "%s %s %s %.2f %s |\n", eventos[i].nome, eventos[i].data, eventos[i].horario,
            eventos[i].valorEntrada, eventos[i].status);
    }

    fclose(fp);
    return 0;
}

/**
 * @brief Cadastra um novo evento no sistema
 *
 * Solicita ao usuario informacoes sobre o novo evento, como nome, data, horario, valor e status.
 * Valida cada entrada antes de salvar os dados no arquivo de eventos.
 *
 * @return int Retorna 0 em caso de sucesso, 1 em caso de erro ao abrir o arquivo.
 */
int eventos_create()
{
    Evento eventos[MAX_EVENTOS]; // Vetor para carregar eventos existentes
    Evento novo_evento;          // Variavel para o novo evento
    int count;

    // Carrega os eventos existentes
    count = carregar_eventos(eventos, MAX_EVENTOS);
    if (count == -1) // Erro real ao carregar
    {
        return 1;
    }
    // Se count == 0, o arquivo estava vazio ou nao existia, o que e ok.

    // Verifica se ha espaco no vetor para o novo evento
    if (count >= MAX_EVENTOS) {
        printf("Erro: Limite maximo de eventos atingido. Nao e possivel cadastrar mais.\n\n");
        pausar();
        limpar_tela();
        return 1;
    }

    printf("| Cadastro de eventos |\n");
    printf("------------------------------\n");

    // Solicita e valida o nome do evento
    bool nome_invalido, contem_espacos;
    do
    {
        printf("Digite o nome do evento (ex: carnaval_2025): ");
        scanf(" %49[^\n]", novo_evento.nome); // Le para o novo_evento

        nome_invalido = validar_tamanho_string(novo_evento.nome, 3, 49);
        contem_espacos = contem_espaco(novo_evento.nome);

        // Verifica se o nome ja existe antes de validar tamanho e espacos
        if (evento_existe(novo_evento.nome)) {
            printf("Nome do evento ja existe! Digite outro nome.\n\n");
            nome_invalido = true; // Forca a repeticao do loop
        } else if (nome_invalido)
        {
            printf("Nome invalido! Deve ter entre 3 e 50 caracteres.\n\n");
        }
        else if (contem_espacos)
        {
            printf("Nome invalido! Nao deve conter espacos.\n\n");
        }
    } while (nome_invalido || contem_espacos);

    // Solicita e valida a data do evento
    bool data_existente;
    do
    {
        printf("Digite a data do evento (ex: 30/05/2003): ");
        scanf("%s", novo_evento.data); // Le para o novo_evento

        data_existente = validar_data(novo_evento.data);

        if (!data_existente)
        {
            printf("Data invalida! A data nao existe.\n\n");
        }
    } while (!data_existente);

    // Solicita e valida o horario do evento
    bool horario_valido;
    do
    {
        printf("Digite a hora do evento (ex: 15:45): ");
        scanf("%s", novo_evento.horario); // Le para o novo_evento

        horario_valido = validar_horario(novo_evento.horario);
        if (!horario_valido)
        {
            printf("Hora invalida! Deve estar no formato HH:MM.\n\n");
        }
    } while (!horario_valido);

    // Solicita e valida o valor do evento
    bool valor_valido;
    do
    {
        printf("Digite o valor do evento (ex: 30,45): ");
        scanf("%f", &novo_evento.valorEntrada); // Le para o novo_evento

        valor_valido = novo_evento.valorEntrada >= 0; // Valor pode ser 0 (entrada gratuita)
        if (!valor_valido)
        {
            printf("Valor invalido! Deve ser um numero positivo ou zero.\n\n");
        }
    } while (!valor_valido);

    // Exibe os status disponiveis e solicita o status do evento
    listar_status_evento();
    bool status_valido;
    do
    {
        int status;
        printf("Digite o status do evento: ");
        scanf("%d", &status);

        strcpy(novo_evento.status, status_evento(status)); // Le para o novo_evento
        status_valido = strcmp(novo_evento.status, "invalido") != 0;

        if (!status_valido)
        {
            printf("Status invalido! Escolha um status valido.\n\n");
        }
    } while (!status_valido);

    // Adiciona o novo evento ao vetor
    eventos[count] = novo_evento;
    count++; // Incrementa o contador

    // Salva o vetor completo (antigos + novo) no arquivo, SOBRESCREVENDO-O
    int salvou_com_sucesso = salvar_eventos(eventos, count);
    if (salvou_com_sucesso != 0)
    {
        printf("Erro ao salvar o evento no arquivo.\n\n");
        return 1;
    }

    printf("Evento cadastrado com sucesso!\n\n");
    pausar();
    limpar_tela();

    return 0;
}

/**
 * @brief Lista todos os eventos cadastrados
 *
 * Le os eventos do arquivo e exibe em formato tabular no console.
 *
 * @return int Retorna 0 em caso de sucesso, 1 em caso de erro ao abrir o arquivo.
 */
int eventos_list()
{
    Evento eventos[MAX_EVENTOS]; // Vetor para armazenar ate MAX_EVENTOS
    int count = 0;               // Contador para o numero de eventos

    // Carrega os eventos existentes
    count = carregar_eventos(eventos, MAX_EVENTOS);

    if (count == -1) // Erro real na abertura do arquivo
    {
        return 1;
    }
    else if (count == 0) // Nenhum evento para listar
    {
        printf("Nenhum evento cadastrado.\n\n");
        pausar();
        limpar_tela();
        return 0;
    }

    // Exibe o cabecalho da tabela
    printf("|    Listagem de eventos:     |\n");
    printf("------------------------------|\n");
    printf("+-----------------------------------------------------------------------------------------------+\n");
    printf("| %-40s | %-12s | %-7s | %-13s | %-9s |\n", "Nome", "Data", "Horario", "Entrada", "Status");
    printf("|-----------------------------------------------------------------------------------------------|\n");

    // Exibe os eventos armazenados no vetor
    for (int i = 0; i < count; i++)
    {
        printf("| %-40s | %-12s | %-7s | R$ %-10.2f | %-9s |\n", eventos[i].nome, eventos[i].data, eventos[i].horario,
            eventos[i].valorEntrada, eventos[i].status);
    }
    printf("+-----------------------------------------------------------------------------------------------+\n");

    pausar();
    limpar_tela();
    return 0;
}

/**
 * @brief Atualiza um evento existente no sistema.
 *
 * Le todos os eventos para um vetor, encontra o evento pelo nome,
 * permite ao usuario atualizar seus campos (exceto o nome), validando
 * cada entrada antes de modificar o vetor. Entao, sobrescreve o arquivo
 * com os dados atualizados.
 *
 * @return int Retorna 0 em caso de sucesso, 1 em caso de erro.
 */
int eventos_update()
{
    Evento eventos[MAX_EVENTOS];
    int count = 0;
    char nome_alvo[50];
    int indice_encontrado = -1; // Usado para armazenar o indice do evento encontrado

    // Variaveis temporarias para as novas entradas antes da validacao
    char nova_data[11];   // "dd/mm/yyyy\0"
    char novo_horario[6]; // "HH:MM\0"
    float novo_valorEntrada;
    int novo_status_opcao;
    char novo_status_str[10]; // Para a string do status

    // 1. Le todos os eventos existentes para o vetor
    count = carregar_eventos(eventos, MAX_EVENTOS);
    if (count == -1) // Erro real ao carregar
    {
        return 1;
    }
    else if (count == 0) // Nenhum evento para atualizar
    {
        printf("Nenhum evento cadastrado para atualizar.\n\n");
        pausar();
        limpar_tela();
        return 0;
    }

    printf("| Atualizacao de eventos |\n");
    printf("--------------------------------\n");

    // 2. Solicita o nome do evento a ser atualizado
    bool nome_encontrado_valido = false;
    do {
        printf("Digite o nome do evento que deseja alterar: ");
        scanf(" %49[^\n]", nome_alvo);

        // 3. Procura o evento no vetor
        for (int i = 0; i < count; i++) {
            if (strcmp(nome_alvo, eventos[i].nome) == 0) {
                indice_encontrado = i;
                nome_encontrado_valido = true;
                break;
            }
        }

        if (!nome_encontrado_valido) {
            printf("Evento '%s' nao encontrado. Digite um nome de evento existente.\n\n", nome_alvo);
        }
    } while (!nome_encontrado_valido);

    // 4. Se o evento foi encontrado, solicita e valida os novos dados (exceto o nome)
    printf("\n\nDigite os novos dados:\n");

    // Solicita e valida a nova data do evento
    bool data_existente;
    do
    {
        printf("Nova data do evento (atual: %s): ", eventos[indice_encontrado].data);
        scanf("%s", nova_data); // Le para a variavel temporaria

        data_existente = validar_data(nova_data); // Valida a variavel temporaria
        if (!data_existente)
        {
            printf("Data invalida! A data nao existe.\n\n");
        }
    } while (!data_existente);
    strcpy(eventos[indice_encontrado].data, nova_data); // Somente copia apos validacao

    // Solicita e valida o novo horario do evento
    bool horario_valido;
    do
    {
        printf("Novo horario do evento (atual: %s): ", eventos[indice_encontrado].horario);
        scanf("%s", novo_horario); // Le para a variavel temporaria

        horario_valido = validar_horario(novo_horario); // Valida a variavel temporaria
        if (!horario_valido)
        {
            printf("Hora invalida! Deve estar no formato HH:MM.\n\n");
        }
    } while (!horario_valido);
    strcpy(eventos[indice_encontrado].horario, novo_horario); // Somente copia apos validacao

    // Solicita e valida o novo valor do evento
    bool valor_valido;
    do
    {
        printf("Novo valor do evento (atual: %.2f): ", eventos[indice_encontrado].valorEntrada);
        scanf("%f", &novo_valorEntrada); // Le para a variavel temporaria

        valor_valido = novo_valorEntrada >= 0;
        if (!valor_valido)
        {
            printf("Valor invalido! Deve ser um numero positivo ou zero.\n\n");
        }
    } while (!valor_valido);
    eventos[indice_encontrado].valorEntrada = novo_valorEntrada; // Somente copia apos validacao

    // Exibe os status disponiveis e solicita o novo status do evento
    listar_status_evento();
    bool status_valido;
    do
    {
        printf("Novo status do evento (atual: %s): ", eventos[indice_encontrado].status);
        scanf("%d", &novo_status_opcao); // Le para a variavel temporaria

        // Tenta converter para string para validacao
        strcpy(novo_status_str, status_evento(novo_status_opcao));
        status_valido = strcmp(novo_status_str, "invalido") != 0;

        if (!status_valido)
        {
            printf("Status invalido! Escolha um status valido.\n\n");
        }
    } while (!status_valido);
    strcpy(eventos[indice_encontrado].status, novo_status_str); // Somente copia apos validacao

    // 5. Sobrescreve o arquivo original com os dados atualizados do vetor
    int salvou_com_sucesso = salvar_eventos(eventos, count);
    if (salvou_com_sucesso != 0)
    {
        printf("Erro ao salvar as alteracoes no arquivo.\n\n");
        return 1;
    }

    printf("Dados do evento atualizados com sucesso!\n\n");
    pausar();
    limpar_tela();
    return 0;
}

/**
 * @brief Lista os status dos eventos disponiveis
 *
 * Exibe uma tabela com os status possiveis para os eventos,
 * incluindo "Concluido", "Cancelado" e "Agendado".
 *
 * @return void
 */
void listar_status_evento()
{
    printf("\nStatus dos eventos:\n");
    printf("+--------------------------------+\n");
    printf("| [1] Concluido                  |\n");
    printf("| [2] Cancelado                  |\n");
    printf("| [3] Agendado                   |\n");
    printf("+--------------------------------+\n");
}

/**
 * @brief Converte o status numerico de um evento para uma string
 *
 * Recebe um inteiro representando o status do evento e retorna uma string
 *
 * @param status O status do evento (1: concluido, 2: cancelado, 3: agendado)
 *
 * @return const char* A string correspondente ao status do evento
 */
const char *status_evento(int status)
{
    switch (status)
    {
    case 1:
        return "concluido";
    case 2:
        return "cancelado";
    case 3:
        return "agendado";
    default:
        return "invalido";
    }
}

/**
 * @brief Verifica se um evento existe pelo nome
 *
 * @param nome O nome do evento a ser verificado
 * @return int 1 se o evento existir, 0 caso contrario
 */
int evento_existe(const char *nome)
{
    Evento eventos[MAX_EVENTOS];
    int count = carregar_eventos(eventos, MAX_EVENTOS);

    if (count == -1)
    {
        return 0; // Erro ao abrir o arquivo, assumimos que nao existe
    }

    for (int i = 0; i < count; i++)
    {
        if (strcmp(eventos[i].nome, nome) == 0)
        {
            return 1; // Evento encontrado
        }
    }

    return 0; // Evento nao encontrado
}

/**
 * @brief Verifica se um evento ja passou
 *
 * @param nome O nome do evento a ser verificado
 * @return int 1 se o evento ja passou, 0 caso contrario
 */
int evento_passado(const char *nome)
{
    Evento eventos[MAX_EVENTOS];
    int count = carregar_eventos(eventos, MAX_EVENTOS);

    if (count == -1)
    {
        return 0; // Erro ao abrir o arquivo
    }

    for (int i = 0; i < count; i++)
    {
        if (strcmp(eventos[i].nome, nome) == 0)
        {
            // Usa a funcao comparar_data para verificar se o evento ja passou
            return comparar_data(eventos[i].data);
        }
    }

    return 0; // Evento nao encontrado ou nao passado
}
