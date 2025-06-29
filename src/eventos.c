#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <time.h>
#include <stdbool.h>

#include "../include/eventos.h"
#include "../utils/utils.h"

#define MAX_EVENTOS 100

/**
 * @brief Cadastra um novo evento no sistema
 *
 * Solicita ao usuário informações sobre o novo evento, como nome, data, horário, valor e status.
 * Valida cada entrada antes de salvar os dados no arquivo de eventos.
 *
 * @return int Retorna 0 em caso de sucesso, 1 em caso de erro ao abrir o arquivo.
 */
int eventos_create()
{
    Evento evento;
    FILE *fp;

    // Abre o arquivo para adicionar novos eventos
    fp = fopen("../../data/eventos.txt", "a+");
    if (fp == NULL)
    {
        printf("Erro ao abrir o arquivo de eventos.\n\n");
        return 1;
    }

    // Solicita e valida o nome do evento
    bool nome_invalido, contem_espacos;
    do
    {
        printf("Digite o nome do evento (ex: carnaval_2025): ");
        scanf(" %49[^\n]", evento.nome);

        nome_invalido = validar_tamanho_string(evento.nome, 3, 49);
        contem_espacos = contem_espaco(evento.nome);

        if (nome_invalido)
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
        scanf("%s", evento.data);

        data_existente = validar_data(evento.data);

        if (!data_existente)
        {
            printf("Data invalida! A data não existe.\n\n");
        }
    } while (!data_existente);

    // Solicita e valida o horario do evento
    bool horario_valido;
    do
    {
        printf("Digite a hora do evento (ex: 15:45): ");
        scanf("%s", evento.horario);

        horario_valido = validar_horario(evento.horario);
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
        scanf("%f", &evento.valorEntrada);

        valor_valido = evento.valorEntrada > 0;
        if (!valor_valido)
        {
            printf("Valor invalido! Deve ser um numero positivo.\n\n");
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

        strcpy(evento.status, status_evento(status));
        status_valido = strcmp(evento.status, "invalido") != 0;

        if (!status_valido)
        {
            printf("Status invalido! Escolha um status valido.\n\n");
        }
    } while (!status_valido);

    // Grava o evento no arquivo
    fprintf(fp, "%s %s %s %.2f %s |\n", evento.nome, evento.data, evento.horario, evento.valorEntrada, evento.status);

    fclose(fp);
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
    Evento eventos[100]; // Vetor para armazenar ate 100 eventos
    int count = 0;       // Contador para o numero de eventos
    FILE *fp;

    // Abre o arquivo para leitura
    fp = fopen("../../data/eventos.txt", "r");
    if (fp == NULL)
    {
        printf("Erro ao abrir o arquivo de eventos.\n\n");
        return 1;
    }

    // Le os eventos do arquivo e armazena no vetor
    while (fscanf(fp, "%49s %10s %5s %f %9s |", eventos[count].nome, eventos[count].data, eventos[count].horario,
                  &eventos[count].valorEntrada, eventos[count].status) == 5)
    {
        count++;
        if (count >= 100)
        {
            printf("Aviso: Numero maximo de eventos (100) atingido. Alguns eventos podem nao ser exibidos.\n\n");
            break;
        }
    }
    fclose(fp);

    // Exibe o cabecalho da tabela
    printf("\nLista de eventos:\n");
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
    FILE *fp;
    char nome_alvo[50];
    int indice_encontrado = -1; // Usado para armazenar o indice do evento encontrado

    // Variaveis temporarias para as novas entradas antes da validacao
    char nova_data[11];   // "dd/mm/yyyy\0"
    char novo_horario[6]; // "HH:MM\0"
    float novo_valorEntrada;
    int novo_status_opcao;
    char novo_status_str[10]; // Para a string do status

    // 1. Le todos os eventos existentes para o vetor
    fp = fopen("../../data/eventos.txt", "r");
    if (fp == NULL)
    {
        printf("Erro ao abrir o arquivo de eventos para leitura.\n\n");
        return 1;
    }
    while (fscanf(fp, "%49s %10s %5s %f %9s |", eventos[count].nome, eventos[count].data, eventos[count].horario,
                  &eventos[count].valorEntrada, eventos[count].status) == 5)
    {
        count++;
        if (count >= MAX_EVENTOS)
        {
            printf("Aviso: Capacidade maxima de eventos (%d) atingida durante a leitura para atualizacao.\n\n", MAX_EVENTOS);
            break;
        }
    }
    fclose(fp);

    // 2. Solicita o nome do evento a ser atualizado
    printf("Digite o nome do evento que deseja alterar: ");
    scanf(" %49[^\n]", nome_alvo);

    // 3. Procura o evento no vetor
    for (int i = 0; i < count; i++)
    {
        if (strcmp(nome_alvo, eventos[i].nome) == 0)
        {
            indice_encontrado = i;
            break;
        }
    }

    if (indice_encontrado == -1)
    {
        printf("Evento '%s' nao encontrado.\n\n", nome_alvo);
        pausar();
        limpar_tela();
        return 1;
    }

    // 4. Se o evento foi encontrado, solicita e valida os novos dados (exceto o nome)
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
        // %f nao precisa de & antes de novo_valorEntrada se novo_valorEntrada ja for um ponteiro, mas aqui e float normal
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
    fp = fopen("../../data/eventos.txt", "w"); // Abre em modo de escrita para truncar o arquivo
    if (fp == NULL)
    {
        printf("Erro ao reabrir o arquivo de eventos para escrita.\n\n");
        return 1;
    }

    for (int i = 0; i < count; i++)
    {
        fprintf(fp, "%s %s %s %.2f %s |\n", eventos[i].nome, eventos[i].data, eventos[i].horario,
                eventos[i].valorEntrada, eventos[i].status);
    }
    fclose(fp);

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

int evento_existe(const char *nome)
{
    Evento evento;
    FILE *fp = fopen("../../data/eventos.txt", "r");
    if (fp == NULL)
    {
        return 0;
    }

    while (fscanf(fp, "%49s %10s %5s %f %9s |", evento.nome, evento.data, evento.horario, &evento.valorEntrada, evento.status) == 5)
    {
        if (strcmp(evento.nome, nome) == 0)
        {
            fclose(fp);
            return 1;
        }
    }

    fclose(fp);
    return 0;
}

int evento_passado(const char *nome)
{
    Evento evento;
    FILE *fp = fopen("../../data/eventos.txt", "r");
    if (fp == NULL)
    {
        return 0;
    }

    while (fscanf(fp, "%49s %10s %5s %f %9s |", evento.nome, evento.data, evento.horario, &evento.valorEntrada, evento.status) == 5)
    {
        if (strcmp(evento.nome, nome) == 0)
        {
            fclose(fp);

            // Usa a funcao comparar_data para verificar se o evento ja passou
            return comparar_data(evento.data);
        }
    }

    fclose(fp);
    return 0;
}
