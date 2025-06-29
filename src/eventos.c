#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <time.h>
#include <locale.h>
#include <stdbool.h>

#include "../include/eventos.h"
#include "../utils/utils.h"

/**
 * @brief Cadastra um novo evento no sistema
 * 
 * Solicita ao usuário informações sobre o novo evento, como nome, data, horário, valor e status.
 * Valida cada entrada antes de salvar os dados no arquivo de eventos.
 * 
 * @return int Retorna 0 em caso de sucesso, 1 em caso de erro ao abrir o arquivo.
 */
int eventos_create() {
    Evento evento;
    FILE *fp;

    // Abre o arquivo para adicionar novos eventos
    fp = fopen("../../data/eventos.txt", "a+");
    if (fp == NULL) {
        printf("Erro ao abrir o arquivo de eventos.\n");
        return 1;
    }

    // Solicita e valida o nome do evento
    bool nome_valido, contem_espacos;
    do {
        printf("Digite o nome do evento (ex: carnaval_2025): ");
        scanf(" %49[^\n]", evento.nome);

        nome_valido = validar_tamanho_string(evento.nome, 1, 49);
        contem_espacos = contem_espaco(evento.nome);

        if (!nome_valido) {
            printf("Nome inválido! Deve ter entre 1 e 50 caracteres.\n\n");
        } else if (contem_espacos) {
            printf("Nome inválido! Não deve conter espaços.\n\n");
        }
    } while (!nome_valido || contem_espacos);

    // Solicita e valida a data do evento
    bool data_formato_invalido, data_inexistente;
    do {
        printf("Digite a data do evento (ex: 30/05/2003): ");
        scanf("%s", evento.data);

        data_formato_invalido = validar_com_regex(evento.data, "^([0-2][0-9]|(3)[0-1])/(0[1-9]|1[0-2])/[0-9]{4}$");
        data_inexistente = validar_data(evento.data);

        if (data_formato_invalido) {
            printf("Data inválida! Deve estar no formato dd/mm/yyyy.\n");
        } else if(data_inexistente) {
            printf("Data inválida! A data não existe.\n");
        }
    } while (data_formato_invalido || data_inexistente);

    // Solicita e valida o horário do evento
    bool horario_valido;
    do {
        printf("Digite o horário do evento (ex: 15:45): ");
        scanf("%s", evento.horario);

        horario_valido = !validar_com_regex(evento.horario, "^(0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$");
        if (!horario_valido) {
            printf("Horário inválido! Deve estar no formato HH:MM 24horas.\n");
        }
    } while (!horario_valido);

    // Solicita e valida o valor do evento
    bool valor_valido;
    do {
        printf("Digite o valor do evento: ");
        scanf("%f", &evento.valorEntrada);

        if (evento.valorEntrada < 0) {
            printf("Valor inválido! Deve ser um número positivo.\n");
        }
    } while (!valor_valido);

    // Exibe os status disponíveis e solicita o status do evento
    listar_status_evento();
    bool status_valido;
    do {
        int status;
        printf("Digite o status do evento: ");
        scanf("%d", &status);

        strcpy(evento.status, status_evento(status));
        status_valido = strcmp(evento.status, "invalido") != 0;

        if (!status_valido) {
            printf("Status inválido! Escolha um status válido.\n");
        }
    } while (!status_valido);

    // Grava o evento no arquivo
    fprintf(fp, "%s %s %s %.2f %s |\n", evento.nome, evento.data, evento.horario, evento.valorEntrada, evento.status);

    fclose(fp);
    limpar_tela();
    return 0;
}

/**
 * @brief Lista todos os eventos cadastrados
 * 
 * Lê os eventos do arquivo e exibe em formato tabular no console.
 * 
 * @return int Retorna 0 em caso de sucesso, 1 em caso de erro ao abrir o arquivo.
 */
int eventos_list() {
    Evento eventos[100]; // Vetor para armazenar até 100 eventos
    int count = 0;       // Contador para o número de eventos
    FILE *fp;

    // Abre o arquivo para leitura
    fp = fopen("../../data/eventos.txt", "r");
    if (fp == NULL) {
        printf("Erro ao abrir o arquivo de eventos.\n");
        return 1;
    }

    // Lê os eventos do arquivo e armazena no vetor
    while (fscanf(fp, "%49s %10s %5s %f %9s |", eventos[count].nome, eventos[count].data, eventos[count].horario, 
                  &eventos[count].valorEntrada, eventos[count].status) == 5) {
        count++;
        if (count >= 100) {
            printf("Aviso: Número máximo de eventos (100) atingido. Alguns eventos podem não ser exibidos.\n");
            break;
        }
    }
    fclose(fp);

    // Exibe o cabeçalho da tabela
    printf("\nLista de eventos:\n");
    printf("|-----------------------------------------------------------------------------------------------|\n");
    printf("| %-40s | %-12s | %-7s | %-13s | %-9s |\n", "Nome", "Data", "Horário", "Entrada", "Status");
    printf("|-----------------------------------------------------------------------------------------------|\n");

    // Exibe os eventos armazenados no vetor
    for (int i = 0; i < count; i++) {
        printf("| %-40s | %-12s | %-7s | R$ %-10.2f | %-9s |\n", eventos[i].nome, eventos[i].data, eventos[i].horario, 
               eventos[i].valorEntrada, eventos[i].status);
    }
    printf("|-----------------------------------------------------------------------------------------------|\n");

    pausar();
    limpar_tela();
    return 0;
}

/**
 * @brief Atualiza os status de um evento
 * 
 * Solicita o nome do evento a ser atualizado e, se encontrado, permite a alteração do status.
 * Utiliza um arquivo temporário para realizar a atualização de forma segura.
 * 
 * @return int Retorna 0 em caso de sucesso, 1 em caso de erro ou se o evento não for encontrado.
 */
int eventos_update() {
    FILE *fp_original, *fp_temp;
    Evento evento;
    char nome_alvo[50];
    int encontrado = 0;

    // Solicita o nome do evento a ser atualizado
    printf("Digite o nome do evento que deseja alterar o status: ");
    scanf("%s", nome_alvo);

    // Abre o arquivo original para leitura
    fp_original = fopen("../../data/eventos.txt", "r");
    if (fp_original == NULL) {
        printf("Erro ao abrir o arquivo de eventos.\n");
        return 1;
    }

    // Abre um arquivo temporário para escrita
    fp_temp = fopen("../../data/temp.txt", "w");
    if (fp_temp == NULL) {
        printf("Erro ao criar arquivo temporário.\n");
        fclose(fp_original);
        return 1;
    }

    // Lê cada evento do arquivo original
    while (fscanf(fp_original, "%49s %10s %5s %f %9s |", evento.nome, evento.data, evento.horario, &evento.valorEntrada, evento.status) == 5) {
        if (strcmp(nome_alvo, evento.nome) != 0) {
            // Copia o evento para o arquivo temporário se não for o alvo
            fprintf(fp_temp, "%s %s %s %.2f %s |\n", evento.nome, evento.data, evento.horario, evento.valorEntrada, evento.status);
        } else {
            encontrado = 1;

            // Solicita o novo status do evento
            listar_status_evento();
            bool status_valido;
            do {
                int status;
                printf("Digite o novo status do evento (atual: %s): ", evento.status);
                scanf("%d", &status);

                strcpy(evento.status, status_evento(status));
                status_valido = strcmp(evento.status, "invalido") != 0;

                if (!status_valido) {
                    printf("Status inválido! Escolha um status válido.\n");
                }
            } while (!status_valido);

            // Grava o evento atualizado no arquivo temporário
            fprintf(fp_temp, "%s %s %s %.2f %s |\n", evento.nome, evento.data, evento.horario, evento.valorEntrada, evento.status);
        }
    }

    fclose(fp_original);
    fclose(fp_temp);

    // Substitui o arquivo original pelo temporário
    remove("../../data/eventos.txt");
    rename("../../data/temp.txt", "../../data/eventos.txt");

    if (!encontrado) {
        printf("Evento não encontrado.\n");
        pausar();
        limpar_tela();
        return 1;
    }

    printf("Dados atualizados com sucesso!\n");
    limpar_tela();
    return 0;
}

/**
 * @brief Lista os status dos eventos disponíveis
 * 
 * Exibe uma tabela com os status possíveis para os eventos,
 * incluindo "Concluido", "Cancelado" e "Agendado".
 * 
 * @return void
 */
void listar_status_evento() {
    printf("\nStatus dos eventos:\n");
    printf("╔══════════════════════════════╗\n");
    printf("║ [1] Concluido                ║\n");
    printf("║ [2] Cancelado                ║\n");
    printf("║ [3] Agendado                 ║\n");
    printf("╚══════════════════════════════╝\n");
}

/**
 * @brief Converte o status numérico de um evento para uma string
 * 
 * Recebe um inteiro representando o status do evento e retorna uma string
 * 
 * @param status O status do evento (1: concluido, 2: cancelado, 3: agendado)
 * 
 * @return const char* A string correspondente ao status do evento
 */
const char* status_evento(int status) {
    switch (status) {
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

int evento_existe(const char *nome) {
    Evento evento;
    FILE *fp = fopen("../../data/eventos.txt", "r");
    if (fp == NULL) {
        return 0;
    }

    while (fscanf(fp, "%49s %10s %5s %f %9s |", evento.nome, evento.data, evento.horario, &evento.valorEntrada, evento.status) == 5) {
        if (strcmp(evento.nome, nome) == 0) {
            fclose(fp);
            return 1;
        }
    }

    fclose(fp);
    return 0;
}

int evento_passado(const char *nome) {
    Evento evento;
    FILE *fp = fopen("../../data/eventos.txt", "r");
    if (fp == NULL) {
        return 0;
    }

    while (fscanf(fp, "%49s %10s %5s %f %9s |", evento.nome, evento.data, evento.horario, &evento.valorEntrada, evento.status) == 5) {
        if (strcmp(evento.nome, nome) == 0) {
            fclose(fp);

            // Usa a função comparar_data para verificar se o evento já passou
            return comparar_data(evento.data);
        }
    }

    fclose(fp);
    return 0;
}